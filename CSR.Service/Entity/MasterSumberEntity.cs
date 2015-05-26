using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_Sumber]", true, false, "", "usp_SaveMasterSumber", "usp_ReadMasterSumber", "usp_UpdateMasterSumber", "usp_DeleteMasterSumber")]
    public class MasterSumberEntity
    {
        [Column(name: "MS_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string Sumber_Kode { get; set; }

        [Column(name: "MS_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Sumber_Nama { get; set; }
    }
}
