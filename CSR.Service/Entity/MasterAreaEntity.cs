using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_Area]", true, false, "", "usp_SaveMasterArea", "usp_ReadMasterArea", "usp_UpdateMasterArea", "usp_DeleteMasterArea")]
    public class MasterAreaEntity
    {
        [Column(name: "Area_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string AreaCode { get; set; }

        [Column(name: "Area_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string AreaName { get; set; }
    }
}
