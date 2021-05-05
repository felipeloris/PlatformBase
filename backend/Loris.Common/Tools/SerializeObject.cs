using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Loris.Common.Tools
{
    public static class SerializeObject
    {

        private class StringWriterWithEncoding : StringWriter
        {
            Encoding encoding;

            public StringWriterWithEncoding(Encoding encoding)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return encoding; }
            }
        }

        /// <summary>
        /// Serializa um objeto em XML
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="encoding">Encoding usado para gerar o XML (default = utf-16) 
        /// Padrão Português Brasileiro = "iso-8859-1"
        /// </param>
        /// <returns></returns>
        public static string ObjectToXml(object obj, string encoding = "utf-16")
        {
            if (obj == null)
            {
                return null;
            }

            // Serializa
            var tipo = obj.GetType();
            //var w = new StringWriterWithEncoding(Encoding.GetEncoding("iso-8859-1"));
            var w = new StringWriterWithEncoding(Encoding.GetEncoding(encoding));
            //StringWriter w = new StringWriter();
            var s = new XmlSerializer(tipo);
            s.Serialize(w, obj);

            // Insere o tipo de objeto
            var doc = new XmlDocument();
            doc.LoadXml(w.ToString());
            doc.DocumentElement?.SetAttribute("tipo_classe", tipo.AssemblyQualifiedName);

            return doc.OuterXml;
        }

        public static string ObjectToXml(object obj)
        {
            var sb = new StringBuilder();
            var ser = new DataContractSerializer(obj.GetType());

            using (var xmlWriter = XmlWriter.Create(sb))
            {
                ser.WriteObject(xmlWriter, obj);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Deserializa um XML em um objeto
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object XmlToObject(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }

            // Descobre o tipo de objeto
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            if (doc.DocumentElement != null)
            {
                var nmTipo = doc.DocumentElement.GetAttribute("tipo_classe");
                var tipo = Type.GetType(nmTipo, true);

                // Deserializa
                return XmlToObject(xml, tipo);
            }
            return null;
        }

        private static object XmlToObject(string xml, Type tipo)
        {
            var r = new StringReader(xml);
            var s = new XmlSerializer(tipo);
            return s.Deserialize(r);
        }

        /// <summary>
        /// Serializa um objeto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MemoryStream ObjectToStream(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var bin = new BinaryFormatter();
            var stream = new MemoryStream();
            bin.Serialize(stream, obj);
            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Deserealiza um objeto
        /// </summary>
        /// <param name="msData"></param>
        /// <returns></returns>
        public static object StreamToObject(MemoryStream msData)
        {
            if (msData == null)
            {
                return null;
            }

            var bin = new BinaryFormatter();

            return bin.Deserialize(msData);
        }

        /// <summary>
        /// Serializa um objeto que tenha o atributo [Serializable]
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var bin = new BinaryFormatter();
            var stream = new MemoryStream();
            bin.Serialize(stream, obj);

            return stream.ToArray();
        }

        /// <summary>
        /// Deserializa um objeto que tenha o atributo [Serializable]
        /// </summary>
        /// <param name="objSerial"></param>
        /// <returns></returns>
        public static object ByteArrayToObject(byte[] objSerial)
        {
            if (objSerial == null || objSerial.Count() == 0)
            {
                return null;
            }

            var bin = new BinaryFormatter();
            var stream = new MemoryStream(objSerial);
            return bin.Deserialize(stream);
        }

        /// <summary>
        /// Serializa um objeto que tenha o atributo [Serializable]
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="encoding">Encoding usado para gerar o XML (default = utf-16) 
        /// Padrão Português Brasileiro = "iso-8859-1"
        /// </param>
        /// <returns></returns>
        public static string ObjectToString(object obj, string encoding = "utf-16")
        {
            var objByte = ObjectToByteArray(obj);
            if (objByte == null)
            {
                return null;
            }

            var objStr = Encoding.GetEncoding(encoding).GetString(objByte);
            return objStr;
        }

        public static object StringToObject(string objInString, string encoding = "utf-16")
        {
            if ((objInString == null) || (objInString.Length == 0))
            {
                return null;
            }

            var objByte = Encoding.GetEncoding(encoding).GetBytes(objInString);
            var obj = ByteArrayToObject(objByte);
            return obj;
        }

        /// <summary>
        /// Converte ByteArray para string Hexadecimal
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] byteArray)
        {
            var hex = new StringBuilder(byteArray.Length * 2);
            foreach (var b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static void WriteObjectXml(string path, object obj)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                var writer = XmlDictionaryWriter.CreateTextWriter(fs);
                var ser = new DataContractSerializer(obj.GetType());
                ser.WriteObject(writer, obj);
                writer.Close();
                fs.Close();
            }
        }

        public static object ReadObjectXml(string path, Type tObj)
        {
            object obj = null;
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                var ser = new DataContractSerializer(tObj);
                obj = ser.ReadObject(reader);
                fs.Close();
            }
            return obj;
        }

        public static byte[] DcObjectToByteArray(object obj)
        {
            var ser = new DataContractSerializer(obj.GetType());
            var mem = new MemoryStream();
            ser.WriteObject(mem, obj);
            return mem.ToArray();
        }

        public static object DcByteArrayToObject(Type type, byte[] byteArray)
        {
            var ser = new DataContractSerializer(type);
            var mem = new MemoryStream(byteArray);
            return ser.ReadObject(mem);
        }

        /// <summary>
        /// Serializa um objeto para Json
        /// </summary>
        public static string ToJson<T>(T obj)
        {
            return JsonSerializer.Serialize<T>(obj);
        }

        public static T FromJson<T>(string strObj)
        {
            return JsonSerializer.Deserialize<T>(strObj);
        }
    }
}
