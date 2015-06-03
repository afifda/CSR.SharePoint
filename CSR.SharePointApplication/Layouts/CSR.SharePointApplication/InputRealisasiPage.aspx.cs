﻿using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using CSR.Service.BusinessLogic;
using System.Web.Script.Serialization;
using CSR.Service.Entity;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace CSR.SharePointApplication.Layouts.CSR.SharePointApplication
{
    public partial class InputRealisasiPage : BaseLayoutPages
    {
        public const string DocLibProgram = "RealisasiDocLib";
        private static bool IsEdit = false;
        private static bool IsPlanned = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidUser)
            {
                string web = SPContext.Current.Web.Url;
                Response.Redirect(web + "/_layouts/15/eWorkflow.WebAccess/ErrorPage.aspx?ErrCode=NotAuthorized", true);
            }
            string strTransNo = Request.QueryString["TransaksiNo"];
            string strRealNo = Request.QueryString["RealisasiNo"];

            if (!string.IsNullOrEmpty(strTransNo))
            {
                this.hfTransaksiNo.Value = strTransNo;
            }
            if (string.IsNullOrEmpty(this.hfTransaksiNo.Value))
            {
                IsPlanned = false;
            }

            if (!string.IsNullOrEmpty(strRealNo))
            {
                this.hfRealisasiNo.Value = strRealNo;
            }
            if (!string.IsNullOrEmpty(this.hfRealisasiNo.Value))
            {
                IsEdit = true;
            }            
        }

        [System.Web.Services.WebMethod]
        public static string LoadRealisasi(string realisasiNo, string transaksiNo)
        {
            RealisasiEntity realisasi = new RealisasiEntity() { RealisasiNo = realisasiNo };
            RealisasiByTransaksiNoEntity realisasiByTrans = new RealisasiByTransaksiNoEntity() { TransaksiNo = transaksiNo };
            if (string.IsNullOrEmpty(realisasiNo) && string.IsNullOrEmpty(transaksiNo)) return string.Empty;
            string result = string.Empty;
            try
            {
                ProgramLogic logic = new ProgramLogic();
                if (!string.IsNullOrEmpty(realisasi.RealisasiNo))
                {
                    realisasi = logic.SPRead<RealisasiEntity>(realisasi).FirstOrDefault();
                    realisasi.AttachmentList = logic.GetAttachments(new AttachmentEntity() { TransaksiNo = realisasiNo });
                    result = new JavaScriptSerializer().Serialize(realisasi);
                }
                else if (!string.IsNullOrEmpty(realisasiByTrans.TransaksiNo))
                {
                    ProgramEntity program = logic.SPRead<ProgramEntity>(new ProgramEntity() { TransaksiNo = transaksiNo }).FirstOrDefault();
                    realisasiByTrans.KP_Kode = program.KP_Kode;
                    realisasiByTrans.BP_Kode = program.BP_Kode;
                    realisasiByTrans.Judul_Program = program.Judul_Program;
                    realisasiByTrans.Area_Kode = program.Area_Kode;

                    result = new JavaScriptSerializer().Serialize(realisasiByTrans);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
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
        public static string SaveRealisasi(string programString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RealisasiEntity realisasiEntity = (RealisasiEntity)serializer.Deserialize(programString, typeof(RealisasiEntity));
            string SiteURL = SPContext.Current.Web.Url;
            string DocLib = DocLibProgram;
            int status = 0;
            try
            {
                ProgramLogic logic = new ProgramLogic();
                if (IsEdit)
                {
                    realisasiEntity.Last_Modified_Date = DateTime.Now;
                    realisasiEntity.Last_Modified_By = User.UserName;
                    logic.SPUpdate<RealisasiEntity>(realisasiEntity);
                    if (realisasiEntity.AttachmentList.Count != 0)
                    {
                        realisasiEntity.AttachmentList.ForEach(a => a.TransaksiNo = realisasiEntity.RealisasiNo);
                        status = logic.SaveAttachment(realisasiEntity.AttachmentList);
                        status = logic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, realisasiEntity.AttachmentList);
                    }

                }
                else if (IsPlanned)
                {
                    List<RealisasiByTransaksiNoEntity> realisasiList = logic.SPRead<RealisasiByTransaksiNoEntity>(new RealisasiByTransaksiNoEntity() { TransaksiNo = realisasiEntity.TransaksiNo });
                    
                    if (realisasiList == null || realisasiList.Count == 0)
                    {
                        realisasiEntity.RealisasiNo = realisasiEntity.TransaksiNo + "-1";
                    }
                    else if (realisasiList.Count == 1 && string.IsNullOrEmpty(realisasiList[0].RealisasiNo))
                    {
                        realisasiEntity.RealisasiNo = realisasiEntity.TransaksiNo + "-1";
                    }
                    else
                    {
                        List<string> noList = (from r in realisasiList
                                               orderby r.Created_Date descending
                                               select r.RealisasiNo.Split('-')[4]).ToList();
                        int lastNo = int.Parse(noList[0]);
                        realisasiEntity.RealisasiNo = realisasiEntity.TransaksiNo + "-" + (lastNo + 1).ToString();
                    }
                    realisasiEntity.Created_Date = DateTime.Now;
                    realisasiEntity.Created_By = User.UserName;
                    realisasiEntity.Last_Modified_Date = DateTime.Now;
                    realisasiEntity.Last_Modified_By = User.UserName;
                    logic.SPSave<RealisasiEntity>(realisasiEntity);
                    if (realisasiEntity.AttachmentList.Count != 0)
                    {
                        realisasiEntity.AttachmentList.ForEach(a => a.TransaksiNo = realisasiEntity.RealisasiNo);
                        status = logic.SaveAttachment(realisasiEntity.AttachmentList);
                        status = logic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, realisasiEntity.AttachmentList);
                    }
                }
                else
                {
                    ProgramEntity program = new ProgramEntity() 
                    {
                        Area_Kode = realisasiEntity.Area_Kode,
                        KP_Kode = realisasiEntity.KP_Kode,
                        BP_Kode = realisasiEntity.BP_Kode,
                        Judul_Program = realisasiEntity.Judul_Program,
                        Waktu_Mulai = realisasiEntity.WaktuMulai,
                        Waktu_Sampai = realisasiEntity.WaktuSelesai,
                        Jumlah_Anggaran = realisasiEntity.SumberDanaPersero + realisasiEntity.SumberDanaPGE + realisasiEntity.SumberPKBL,
                        Created_Date = DateTime.Now,
                        Created_By = User.UserName,
                        Last_Modified_Date = DateTime.Now,
                        Last_Modified_By = User.UserName
                    };
                    realisasiEntity.TransaksiNo = logic.SPSaveWithOutput<ProgramEntity>(program, "TransaksiNo");
                    realisasiEntity.RealisasiNo = realisasiEntity.TransaksiNo + "-1";
                    realisasiEntity.Created_Date = DateTime.Now;
                    realisasiEntity.Created_By = User.UserName;
                    realisasiEntity.Last_Modified_Date = DateTime.Now;
                    realisasiEntity.Last_Modified_By = User.UserName;
                    logic.SPSave<RealisasiEntity>(realisasiEntity);
                    if (realisasiEntity.AttachmentList.Count != 0)
                    {
                        realisasiEntity.AttachmentList.ForEach(a => a.TransaksiNo = realisasiEntity.RealisasiNo);
                        status = logic.SaveAttachment(realisasiEntity.AttachmentList);
                        status = logic.SaveAttachmentToSharePointLibrary(SiteURL, DocLib, realisasiEntity.AttachmentList);
                    }
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
