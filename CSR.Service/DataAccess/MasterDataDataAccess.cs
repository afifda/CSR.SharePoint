using CSR.Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSR.Service.DataAccess
{
    public class MasterDataDataAccess : BaseSPDataAccess
    {
        public List<int> GetAvailableYear()
        {
            List<int> yearList = new List<int>();
            IDataReader reader = null;
            try 
            { 
                SqlCommand command = new SqlCommand("usp_GetYear");
                command.CommandType = CommandType.StoredProcedure;
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                { 
                    yearList.Add((int)reader["Available_Year"]);
                }
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return yearList;
        }
    }
}
