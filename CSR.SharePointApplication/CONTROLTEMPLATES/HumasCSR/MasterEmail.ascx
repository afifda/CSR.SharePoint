<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterEmail.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.MasterEmail" %>
<link href="../../_layouts/15/CSR.SharePointApplication/CSS/SML_template.css" rel="stylesheet" />

<div class="border">
    <div id="wrapping" class="clearfix">
        <div class="title-h1">Master Area</div>
        <div class="div">
             <table id="tblMasterArea" class="tabelgrid">
                  <thead>                
                    <tr>   
                       <th class="header-grid">Area Code</th>
                       <th class="header-grid">To</th>
                       <th class="header-grid">Subject</th>
                        <th class="header-grid">Message</th>
                    </tr>
                 </thead>
           </table>
        </div>
        <div class="button-template2">
            <input type="button" id="btnAddMasterArea" value="Add"/>
        </div>
        
        <div class="modal" id="modalMasterArea">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Detail User</h4>
                    </div>
                    <div class="modal-body">Email
                        <div>
                            <div class="div">
                                <span class="lbl">Area</span>
                                <span class="titikdua">:</span>
                                <input name="txtArea" class="input" id="txtArea" type="text" />
                            </div>
                            <div class="div">
                                <span class="lbl">To</span>
                                <span class="titikdua">:</span>
                                <input name="txtTo" class="input" id="txtTo" type="text" />
                            </div>
                            <div class="div">
                                <span class="lbl">Subject</span>
                                <span class="titikdua">:</span>
                                <input name="txtSubject" class="input" id="txtSubject" type="text" />
                            </div>   
                            <div class="div">
                                <span class="lbl">Message</span>
                                <span class="titikdua">:</span>
                                <input name="txtMessage" class="input" id="txtMessage" type="text" />
                            </div>                                                
                        </div>                
                    </div>
                    <div class="modal-footer">
                        <input type="button" id="btnSaveMasterArea" value="Add" class="button"/>
                        <button type="button" data-dismiss="modal" class="button">Close</button>
                    </div>
                </div>
            </div>
        </div>    
    </div>
</div>
<script src="../../_layouts/15/CSR.SharePointApplication/js/MasterEmail.js"></script>
