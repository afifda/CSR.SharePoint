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
                Response.Redirect(web + "/_layouts/15/CSR.SharePointApplication/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            this.hfIsAdmin.Value = "0";

            if (UserInformation.AreaName == "Jakarta")
            {
                this.hfIsAdmin.Value = "1";
            }

            this.hfSelectedYear.Value = DateTime.Now.Year.ToString();
            string strYear = Request.QueryString["Year"];
            if (string.IsNullOrEmpty(strYear))
            {
                int _year;
                if (int.TryParse(strYear, out _year)) this.hfSelectedYear.Value = _year.ToString(); 
            }

            this.hfSelectedYear.Value = UserInformation.AreaCode;
            string strArea = Request.QueryString["Area"];
            if (string.IsNullOrEmpty(strYear))
            {
                this.hfSelectedArea.Value = strArea;
            }
        }
    }
}
