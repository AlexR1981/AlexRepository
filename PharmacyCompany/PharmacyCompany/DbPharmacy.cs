using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;


namespace PharmacyCompany
{
    class DbPharmacy
    {
        /// <summary>
        /// Сохранение данных о товарном наименовании в БД
        /// </summary>        
        /// <returns></returns>
        public static bool SetProductName(ProductName product)
        {
            bool result = false;

            string[] strPar = new string[1];
            string[] strVal = new string[1];
            strPar[0] = "@AName";
            strVal[0] = product.name;

            result = SetDataDB("dbo.[SP_SetProductName]", "Не удалось сохранить данные о товаре в БД!", strPar, strVal);
            return result;
        }
        /// <summary>
        /// Сохранение данных об аптеке в БД
        /// </summary>        
        /// <returns></returns>
        public static bool SetPharmacy(Pharmacy pharmacy)
        {
            bool result = false;

            string[] strPar = new string[3];
            string[] strVal = new string[3];
            strPar[0] = "@AName";
            strVal[0] = pharmacy.name;
            strPar[1] = "@AAdress";
            strVal[1] = pharmacy.adress;
            strPar[2] = "@APhone";
            strVal[2] = pharmacy.phone;

            result = SetDataDB("dbo.[SP_SetPharmacy]", "Не удалось сохранить данные об аптеке в БД!", strPar, strVal);
            return result;
        }
        /// <summary>
        /// Сохранение данных об складе в БД
        /// </summary>        
        /// <returns></returns>
        public static bool SetWareHouse(WareHouse wareHouse)
        {
            bool result = false;

            using (SqlCommand comm = DbConnection.GetNewCommandPharmacy("[dbo].SP_SetWarehouse", CommandType.StoredProcedure))
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@AName", SqlDbType.Text).Value = wareHouse.name;
                comm.Parameters.Add("@APharmacyID", SqlDbType.Int).Value = wareHouse.pharmacyId;
                try
                {
                    comm.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось сохранить данные о складе в БД!", ex);
                }
            }                       
            return result;
        }
        /// <summary>
        /// Сохранение данных о партии в БД
        /// </summary>        
        /// <returns></returns>
        public static bool SetBatch(Batch batch)
        {
            bool result = false;

            using (SqlCommand comm = DbConnection.GetNewCommandPharmacy("[dbo].SP_SetBatch", CommandType.StoredProcedure))
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                comm.CommandType = CommandType.StoredProcedure;                                
                comm.Parameters.Add("@AProductNameID", SqlDbType.Int).Value = batch.productNameId;
                comm.Parameters.Add("@AWarehouseID", SqlDbType.Int).Value = batch.wareHouseId;
                comm.Parameters.Add("@AProductCount", SqlDbType.Int).Value = batch.productCount;
                try
                {
                    comm.ExecuteNonQuery();
                    Console.WriteLine("Сохранена информации о партии");    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Не удалось сохранить данные о партии в БД!", ex);
                }
            }
            return result;
        }
        /// <summary>
        /// Удаление партии
        /// </summary>
        /// <param name="pId"></param>
        public static void DeleteBatch(int pId)
        {
            if (DbPharmacy.DeleteDataDB("SP_DelBatch", pId, "Не удалось удалить партию " + pId))
                Console.WriteLine("Удалено " + pId);
        }
       
        /// <summary>
        /// Получение данных в виде DataTable из хранимой процедуры
        /// </summary>        
        /// <returns></returns>
        public static DataTable GetDataDB(string procedureName, string errorText, string paramName = "", int paramValue = 0)
        {
            DataTable result = new DataTable();

            using (SqlCommand comm = DbConnection.GetNewCommandPharmacy(procedureName, CommandType.StoredProcedure))
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                if (paramValue > 0)
                {
                    comm.Parameters.Add(paramName, SqlDbType.Int).Value = paramValue;
                }
                try
                {
                    da.Fill(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при получении данных из БД!", ex);
                }
            }

            return result;
        }
        /// <summary>
        /// Запись информации в БД
        /// </summary>        
        /// <returns></returns>
        public static bool SetDataDB(string procedureName, string errorText, string[] paramName, string[] paramValue)
        {
            using (SqlCommand comm = DbConnection.GetNewCommandPharmacy(procedureName, CommandType.StoredProcedure))
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                comm.CommandType = CommandType.StoredProcedure;
                int i = 0;
                foreach (string pName in paramName)
                {
                    comm.Parameters.Add(pName, SqlDbType.Text).Value = paramValue[i];
                    i++;
                }

                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(errorText, ex);
                }
            }
            return true;
        }
        /// <summary>
        /// Удаление записи из БД
        /// </summary>        
        /// <returns></returns>
        public static bool DeleteDataDB(string procedureName, int idItem, string errorText)
        {
            using (SqlCommand comm = DbConnection.GetNewCommandPharmacy(procedureName, CommandType.StoredProcedure))
            {
                SqlDataAdapter da = new SqlDataAdapter(comm);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@AID", SqlDbType.Int).Value = idItem;
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(errorText, ex);
                }
            }
            return true;
        }
    }
}
