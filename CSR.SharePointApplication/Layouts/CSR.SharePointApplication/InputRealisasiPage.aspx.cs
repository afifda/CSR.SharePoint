using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CSR.Service.BusinessLogic;
using System.Web.Script.Serialization;
using CSR.Service.Entity;
using System.Web;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class InputRealisasiPage : BaseLayoutPages
    {
        public const string DocLibProgram = "RealisasiDocLib";
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
            RealisasiEntity realisasiEntity = (RealisasiEntity)serializer.Deserialize(programString, typeof(RealisasiEntity));
            string SiteURL = SPContext.Current.Web.Url;
            string DocLib = System.Configuration.ConfigurationManager.AppSettings[DocLibProgram];
            int status = 0;
            BaseLogic BaseLogic = new BaseLogic();
            try
            {
                ProgramLogic logic = new ProgramLogic();
                if (IsEdit)
                {
                    realisasiEntity.Last_Modified_Date = DateTime.Now;
                    realisasiEntity.Last_Modified_By = User[0].UserName;
                    logic.SPUpdate<RealisasiEntity>(realisasiEntity);

                }
                else
                {
                    realisasiEntity.Created_Date = DateTime.Now;
                    realisasiEntity.Created_By = User[0].UserName;
                    realisasiEntity.Last_Modified_Date = DateTime.Now;
                    realisasiEntity.Last_Modified_By = User[0].UserName;
                    logic.SPSave<RealisasiEntity>(realisasiEntity);
                    if (realisasiEntity.AttachmentList.Count != 0)
                    {
                        
                        status = BaseLogic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, realisasiEntity.AttachmentList);
                    }
                    //sendEMail
                    BaseLogic.sendEMailThroughGmail(realisasiEntity.TransaksiNo);
                }
            }
            catch (Exception ex)
            {
                return string.Format("Telah terjadi error. ({0})", ex.Message);
            }
            return "Success. Realisasi Dan Lampiran File telah disimpan.";
        }
    }
}
