using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using CSR.Service.Entity;
using CSR.Service.BusinessLogic;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class MasterDataPage : BaseLayoutPages
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidUser)
            {
                Response.Redirect("/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            string strMasterType = Request.QueryString["MasterType"];
            if (string.IsNullOrEmpty(strMasterType))
            {
                ShowStatusBar(this, "Information", "Query string MasterType is not found");
                return;
            }

            string controlTemplate = MasterInputControl(strMasterType);
            if (string.IsNullOrEmpty(controlTemplate))
            {
                ShowStatusBar(this, "Information", string.Format("Master Data {0} is not found", strMasterType));
                return;
            }

            SetPageTitle(this.Page, MasterInputTitle(strMasterType));

            Control control = Page.LoadControl(Constant.CONTROL_TEMPLATES_PATH + controlTemplate);
            ControlContainer.Controls.Add(control);
        }

        #region Master Area
            [System.Web.Services.WebMethod]
            public static List<MasterAreaEntity> LoadMasterArea()
            {
                List<MasterAreaEntity> areaList = null;
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    areaList = logic.SPRead<MasterAreaEntity>(new MasterAreaEntity() { AreaCode = "" });
                }
                catch (Exception ex)
                {
                    return areaList;
                }
                return areaList;
            }

            [System.Web.Services.WebMethod]
            public static string SaveMasterArea(string masterAreaString, bool isEdit )
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MasterAreaEntity masterArea = (MasterAreaEntity)serializer.Deserialize(masterAreaString, typeof(MasterAreaEntity));
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    if (isEdit) logic.SPUpdate<MasterAreaEntity>(masterArea);
                    else logic.SPSave<MasterAreaEntity>(masterArea);
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("duplicate")) return string.Format("Kode {0} yang anda masukkan telah tersedia", masterArea.AreaCode);
                    else return string.Format("Telah terjadi error. ({0})", sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return string.Format("Telah terjadi error. ({0})", ex.Message);
                }
                return "Success. Master Area telah disimpan.";
            }
        
            [System.Web.Services.WebMethod]
            public static string DeleteMasterArea(string kodeArea)
            {
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    logic.SPDelete<MasterAreaEntity>(new MasterAreaEntity() { AreaCode = kodeArea });
                }
                catch (Exception ex)
                {
                    return "Telah terjadi error";
                }
                return "Success";
            }        
        #endregion

        #region Master Bidang
            [System.Web.Services.WebMethod]
            public static List<MasterBidangProgramEntity> LoadMasterBidang()
            {
                List<MasterBidangProgramEntity> bidangList = null;
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    bidangList = logic.SPRead<MasterBidangProgramEntity>(new MasterBidangProgramEntity() { BP_Kode = 0 });
                }
                catch (Exception ex)
                {
                    return bidangList;
                }
                return bidangList;
            }
            [System.Web.Services.WebMethod]
            public static string SaveMasterBidang(string masterBidangString, bool isEdit)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MasterBidangProgramEntity masterBidang = (MasterBidangProgramEntity)serializer.Deserialize(masterBidangString, typeof(MasterBidangProgramEntity));
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    if (isEdit) logic.SPUpdate<MasterBidangProgramEntity>(masterBidang);
                    else logic.SPSave<MasterBidangProgramEntity>(masterBidang);
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("duplicate")) return string.Format("Kode {0} yang anda masukkan telah tersedia", masterBidang.BP_Kode);
                    else return string.Format("Telah terjadi error. ({0})", sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return string.Format("Telah terjadi error. ({0})", ex.Message);
                }
                return "Success. Master Bidang telah disimpan.";
            }

            [System.Web.Services.WebMethod]
            public static string DeleteMasterBidang(int kodeBidang)
            {
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    logic.SPDelete<MasterBidangProgramEntity>(new MasterBidangProgramEntity() { BP_Kode = kodeBidang });                    
                }
                catch (Exception ex)
                {
                    return "Telah terjadi error";
                }
                return "Success";
            }
        #endregion

        #region Master Kategori
            [System.Web.Services.WebMethod]
            public static List<MasterKategoriProgramEntity> LoadMasterKategori()
            {
                List<MasterKategoriProgramEntity> kategoriList = null;
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    kategoriList = logic.SPRead<MasterKategoriProgramEntity>(new MasterKategoriProgramEntity() { KP_Kode = 0 });
                }
                catch (Exception ex)
                {
                    return kategoriList;
                }
                return kategoriList;
            }

            [System.Web.Services.WebMethod]
            public static string SaveMasterKategori(string masterKategoriString, bool isEdit)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MasterKategoriProgramEntity masterKategori = (MasterKategoriProgramEntity)serializer.Deserialize(masterKategoriString, typeof(MasterKategoriProgramEntity));
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    if (isEdit) logic.SPUpdate<MasterKategoriProgramEntity>(masterKategori);
                    else logic.SPSave<MasterKategoriProgramEntity>(masterKategori);
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("duplicate")) return string.Format("Kode {0} yang anda masukkan telah tersedia", masterKategori.KP_Kode);
                    else return string.Format("Telah terjadi error. ({0})", sqlEx.Message); 
                }
                catch (Exception ex)
                {
                    return string.Format("Telah terjadi error. ({0})", ex.Message);
                }
                return "Success. Master Kategori telah disimpan.";
            }

            [System.Web.Services.WebMethod]
            public static string DeleteMasterKategori(int kodeKategori)
            {
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    logic.SPDelete<MasterKategoriProgramEntity>(new MasterKategoriProgramEntity() { KP_Kode = kodeKategori });
                }
                catch (Exception ex)
                {
                    return "Telah terjadi error";
                }
                return "Success";
            }
        #endregion
        
        #region UserControl
            [System.Web.Services.WebMethod]
            public static List<MasterUserEntity> LoadMasterUser()
            {
                List<MasterUserEntity> UserList = null;
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    UserList = logic.SPRead<MasterUserEntity>(new MasterUserEntity() { No_Pegawai = "" });
                }
                catch
                {
                    return UserList;
                }
                return UserList;
            }
            [System.Web.Services.WebMethod]
            public static string SaveMasterUser(string masterUserString, bool isEdit)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                MasterUserEntity masterUser = (MasterUserEntity)serializer.Deserialize(masterUserString, typeof(MasterUserEntity));
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    if (isEdit) logic.SPUpdate<MasterUserEntity>(masterUser);
                    else logic.SPSave<MasterUserEntity>(masterUser);
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("duplicate")) return string.Format("Nomor Pegawai {0} yang anda masukkan telah tersedia", masterUser.No_Pegawai);
                    else return string.Format("Telah terjadi error. ({0})", sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return string.Format("Telah terjadi error. ({0})", ex.Message);
                }
                return "Success. Master User telah disimpan.";
            }
            [System.Web.Services.WebMethod]
            public static string DeleteMasterUser(string NoPegawai)
            {
                try
                {
                    MasterDataLogic logic = new MasterDataLogic();
                    logic.SPDelete<MasterUserEntity>(new MasterUserEntity() { No_Pegawai = NoPegawai });
                }
                catch
                {
                    return "Telah terjadi error";
                }
                return "Success";
            }
        #endregion

    }
}
