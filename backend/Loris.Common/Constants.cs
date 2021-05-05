using System;
using System.Collections.Generic;
using System.Text;

namespace Loris.Common
{
    public sealed class Constants
    {
        #region TimeSpanEnum To String

        public const string TimeSpanEnumDays = "D";

        public const string TimeSpanEnumHours = "H";

        public const string TimeSpanEnumMinutes = "M";

        public const string TimeSpanEnumSeconds = "S";

        #endregion

        public const string MMddyyyy = "MM/dd/yyyy";
        public const string ddMMyyyy = "dd/MM/yyyy";

        public const int LoginTypeLanguageCode = 1;
        public const int LoginTypeVersion = 45;
        public const int LoginTypeUnitId = 1;
        //Language
        public const string CodePtBr = "pt-BR";
        public const string CodeEnUs = "en-US";
        public const string CodeEsEs = "es-ES";

        public const string DefaultDecimalSeparator = ",";
        public const string DefaultCulture = "pt-BR";
        public const string DefaultDateFormat = "dd/MM/yyyy";

        public const string CryptographyKey = "bf555c30b7ab4e198ceb5e3a7213f0f9";
        public const string AuthorizationKey = "ib6xdYifT9otzB8EhQHTPA==";

        public const int InternalPageSize = 500;
    }
}
