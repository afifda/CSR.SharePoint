using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;


namespace CSR.Service.Entity
{
    [Table("[Tbl_Email]", true, false, "", "usp_SaveEmail", "usp_ReadEmail", "usp_UpdateEmail", "usp_DeleteEmail")]
    public class EmailEntyti
    {
        [Column(name: "Area", isDeleteParam: true, isUpdateParam: true, isAllowNull: false, isReadParam: true, isInsertParam: true, isPrimaryKey: true)]
        public string Area { get; set; }

        [Column(name: "To", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string To { get; set; }
        [Column(name: "Subject", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Subject { get; set; }
        [Column(name: "Message", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Message { get; set; }

        [Column(name: "Bidang", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Bidang { get; set; }

        [Column(name: "Kepada", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Kepada { get; set; }

        [Column(name: "Type", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string Type { get; set; }

        [Column(name: "URL", isUpdateParam: true, isAllowNull: false, isInsertParam: true)]
        public string URL { get; set; }
        public string Area_Nama { get; set; }
        public string BP_Nama { get; set; }
        public string NameType { get; set; }



    }
}
