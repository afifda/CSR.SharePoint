using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using CSR.Service.Entity;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CSR.Service.BusinessLogic;
using System.Linq;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class GenericHandler : IHttpHandler
    {
        private const string TEMP_FILE = "TempFile";
        private const string SITE_URL = "SiteCSR";
        private const string REAL_DOC_LIB = "RealDocLib";
        private const string DOC_LIB_PROGRAM = "DocLibProgram";
        string tempFolder = System.Configuration.ConfigurationManager.AppSettings[TEMP_FILE];
        string SiteURL = System.Configuration.ConfigurationManager.AppSettings[SITE_URL];
        string DocLibProgram = System.Configuration.ConfigurationManager.AppSettings[DOC_LIB_PROGRAM];
        string RealDocLib = System.Configuration.ConfigurationManager.AppSettings[REAL_DOC_LIB];
        private static MasterUserByUserNameEntity UserInformation
        {
            get
            {
                return new BaseLogic().UserInformation(SPContext.Current.Web.CurrentUser.LoginName);
            }
        }
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request.Params["Method"];
            switch (method.ToLower())
            {
                case "uploadfileattachment":
                    UploadFileAttachment(context);
                    break;
                case "downloadfileattachment":
                    string docPath = context.Request.Params["DocPath"];
                    string reqNo = context.Request.Params["ReqNo"];
                    DownloadFileAttachment(context, docPath, reqNo);
                    break;
            }
            throw new NotImplementedException();
        }

        private void UploadFileAttachment(HttpContext context)
        {
            List<AttachmentEntity> attacmentList = new List<AttachmentEntity>();
            AttachmentEntity attachment = new AttachmentEntity();
            if (context.Request.Files.Count > 0)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string fileTime = DateTime.Now.ToFileTime().ToString();
                    //string tempFolder = HttpContext.Current.Server.MapPath("/Temp_File");
                    string tempFolder = System.Configuration.ConfigurationManager.AppSettings[TEMP_FILE];
                    if (!Directory.Exists(tempFolder))
                    {
                        Directory.CreateDirectory(tempFolder);
                    }
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFile file = files[i];
                        string fnamefile = Path.GetFileName(file.FileName);
                        string fpathfile = Path.GetFullPath(file.FileName);
                        string fname = tempFolder + fnamefile;
                        string ftemporary = fname;
                        file.SaveAs(fname);
                        attachment.NamaPath = fpathfile;
                        attachment.NamaFile = fnamefile;
                        attachment.TempPath = fname;
                        attacmentList.Add(attachment);

                    }
                });

            }
            context.Response.Write(new JavaScriptSerializer().Serialize(attacmentList));
        }

        private void DownloadFileAttachment(HttpContext context, string docPath, string reqNo)
        {
            string message = string.Empty;
  

            //UserInformationLogic userInformationLogic = new UserInformationLogic();
            
            try
            {
                //UserInformationCRUD userInfoCRUD = userInformationLogic.GetUserInformation();
                //List<string> nrkList = new List<string>();

                //TransportRequestParameterCriteria request = new TransportRequestParameterCriteria() { ReqTransport_No = reqNo };
                //nrkList.AddRange(new ApprovalPathInformationLogic().GetApproverPathInfo(request).Select(n => n.NRK));
                //nrkList.Add(new UserInformationLogic().GetRequestorNameByRequestNo(reqNo).NRK);
 
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    //WorkflowCRUD workflowCRUD = new WorkflowLogic().GetWorkflow((int)Workflow.P3);
                    string fullPath = SiteURL + "/" + docPath;
                    int lastIndex = docPath.LastIndexOf("/");
                    string fileName = docPath.Substring(lastIndex + 1, (docPath.Length - lastIndex - 1));

                    using (SPSite currentSite = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = currentSite.OpenWeb(SPContext.Current.Web.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            string strContentType = string.Empty;
                            SPList docLib = web.Lists.TryGetList(docPath.Split('/')[0]);
                            if (docLib.DoesUserHavePermissions(SPContext.Current.Web.CurrentUser, SPBasePermissions.FullMask))
                            {
                                SPFolder SubFolder = (from SPFolder folder in docLib.RootFolder.SubFolders
                                                      where folder.Url == docLib.RootFolder.Url + "/" + reqNo
                                                      select folder).FirstOrDefault();

                                SPFile tempFile = SubFolder.Files[fileName];
                                string[] file = fileName.Split('.');
                                int a = file.Length;

                                byte[] obj = (byte[])tempFile.OpenBinary();

                                // Get the extension of File to determine the file type
                                string casestring = "";
                                casestring = file[a - 1].ToString();
                                switch (casestring)
                                {
                                    case "txt":
                                        strContentType = "text/plain";
                                        break;
                                    case "htm":
                                        strContentType = "text/html";
                                        break;
                                    case "html":
                                        strContentType = "text/html";
                                        break;
                                    case "rtf":
                                        strContentType = "text/richtext";
                                        break;
                                    case "jpg":
                                        strContentType = "image/jpeg";
                                        break;
                                    case "jpeg":
                                        strContentType = "image/jpeg";
                                        break;
                                    case "gif":
                                        strContentType = "image/gif";
                                        break;
                                    case "bmp":
                                        strContentType = "image/bmp";
                                        break;
                                    case "mpg":
                                        strContentType = "video/mpeg";
                                        break;
                                    case "mpeg":
                                        strContentType = "video/mpeg";
                                        break;
                                    case "avi":
                                        strContentType = "video/avi";
                                        break;
                                    case "pdf":
                                        strContentType = "application/pdf";
                                        break;
                                    case "doc":
                                        strContentType = "application/msword";
                                        break;
                                    case "docx":
                                        strContentType = "application/msword";
                                        break;
                                    case "dot":
                                        strContentType = "application/msword";
                                        break;
                                    case "csv":
                                        strContentType = "application/vnd.msexcel";
                                        break;
                                    case "xls":
                                        strContentType = "application/vnd.msexcel";
                                        break;
                                    case "xlsx":
                                        strContentType = "application/vnd.msexcel";
                                        break;
                                    case "xlt":
                                        strContentType = "application/vnd.msexcel";
                                        break;
                                    default:
                                        strContentType = "application/octet-stream";
                                        break;
                                }
                                context.Response.ClearContent();
                                context.Response.ClearHeaders();
                                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName.ToString());
                                context.Response.ContentType = strContentType;
                                //Check that the client is connected and has not closed the connection after the request
                                if (context.Response.IsClientConnected)
                                {
                                    context.Response.BinaryWrite(obj);
                                }
                            }
                            else context.Response.Write("You are not authorized to open this file");
                        }
                    }
                });
            }
            catch 
            {
                context.Response.Write("Download failed");
            }
            finally
            {
                context.Response.Flush();
                context.Response.Close();
            }
        }
    }
}
