using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace PharmacyCompany
{
    public static class XML
    {
        /// <summary>
        /// Получение строки подключения из XML файла
        /// </summary>  
        public static string GetConnectionString()
        {
            string connectionString = String.Empty;
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load("connectionstring.xml");
                connectionString = xmlDocument.SelectSingleNode("connectionstring").InnerText;
            }
            catch
            {
                Console.WriteLine("Ошибка чтения файла со строкой подключения.");
            }
            return connectionString;

        }
    }
}
