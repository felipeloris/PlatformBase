using System;
using System.Collections.Generic;
using System.Linq;
using Loris.Services;
using Loris.Entities;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Services;
using Loris.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loris.Common.Test.Services
{
    [TestClass]
    public class AuthServiceTest
    {
        /*
        select * from public.auth_user;
        select * from public.auth_role;
        select * from public.auth_user_role;
        select * from public.auth_resource;
        select * from public.auth_role_resource;         
         */

        private const string UserIdent = "admin";
        private const string UserPwd = "admin73";
        private const string RoleAdmin = "Admin";
        private const string RoleUser = "User";

        private Database Database { get; } = DatabaseHelper.RecoverDatabase();
        private IUserAuthentication UserAuthentication { get; } = new BasicUserAuthentication();

        [TestMethod]
        public void PreparerDatabase() 
        {
            TestAddAdminUser();
            TesteGetUserByParameter();
            TestAddAdminRoles();
            TestAddRolesToAdmin();
            TestAddResources();
            TestAddResourcesToRole();
        }

        [TestMethod]
        public void TestAddAdminUser()
        {
            try
            {
                var user = new AuthUser()
                {
                    ExtenalId = UserIdent,
                    Nickname = "User Administrator",
                    Password = UserPwd,
                    Email = "felipe@loris.com.br",
                    Language = Languages.Portuguese,
                    LoginStatus = LoginStatus.NotLogged,
                    LoginAt = DateTime.MinValue,
                    DtBlocked = null,
                    DtExpiredPwd = null, 
                    WrondPwdAttempts = 0,
                    Note = "Administrator",
                    PersonId = null,
                    KeyChangePwd = Guid.NewGuid().ToString()
                };

                var service = new AuthUserService(Database, UserAuthentication);
                service.Add(user).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TesteGetUserByParameter()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user1 = userService.GetUser(UserIdent, false).Result;
                var pgrsUser = userService.Get(new RequestParameter
                {
                    Filters = new List<ReqParamFilter>() {
                        new ReqParamFilter{
                            Field="Email",
                            Value="felipe@loris.com.br",
                            Condition=ReqParamCondition.Equal }}
                }).Result;
                var user2 = pgrsUser.Results[0];

                Assert.AreEqual(user1.Nickname, user2.Nickname);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddAdminRoles()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;

                var roles = new List<AuthRole>();
                roles.Add(new AuthRole() { Name = RoleAdmin });
                roles.Add(new AuthRole() { Name = RoleUser });

                var service = new AuthRoleService(user, Database);
                //var x = roles.Select(r => service.Add(r).Result);
                foreach (var role in roles)
                    service.Add(role).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddRolesToAdmin()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;

                var roleService = new AuthRoleService(user, Database);
                var roles = roleService.Get().Result;
                foreach (var role in roles)
                    roleService.AddRoleToUser(user.Id, role.Id).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddResources()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;

                var resources = new List<AuthResource>();
                resources.Add(new AuthResource() { Code = "AA001", Description = "Cadastro de usuários", Dictionary = "lbl_user_registration" });
                resources.Add(new AuthResource() { Code = "AA002", Description = "Cadastro de funções", Dictionary = "lbl_role_registration" });
                resources.Add(new AuthResource() { Code = "AA003", Description = "Cadastro de recursos", Dictionary = "lbl_resource_registration" });

                var resourceService = new AuthResourceService(user, Database);
                foreach (var resource in resources)
                    resourceService.Add(resource).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddResourcesToRole()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;

                var roleService = new AuthRoleService(user, Database);
                var roles = roleService.Get().Result;

                var resourceService = new AuthResourceService(user, Database);
                var resources = resourceService.Get().Result;

                var roleAdmin = roles.FirstOrDefault(x => x.Name.Equals(RoleAdmin));
                var roleUser = roles.FirstOrDefault(x => x.Name.Equals(RoleUser));

                foreach (var resource in resources)
                {
                    roleService.AddResourceToRole(roleAdmin.Id, resource.Id, AccessPermission.All).Wait();
                    roleService.AddResourceToRole(roleUser.Id, resource.Id, AccessPermission.Read | AccessPermission.Report).Wait();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestDeleteUserAndRoles()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;

                var roleService = new AuthRoleService(user, Database);
                var roles = roleService.Get().Result;

                var resourceService = new AuthResourceService(user, Database);
                var resources = resourceService.Get().Result;

                foreach (var resource in resources)
                    resourceService.Delete(resource).Wait();

                foreach (var role in roles)
                    roleService.DeleteUserRole(user.Id, role.Id).Wait();
                
                foreach (var role in roles)
                    roleService.Delete(role.Id).Wait();

                userService.Delete(user.Id).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGenerateKey()
        {
            try
            {
                var userService = new AuthUserService(Database, UserAuthentication);
                userService.GenerateKey(UserIdent).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestChangePasswordWithToken_InvalidPwd()
        {
            try
            {
                var testPwd = "testPwd@123";
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;
                user.Password = testPwd;
                userService.Update(user, 0).Wait();

                var status = userService.ChangePasswordWithKey(UserIdent, user.KeyChangePwd, testPwd).Result;
                if (status != ChangePwdStatus.InvalidNewPassword)
                    Assert.Fail("Expected 'InvalidNewPassword' status!");

                user.Password = UserPwd;
                userService.Update(user, 0).Wait();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestChangePasswordWithToken_ValidPwd()
        {
            try
            {
                var testPwd = "testPwd@123";
                var userService = new AuthUserService(Database, UserAuthentication);
                var user = userService.GetUser(UserIdent, false).Result;
                user.Password = testPwd;
                user.Nickname = "xpto";
                userService.Update(user, 0).Wait();

                var status = userService.ChangePasswordWithKey(UserIdent, user.KeyChangePwd, UserPwd).Result;
                if (status != ChangePwdStatus.PasswordChanged)
                    Assert.Fail("Expected 'PasswordChanged' status!");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
