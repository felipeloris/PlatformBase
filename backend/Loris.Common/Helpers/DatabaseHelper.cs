using Loris.Common.Cryptography;
using Loris.Common.Domain.Entities;
using System.IO;
using System.Text;
using System;
using Loris.Common.Log;
using Loris.Common.Tools;

namespace Loris.Common.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly LogManager Logger = new LogManager(typeof(DatabaseHelper));

        public static string GetConfigFileLoc()
        {
            // Nome do arquivo de configuração
            var configDbFile = GlobalFunction.ReadConfigValue("Loris:ConfigDbFile", "");
            if (configDbFile.Substring(configDbFile.Length - 9) != ".dbconfig")
            {
                configDbFile = string.Concat(configDbFile, ".dbconfig");
            }

            // Caminho do arquivo de configuração
            var configPath = GlobalFunction.PathConfig();
            configDbFile = string.Concat(configPath, "\\", configDbFile);

            return configDbFile;
        }

        public static void SaveDatabase(Database configDb, string configDbFile = null)
        {
            Logger.LogDebugStart("SaveDatabase");

            try
            {
                // Retorna o arquivo de configuração
                if (string.IsNullOrEmpty(configDbFile))
                    configDbFile = GetConfigFileLoc();

                // Verifica se o arquivo existe
                if (!File.Exists(configDbFile))
                    throw new FileNotFoundException(configDbFile);

                var jsonConfig = SerializeObject.ToJson(configDb);
                var jsonConfigCrypt = CipherUtility.AesEncrypt(jsonConfig);

                File.WriteAllText(configDbFile, jsonConfigCrypt, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("SaveDatabase");
            }
        }

        public static Database RecoverDatabase(string configDbFile = null)
        {
            try
            {
                Logger.LogDebugStart("RecoverDatabase");

                // Retorna o arquivo de configuração
                if (string.IsNullOrEmpty(configDbFile))
                    configDbFile = GetConfigFileLoc();

                // Verifica se o arquivo existe
                if (!File.Exists(configDbFile))
                    throw new FileNotFoundException(configDbFile);

                // Arquivo existe, lê as linhas e decriptografa
                var jsonConfigCrypt = File.ReadAllText(configDbFile, Encoding.UTF8);
                var jsonConfig = CipherUtility.AesDecrypt(jsonConfigCrypt);
                var database = SerializeObject.FromJson<Database>(jsonConfig);

                return database;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("RecoverDatabase");
            }
        }
    }
}
