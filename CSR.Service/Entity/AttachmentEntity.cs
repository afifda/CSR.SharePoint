using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSR.Service.Entity
{
    public class AttachmentEntity
    {
        public string TransaksiNo { get; set; }
        public string NamaFile { get; set; }
        public string NamaPath { get; set; }
        public string Flag { get; set; }
        public int? NoUrut { get; set; }
        public string TempPath { get; set; }
        
    }
}
