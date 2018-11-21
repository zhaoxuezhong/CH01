<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<BookShop.Models.Category>>" %>

<%
    foreach (var item in Model)
    {
%>
    <li><a href="<%=Url.Content("~/Books/List?categoryId="+item.Id+"&order=PublishDate") %>" target="_blank"><%=item.Name %></a></li>
<%
    }
%>