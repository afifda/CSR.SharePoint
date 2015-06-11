﻿using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using CSR.Service.Entity;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using CSR.Service.BusinessLogic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Services;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    [Guid("8FE07083-259F-44AC-BC83-154A76AB5F88")]
    public partial class GenericHandler : IHttpHandler
    {        
        string tempFolder = System.Configuration.ConfigurationManager.AppSettings[Constant.TEMP_FILE];
        string SiteURL = System.Configuration.ConfigurationManager.AppSettings[Constant.SITE_URL];
        string DocLibProgram = System.Configuration.ConfigurationManager.AppSettings[Constant.DOC_LIB_PROGRAM];
        string RealDocLib = System.Configuration.ConfigurationManager.AppSettings[Constant.REAL_DOC_LIB];
        private static MasterUserByUserNameEntity UserInformation
        {
            get
            {
                return new BaseLogic().UserInformation(SPContext.Current.Web.CurrentUser.LoginName);
            }
        }
        public bool IsReusable
        {
            get { return false; }
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
                case "getavailableyear":
                    GetAvailableYear(context);
                    break;
                case "loadprogramlist":
                    int year1;
                    if (!int.TryParse(context.Request.Params["Year"], out year1)) break;
                    LoadProgramList(context, year1);
                    break;
                case "confirmprogramlist":
                    int year2;
                    if (!int.TryParse(context.Request.Params["Year"], out year2)) break;
                    UpdateLockedProgram(context, year2);
                    break;
            }
        }

        private void UpdateLockedProgram(HttpContext context, int year)
        {
            List<ProgramEntity> program = new BaseLogic().SPRead<ProgramEntity>(new ProgramEntity() { TransaksiNo = "" });
            List<string> transNo = (from p in program
                                    where p.Area_Kode == UserInformation.AreaCode
                                    select p.TransaksiNo).ToList();
            try
            {
                int row = new BaseLogic().UpdateLockedStatus(transNo, "P", true);
                //MasterUserByUserNameEntity UserInformationNew = new MasterUserByUserNameEntity();
                //UserInformationNew = UserInformation;
                //BaseLogic baselogic = new BaseLogic();
                //baselogic.sendEmailRencanaProgram(UserInformationNew.AreaCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadProgramList(HttpContext context, int year)
        {
            List<ProgramEntity> programList = new ProgramLogic().GetProgramList(year);

            MasterUserByUserNameEntity UserInformationNew = new MasterUserByUserNameEntity();
            UserInformationNew = UserInformation;
            if (UserInformationNew.AreaName != "Jakarta")
            {
                programList = (from p in programList
                              where p.Area_Kode == UserInformation.AreaCode
                              select p).ToList();
            }
            context.Response.Write(new JavaScriptSerializer().Serialize(programList));
        }

        private void GetAvailableYear(HttpContext context)
        {
            List<int> Available_Year = new MasterDataLogic().GetAvailableYear();
            context.Response.Write(new JavaScriptSerializer().Serialize(Available_Year));
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
                    string tempFolder = System.Configuration.ConfigurationManager.AppSettings[Constant.TEMP_FILE];
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
            try
            {
                var curSite = SPContext.Current.Site;
                var curWeb = SPContext.Current.Web;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {                    
                    string fullPath = SiteURL + "/" + docPath;
                    int lastIndex = docPath.LastIndexOf("/");
                    string fileName = docPath.Substring(lastIndex + 1, (docPath.Length - lastIndex - 1));
                 
                    //using (var site = new SPSite(SPContext.Current.Site.ID))
                    using (var site = new SPSite(curSite.ID))    
                    {
                        using (SPWeb web = site.OpenWeb(curWeb.ID))
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
