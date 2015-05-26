using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_Lokasi]", true, false, "", "usp_SaveMasterLokasi", "usp_ReadMasterLokasi", "usp_UpdateMasterLokasi", "usp_DeleteMasterLokasi")]
    public class MasterLokasiEntity
    {
        [Column(name: "Lokasi_Kode", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string Lokasi_Kode { get; set; }

        [Column(name: "Lokasi_Nama", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Lokasi_Nama { get; set; }
    }
}
