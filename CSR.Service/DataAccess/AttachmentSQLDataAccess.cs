using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSR.Service.Entity;
using System.Data.SqlClient;
using System.Data;
using UtilityLibrary;
namespace CSR.Service.DataAccess
{
    public class AttachmentSQLDataAccess : BaseSPDataAccess
    {
        private const string USP_GET_PAGING_ATTACHMENT = "usp_ReadAttachment";
        private const string USP_SAVE_ATTACHMENT = "usp_SaveAttachment";

        private const string PARAM_REQUEST_NO = "@TransaksiNo";
        private const string PARAM_TVP_ATTACHMENT = "@attachmentList";

        public List<AttachmentEntity> GetAttachments(AttachmentEntity attachmententity)
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            SqlCommand command = new SqlCommand(USP_GET_PAGING_ATTACHMENT);
            command.CommandType = CommandType.StoredProcedure;
            List<AttachmentEntity> entityList = null;
            IDataReader reader = null;
            
            command.Parameters.Add(PARAM_REQUEST_NO, attachmententity.TransaksiNo);  
            try
            {
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                entityList = mapData.MapDataToEntityList<AttachmentEntity>(reader);
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
            return entityList;
        }

        public int SaveAttachment(List<AttachmentEntity> attachmentList)
        {
            SqlCommand command = new SqlCommand(USP_SAVE_ATTACHMENT);
            command.CommandType = CommandType.StoredProcedure;
            int rowAffected = 0;
            IDataReader reader = null;
            DataTable attachmentTable = ToDataTable<AttachmentEntity>(attachmentList);
            attachmentTable.Columns.Remove("TempPath");
            ReorderTable(ref attachmentTable, "TransaksiNo", "NamaFile", "NamaPath", "Flag", "NoUrut");
            AddInParameter(command, PARAM_TVP_ATTACHMENT, attachmentTable);

            try
            {
                OpenConnection(command, this.Connection);
                rowAffected = command.ExecuteNonQuery();
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

            return rowAffected;
        }
    }
}
