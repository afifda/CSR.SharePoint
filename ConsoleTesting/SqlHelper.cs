using MigrationTools.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace ConsoleTesting
{
    class SqlHelper : DoubleASqlTextCommand
    {
        public string CreateTable<T>(T entity) where T : new()
        {
            string createCommand = WriteColumnMappings<T>();
            SqlCommand command = base.Read<T>(entity);
            return createCommand;
        }
    }
}
