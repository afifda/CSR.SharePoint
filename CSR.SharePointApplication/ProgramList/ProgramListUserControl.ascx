<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramListUserControl.ascx.cs" Inherits="CSR.SharePointApplication.ProgramList.ProgramListUserControl" %>

<div id="wrapping" class="clearfix">
    <%--<div class="title-h1">Input Program</div>--%>
        <div id="left-side">
            <div class="div">
                <select id="ddlYear" class="input"></select>
            </div>
        </div>
        <div id="right-side"> 
        </div>
</div>
<div id="wrapping" class="clearfix">
    <div id="one-side">
        <%--<div class="title-h1">Program List</div>--%>
        <%--<div class="div">
             <select id="ddlYear" class="input"></select>
        </div>--%>
        <div class="button-template">
            <input type="button" id="btnGenerateTable" value="Lihat" class="button"/>
        </div>
        <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>        
    </div>
    <br /><br />
    <%--<table id="tblProgramList" class="display" cellspacing="1" width="100%">--%>
    <table id="tblProgramList" class="tabelgrid">
        <thead>
            <%--<tr>                
                <th title="Program No"></th>
                <th title="Bidang Program"></th>
                <th title="Judul Program"></th>
                <th title="Kategori"></th>
                <th title="Area"></th>
                <th title="Keterangan"></th>
                <th title="Jumlah Anggaran"></th>
            </tr>--%>
            <tr>                
                <th class="header-grid"> Program No</th>
                <th class="header-grid"> Bidang Program</th>
                <th class="header-grid"> Judul Program</th>
                <th class="header-grid"> Kategori</th>
                <th class="header-grid"> Area</th>
                <th class="header-grid"> Keterangan</th>
                <th class="header-grid"> Jumlah Anggaran</th>
            </tr>
         </thead>
    </table>
</div>
 <div class="modal-footer">
    <input type="button" id="btnConfirm" value="Kirim dan Kunci" class="button"/>
</div>

<link href="../../_layouts/15/CSR.SharePointApplication/CSS/jquery.dataTables.min.css" rel="stylesheet" />
<script src="../_layouts/15/CSR.SharePointApplication/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../_layouts/15/CSR.SharePointApplication/js/ProgramList.js" type="text/javascript"></script>
