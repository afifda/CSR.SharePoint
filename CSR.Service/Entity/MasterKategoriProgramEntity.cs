using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_KategoriProgram]", true, false, "", "usp_SaveMasterKategoriProgram", "usp_ReadMasterKategoriProgram", "usp_UpdateMasterKategoriProgram", "usp_DeleteMasterKategoriProgram")]
    public class MasterKategoriProgramEntity
    {
        [Column(name: "KP_Kode", spParamName: "KP_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: false, isPrimaryKey: true)]
        public int KP_Kode { get; set; }

        [Column(name: "KP_Nama", spParamName: "KP_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string KP_Nama { get; set; }
    }
}
