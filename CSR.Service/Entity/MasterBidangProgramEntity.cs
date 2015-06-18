using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_BidangProgram]", true, false, "", "usp_SaveMasterBidangProgram", "usp_ReadMasterBidangProgram", "usp_UpdateMasterBidangProgram", "usp_DeleteMasterBidangProgram")]
    public class MasterBidangProgramEntity
    {
        [Column(name: "KP_Kode", isDeleteParam: false, isUpdateParam: true, isAllowNull: false, isReadParam: false, isInsertParam: true)]
        public int KP_Kode { get; set; }

        [Column(name: "KP_Nama", isUpdateParam: false, isAllowNull: true, isInsertParam: false)]
        public string KP_Nama { get; set; }

        [Column(name: "BP_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: false, isPrimaryKey: true)]
        public int BP_Kode { get; set; }

        [Column(name: "BP_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string BP_Nama { get; set; }
    }
}
