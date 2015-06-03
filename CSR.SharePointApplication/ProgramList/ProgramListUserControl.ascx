﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramListUserControl.ascx.cs" Inherits="CSR.SharePointApplication.ProgramList.ProgramListUserControl" %>
<div id="wrapping" class="clearfix">
    <div id="one-side">
        <div class="title-h1">Program List</div>
        <div class="div">
             <select id="ddlYear" class="input"></select>
        </div>
        <div class="button-template">
            <input type="button" id="btnGenerateTable" value="Lihat" class="button"/>
        </div>
        <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>        
    </div>
    <br /><br />
    <table id="tblProgramList" class="display au-policy-list" cellspacing="0" width="100%">
        <thead>
            <tr>                
                <th title="Program No"></th>
                <th title="Bidang Program"></th>
                <th title="Judul Program"></th>
                <th title="Kategori"></th>
                <th title="Area"></th>
                <th title="Keterangan"></th>
                <th title="Jumlah Anggaran"></th>
            </tr>
            <tr>                
                <th> Program No</th>
                <th> Bidang Program</th>
                <th> Judul Program</th>
                <th> Kategori</th>
                <th> Area</th>
                <th> Keterangan</th>
                <th> Jumlah Anggaran</th>
            </tr>
        </thead>

    </table>
</div>
<script src="../_layouts/15/CSR.SharePointApplication/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../_layouts/15/CSR.SharePointApplication/js/ProgramList.js" type="text/javascript"></script>
