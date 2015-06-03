using CSR.Service.DataAccess;
using CSR.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSR.Service.BusinessLogic
{
    public class ProgramLogic :  BaseLogic
    {
        public List<ProgramEntity> GetProgramList(int year)
        {
            return new ProgramDataAccess().GetProgramList(year);
        }
    }
}
