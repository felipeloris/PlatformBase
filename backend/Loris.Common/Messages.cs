using Loris.Common.Tools;
using System;

namespace Loris.Common
{
    public class Messages
    {
        public static string SystemError(Guid guid)
        {
            var currCulture = CultureHelper.GetCurrentCulture();

            var msg = "Aconteceu um erro no sistema! Informe esse ID para o administrador: {0}";
            if (currCulture == Languages.English)
                msg = "There was an error in the system! Enter this ID for the administrator: {0}";
            else if (currCulture == Languages.Spanish)
                msg = "¡Hubo un error del sistema! \nInserte este ID al administrador: {0}";

            return msg;
        }

        public static string RecordAltered()
        {
            var currCulture = CultureHelper.GetCurrentCulture();

            var msg = "O registro foi alterado pelo usuário {0} às {1}! Deseja salvar assim mesmo?";
            if (currCulture == Languages.English)
                msg = "The record was changed by user {0} at {1}! Do you want to save anyway?";
            else if (currCulture == Languages.Spanish)
                msg = "El registro fue cambiado por el usuario {0} en {1}. ¿Quieres guardar de todos modos?";

            return msg;
        }

        public static string UniqueViolation()
        {
            var currCulture = CultureHelper.GetCurrentCulture();

            var msg = "O objeto já existe no sistema!";
            if (currCulture == Languages.English)
                msg = "The object already exists in the system!";
            else if (currCulture == Languages.Spanish)
                msg = "¡El objeto ya existe en el sistema!";

            return msg;
        }

        public static string DbOperationViolation()
        {
            var currCulture = CultureHelper.GetCurrentCulture();

            var msg = "Não foi possível executar a ação. Objeto em uso!";
            if (currCulture == Languages.English)
                msg = "The action could not be performed. Object in use!";
            else if (currCulture == Languages.Spanish)
                msg = "No se pudo realizar la acción. ¡Objeto en uso!";

            return msg;
        }
    }
}
