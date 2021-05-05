using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Loris.Common.Helpers;

namespace Loris.Common.Tools
{
    public static class ValidationTools
    {
        /// <summary>
        /// Trata campo string vazio
        /// </summary>
        /// <param name="input">Valor String</param>
        /// <returns>String vazia ""</returns>
        public static string ValidEmptyString(string input)
        {
            string emptyString = "";

            if (!string.IsNullOrEmpty(input))
                emptyString = input;

            if (emptyString.Trim() == "-")
                emptyString = "";

            return emptyString;
        }

        /// <summary>
        ///  Válidar Cpf
        /// </summary>
        /// <param name="cpf">valor do Cpf</param>
        /// <returns>True - Validado</returns>
        public static bool IsCpf(string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            var tempCpf = cpf.Substring(0, 9);
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += NumberHelper.ConvertToInt(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += NumberHelper.ConvertToInt(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Válidar Cnpj
        /// </summary>
        /// <param name="cnpj">valor do cnpj</param>
        /// <returns>True - Validado</returns>
        public static bool IsCnpj(string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            var tempCnpj = cnpj.Substring(0, 12);

            int soma = 0;
            for (var i = 0; i < 12; i++)
            {
                soma += NumberHelper.ConvertToInt(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            var resto = (soma % 11);
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += NumberHelper.ConvertToInt(tempCnpj[i].ToString(CultureInfo.InvariantCulture)) * multiplicador2[i];
            }

            resto = (soma % 11);
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString(CultureInfo.InvariantCulture);

            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Validar cnpj ou cpf
        /// </summary>
        /// <param name="value">valor a ser validado</param>
        /// <returns>true - Válido</returns>
        public static bool DocNumValidate(string value)
        {

            const string rfc = @"^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3})?)";
            const string rfcMoral = @"^(([A-Z]|[a-z]){3})([0-9]{6})";


            //Verifica se foi passado valor
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            //Se houver letras utiliza a validação
            //de documento do méxico
            if (value.Any(char.IsLetter))
            {

                //Se possuir 9 digitos é PJ (rfcMoral)
                if (value.Length == 9)
                {
                    return Regex.IsMatch(value, rfcMoral, RegexOptions.IgnoreCase);
                }

                //Se possuir > 9 digitos é PF (rfc)

                //Nao sei a utilizadade disso, mas estava 
                //na funcao original feita em java script 
                if (value.Length == 12)
                {
                    value = string.Concat(" ", value);
                }

                return Regex.IsMatch(value, rfc, RegexOptions.IgnoreCase);

            }

            switch (value.Length)
            {
                case 11:
                    return IsCpf(value);
                case 14:
                    return IsCnpj(value);
            }

            return false;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc">conteúdo a ser mascarado</param>
        /// <returns>conteúdo mascarado</returns>
        public static string MascaraContaCorrente(string cc)
        {
            if (cc == null)
            {
                return null;
            }

            var mascaraConta = cc.Trim();
            var mascaraRet = "";
            var intLength = mascaraConta.Length;
            var intMascara = mascaraConta.IndexOf("x");

            if (intLength < 7 || intMascara > -1)
            {
                return mascaraConta;
            }

            int x = 0;

            while (x < intLength)
            {
                switch (x)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        mascaraRet = mascaraRet + mascaraConta.Substring(x, 1);
                        break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        mascaraRet = mascaraRet + "x";
                        break;
                    default:
                        mascaraRet = mascaraRet + mascaraConta.Substring(x, 1);
                        break;
                }
                x = x + 1;
            }
            return mascaraRet;
        }

        /// <summary>
        /// Função para validar Email
        /// </summary>
        /// <param name="Email">Email a ser validado</param>
        /// <returns>True ou False</returns>
        public static bool ValidarEmail(string Email)
        {
            var validEmail = false;
            var indexArr = Email.IndexOf("@");
            if (indexArr > 0)
            {
                if (Email.IndexOf("@", indexArr + 1) > 0)
                {
                    return validEmail;
                }

                var indexDot = Email.IndexOf(".", indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < Email.Length)
                    {
                        var indexDot2 = Email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {
                            validEmail = true;
                        }
                    }
                }
            }
            return validEmail;
        }

        /// <summary>
        /// Validar cnpj ou cpf
        /// </summary>
        /// <param name="valor">valor a ser validado</param>
        /// <returns>true - Válido</returns>
        public static bool ValidCpfCnpj(string valor)
        {
            return valor.Length != 14 ? IsCpf(valor) : IsCnpj(valor);
        }

        #region Métodos para simular as validações do Castle

        public static bool ValidateInvalidLength(string txt, int minLength, int maxLength)
        {
            if (txt == null)
            {
                return true;
            }

            var length = txt.Trim().Length;
            if ((length < minLength) || (length > maxLength))
            {
                return true;
            }
            return false;
        }

        public static bool ValidatorIsEmptyOrNull(string txt)
        {
            return ValidateInvalidLength(txt, 1, Int32.MaxValue);
        }

        public static bool ValidateOutOfRange(string txt, long minRange, long maxRange)
        {
            long number = 0;
            if (!long.TryParse(txt, out number))
            {
                return true;
            }
            return ValidateOutOfRange(number, minRange, maxRange);
        }

        public static bool ValidateOutOfRange(long number, long minRange, long maxRange)
        {
            if ((number < minRange) || (number > maxRange))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDateTimeNotOk(string txt, DateTime? minRange = null, DateTime? maxRange = null)
        {
            if (minRange == null)
            {
                minRange = DateTime.MinValue;
            }

            if (maxRange == null)
            {
                maxRange = DateTime.MaxValue;
            }

            var datetime = DateTime.MinValue;
            if (!DateTime.TryParse(txt, out datetime))
            {
                return true;
            }

            if ((datetime < minRange) || (datetime > maxRange))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateTimespanNotOk(string txt, TimeSpan? minRange = null, TimeSpan? maxRange = null)
        {
            if (minRange == null)
            {
                minRange = TimeSpan.MinValue;
            }

            if (maxRange == null)
            {
                maxRange = TimeSpan.MaxValue;
            }

            var time = TimeSpan.MinValue;
            if (!TimeSpan.TryParse(txt, out time))
            {
                return true;
            }

            if ((time < minRange) || (time > maxRange))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateRegExpNotOk(string txt, string pattern)
        {
            var regexRule = new Regex(pattern, RegexOptions.Compiled);

            if (txt != null && txt.Trim() != "")
            {
                return !regexRule.IsMatch(txt);
            }

            return true;
        }

        public static bool ValidateEmail(string txt)
        {
            var pattern = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
            return !ValidateRegExpNotOk(txt, pattern);
        }

        #endregion
    }
}
