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
        [Column(name: "BP_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: false, isPrimaryKey: true)]
        public string BP_Kode { get; set; }

        [Column(name: "BP_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string BP_Nama { get; set; }
    }
}
