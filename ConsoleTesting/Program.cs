using CSR.Service.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlHelper sql = new SqlHelper();
            Console.WriteLine(sql.CreateTable<MasterAreaEntity>(new MasterAreaEntity()));
        }
    }
}
