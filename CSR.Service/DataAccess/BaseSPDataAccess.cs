using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.DataAccess
{
    public class BaseSPDataAccess : DoubleASqlSPCommand
    {
        public DoubleASqlConnection Connection { get; set; }
        bool IsTransOperation;

        public void SetTransactionOperation(bool isTransaction = true)
        {
            this.IsTransOperation = isTransaction;
        }
        public BaseSPDataAccess(bool isTransaction = false)
        {
            Connection = new DoubleASqlConnection();
            //Connection.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringOptions.CSRConnection.ToString()].ConnectionString;
            Connection.ConnectionString = @"Data Source=sqlpge02;Initial Catalog=PGE_CSR;Persist Security Info=True;User ID=sa;Password=sqlserver2012PGE";
        }
        public BaseSPDataAccess(ConnectionStringOptions connectionString, bool isTransaction = false)
        {
            Connection = new DoubleASqlConnection();
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString.ToString()].ConnectionString;
        }

        public virtual int Save<T>(T entity)
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

        public string SaveWithOutput<T>(T entity, string outputParam)
        {
            string output = string.Empty;
            try
            {
                SqlCommand command = base.Save<T>(entity);
                command.Parameters[outputParam].Direction = ParameterDirection.Output;
                command.Parameters[outputParam].Size = -1;
                OpenConnection(command, this.Connection);
                command.ExecuteNonQuery();
                output = Convert.ToString(command.Parameters[outputParam].Value.ToString());
                CloseCommitConnection();
            }
            catch (Exception ex)
            {
                CloseRollbackConnection();
                throw ex;
            }
            return output;
        }

        public virtual int Update<T>(T entity)
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

        public virtual int Delete<T>(T entity)
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

        public virtual int Delete<T>(string key)
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

        public virtual List<T> Read<T>(T entity) where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            List<T> entityList = null;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(entity);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                entityList = mapData.MapDataToEntityList<T>(reader);
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

        public virtual List<T> Read<T>(string key) where T : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            List<T> entityList = null;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T>(key);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                entityList = mapData.MapDataToEntityList<T>(reader);
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

        public virtual T1 ReadWithDetails<T1, T2>(T1 entity, string detailName) where T1 : new() where T2 : new()
        {
            DoubleASqlMapper mapData = new DoubleASqlMapper();
            T1 outEntity;
            IDataReader reader = null;
            try
            {
                SqlCommand command = base.Read<T1>(entity);
                OpenConnection(command, this.Connection);
                reader = command.ExecuteReader();
                outEntity = mapData.MapDataToListWithDetails<T1, T2>(reader, detailName);
            }
            catch (Exception ex)
            {
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

        protected void AddInParameter(SqlCommand command, string name, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;

            command.Parameters.Add(parameter);
        }

        public void ReorderTable(ref DataTable table, params String[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                table.Columns[columns[i]].SetOrdinal(i);
            }
        }
    }
}
