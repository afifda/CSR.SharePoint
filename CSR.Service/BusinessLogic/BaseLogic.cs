using CSR.Service.DataAccess;
using CSR.Service.Entity;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.BusinessLogic
{
    public class BaseLogic
    {
        bool IsTransOperation;
        BaseDataAccess DataAccess;
        BaseSPDataAccess SPDataAccess;
        public void SetTransactionOperation(bool isTransaction = true)
        {
            DataAccess.SetTransactionOperation(isTransaction);
        }
        public BaseLogic()
        {
            DataAccess = new BaseDataAccess();
            SPDataAccess = new BaseSPDataAccess();
        }

        public void SetStoredProcedureOperation()
        {
            SPDataAccess = new BaseSPDataAccess();
        }

        public void SetSqlNativeOperation()
        {
            DataAccess = new BaseDataAccess();
        }

        public BaseLogic(bool isStoredProcedureOperation)
        {
            if (isStoredProcedureOperation)
            {
                SPDataAccess = new BaseSPDataAccess();
            }
            else
            {
                DataAccess = new BaseDataAccess();
            }
            
        }
        public virtual int Save<T>(T entity)
        {
            return DataAccess.SaveEntity<T>(entity);
        }
        public virtual int SPSave<T>(T entity)
        {
            return SPDataAccess.Save<T>(entity);
        }

        public string SPSaveWithOutput<T>(T entity, string outputParam)
        {
            return SPDataAccess.SaveWithOutput<T>(entity, outputParam);
        }

        public virtual int SaveBulk<T>(List<T> entityList)
        {
            return DataAccess.SaveBulkEntity<T>(entityList);
        }

        public virtual int Update<T>(T entity)
        {
            return DataAccess.UpdateEntity<T>(entity);
        }

        public virtual int SPUpdate<T>(T entity)
        {
            return SPDataAccess.Update<T>(entity);
        }

        public virtual int Delete<T>(T entity)
        {
            return DataAccess.DeleteEntity<T>(entity);
        }

        public virtual int SPDelete<T>(string key)
        {
            return SPDataAccess.Delete<T>(key);
        }

        public virtual int SPDelete<T>(T entity)
        {
            return SPDataAccess.Delete<T>(entity);
        }
        public virtual int Delete<T>(string key)
        {
            return DataAccess.DeleteEntity<T>(key);
        }

        public virtual List<T> SPRead<T>(string key) where T : new()
        {
            return SPDataAccess.Read<T>(key);
        }

        public virtual T1 SPReadWithDetails<T1, T2>(T1 entity, string detailName)
            where T1 : new()
            where T2 : new()
        {
            return SPDataAccess.ReadWithDetails<T1, T2>(entity, detailName);
        }

        public virtual List<T> SPRead<T>(T item) where T : new()
        {
            return SPDataAccess.Read<T>(item);
        }

        public virtual List<T> Read<T>(T entity) where T : new()
        {
            return DataAccess.ReadEntity<T>(entity);
        }

        public virtual DataTable Read<T>(string key, string condition = "") where T : new()
        {
            return DataAccess.ReadEntityToDataTable<T>(key, condition);
        }

        public T ReadScalar<T>(string sql)
        {
            return DataAccess.ReadScalar<T>(sql);
        }

        public int DeleteTable(string table)
        {
            return DataAccess.DeleteTable(table);
        }

        public virtual T1 ReadWithDetails<T1, T2>(T1 entity) where T1 : new() where T2 : new()
        {
            return DataAccess.ReadWithDetailsEntity<T1, T2>(entity);
        }

        public void SetReadOption(bool isReadAll, bool filteredByPK = true)
        {
            DataAccess.ReadOption(isReadAll, filteredByPK);
        }

        public DataTable LoadExcelFile<T>(string FilePath, string Extension) where T : new()
        {
            BaseDataAccess dataAccess = null;
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    dataAccess = new BaseDataAccess(ConnectionStringOptions.Excel03ConString);
                    break;

                case ".xlsx": //Excel 07
                    dataAccess = new BaseDataAccess(ConnectionStringOptions.Excel07ConString);
                    break;
            }
            string conStr = String.Format(dataAccess.Connection.ConnectionString, FilePath, "YES");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet

            connExcel.Open();
            T entity = new T();
            SqlCommand commandSql = dataAccess.Read<T>(entity);
            cmdExcel.CommandText = commandSql.CommandText;//"SELECT a.Request_No, a.Category, a.Sub_Category From [Advance_Payment_Info$] a"; 
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            //DataTable result = dt.Clone();
            //SetDataTableColumnDataType<T>(result);
            //result.Load(dt.CreateDataReader());

            return dt;
        }

        protected void SetDataTableColumnDataType<T>(DataTable dt)
        { 
            System.Reflection.PropertyInfo[] _props = typeof(T).GetProperties();

            foreach (System.Reflection.PropertyInfo propInfo in _props)
            {
                var attributes = propInfo.GetCustomAttributes(false);
                var columnMapping = attributes.FirstOrDefault(a => a.GetType() == typeof(ColumnAttribute));
                if (columnMapping != null)
                {
                    ColumnAttribute mapsto = columnMapping as ColumnAttribute;
                    if (!mapsto.ToBeRead) continue;
                    string colIndex = mapsto.UseAlias ? propInfo.Name : mapsto.ColumnName;
                    dt.Columns[colIndex].DataType = propInfo.PropertyType;
                }
            }
        }

        public void BulkCopy<T>(DataTable dt, string tableName, int batch = 10, string log = "")
        {
            BaseDataAccess dataAccess = DataAccess;
            dt.TableName = tableName;
            using (SqlConnection destinationConnection =
                       new SqlConnection(dataAccess.Connection.ConnectionString))
            {
                destinationConnection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = batch;

                    // Add your column mappings here
                    System.Reflection.PropertyInfo[] _props = typeof(T).GetProperties();
                    foreach (System.Reflection.PropertyInfo propInfo in _props)
                    {
                        if (dt.Columns.IndexOf(propInfo.Name) > -1)
                        bulkCopy.ColumnMappings.Add(propInfo.Name, propInfo.Name);
                    }
                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        LogMessageToFile(dt.Rows.Count.ToString() + " row(s) have been successsfully migrated to " + tableName + ".", log);
                    }
                    catch (Exception ex)
                    {
                        LogMessageToFile("Migration to " + tableName + " failed. Please delete the row(s) inside the table and try again", log);
                        throw ex;
                    }
                }
            }
        }
        
        public DataSet BulkCopyToStagingTable<T>(int wf_id, string file, string ext, string stagingTable, int batch = 10, string log = "") where T : new()
        {
            DataSet ds = new DataSet();            
            DataTable dt = LoadExcelFile<T>(file, ext);
            dt.TableName = stagingTable;
            DeleteTable(stagingTable);
            BulkCopy<T>(dt, stagingTable, batch, log);
            ds.Tables.Add(dt);
            return ds;
        }

        public void LogMessageToFile(string msg, string logPath)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(logPath, System.IO.FileMode.Append))
            {                
                string logLine = System.String.Format("[{0:G}]{2}{1}.{3}", System.DateTime.Now, msg, "\t", Environment.NewLine);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.WriteLine(logLine);
                
                sw.Flush();
                sw.Close();                
            }
            
        }

        public DataTable CreateDataTableFromEntity<T>()
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            var tabAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(TableAttribute));
            if (tabAttribute != null)
            {
                TableAttribute mapsto = tabAttribute as TableAttribute;
                dt.TableName = mapsto.Name;
            }
            System.Reflection.PropertyInfo[] _props = type.GetProperties();

            foreach (System.Reflection.PropertyInfo propInfo in _props)
            {
                dt.Columns.Add(propInfo.Name);
            }
            return dt;
        }

        public List<AttachmentEntity> GetAttachments(AttachmentEntity attachmntEntity)
        {
            return new AttachmentSQLDataAccess().GetAttachments(attachmntEntity);
        }

        public int SaveAttachment(List<AttachmentEntity> attachmentList)
        {
            return new AttachmentSQLDataAccess().SaveAttachment(attachmentList);
        }

        public int SaveAttachmentToSharePointLibrary(string SiteURL, string DocLib, List<AttachmentEntity> attachmentCRUDList)
        {
            int result = 0;
            
            
            if (attachmentCRUDList != null)
            {
                BaseDataAccess BaseDataAccess = new BaseDataAccess();
                result = BaseDataAccess.SaveAttachment(SiteURL, DocLib, attachmentCRUDList);
            }
            return result;
        }
        public int DeleteFolderAttach(string SiteURL, string DocLib, List<AttachmentEntity> attachmentCRUDList)
        {
            int result = 0;

            if (attachmentCRUDList != null)
            {
                BaseDataAccess BaseDataAccess = new BaseDataAccess();
                result = BaseDataAccess.DeleteFolderLibrary(SiteURL, DocLib, attachmentCRUDList);
            }

            return result;
        }

        public List<AttachmentEntity> DownloadFile(string SiteURL,string Doclib, List<AttachmentEntity> attachmentCRUDList)
        {

            if (attachmentCRUDList != null)
            {
                BaseDataAccess BaseDataAccess = new BaseDataAccess();
                attachmentCRUDList = BaseDataAccess.DownloadFile(SiteURL,Doclib, attachmentCRUDList);
            }
            return attachmentCRUDList;
        }

        public int UpdateLockedStatus(List<string> transNo, string type, bool locked)
        {
            return new BaseDataAccess().UpdateLockedStatus(transNo, type, locked);
        }

        public MasterUserByUserNameEntity UserInformation(string loginName)
        {
            return new MasterDataLogic().SPRead<MasterUserByUserNameEntity>(new MasterUserByUserNameEntity() { UserName = loginName }).FirstOrDefault();
        }

        public void sendEmailThroughGmail(string Area_Code, string transaksi_No, string Bidang, string SubjectType)
        {
            //get data email
            List<EmailEntyti> EmailEntyti = null;
            MasterDataLogic logic = new MasterDataLogic();
            EmailEntyti = logic.SPRead<EmailEntyti>(new EmailEntyti() { Area = "" });
            MasterUserByUserNameEntity UserInformationNew = new MasterUserByUserNameEntity();
            UserInformationNew = UserInformation(SPContext.Current.Web.CurrentUser.LoginName);
            string URL = "<a href=" + SPContext.Current.Web.Url + "/_layouts/15/CSR.SharePointApplication/InputProgramPage.aspx?TransaksiNo=" + transaksi_No + "> Lihat Laporan Disini </a>";

            MailMessage mM = new MailMessage();
            mM.From = new MailAddress("portal.pge1@pertamina.com");//appsetting
            mM.Subject = SubjectType;
            mM.IsBodyHtml = true;
            for (int i = 0; i < EmailEntyti.Count; i++)
            {
                try
                {

                    mM.To.Add(EmailEntyti[i].To);
                    mM.Body = "<html><head>Yth Bapak/Ibu " + EmailEntyti[i].Kepada + "</head><body><dl><dt>Bapak/Ibu " + UserInformationNew.Nama_Pegawai + "  di Area " + EmailEntyti[i].Area_Nama + " telah memasukkan data CSR bidang " + Bidang + "</dt></br><dt>Silahkan Melakukan Review melalui link Berikut:" + URL + "'</dt></br><dt></dt></br><dt>Terima Kasih</dt></dl></body></html>";


                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            SmtpClient sC = new SmtpClient("10.1.32.165");
            sC.Credentials = new NetworkCredential("portal.pge1", "pertaminapge");
            sC.Send(mM);

        }
        public void SendMailKirimKunci(string Area_Code, string Subject)
        {
            List<EmailEntyti> EmailEntyti = null;
            MasterDataLogic logic = new MasterDataLogic();
            EmailEntyti = logic.SPRead<EmailEntyti>(new EmailEntyti() { Area = "" });
            MasterUserByUserNameEntity UserInformationNew = new MasterUserByUserNameEntity();
            UserInformationNew = UserInformation(SPContext.Current.Web.CurrentUser.LoginName);
            MailMessage mM = new MailMessage();
            mM.From = new MailAddress("portal.pge1@pertamina.com");//appsetting
            mM.Subject = Subject;
            mM.IsBodyHtml = true;
            for (int i = 0; i < EmailEntyti.Count; i++)
            {
                mM.To.Add(EmailEntyti[0].To);
                mM.Body = "<html><head>Yth Bapak/Ibu " + EmailEntyti[0].Kepada + "</head><body><dl><dt>Bapak/Ibu " + UserInformationNew.Nama_Pegawai + "  di Area " + EmailEntyti[0].Area_Nama + " telah mengentri Program CSR </dt></dl></br><dt>Terima Kasih</dt></dl></body></html>";
            }
            SmtpClient sC = new SmtpClient("10.1.32.165");
            sC.Credentials = new NetworkCredential("portal.pge1", "pertaminapge");
            sC.Send(mM);

        }
    }
}
