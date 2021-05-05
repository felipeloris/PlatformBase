using System;
using System.IO;
using System.Net;

namespace Loris.Common.Tools
{
    public static class SoapTools
    {
        public static string InvokeSOAP_HTTP(string url, string soapRequest,
            string credentialUser = null, string credentialPassword = null)
        {
            string soapRetorno = null;

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                //req.Headers.Add("SOAPAction\"http://tempuri.org/Register\"");
                req.ContentType = "text/xml;charset=\"utf-8\"";
                req.Accept = "text/xml";
                req.Method = "POST";

                if (!string.IsNullOrEmpty(credentialUser) && !string.IsNullOrEmpty(credentialPassword))
                {
                    req.Credentials = new NetworkCredential(credentialUser, credentialPassword);
                    req.PreAuthenticate = true;
                }

                // Requisição
                using (var stm = req.GetRequestStream())
                {
                    using (var stmw = new StreamWriter(stm))
                    {
                        stmw.Write(soapRequest);
                    }
                }

                // Resposta
                var webResponse = req.GetResponse();
                var responseStream = webResponse.GetResponseStream();

                if (responseStream != null)
                {
                    var reader = new StreamReader(responseStream);
                    soapRetorno = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (WebException ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        throw new Exception("Erro protocolo!", ex);

                    case WebExceptionStatus.ConnectFailure:
                        throw new Exception("Falha na conexão!", ex);

                    case WebExceptionStatus.Timeout:
                        throw new TimeoutException("Servidor não respondeu em tempo hábil!", ex);
                }
            }

            return soapRetorno;
        }
    }
}
