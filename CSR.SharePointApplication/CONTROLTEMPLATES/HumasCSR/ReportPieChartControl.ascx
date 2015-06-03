﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportPieChartControl.ascx.cs" Inherits="CSR.SharePointApplication.CONTROLTEMPLATES.HumasCSR.ReportControl" %>

<div class="border">
    <div id="wrapping" class="clearfix">
       <div class="title-h1">Laporan Pie Chart</div>
        <div id="left-side">
         <div class="div">
                <span class="lbl">Waktu <span class="red">*</span></span>
                <span class="titikdua">:</span>
                <input name="txtDueDate" class="input-withbutton2" id="dateFrom" type="text"/>
                <span> s/d  </span> 
                <input name="txtDueDate" class="input-withbutton2" id="dateTo" type="text"/>    
            </div>
         <div class="div">
                <span class="lbl">Area</span>
                <span class="titikdua">:</span>
                <select id="ddlArea" class="input"></select>                
            </div>
        <div class="div">
                <span class="lbl">Kategori Program</span>
                <span class="titikdua">:</span>
                <select id="ddlKategori" class="input"></select>                 
            </div> 
            <div class="div">
                <span class="lbl">Bidang Program</span>
                <span class="titikdua">:</span>
                <select id="ddlBidang" class="input"></select>                
            </div>
          </div>  
        </div>
    </div>    
</div>
<div class="button-template2">
    <input type="button" id="btnExecute" class="button" value="Lihat Report"/>
</div>

<script src="../../../_layouts/15/CSR.SharePointApplication/js/ReportPieChart.js"></script>