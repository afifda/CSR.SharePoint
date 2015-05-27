<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterKategoriControl.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.HumasCSR.MasterKategoriControl" %>
<link href="../../../_layouts/15/CSR.SharePointApplication/CSS/SML_template.css" rel="stylesheet" />
<div class="border">
    <div id="wrapping" class="clearfix">
        <div class="title-h1">Master Area</div>
        <div class="div">
             <table id="tblMasterKategori" class="tabelgrid">
                  <thead>                
                    <tr>   
                       <th class="header-grid">Nama Kategori</th>
                       <th class="header-grid">Action</th>
                    </tr>
                 </thead>
           </table>
        </div>
        <div class="button-template2">
            <input type="button" id="btnAddMasterKategori" value="Add"/>
        </div>
        
        <div class="modal" id="modalMasterKategori">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Detail Kategori</h4>
                    </div>
                    <div class="modal-body">
                        <div>                            
                            <div class="div">
                                <span class="lbl">Nama Kategori</span>
                                <span class="titikdua">:</span>
                                <input name="txtNamaKategori" class="input" maxlength="50" id="txtNamaKategori" type="text" />
                            </div>                                                   
                        </div>                
                    </div>
                    <div class="modal-footer">
                        <input type="button" id="btnSaveMasterKategori" value="Add" class="button"/>
                        <button type="button" data-dismiss="modal" class="button">Close</button>
                    </div>
                </div>
            </div>
        </div>    
    </div>
</div>
<script src="../../../_layouts/15/CSR.SharePointApplication/js/MasterKategori.js" type="text/javascript"></script>
