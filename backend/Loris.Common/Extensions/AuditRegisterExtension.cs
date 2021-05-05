using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Loris.Common.Helpers;
using System;

namespace Loris.Common.Extensions
{
    public static class AuditRegisterExtension
    {
        /// <summary>
        /// Verifica se o controle de registro foi alterado por outro usuário
        /// </summary>
        /// <returns></returns>
        public static void CanChangeDataRecord(this IAuditRegister auditRegister, IAuditRegister auditRegisterDb, int anotherUserSecondsToChange)
        {
            if ((auditRegister != null) && (auditRegisterDb != null))
            {
                var ctrlModifiedIn = DateHelper.ConvertLongToDateTime(auditRegister.CtrlModifiedIn);
                var ctrlModifiedBy = auditRegister.CtrlModifiedBy;

                var ctrlModifiedInDb = DateHelper.ConvertLongToDateTime(auditRegisterDb.CtrlModifiedIn);
                var ctrlModifiedByDb = auditRegisterDb.CtrlModifiedBy;

                // Se for o mesmo usuário, permite realizar a gravação do registro
                if (ctrlModifiedByDb?.Equals(ctrlModifiedBy, StringComparison.InvariantCultureIgnoreCase) ?? true)
                    return;

                // Outro usuário, realizando a alteração antes do tempo permitido
                if (ctrlModifiedIn < ctrlModifiedInDb.AddSeconds(anotherUserSecondsToChange))
                    throw new RecordChangedException(ctrlModifiedByDb, ctrlModifiedInDb);
            }
        }

        public static void SetRegisterControl(this IAuditRegister auditRegister, IAuditRegister auditRegisterDb, ILogin login)
        {
            // Verifica se o objeto herda a interface 'IRegisterControl'
            if (auditRegister != null)
            {
                if (auditRegisterDb == null)
                {
                    auditRegister.CtrlCreatedIn = DateHelper.ConvertDateTimeToLong(DateTime.Now);
                    auditRegister.CtrlCreatedBy = login?.ExtenalId;
                }
                auditRegister.CtrlModifiedIn = DateHelper.ConvertDateTimeToLong(DateTime.Now);
                auditRegister.CtrlModifiedBy = login?.ExtenalId;
            }
        }
    }
}
