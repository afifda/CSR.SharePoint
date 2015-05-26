using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Tbl_FilePendukungRealisasi]", true, false, "", "usp_SaveFilePendukungRealisasi", "usp_ReadFilePendukungRealisasi", "usp_UpdateFilePendukungRealisasi", "usp_DeleteFilePendukungRealisasi")]
    public class FilePendukungRealisasiEntity
    {
        [Column(name: "TransaksiNo", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string No_Transaksi_Realisasi { get; set; }

        [Column(name: "NamaFile", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Nama_File { get; set; }
        [Column(name: "NamaPath", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Path_File { get; set; }
        [Column(name: "NoUrut", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string No_Urut { get; set; }
    }
}
