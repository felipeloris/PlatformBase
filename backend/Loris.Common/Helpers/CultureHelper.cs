using System;
using System.Globalization;
using System.Threading;
using Loris.Common.Extensions;

namespace Loris.Common.Tools
{
    /// <summary>
    /// Ferramentas para manipular cultura de aplicação e de interface
    /// </summary>
    public class CultureHelper
    {
        /// <summary>
        /// Metodo para alterar a Cultura UI dos arquivos e classes resource
        /// </summary>
        /// <param name="languageCode">Codigo numerico que representa o idioma desejado:Portuguese = 1 ou default,English = 2,Spanish = 3  </param>
        public static void SetCulture(Languages languageCode)
        {
            var cultura = languageCode.GetCultureInfoName();

            // Threads
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultura);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultura);
        }

        public static Languages GetCurrentCulture()
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            if (culture.Name == Constants.CodeEnUs)
                return Languages.English;
            else if (culture.Name == Constants.CodeEsEs)
                return Languages.Spanish;
            else
                return Languages.Portuguese;
        }

        /// <summary>
        /// Devolve a estrutura de idioma 
        /// </summary>
        /// <param name="cultureInfo">Identificador de idioma no formato ("en-US"/"pt-BR"/"es-ES")</param>
        /// <returns></returns>
        public static Languages GetLanguage(string cultureInfo)
        {
            switch (cultureInfo)
            {
                case Constants.CodeEnUs:
                    return Languages.English;
                case Constants.CodeEsEs:
                    return Languages.Spanish;
                default:
                    return Languages.Portuguese;
            }
        }

        /// <summary>
        /// Método usado para retornar o idioma pelo valor ou nome do enumerador
        /// </summary>
        /// <param name="value">valor ou nome do enumerador</param>
        /// <returns></returns>
        public static Languages GetLanguage(byte value)
        {
            Languages language;

            if (Enum.IsDefined(typeof(Languages), value))
            {
                language = (Languages)Enum.ToObject(typeof(Languages), value);
            }
            else
            {
                throw new ApplicationException($"Languages undefined! {value}");
            }

            return language;
        }
    }
}
