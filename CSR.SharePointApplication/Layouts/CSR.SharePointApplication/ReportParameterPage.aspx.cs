using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class ReportParameterPage : BaseLayoutPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidUser)
            {
                Response.Redirect("/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            string stReportType = Request.QueryString["ReportType"];
            if (string.IsNullOrEmpty(stReportType))
            {
                ShowStatusBar(this, "Information", "Query string ReportType is not found");
                return;
            }

            string controlTemplate = ReportInputControl(stReportType);
            if (string.IsNullOrEmpty(controlTemplate))
            {
                ShowStatusBar(this, "Information", string.Format("Report {0} is not found", stReportType));
                return;
            }

            SetPageTitle(this.Page, MasterInputTitle(stReportType));

            Control control = Page.LoadControl(Constant.CONTROL_TEMPLATES_PATH + controlTemplate);
            ControlContainer.Controls.Add(control);
        }
    }
}
