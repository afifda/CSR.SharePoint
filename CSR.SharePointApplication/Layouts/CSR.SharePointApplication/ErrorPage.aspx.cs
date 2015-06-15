using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class ErrorPage : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string QueryString = Request.QueryString["ErrCode"];
            if (string.IsNullOrEmpty(QueryString)) lblErrorMessage.Text = Constant.ERROR_DEFAULT;
            switch (QueryString)
            { 
                case "NotAuthorized":
                    lblErrorMessage.Text = Constant.ERROR_NOT_AUTHORIZED;
                    break;
                default:
                    lblErrorMessage.Text = Constant.ERROR_DEFAULT;
                    break;
            }
        }
    }
}
