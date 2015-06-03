using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSR.SharePointApplication
{
    static class Constant
    {
        #region Control Template
        public const string CONTROL_TEMPLATES_PATH = @"~/_CONTROLTEMPLATES/15/HumasCSR/";
        public const string MASTER_AREA_CONTROL = "MasterAreaControl.ascx";
        public const string MASTER_BIDANG_PROGRAM_CONTROL = "MasterBidangControl.ascx";
        public const string MASTER_KATEGORI_PROGRAM_CONTROL = "MasterKategoriControl.ascx";
        public const string MASTER_USER_CONTROL = "MasterUserControl.ascx";
        public const string MASTER_EMAIL_CONTROL = "MasterEmail.ascx";
        public const string MASTER_REALISASI_PROGRAM_CONTROL = "RealisasiProgramControl.ascx";

        public const string MASTER_AREA_TYPE = "MasterArea";
        public const string MASTER_BIDANG_PROGRAM_TYPE = "MasterBidangProgram";
        public const string MASTER_KATEGORI_PROGRAM_TYPE = "MasterKategoriProgram";
        public const string MASTER_USER_TYPE = "MasterUser";
        public const string MASTER_EMAIL_TYPE = "MasterEmail";

        public const string MASTER_AREA_TITLE = "Master Data Area";
        public const string MASTER_BIDANG_PROGRAM_TITLE = "Master Data Bidang Program";
        public const string MASTER_KATEGORI_PROGRAM_TITLE = "Master Data Kategori Program";
        public const string MASTER_USER_TITLE = "Master Data User";
        public const string MASTER_EMAIl_TITLE = "Master Data EMail";
        public const string MASTER_REALISASI_TITLE = "Master Data Realisasi Program";
        #endregion

        #region Message
        public static string ERROR_NOT_AUTHORIZED = string.Format("Anda tidak mempunyai hak akses pada laman ini.{0}Hubungi Administrator untuk mendapatkan hak akses.", Environment.NewLine);
        public static string ERROR_DEFAULT = string.Format("Halaman ini mengalami error.{0}Hubungi Administrator.", Environment.NewLine);
        #endregion

        #region Value
        public const string DocLibProgram = "BidangProgramDocLib";
        public const string DocLibRealisasi = "RealisasiDocLib";
        #endregion


    }
}
