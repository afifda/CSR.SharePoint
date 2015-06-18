<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_CONTROLTEMPLATES/15/HumasCSR/Attachment.ascx" TagPrefix="uc1" TagName="Attachment" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealisasiProgramControl.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.HumasCSR.RealisasiProgramControl" %>

<div class="border">
    <div id="wrapping" class="clearfix">
       <div class="title-h1">Input Realisasi</div>
        <div id="left-side">
             <div class="div">
                <span class="lbl">Transaksi No <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input type="text" id="txtTransaksiNo"  class="input" />           
            </div>
            <div class="div">
                <span class="lbl">Kategori Program <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <select id="ddlKategori" class="input"></select>                 
            </div> 
            <div class="div">
                <span class="lbl">Bidang Program <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <select id="ddlBidang" class="input"></select>                
            </div>
            <div class="div">
                <span class="lbl">Judul Program <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input type="text" id="txtJudul" class="input" />
            </div> 
            <div class="div">
                <span class="lbl">Area <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <select id="ddlArea" class="input"></select>                
            </div>
            <div class="div">
                <span class="lbl">Waktu <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input name="txtDueDate" class="input-withbutton2" id="dateFrom" type="text"/>
                <span> s/d </span> 
                <input name="txtDueDate" class="input-withbutton2" id="dateTo" type="text"/>    
            </div>
        </div>        
        <div id="right-side">            
            <div class="div">
                <span class="lbl">Pelaksana <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input type="text" id="txtPelaksana" class="input" />
            </div> 
            <div class="div">
                <span class="lbl">Penerima <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input type="text" id="txtPenerima" class="input" />           
            </div> 
            <%--<div class="div">
                <span class="lbl">Jumlah <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input type="text" id="txtJumlah" class="input currencyFormat" onkeypress="return isNumberKey(event)"/>
            </div>            --%>
            <div class="div">
                <span class="lbl">Sumber PGE Pusat (IDR) </span>
                <span class="titikdua">:</span>
                <input type="text" id="txtSumberPGEPusat" class="input currencyFormat" onkeypress="return isNumberKey(event)" />           
            </div> 
            <div class="div">
                <span class="lbl">Sumber PGE Area (IDR) </span>
                <span class="titikdua">:</span>
                <input type="text" id="txtSumberPGEArea" class="input currencyFormat" onkeypress="return isNumberKey(event)" />           
            </div> 
            <div class="div">
                <span class="lbl">Sumber Persero (IDR) </span>
                <span class="titikdua">:</span>
                <input type="text" id="txtSumberPersero" class="input currencyFormat" onkeypress="return isNumberKey(event)" />           
            </div> 
            <div class="div">
                <span class="lbl">Sumber PKBL (IDR) </span>
                <span class="titikdua">:</span>
                <input type="text" id="txtSumberPKBL" class="input currencyFormat" onkeypress="return isNumberKey(event)" />           
            </div> 
        </div>
    </div>    
</div>
<br />
<div class="border">
    <div id="wrapping" class="clearfix">
        <div class="title-h1">File Pendukung</div>
        <uc1:Attachment runat="server" id="Attachment" />
    </div>
</div>
<br />
<div class="border">
    <div id="wrapping" class="clearfix">
        <div id="bottom-side">
            <div class="div">
                <span class="lbl-bottom" >Keterangan </span>
                <span class="titikdua">:</span>
                <asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" Rows="5" CssClass="input-textarea-bottom" ClientIDMode="Static"></asp:TextBox>
            </div>
       </div>
    </div>
</div>
<div class="button-template2">
    <input type="button" id="btnAddProgram" class="button" value="Simpan"/>
    <input type="button" id="btnBatal" class="button" value="Batal"/>    
</div>
<script src="../../../_layouts/15/CSR.SharePointApplication/js/date.js"></script>
<script src="../../../_layouts/15/CSR.SharePointApplication/js/jquery.formatCurrency-1.4.0.min.js"></script>
<script src="../../../_layouts/15/CSR.SharePointApplication/js/RealisasiProgram.js"></script>