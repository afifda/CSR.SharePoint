using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Tbl_FilePendukungProgram]", true, false, "", "usp_SaveFilePendukungProgram", "usp_ReadFilePendukungProgram", "usp_UpdateFilePendukungProgram", "usp_DeleteFilePendukungProgram")]
    public class FilePendukungProgramEntity
    {
        [Column(name: "TransaksiNo", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string No_Transaksi { get; set; }

        [Column(name: "NamaFile", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Nama_File { get; set; }
        [Column(name: "NamaPath", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Path_File { get; set; }
        [Column(name: "Flag", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Flag { get; set; }
        [Column(name: "NoUrut", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string No_Urut { get; set; }
    }
}
