using CSR.Service.BusinessLogic;
using CSR.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterKategoriProgramEntity kategori = new MasterKategoriProgramEntity()
            {
                KP_Kode = 0,
                KP_Nama = "CSR"
            };
            MasterDataLogic masterLogic = new MasterDataLogic(true);
            masterLogic.SPSave<MasterKategoriProgramEntity>(kategori);
            Console.ReadKey();
            List<MasterKategoriProgramEntity> kategoriRead = masterLogic.SPRead<MasterKategoriProgramEntity>("1");
            foreach (MasterKategoriProgramEntity k in kategoriRead)
            {
                Console.WriteLine(k.KP_Kode + " - " + k.KP_Nama);
            }
            Console.ReadKey();
            kategori.KP_Nama = "Kehumasan";
            masterLogic.SPUpdate<MasterKategoriProgramEntity>(kategori);
            Console.ReadKey();
            masterLogic.SPDelete<MasterKategoriProgramEntity>("1");
            Console.ReadKey();
        }

        private static void LogMessageToFile(string msg, string logPath)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(logPath, System.IO.FileMode.Append))
            {
                string logLine = System.String.Format("{0} {1}", msg, Environment.NewLine);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.WriteLine(logLine);

                sw.Flush();
                sw.Close();
            }

        }

        private static void CreateSPText()
        {
            //string logPath = "C:\\Users\\administrator.CONTOSO\\Documents\\TestFrameWork\\CreateSP_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".txt";
            //SqlHelper sql = new SqlHelper();
            //string DBObject = sql.CreateTable<FilePendukungRealisasiEntity>().CommandText;
            //LogMessageToFile(DBObject, logPath);
            //Console.WriteLine(sql.CreateTable<FilePendukungRealisasiEntity>().CommandText);
            //Console.ReadKey();
            //DBObject = sql.CreateSPSave<FilePendukungRealisasiEntity>().CommandText;
            //LogMessageToFile(DBObject, logPath);
            //Console.WriteLine(sql.CreateSPSave<FilePendukungRealisasiEntity>().CommandText);
            //Console.ReadKey();
            //DBObject = sql.CreateSPUpdate<FilePendukungRealisasiEntity>().CommandText;
            //LogMessageToFile(DBObject, logPath);
            //Console.WriteLine(sql.CreateSPUpdate<FilePendukungRealisasiEntity>().CommandText);
            //Console.ReadKey();
            //DBObject = sql.CreateSPDelete<FilePendukungRealisasiEntity>().CommandText;
            //LogMessageToFile(DBObject, logPath);
            //Console.WriteLine(sql.CreateSPDelete<FilePendukungRealisasiEntity>().CommandText);
            //Console.ReadKey();
            //DBObject = sql.CreateSPRead<FilePendukungRealisasiEntity>().CommandText;
            //LogMessageToFile(DBObject, logPath);
            //Console.WriteLine(sql.CreateSPRead<FilePendukungRealisasiEntity>().CommandText);
            //Console.ReadKey();
        }
    }
}
