using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Loris.Common.Log;

namespace Loris.Common.Tools
{
    public static class GlobalFunction
    {
        private static readonly LogManager Logger = new LogManager(typeof(GlobalFunction));
        private static IConfigurationRoot _configuration;

        public static string ReadConfigValue(string key, string defaultValue = null)
        {
            string configValue;

            try
            {
                //configValue = ConfigurationManager.AppSettings[key] ?? defaultValue ?? string.Empty;

                // Acessa "appsettings.json"
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
                    _configuration = builder.Build();
                }

                configValue = _configuration[key] ?? defaultValue ?? string.Empty;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                configValue = defaultValue ?? string.Empty;
            }

            return configValue;
        }

        public static string PathApplication()
        {
            // AppDomain.CurrentDomain.BaseDirectory
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string PathConfig()
        {
            var defaultPath = PathApplication() + "\\Config";
            var pathConfig = ReadConfigValue("Loris:ConfigPath", defaultPath);
            if (!Directory.Exists(pathConfig))
            {
                throw new ArgumentException($"'{pathConfig}' not exist!");
            }
            return pathConfig;
        }

        public static string EncryptPwd(string password)
        {
            var key = new string(new[] { (char)81, (char)64, (char)82, (char)37, (char)38, (char)89, (char)74 });
            var tamChave = key.Length;

            var arrPassword = new string[password.Length + 1];
            for (var i = 0; i < password.Length; i++)
            {
                arrPassword[i + 1] = password.Substring(i, 1);
            }

            for (var x = 1; x <= password.Length; x++)
            {
                var resto = (x % tamChave);
                var restoZero = (resto == 0) ? -1 : 0;
                var inicio = (resto - tamChave * restoZero);

                // objeto "Strings" é disponibilizado pelo pacote Microsft.VisualBasic
                //var Pass = Strings.Asc(Strings.Mid(key, inicio, 1));
                var pass = Asc(Mid(key, inicio, 1));
                //var arrPassword[x] = Strings.Chr(Strings.Asc(arrPassword[x]) ^ pass).ToString();
                arrPassword[x] = Chr(Asc(arrPassword[x]) ^ pass);
            }

            var retorno = string.Empty;
            for (var i = 1; i < arrPassword.Length; i++)
            {
                retorno += arrPassword[i];
            }

            return retorno;
        }

        public static string Chr(int charCode)
        {
            if (charCode > 255)
            {
                throw new ArgumentOutOfRangeException("CharCode", charCode, "CharCode must be between 0 and 255.");
            }
            return Encoding.Default.GetString(new[] { (byte)charCode });
        }

        public static int Asc(string letra)
        {
            //return (int)(Convert.ToChar(letra));
            return Asc(letra[0]);
        }

        public static int Asc(char c)
        {
            int converted = c;
            if (converted >= 0x80)
            {
                byte[] buffer = new byte[2];
                if (Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
                {
                    converted = buffer[0];
                }
                else
                {
                    // troca de byte entre posição 1 e 2
                    converted = buffer[0] << 16 | buffer[1];
                }
            }
            return converted;
        }

        private static string Mid(string texto, int posIni, int qtd)
        {
            return texto.Substring(posIni - 1, qtd);
        }

        /*
         * => incompatibilidade com Docker Linux?!?!?!!?!
         * 
        public static string ReadRegistryValue(string key, string subkey)
        {
            var software = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            return software?.OpenSubKey(key)?.GetValue(subkey)?.ToString();
        }

        public static bool SaveRegistryValue(string key, string subkey, string value)
        {
            var software = Registry.LocalMachine.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree);
            var registryKey = software?.CreateSubKey(key);
            if (registryKey == null)
            {
                return false;
            }

            try
            {
                registryKey.SetValue(subkey, value);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                registryKey.Close();
            }
            return true;
        }
        */
    }
}
