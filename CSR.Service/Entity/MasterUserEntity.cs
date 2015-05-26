using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mst_User]", true, false, "", "usp_SaveMasterUser", "usp_ReadMasterUser", "usp_UpdateMasterUser", "usp_DeleteMasterUser")]
    public class MasterUserEntity
    {
        [Column(name: "No_Pegawai", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string No_Pegawai { get; set; }

        [Column(name: "Nama_Pegawai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Nama_Pegawai { get; set; }
        [Column(name: "UserName", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string UserName { get; set; }
        [Column(name: "Kode_Area", isUpdateParam: true, isAllowNull: false, isInsertParam: true, isForeignKey: true, refTable: "Mst_Area", refColumn: "Area_Kode", refDisplayColumn: "Area_Nama", columnDisplayLink: "AreaName")]
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
    }

    [Table("[Mst_User]", true, false, "", "usp_SaveMasterUserByUserName", "usp_ReadMasterUserByUserName", "usp_UpdateMasterUserByUserName", "usp_DeleteMasterUserByUserName")]
    public class MasterUserByUserNameEntity
    {
        [Column(name: "No_Pegawai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string No_Pegawai { get; set; }

        [Column(name: "Nama_Pegawai", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Nama_Pegawai { get; set; }
        [Column(name: "UserName", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string UserName { get; set; }
        [Column(name: "Kode_Area", isUpdateParam: true, isAllowNull: false, isInsertParam: true, isForeignKey: true, refTable: "Mst_Area", refColumn: "Area_Kode", refDisplayColumn: "Area_Nama", columnDisplayLink: "AreaName")]
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
    }
}
