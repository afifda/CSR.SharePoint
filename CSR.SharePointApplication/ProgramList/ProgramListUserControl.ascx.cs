using CSR.Service.BusinessLogic;
using CSR.Service.Entity;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CSR.SharePointApplication.ProgramList
{
    public partial class ProgramListUserControl : UserControl
    {
        private static MasterUserByUserNameEntity UserInformation
        {
            get
            {
                return new BaseLogic().UserInformation(SPContext.Current.Web.CurrentUser.LoginName);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserInformation == null)
            {
                string web = SPContext.Current.Web.Url;
                Response.Redirect(web + "/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            this.hfIsAdmin.Value = "0";

            if (UserInformation.AreaName == "Jakarta")
            {
                this.hfIsAdmin.Value = "1";
            }
        }
    }
}
