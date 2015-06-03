using CSR.Service.BusinessLogic;
using CSR.Service.Entity;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;


namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public class BaseLayoutPages : LayoutsPageBase
    {   
        protected static List<MasterUserByUserNameEntity> User
        {
            get
            {
                return new MasterDataLogic().SPRead<MasterUserByUserNameEntity>(new MasterUserByUserNameEntity() { UserName = SPContext.Current.Web.CurrentUser.LoginName });
            }
        }

        protected static bool IsValidUser
        {
            get
            {
                return User.Count > 0;
            }
        }

        protected string MasterInputControl(string masterType)
        {
            switch (masterType)
            {
                case Constant.MASTER_AREA_TYPE:
                    return Constant.MASTER_AREA_CONTROL;
                case Constant.MASTER_BIDANG_PROGRAM_TYPE:
                    return Constant.MASTER_BIDANG_PROGRAM_CONTROL;
                case Constant.MASTER_KATEGORI_PROGRAM_TYPE:
                    return Constant.MASTER_KATEGORI_PROGRAM_CONTROL;
                case Constant.MASTER_USER_TYPE:
                    return Constant.MASTER_USER_CONTROL;
                case Constant.MASTER_EMAIL_TYPE:
                    return Constant.MASTER_EMAIL_CONTROL;
                default: return string.Empty;
            }
        }
        protected string MasterInputTitle(string masterType)
        {
            switch (masterType)
            {
                case Constant.MASTER_AREA_TYPE:
                    return Constant.MASTER_AREA_TITLE;
                case Constant.MASTER_BIDANG_PROGRAM_TYPE:
                    return Constant.MASTER_BIDANG_PROGRAM_TITLE;
                case Constant.MASTER_KATEGORI_PROGRAM_TYPE:
                    return Constant.MASTER_KATEGORI_PROGRAM_TITLE;
                case Constant.MASTER_USER_TYPE:
                    return Constant.MASTER_USER_TITLE;
                case Constant.MASTER_EMAIL_TYPE:
                    return Constant.MASTER_EMAIl_TITLE;
                default: return string.Empty;
            }
        }

        protected void ShowStatusBar(Page page, string title, string message)
        {
            message = message.Trim(';');
            message = message.Replace(";", "<br/>");
            SPPageStatusSetter pageStatusSetter = new SPPageStatusSetter();
            pageStatusSetter.AddStatus(title, message, SPPageStatusColor.Yellow);
            page.Controls.Add(pageStatusSetter);
        }

        protected void ShowStatusBar(UserControl userControl, string title, string message)
        {
            message = message.Trim(';');
            message = message.Replace(";", "<br/>");
            SPPageStatusSetter pageStatusSetter = new SPPageStatusSetter();
            pageStatusSetter.AddStatus(title, message, SPPageStatusColor.Yellow);
            userControl.Controls.Add(pageStatusSetter);
        }

        protected void SetPageTitle(Page page, string title)
        {
            LiteralControl titleControl = (LiteralControl)page.Master.FindControl("PlaceHolderPageTitle").Controls[0];
            titleControl.Text = title;
        }

        private const string JS_VERSION_FORMAT = "?v={0}";
        private const string JS_DATE_VERSION_FORMAT = "yyyyMMddhhmmss";
        protected static string JSVersion
        {
            get
            {
                return string.Format(JS_VERSION_FORMAT, DateTime.Now.ToString(JS_DATE_VERSION_FORMAT));
            }
        }
    }
}
