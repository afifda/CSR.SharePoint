using CSR.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.DataAccess
{
    public class ProgramDataAccess : BaseSPDataAccess
    {
        public List<ProgramEntity> GetProgramList(int year)
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            List<ProgramEntity> programList = null;
            IDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand("usp_ReadProgramList");
                command.Parameters.AddWithValue("@Year", year);
                command.CommandType = CommandType.StoredProcedure;
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                programList = mapData.MapDataToEntityList<ProgramEntity>(reader);
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
            return programList;
        }
    }
}
