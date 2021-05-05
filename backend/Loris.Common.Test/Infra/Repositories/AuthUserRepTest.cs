using System;
using System.Collections.Generic;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Infra.Repositories;
using Loris.Common;
using Loris.Common.Cryptography;
using Loris.Common.Domain.Entities;
using Loris.Common.EF.Helpers;
using Loris.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IotMonitorTest.Data
{
    [TestClass]
    public class AuthUserRepTest
    {
        private Database Database { get; } = DatabaseHelper.RecoverDatabase();

        [TestMethod]
        public void TestSaveUserAndRole()
        {
            try
            {
                using (var context = EfCoreHelper.GetContext<LorisContext>(Database.ConnString))
                {
                    var userRep = new AuthUserRep(context);
                    var user = new AuthUser();
                    var roleRep = new AuthRoleRep(context);
                    var roles = new List<AuthRole>();

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        // Adiciona o usuário
                        user.ExtenalId = "felipe@loris.com.br";
                        user.Password = Md5Cryptography.Encrypt("admin73", Constants.CryptographyKey, true);
                        user.Nickname = "Felipe";
                        user.Email = "felipe@loris.com.br";
                        user.Language = Languages.Portuguese;
                        user.LoginStatus = LoginStatus.NotLogged;
                        user.KeyChangePwd = Guid.NewGuid().ToString();
                        user.Note = "cadastro automático";
                        userRep.Add(user).Wait();

                        // Adiciona as Role's
                        roles.Add(new AuthRole() { Name = "Admin" });
                        roles.Add(new AuthRole() { Name = "User" });
                        roleRep.AddRange(roles).Wait();

                        // Efetua a transação
                        transaction.Commit();
                    }

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        // Adiciona as roles ao usuário
                        //var dbSet = context.Set<AuthRole>();
                        foreach (var role in roles)
                        {

                            roleRep
                                .AddRoleToUser(new AuthUserRole() { AuthUserId = user.Id, AuthRoleId = role.Id})
                                .Wait();
                            /*
                            var userRole = new AuthUserRole();
                            userRole.AuthUser = user;
                            userRole.AuthRole = role;
                            dbSet.AddAsync(userRole);
                            context.SaveChangesAsync();
                            */
                        }

                        // Efetua a transação
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGetRolesFromUser()
        {
            try
            {
                var userRep = new AuthUserRep(Database);
                var user = userRep.GetUser("felipe@loris.com.br").Result;
                var roleRep = new AuthRoleRep(Database);
                var roles = roleRep.GetUserRoles(user.Id).Result;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
