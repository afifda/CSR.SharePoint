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
                InputPage = logic.GetInputProgramPage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JavaScriptSerializer().Serialize(InputPage);
        }

        [System.Web.Services.WebMethod]
        public static string SaveProgram(string programString)
        {            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ProgramEntity programEntity = (ProgramEntity)serializer.Deserialize(programString, typeof(ProgramEntity));
            string SiteURL = SPContext.Current.Web.Url;
            string DocLib = System.Configuration.ConfigurationManager.AppSettings[DocLibProgram];
            int status = 0;
            try
            {
                ProgramLogic logic = new ProgramLogic();
                if (IsEdit)
                {
                    programEntity.Last_Modified_Date = DateTime.Now;
                    programEntity.Last_Modified_By = User[0].UserName;
                    logic.SPUpdate<ProgramEntity>(programEntity);
                    
                }
                else
                {
                    programEntity.Created_Date = DateTime.Now;
                    programEntity.Created_By = User[0].UserName;
                    programEntity.Last_Modified_Date = DateTime.Now;
                    programEntity.Last_Modified_By = User[0].UserName;
                    logic.SPSave<ProgramEntity>(programEntity);
                    if (programEntity.AttachmentList.Count != 0)
                    {
                        BaseLogic BaseLogic = new BaseLogic();
                        status = BaseLogic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, programEntity.AttachmentList);
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
