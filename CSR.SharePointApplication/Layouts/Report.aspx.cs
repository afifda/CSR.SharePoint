using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;
using System.Configuration;

namespace CSR.SharePointApplication.Layouts
{
    public class ReportServerConnection : IReportServerConnection2
    {
        public ReportServerConnection()
        {
        }
        public IEnumerable<Cookie> Cookies
        {
            get { return null; }
        }

        public IEnumerable<string> Headers
        {
            get { return null; }
        }

        public Uri ReportServerUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["ReportServerUrl"];
                if (string.IsNullOrEmpty(url))
                    throw new Exception("Missing url from the Web.config file");
                return new Uri(url);
            }
        }

        public int Timeout
        {
            get { return 60000; }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(
                string lpszUsername,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                out IntPtr phToken);

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {               
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                string userName = ConfigurationManager.AppSettings["ReportViewerUser"];
                if (string.IsNullOrEmpty(userName))
                    throw new Exception("Missing user name from Web.config file");
                string password = ConfigurationManager.AppSettings["ReportViewerPassword"];
                if (string.IsNullOrEmpty(password))
                    throw new Exception("Missing password from Web.config file");
                string domain = ConfigurationManager.AppSettings["ReportViewerDomain"];
                if (string.IsNullOrEmpty(domain))
                    throw new Exception("Missing domain from Web.config file");
                return new NetworkCredential(userName, password, domain);
                //return null;
            }
        }
    }
    public partial class Report : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && Request.QueryString.Count > 0)
            {
                string reportType = Request.Params["ReportType"].Trim();
                if (string.IsNullOrEmpty(reportType)) Response.Redirect("/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx", true);
                try
                {
                    List<ReportParameter> reportParam = new List<ReportParameter>();
                    string rdlFile = string.Empty;
                    switch (reportType)
                    { 
                        case Constant.REPORT_PIE_CHART_TYPE:
                            break;
                        case Constant.REPORT_BAR_CHART_TYPE:
                            break;
                        case Constant.REPORT_DETAIL_REPORT_TYPE:
                            string tglMulai = Request.Params["WaktuFrom"];
                            string tglSelesai = Request.Params["WaktuTo"];
                            string Area = Request.Params["Area"];
                            string Kategori = Request.Params["Kategori"];
                            string Bidang = Request.Params["Bidang"];
                            ReportParameter KP_Kode = new ReportParameter("KP_Kode", Kategori);
                            ReportParameter Area_Kode = new ReportParameter("Area_Kode", Area);
                            ReportParameter BP_Kode = new ReportParameter("BP_Kode", Bidang);
                            ReportParameter TglMulai = new ReportParameter("TglMulai", tglMulai);
                            ReportParameter TglSelesai = new ReportParameter("TglSelesai", tglSelesai);
                            reportParam.Add(KP_Kode);
                            reportParam.Add(Area_Kode);
                            reportParam.Add(BP_Kode);
                            reportParam.Add(TglMulai);
                            reportParam.Add(TglSelesai);
                            break;
                    }
                    this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    this.ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerConnection();
                    this.ReportViewer1.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportViewerSubfolder"] + rdlFile;
                    this.ReportViewer1.ServerReport.ReportServerUrl = new Uri((ConfigurationManager.AppSettings["ReportServerUrl"]));
                    
                    this.ReportViewer1.ServerReport.SetParameters(reportParam);

                    this.ReportViewer1.SizeToReportContent = true;
                    this.ReportViewer1.AsyncRendering = false;
                    this.ReportViewer1.ShowToolBar = false;
                    this.ReportViewer1.ShowParameterPrompts = false;
                    this.ReportViewer1.ServerReport.Refresh();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
