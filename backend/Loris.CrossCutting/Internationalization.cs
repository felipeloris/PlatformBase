using Loris.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Loris.Common.Extensions;
using System.Text;
using System.Web;
using System.Text.Json;
using Loris.Resources;
using System.Globalization;
using Loris.Common.Tools;

namespace Loris.CrossCutting
{
    public static class Internationalization
    {
        private static Dictionary<string, string> MakeDictionary(Dictionary<string, string> dic, Languages language, ResourceManager rm)
        {
            var resourceSet = rm.GetResourceSet(language.GetCultureInfo(), true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                var resourceKey = entry.Key.ToString();
                var resource = entry.Value?.ToString() ?? "";

                dic.Add(resourceKey, resource);
            }

            return dic;
        }

        private static void GetDictionaries(Dictionary<string, string> dic, Languages language)
        {
            MakeDictionary(dic, language, BASERES.ResourceManager);
        }

        public static string GetAllByLanguage(Languages language)
        {
            var dic = new Dictionary<string, string>();
            GetDictionaries(dic, language);

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            return JsonSerializer.Serialize(dic, jsonOptions);
        }
        
        public static string GetAll()
        {
            var langs = Enum.GetValues(typeof(Languages));
            var sb = new StringBuilder();
            
            sb.Append("{");
            foreach (Languages lang in langs)
            {
                if (lang == Languages.Undefined)
                    continue;

                var dic = new Dictionary<string, string>();
                GetDictionaries(dic, lang);

                if (dic.Count > 0)
                {
                    sb.Append($"'{lang.GetCultureInfoName()}': {{");
                    foreach (var item in dic)
                    {
                        var key = HttpUtility.HtmlEncode(item.Key);
                        var value = HttpUtility.HtmlEncode(item.Value);

                        sb.Append($"'{key}': '{value}', ");
                    }
                    sb.Append("},");
                }
            }
            sb.Append("}");

            return sb.ToString();
        }

        public static string Get(Languages language, string key, string defaultValue)
        {
            var dic = new Dictionary<string, string>();
            GetDictionaries(dic, language);

            return dic.ContainsKey(key) ? dic[key] : defaultValue;
        }

        public static string LanguageName(Languages languageCode)
        {
            switch (languageCode)
            {
                case Languages.Portuguese:
                    return BASERES.lbl_lang_portuguese;
                case Languages.English:
                    return BASERES.lbl_lang_english;
                case Languages.Spanish:
                    return BASERES.lbl_lang_spanish;
                default:
                    return BASERES.lbl_lang_not_informed;
            }
        }
    }
}
