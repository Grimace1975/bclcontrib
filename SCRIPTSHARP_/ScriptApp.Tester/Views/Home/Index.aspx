<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
        <% Ajax.InitializeScriptlets(); %>
        <% Ajax.AddScriptReference("Script.WebEx"); %>
        <% Ajax.AddScriptReference("ScriptApp"); %>
        <% Ajax.RenderScriptlets(); %>
</asp:Content>
