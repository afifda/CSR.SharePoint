<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Attachment.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.HumasCSR.Attachment" %>


<div class="div">
    <input type="file" id="fuAttachment" CssClass="input" name="fuAttachment[]" multiple  /> <output id="list"></output> 
    <input type="file" id="fuAttachment1" CssClass="input" name="fuAttachment[]" multiple  /> <output id="list"></output>
    <input type="file" id="fuAttachment2" CssClass="input" name="fuAttachment[]" multiple  /> <output id="list"></output> 
</div>
<div class="button-template2">
    <asp:Button ID="btnUpload" runat="server" Text="Upload"   ClientIDMode="Static"/>
</div>

<table id="gvAttachment" class="tabelgrid attachmentcontrol">
    <thead>
        <tr>
            <th class="header-grid">No</th>
            <th class="header-grid">Nama File</th>
            <th class="header-grid">Lokasi File</th>
            <th class="header-grid" hidden="true">Temp File</th>
            <th class="header-grid" id="Action_ID">Action</th>
        </tr>
    </thead>
</table>
<script src="../../../_layouts/15/CSR.SharePointApplication/js/Attachment.js"></script>
