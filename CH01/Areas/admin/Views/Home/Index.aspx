<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/AdminCommon.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminTitle" runat="server">
    后台管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="AdminContent" runat="server">
    <div id="breadcrumb" class="black">您现在的位置：  <a href="#">第三波</a>  >    
    <a href="#">管理员后台</a>  ></div>
    <h2>欢迎进入管理界面</h2>
</asp:Content>
