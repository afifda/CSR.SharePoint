using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CSR.Service.BusinessLogic;
using System.Web.Script.Serialization;
using CSR.Service.Entity;
using System.Web;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class InputProgramPage : BaseLayoutPages
    {
        public const string DocLibProgram = "BidangProgramDocLib";
        private static bool IsEdit = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidUser)
            {
                Response.Redirect("/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            string strTransNo = Request.QueryString["TransaksiNo"];
            if (!string.IsNullOrEmpty(strTransNo))
            {
                this.hfTransaksiNo.Value = strTransNo;
            }
            if (!string.IsNullOrEmpty(this.hfTransaksiNo.Value))
            {
                IsEdit = true;
            }
        }

        [System.Web.Services.WebMethod]
        public static string LoadInputPage()
        {
            object InputPage = null;
            try
            {
                MasterDataLogic logic = new MasterDataLogic();
                InputPage = logic.GetInputProgramPage(User);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JavaScriptSerializer().Serialize(InputPage);
        }

        [System.Web.Services.WebMethod]
        public static string LoadProgram(string transaksiNo)
        {
            ProgramWithRealisasinEntity program = null;
            try
            {
                if (string.IsNullOrEmpty(transaksiNo)) return string.Empty;
                program = new ProgramWithRealisasinEntity() { TransaksiNo = transaksiNo };
                ProgramLogic logic = new ProgramLogic();
                program = logic.SPReadWithDetails<ProgramWithRealisasinEntity, RealisasiEntity>(program, "RealisasiList");
                program.AttachmentList = logic.GetAttachments(new AttachmentEntity() { TransaksiNo = transaksiNo });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JavaScriptSerializer().Serialize(program);
        }

        [System.Web.Services.WebMethod]
        public static string SaveProgram(string programString)
        {            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ProgramEntity programEntity = (ProgramEntity)serializer.Deserialize(programString, typeof(ProgramEntity));
            string SiteURL = SPContext.Current.Web.Url;
            string DocLib = DocLibProgram;
            int status = 0;
            try
            {
                ProgramLogic logic = new ProgramLogic();
                if (IsEdit)
                {
                    programEntity.Last_Modified_Date = DateTime.Now;
                    programEntity.Last_Modified_By = User.UserName;
                    logic.SPUpdate<ProgramEntity>(programEntity);
                    if (programEntity.AttachmentList.Count != 0)
                    {
                        programEntity.AttachmentList.ForEach(a => a.TransaksiNo = programEntity.TransaksiNo);
                        status = logic.SaveAttachment(programEntity.AttachmentList);
                        status = logic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, programEntity.AttachmentList);
                    }
                }
                else
                {
                    programEntity.Created_Date = DateTime.Now;
                    programEntity.Created_By = User.UserName;
                    programEntity.Last_Modified_Date = DateTime.Now;
                    programEntity.Last_Modified_By = User.UserName;
                    programEntity.TransaksiNo = logic.SPSaveWithOutput<ProgramEntity>(programEntity, "TransaksiNo");
                    if (programEntity.AttachmentList.Count != 0)
                    {
                        programEntity.AttachmentList.ForEach(a => a.TransaksiNo = programEntity.TransaksiNo);
                        status = logic.SaveAttachment(programEntity.AttachmentList);
                        status = logic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, programEntity.AttachmentList);
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Format("Telah terjadi error. ({0})", ex.Message);
            }
            return "Success. Program Dan Lampiran File telah disimpan.";            
        }
    }
}
