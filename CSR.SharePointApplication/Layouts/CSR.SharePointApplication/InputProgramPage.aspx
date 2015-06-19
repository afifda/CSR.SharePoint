<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_CONTROLTEMPLATES/15/HumasCSR/BidangProgramControl.ascx" TagPrefix="uc1" TagName="BidangProgramControl" %>

<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputProgramPage.aspx.cs" Inherits="CSR.SharePointApplication.Layouts.CSR.SharePointApplication.InputProgramPage" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
        .loadingModal {
            display:    none;
            position:   fixed;
            z-index:    1000;
            top:        0;
            left:       0;
            height:     100%;
            width:      100%;
            background: rgba( 255, 255, 255, .8 ) 
                        url('/sites/HumasCSR/_layouts/15/CSR.SharePointApplication/images/ajax-loader.gif') 
                        50% 50% 
                        no-repeat;
        }

        body.loading {
            overflow: hidden;   
        }

        body.loading .loadingModal {
            display: block;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="hfTransaksiNo" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfIsAdmin" runat="server" ClientIDMode="Static"/>
    <uc1:BidangProgramControl runat="server" id="BidangProgramControl" />    
    <div class="loadingModal"></div>
    <div id="dialog-confirm"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Input Program
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Input Program
</asp:Content>
