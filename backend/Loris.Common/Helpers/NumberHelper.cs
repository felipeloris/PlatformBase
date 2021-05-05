using System;

namespace Loris.Common.Helpers
{
    public static class NumberHelper
    {
        #region Números inteiros (Int e Long)

        public static int ConvertToInt(string value)
        {
            int iValue = 0;
            try
            {
                var pos = value.IndexOf(",");
                if (pos == 0)
                {
                    pos = value.IndexOf(".");
                }
                if (pos > -1)
                {
                    value = value.Substring(0, pos);
                }

                iValue = Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                //Console.Write(ex);
                throw ex;
            }
            return iValue;
        }

        public static bool ComparingLessThan(int? numberSmaller, int? numberBigger)
        {
            var numS = numberSmaller ?? 0;
            var numB = numberBigger ?? 0;

            if ((numS > 0) && (numB > 0) && (numS > numB))
            {
                return false;
            }
            return true;
        }

        public static bool ComparingLessOrEqualThan(int? numberSmaller, int? numberBigger)
        {
            var numS = numberSmaller ?? 0;
            var numB = numberBigger ?? 0;

            if ((numS > 0) && (numB > 0) && (numS >= numB))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Números flutuantes (Float e Double)

        // É necessário usar a comparação de pontos flutuantes aqui!? Se for necessário, usar 'DoubleHelper'

        public static float ConvertToFloat(string value)
        {
            float fValue = 0;
            try
            {
                fValue = float.Parse(value);
            }
            catch (Exception ex)
            {
                //Console.Write(ex);
                throw ex;
            }
            return fValue;
        }

        public static bool ComparingLessThan(double? numberSmaller, double? numberBigger, bool obrigatory)
        {
            var numS = numberSmaller ?? 0;
            var numB = numberBigger ?? 0;

            if ((DoubleHelper.IsZero(numS) || DoubleHelper.IsZero(numB)
                && (!obrigatory)))
            {
                return true;
            }

            return DoubleHelper.LessThan(numS, numB);
        }

        public static bool ComparingLessOrEqualThan(double? numberSmaller, double? numberBigger)
        {
            var numS = numberSmaller ?? 0;
            var numB = numberBigger ?? 0;

            if ((numS > 0) && (numB > 0) && (numS >= numB))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
