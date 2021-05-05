using System;
using System.Text.RegularExpressions;

namespace Loris.Common.Helpers
{
    public static class GuidHelper
    {
        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

        public static bool IsGuidByRegex(string expression)
        {
            if (expression != null)
            {
                var guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                return guidRegEx.IsMatch(expression);
            }
            return false;
        }
    }
}
