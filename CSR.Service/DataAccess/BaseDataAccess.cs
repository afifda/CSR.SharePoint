using CSR.Service.Entity;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.DataAccess
{
    public class BaseDataAccess : DoubleASqlTextCommand
    {
        
        public DoubleASqlConnection Connection { get; set; }
        bool IsTransOperation;
        
        public void SetTransactionOperation(bool isTransaction = true)
        {
            this.IsTransOperation = isTransaction;
        }
        public BaseDataAccess()
        {
            Connection = new DoubleASqlConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringOptions.CSRConnection.ToString()].ConnectionString;            
        }
        public BaseDataAccess(ConnectionStringOptions connectionString)
        {
            Connection = new DoubleASqlConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString.ToString()].ConnectionString;
        }

        public virtual int SaveEntity<T>(T entity)
        {
            int rowsAffected = 0;            
            try
            {
                SqlCommand command = base.Save<T>(entity);
                OpenConnection(command, this.Connection);
                rowsAffected = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return rowsAffected;
        }

        public virtual int SaveBulkEntity<T>(List<T> entityList)
        {
            int rowsAffected = 0;
            try
            {
                SqlCommand command = base.SaveBulk<T>(entityList);
                OpenConnection(command, this.Connection);
                rowsAffected = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return rowsAffected;
        }

        public virtual int UpdateEntity<T>(T entity)
        {
            int rowsAffected = 0;            
            try
            {
                SqlCommand command = base.Update<T>(entity);
                OpenConnection(command, this.Connection);
                rowsAffected = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return rowsAffected;
        }

        public virtual int DeleteEntity<T>(T entity)
        {
            int rowsAffected = 0;            
            try
            {
                SqlCommand command = base.Delete<T>(entity);
                OpenConnection(command, this.Connection);
                rowsAffected = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return rowsAffected;
        }

        public virtual List<T> ReadEntity<T>(T entity) where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            List<T> entityList = null;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(entity);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                entityList = mapData.MapDataToListWithAttributes<T>(reader);
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

        public virtual List<T> ReadEntity<T>(string key) where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            List<T> entityList = null;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(key);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                entityList = mapData.MapDataToListWithAttributes<T>(reader);
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

        public virtual int DeleteEntity<T>(string key)
        {
            int rowsAffected = 0;
            try
            {
                SqlCommand command = base.Delete<T>(key);
                OpenConnection(command, this.Connection);
                rowsAffected = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return rowsAffected;
        }

        public T ReadScalar<T>(string sql)
        {            
            T data;
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                OpenConnection(command, this.Connection);
                data = (T)command.ExecuteScalar();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            finally
            {
                
            }
            return data;
        }

        public int DeleteTable(string table)
        {
            int data;
            string sql = string.Format("DELETE {0}", table);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                OpenConnection(command, this.Connection);
                data = command.ExecuteNonQuery();
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            finally
            {

            }
            return data;
        }

        public DataTable ReadEntityToDataTable<T>(string key, string condition = "") where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            DataTable dt = new DataTable();
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(key);
                command.CommandText += string.IsNullOrEmpty(condition) ? string.Empty : " " + condition;
                Type type = typeof(T);
                object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), true);
                var tabAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(TableAttribute));
                if (tabAttribute != null)
                {
                    TableAttribute mapsto = tabAttribute as TableAttribute;
                    dt.TableName = mapsto.Name;
                }
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                dt.Load(reader);
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
            return dt;
        }


        public virtual T1 ReadWithDetailsEntity<T1, T2>(T1 entity)
            where T1 : new()
            where T2 : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            T1 outEntity;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T1>(entity);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                outEntity = mapData.MapDataToEntityWithDetailsWithAttributes<T1, T2>(reader);
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
            return outEntity;
        }

        public void ReadOption(bool isReadAll, bool filteredByPK = true)
        {
            SetReadOption(isReadAll, filteredByPK);
        }

        public void OpenConnection(SqlCommand command)
        {
            Connection.OpenConnection();
            command.Connection = this.Connection.DBConnection;
            if (IsTransOperation)
            {
                Connection.BeginTransaction();
                command.Transaction = this.Connection.DBTransaction;
            }
        }

        public void OpenConnection(SqlCommand command, DoubleASqlConnection conn)
        {
            if (conn.DBConnection == null || conn.DBConnection.State == ConnectionState.Closed) conn.OpenConnection();
            command.Connection = conn.DBConnection;
            if (IsTransOperation)
            {
                if (conn.DBTransaction == null) Connection.BeginTransaction();                
                command.Transaction = conn.DBTransaction;
            }
        }

        public void CloseCommitConnection()
        {
            if (IsTransOperation)
            {
                Connection.CommitTransaction();
            }
            Connection.CloseConnection();            
        }

        public void CloseRollbackConnection()
        {
            if (IsTransOperation)
            {
                Connection.RollbackTransaction();
            }
            Connection.CloseConnection();            
        }

        public int UpdateLockedStatus(List<string> transNoList, string type, bool locked)
        {
            SqlCommand command = new SqlCommand("usp_UpdateLocked");
            command.CommandType = CommandType.StoredProcedure;
            int rowAffected = 0;
            IDataReader reader = null;
            DataTable transNoTable = ToDataTableFromStringList(transNoList);
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@TransaksiNo";
            parameter.Value = transNoTable;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Locked", locked);
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

        public void CallUpdateList()
        { 
           
        }

        public DataTable ReadEntityWithExternalFileToDataTable<T>(string key, string condition = "", string columnName = "Col1", string columnPath = "colPath") where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            DataTable dt = new DataTable();
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(key);
                command.CommandText += string.IsNullOrEmpty(condition) ? string.Empty : " " + condition;
                Type type = typeof(T);
                object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), true);
                var tabAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(TableAttribute));
                if (tabAttribute != null)
                {
                    TableAttribute mapsto = tabAttribute as TableAttribute;
                    dt.TableName = mapsto.Name;
                }
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                dt.Load(reader);
                dt.Columns[columnName].ReadOnly = false;
                dt.Columns[columnName].MaxLength = -1;
                foreach (DataRow item in dt.Rows)
                {
                    item[columnName] = ReadExternalFile(item[columnPath].ToString());
                }
                dt.Columns.Remove(columnPath);
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
            return dt;
        }

        public string ReadExternalFile(string FilePath)
        {
            string contents = string.Empty;
            using (System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                contents = sr.ReadToEnd();

                sr.Close();
                sr.Dispose();
            }
            return contents;
        }

        public int SaveAttachment(string siteUrl, string documentLibraryName,List<AttachmentEntity> attachmentCRUDList)/// string fileToUpload, string sharePointSite, string documentLibraryName, string subfolder)
        {

            int result = 0;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(siteUrl))
                {

                    site.AllowUnsafeUpdates = true;

                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList docLib = web.Lists.TryGetList(documentLibraryName);
                        if (docLib == null)
                        {
                            throw new Exception(string.Format("Document libary {0} cannot be found", documentLibraryName));
                        }

                        web.AllowUnsafeUpdates = true;

                        /*** If you want to add inside a folder*******/
                        SPFolder SubFolder = (from SPFolder folder in docLib.RootFolder.SubFolders
                                              where folder.Url == docLib.RootFolder.Url + "/" + attachmentCRUDList[0].TransaksiNo
                                              select folder).FirstOrDefault();

                        if (SubFolder == null)
                            SubFolder = docLib.RootFolder.SubFolders.Add(attachmentCRUDList[0].TransaksiNo);
                        foreach (AttachmentEntity attachmentCRUD in attachmentCRUDList)
                        {
                            string fileToUpload = attachmentCRUD.TempPath;
                            //upload only files that needs to be uploaded, not file that already in the server
                            if (fileToUpload.Contains('\\'))
                            {
                                if (!System.IO.File.Exists(fileToUpload))
                                    throw new FileNotFoundException("File not found.", fileToUpload);

                                //get filename and get path
                                string strFilePath = Path.GetFullPath(fileToUpload);
                                string fileName1 = Path.GetFileName(fileToUpload);

                                //FileStream fs = File.OpenRead(strFilePath);
                                FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                                byte[] FileContent = new byte[fs.Length];
                                fs.Read(FileContent, 0, Convert.ToInt32(fs.Length));
                                fs.Close();

                                //Add the file to the sub-folder
                                SPFile file = SubFolder.Files.Add(SubFolder.Url + "/" + fileName1, FileContent, true);
                                attachmentCRUD.NamaPath = file.Url;
                            }

                        }
                        SubFolder.Update();
                        result = SubFolder.Item.ID;
                        web.AllowUnsafeUpdates = false;
                        
                    }
                }
            });
            return result;
        }
        const string TEMP_FILE = "TempFile";
        public List<AttachmentEntity> DownloadFile(string siteUrl, string documentLibraryName, List<AttachmentEntity> attachmentCRUDList)
        {
            string tempFolder = System.Configuration.ConfigurationManager.AppSettings[TEMP_FILE];

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(siteUrl))
                {
                    site.AllowUnsafeUpdates = true;
                    using (SPWeb web = site.OpenWeb("/Sharepointfree/"))
                    {
                        try
                        {
                            SPList docLib = web.Lists.TryGetList(documentLibraryName);
                            if (docLib == null)
                            {
                                throw new Exception(string.Format("Document libary {0} cannot be found", documentLibraryName));
                            }

                            web.AllowUnsafeUpdates = true;

                            /*** If you want to add inside a folder*******/
                            SPFolder SubFolder = (from SPFolder folder in docLib.RootFolder.SubFolders
                                                  where folder.Name == attachmentCRUDList[0].TransaksiNo
                                                  select folder).FirstOrDefault();


                            if (SubFolder == null)
                                SubFolder = docLib.RootFolder.SubFolders.Add(attachmentCRUDList[0].TransaksiNo);
                            

                            for (int i = 0; i < attachmentCRUDList.Count; i++)
                            {
                                string path = site.Url + "/" + attachmentCRUDList[i].NamaPath;
                                SPFile file = SubFolder.Files[attachmentCRUDList[i].NamaFile];
                                if (file.Exists)
                                {
                                    WebClient client1 = new WebClient();
                                    client1.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                    //stream file on local 
                                    FileStream outStream = new FileStream(tempFolder + file.Name, FileMode.Create);
                                    outStream.Close();
                                    attachmentCRUDList[i].TempPath = tempFolder + file.Name;
                                }
                                else attachmentCRUDList[i].TempPath = string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            });
            return attachmentCRUDList;
        }
        
    }
}
