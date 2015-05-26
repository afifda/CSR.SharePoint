using CSR.Service.DataAccess;
using CSR.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSR.Service.BusinessLogic
{
    public class MasterDataLogic : BaseLogic
    {
        public MasterDataLogic() { }
        public MasterDataLogic(bool isStoredProcedureOperation)
        {
            if (isStoredProcedureOperation)
            {
                SetStoredProcedureOperation();
            }
            else
            {
                SetSqlNativeOperation();
            }
        }

        public object GetInputProgramPage()
        {
            MasterDataDataAccess dataAccess = new MasterDataDataAccess();
            List<MasterKategoriProgramEntity> kategori = SPRead<MasterKategoriProgramEntity>(new MasterKategoriProgramEntity() { KP_Kode = "" });
            List<MasterBidangProgramEntity> bidang = SPRead<MasterBidangProgramEntity>(new MasterBidangProgramEntity() { BP_Kode = "" });
            List<MasterAreaEntity> area = SPRead<MasterAreaEntity>(new MasterAreaEntity() { AreaCode = "" });
            var inputPage = new 
            {
                Kategori = kategori,
                Bidang = bidang,
                Area = area
            };
            return inputPage;
        }
    }
}
