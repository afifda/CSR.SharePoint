using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Tbl_Program]", true, false, "", "usp_SaveProgram", "usp_ReadProgram", "usp_UpdateProgram", "usp_DeleteProgram")]
    public class ProgramEntity
    {
        [Column(name: "TransaksiNo", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true)]
        public string TransaksiNo { get; set; }

        [Column(name: "Judul_Program", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Judul_Program { get; set; }
        [Column(name: "KP_Kode", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public int KP_Kode { get; set; }
        public string KP_Nama { get; set; }
        [Column(name: "BP_Kode", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public int BP_Kode { get; set; }
        public string BP_Nama { get; set; }
        [Column(name: "Area_Kode", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Area_Kode { get; set; }
        public string Area_Nama { get; set; }
        [Column(name: "Jumlah_Anggaran", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public decimal Jumlah_Anggaran { get; set; }
        [Column(name: "Outcome_Diharapkan", isUpdateParam: true, isInsertParam: true)]
        public string Outcome_Diharapkan { get; set; }
        [Column(name: "Waktu_Mulai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime Waktu_Mulai { get; set; }
        [Column(name: "Waktu_Sampai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime Waktu_Sampai { get; set; }
        [Column(name: "Keterangan", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Keterangan { get; set; }
        [Column(name: "Created_Date", isUpdateParam: false, isAllowNull: false, isInsertParam: true)]
        public DateTime Created_Date { get; set; }
        [Column(name: "Last_Modified_Date", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime Last_Modified_Date { get; set; }
        [Column(name: "Created_By", isUpdateParam: false, isAllowNull: false, isInsertParam: true)]
        public string Created_By { get; set; }
        [Column(name: "Last_Modified_By", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Last_Modified_By { get; set; }
        public bool Is_Locked { get; set; }
        public List<AttachmentEntity> AttachmentList { get; set; }
        
    }
}
