using System;

namespace Loris.Common.Domain.Entities
{
    public class Language
    {
        /// <summary>
        /// Construtor sem parametro
        /// </summary>
        public Language() { }

        /// <summary>
        /// Construtor que recebe um código de idioma como byte
        /// </summary>
        /// <param name="languageCode"></param>
        public Language(byte languageCode)
        {
            LanguageNumeric = languageCode;
        }

        public Language(Languages languageCode)
        {
            LanguageCode = languageCode;
        }

        public Languages LanguageCode { get; private set; }

        public byte LanguageNumeric
        {
            set
            {
                if (Enum.IsDefined(typeof(Languages), value))
                    LanguageCode = (Languages)Enum.ToObject(typeof(Languages), value);
                else
                    throw new Exception($"Invalid Language Numeric {value}! ");
            }
            get { return (byte)LanguageCode; }
        }
    }
}
