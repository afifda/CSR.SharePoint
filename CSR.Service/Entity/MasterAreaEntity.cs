using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

namespace CSR.Service.Entity
{
    [Table("[Mgr_Stg_Attachment]", true, false)]
    public class MasterAreaEntity
    {
        [Column(name: "Request_No", isPrimaryKey: true)]
        public string Request_No { get; set; }

        [Column(name: "FILE")]
        public string Attachment_File { get; set; }
        [Column(name: "Document_Link", isToBeRead: false)]
        public string Document_Link { get; set; }
        [Column(name: "Temporary_Path", isToBeRead: false)]
        public string Temporary_Path { get; set; }
    }
}
