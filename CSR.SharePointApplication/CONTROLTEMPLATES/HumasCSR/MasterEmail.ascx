<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterEmail.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.HumasCSR.MasterEmail" %>

<div class="border">
    <div id="wrapping" class="clearfix">
        <div class="title-h1">Master Penerima Email Pemberitahuan</div>
        <div class="div">
             <table id="tblMasterArea" class="tabelgrid">
                  <thead>                
                    <tr>   
                       <th class="header-grid">Area Code</th>
                        <th class="header-grid">Bidang</th>
                        <th class="header-grid">Kepada</th>
                        <th class="header-grid">Type</th>
                        <th class="header-grid">URL</th>
                       <th class="header-grid">Email</th>
                       <th class="header-grid">Subject</th>
                       <th class="header-grid">Message</th>
                       <th class="header-grid">Action</th>
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
                        <h4 class="modal-title">Detail Email</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="div">
                                <span class="lbl">Area <span class="red">*</span></span>
                                <span class="titikdua">:</span>                                
                                <select id="txtArea" class="input"></select>
                            </div>
                            <div class="div">
                                <span class="lbl">Bidang <span class="red">*</span></span>
                                <span class="titikdua">:</span>                                
                                <select id="txtBidang" class="input"></select>
                            </div>
                            <div class="div">
                                <span class="lbl">Kepada <span class="red">*</span></span>
                                <span class="titikdua">:</span>
                                <input name="txtKepada" class="input" id="txtKepada" type="text" />
                            </div>
                            <div class="div">
                                <span class="lbl">TypeProgram</span>
                                <span class="titikdua">:</span>
				                <table>
					                <tr>
						                <td>
							                <input type="radio" name="Program" value="P" rows="3" id="RadioProgram">Program </input>
						                </td>
						                <td>
							                <input type="radio" name="Realisasi" value="R" rows="3" id="RadioRealisasi">Realisasi </input>
						                </td>		
					                </tr>
				                </table>   
                            </div>
                            <div class="div">
                                <span class="lbl">AlamatURL<span class="red">*</span></span>
                                <span class="titikdua">:</span>
                                <input name="txtURL" class="input" id="txtURL" type="text" />
                            </div>
                            <div class="div">
                                <span class="lbl">Email <span class="red">*</span></span>
                                <span class="titikdua">:</span>
                                <input name="txtTo" class="input" id="txtTo" type="text" />
                            </div>
                            <div class="div">
                                <span class="lbl">Subject <span class="red">*</span></span>
                                <span class="titikdua">:</span>
                                <input name="txtSubject" class="input" id="txtSubject" type="text" />
                            </div>   
                            <div class="div">
                                <span class="lbl">Pesan <span class="red">*</span></span>
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
<script src="../../../_layouts/15/CSR.SharePointApplication/js/MasterEmail.js"type="text/javascript"></script>
