using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;

namespace PharmacyCompany
{
    class DbConnection
    {       
        /// <summary>
        /// Получение экземпляра соединения с БД Pharmacydb
        /// </summary>        
        public static SqlConnection GetNewConnectionPharmacy()
        {
            SqlConnection conn = new SqlConnection(XML.GetConnectionString());

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при установлении соединения с БД Pharmacy", ex);
            }

            return conn;
        }

        /// <summary>
        ///  Получение экземпляра команды для БД Pharmacydb
        /// </summary>
        /// <param name="commandText"></param>        
        public static SqlCommand GetNewCommandPharmacy(string commandText, CommandType commandType)
        {
            SqlConnection conn = DbConnection. GetNewConnectionPharmacy();
            SqlCommand comm = new SqlCommand(commandText, conn);            
            comm.CommandType = commandType;
            return comm;
        }
    }
}
