<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramListUserControl.ascx.cs" Inherits="CSR.SharePointApplication.ProgramList.ProgramListUserControl" %>

<style>
    table.display td:first-child {
        text-align: center;
    }
</style>

<div id="wrapping" class="clearfix">    
    <div id="left-side">
        <div class="div">
            <span class="lbl">Tahun </span>
            <select id="ddlYear" class="input"></select>
            <asp:HiddenField ID="hfSelectedYear" runat="server" ClientIDMode="Static"/>
        </div>
        <div class="div">
            <span class="lbl">Area </span>
            <select id="ddlArea" class="input"></select>
            <asp:HiddenField ID="hfSelectedArea" runat="server" ClientIDMode="Static"/>
        </div>
        <div class="button-template">
            <asp:HiddenField ID="hfIsAdmin" runat="server" ClientIDMode="Static"/>
            <input type="button" id="btnGenerateTable" value="Lihat" class="button"/>
        </div>
    </div>
    <div id="right-side"> 
    </div>
</div>
<div id="wrapping" class="clearfix">
    <div id="one-side">
        
        <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>        
    </div>
    <br /><br />
    <%--<table id="tblProgramList" class="display" cellspacing="1" width="100%">--%>
    <table id="tblProgramList" class="tabelgrid display">
        <thead>
            <%--<tr>    
                <th></th>            
                <th title="Program No"></th>
                <th title="Bidang Program"></th>
                <th title="Judul Program"></th>
                <th title="Kategori"></th>
                <th title="Area"></th>
                <th title="Keterangan"></th>
                <th title="Jumlah Anggaran"></th>
                <th title="Status"></th>
            </tr>--%>
            <tr>         
                <th>
                    <input type="checkbox" id="checkAll" />
                </th>       
                <th class="sorting"> Program No</th>
                <th> Bidang Program</th>
                <th> Judul Program</th>
                <th> Kategori</th>
                <th> Area</th>
                <th> Keterangan</th>
                <th> Jumlah Anggaran</th>
                <th> Status</th>
            </tr>
         </thead>
    </table>
</div>
 <div class="modal-footer">
    <input type="button" id="btnConfirm" value="Kirim dan Kunci" class="button"/>
    <input type="button" id="btnUnlock" value="Buka Kunci" class="button" style="display:none"/>
</div>

<link href="../../_layouts/15/CSR.SharePointApplication/CSS/jquery.dataTables.min.css" rel="stylesheet" />
<script src="../_layouts/15/CSR.SharePointApplication/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../_layouts/15/CSR.SharePointApplication/js/ProgramList.js" type="text/javascript"></script>
