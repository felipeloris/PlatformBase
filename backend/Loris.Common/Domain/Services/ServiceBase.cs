using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Loris.Common.Extensions;
using Loris.Common.Helpers;
using Loris.Common.Log;
using Loris.Common.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;

namespace Loris.Common.Domain.Service
{
    public abstract class ServiceBase<TEntityId> where TEntityId : class, IEntityIdBase
    {
        private static readonly LogManager Logger = new LogManager(typeof(ServiceBase<TEntityId>));
        protected ResourceManager ResourceManager { get; set; }

        protected ILogin Login { get; set; }

        #region Funções auxiliares

        protected abstract TEntityId InternalGetById(object id);

        protected void CanChangeDataRecord(object obj, int anotherUserSecondsToChange)
        {
            var auditRegister = obj as IAuditRegister;
            if (auditRegister != null)
            {
                var id = ((IEntityIdBase)obj).GetId();
                var auditRegisterDb = (IAuditRegister)InternalGetById(id);
                auditRegister.CanChangeDataRecord(auditRegisterDb, anotherUserSecondsToChange);
            }
        }

        protected void SetRegisterControl(object obj)
        {
            var auditRegister = obj as IAuditRegister;
            if (auditRegister != null)
            {
                var id = ((IEntityIdBase)obj).GetId();
                var auditRegisterDb = (IAuditRegister)InternalGetById(id);
                auditRegister.SetRegisterControl(auditRegisterDb, Login);
            }
        }

        /*http://gigi.nullneuron.net/gigilabs/simple-validation-with-data-annotations/
        static void RunValidateProperty(string value)
        {
            var person = new Person();

            var context = new ValidationContext(person) { MemberName = "Name" };
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateProperty(value, context, results);
        }
        */

        protected List<string> GetErrorSummary(object obj)
        {
            var error = new List<string>();
            var errorSummary = new List<ValidationResult>();
            var contexto = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, contexto, errorSummary, true);

            //var rm = SYSRES.ResourceManager;
            var language = Login?.Language ?? Languages.Portuguese;
            var culture = language.GetCultureInfo();
            foreach (var t in errorSummary)
            {
                var item = t.ErrorMessage;

                try
                {
                    var lstSplit = item.Split(';').ToList();
                    item = ResourceManager.GetString(lstSplit[0], culture);
                    lstSplit.RemoveAt(0);
                    var arrSplit = lstSplit.ToArray();

                    error.Add(string.Format(item, arrSplit));
                }
                catch (Exception)
                {
                    error.Add(item);
                }
            }

            return error;
        }

        protected List<string> GetErrorSummary(byte[] serializedObj)
        {
            try
            {
                Logger.LogDebugStart();

                var obj = SerializeObject.ByteArrayToObject(serializedObj);
                return GetErrorSummary(obj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
            finally
            {
                Logger.LogDebugFinish();
            }
        }

        #endregion
    }
}
