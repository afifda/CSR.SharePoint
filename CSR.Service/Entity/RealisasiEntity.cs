using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Tbl_Realisasi]", true, false, "", "usp_SaveRealisasi", "usp_ReadRealisasi", "usp_UpdateRealisasi", "usp_DeleteRealisasi")]
    public class RealisasiEntity
    {
        [Column(name: "RealisasiNo", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true)]
        public string RealisasiNo { get; set; }
        [Column(name: "TransaksiNo", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string TransaksiNo { get; set; }

        [Column(name: "WaktuMulai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime? WaktuMulai { get; set; }

        [Column(name: "WaktuSelesai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime? WaktuSelesai { get; set; }

        [Column(name: "Pelaksana", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Pelaksana { get; set; }

        [Column(name: "Penerima", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Penerima { get; set; }

        [Column(name: "SumberDanaPGEPusat", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPGEPusat { get; set; }

        [Column(name: "SumberDanaPGEArea", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPGEArea { get; set; }

        [Column(name: "SumberDanaPersero", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPersero { get; set; }

        [Column(name: "SumberPKBL", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberPKBL { get; set; }     
        
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
        public bool Is_Locked_Realisasi { get; set; }
        public bool Is_Locked { get; set; }
        public int KP_Kode { get; set; }
        public int BP_Kode { get; set; }
        public string Judul_Program { get; set; }
        public string Area_Kode { get; set; }
        
        public List<AttachmentEntity> AttachmentList { get; set; }
    }

    [Table("[Tbl_Realisasi]", true, false, "", "", "usp_ReadRealisasiByTransactionNo", "", "")]
    public class RealisasiByTransaksiNoEntity
    {
        [Column(name: "RealisasiNo", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string RealisasiNo { get; set; }
        [Column(name: "TransaksiNo", isUpdateParam: true, isAllowNull: false, isInsertParam: true, isReadParam: true)]
        public string TransaksiNo { get; set; }

        [Column(name: "WaktuMulai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime? WaktuMulai { get; set; }

        [Column(name: "WaktuSelesai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public DateTime? WaktuSelesai { get; set; }

        [Column(name: "Pelaksana", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Pelaksana { get; set; }

        [Column(name: "Penerima", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Penerima { get; set; }

        [Column(name: "SumberDanaPGEPusat", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPGEPusat { get; set; }

        [Column(name: "SumberDanaPGEArea", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPGEArea { get; set; }

        [Column(name: "SumberDanaPersero", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberDanaPersero { get; set; }

        [Column(name: "SumberPKBL", isUpdateParam: true, isAllowNull: true, isInsertParam: true)]
        public decimal SumberPKBL { get; set; }

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
        public bool Is_Locked_Realisasi { get; set; }
        public bool Is_Locked { get; set; }
        public int KP_Kode { get; set; }
        public int BP_Kode { get; set; }
        public string Judul_Program { get; set; }
        public string Area_Kode { get; set; }
        public string BP_Nama { get; set; }

        public List<AttachmentEntity> AttachmentList { get; set; }
    }
}
