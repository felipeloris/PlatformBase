using System.Globalization;

namespace Loris.Common.Extensions
{
    public static class LanguagesExtension
    {
        public static CultureInfo GetCultureInfo(this Languages languageCode)
        {
            var cultura = GetCultureInfoName(languageCode);
            var novaCultura = new CultureInfo(cultura);

            return novaCultura;
        }

        /// <summary>
        /// Retorna o código padrão da Cultura/Idioma
        /// associado ao Código de idioma passado como parâmetro
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static string GetCultureInfoName(this Languages languageCode)
        {
            switch (languageCode)
            {
                case Languages.Spanish:
                    return Constants.CodeEsEs;
                case Languages.English:
                    return Constants.CodeEnUs;
                default:
                    return Constants.CodePtBr;
            }
        }
    }
}
