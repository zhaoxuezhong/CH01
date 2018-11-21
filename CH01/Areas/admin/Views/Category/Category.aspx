<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/admin/Views/Shared/AdminCommon.Master" Inherits="System.Web.Mvc.ViewPage<List<BookShop.Models.Category>>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminTitle" runat="server">
    后台管理 - 首页
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContent" runat="server">
<div id="breadcrumb" class="black">您现在的位置：  <a href="#">第三波</a>  >    
    <a href="#">管理员后台</a>  >    <a href="#">图书分类管理</a> </div>

<form id="form1" name="form1" method="post" action="<%=Url.Content("~/admin/category/doAdd")%>">
    <input type="text"  name="name"/><input type="submit" name="button" id="button" value="增加分类" />
<table width="96%" border="0" cellspacing="0" cellpadding="0" align="center" class="data_table">

<thead>
  <tr>
    <%--<th width="10%" scope="col">
   
      <input type="checkbox" name="checkbox" id="checkbox" />
      全选</th>
    <th scope="col">书名</th>--%>
    <th width="20%" scope="col">Id</th>
    <th width="80%" scope="col">分类名称</th>
  </tr>
</thead>
<tbody id="data_body">
    <%
        foreach (var item in Model)
        {
            %>
    <tr>
    <%--<td><input type="checkbox" name="checkbox4" id="checkbox4" /></td>
    <td class="name">C语言程序设计教程（第五版）</td>--%>
    <td><%=item.Id %></td>
    <td><%=item.Name %></td>
  </tr>
    
    <%
        }
     %>
 <%-- <tr>
    <td><input type="checkbox" name="checkbox4" id="checkbox4" /></td>
    <td class="name">C语言程序设计教程（第五版）</td>
    <td>王思涵</td>
    <td>C++,VC++</td>
  </tr>--%>
</tbody>

<%--<tfoot>
  <tr>
    <td colspan="4" class="pages">
    		<a href="#" title="前一页" class="first"><<</a>
            <a href="#" title="" class="current">1</a>
            <a href="#" title="">2</a>
            <a href="#" title="">3</a>
            <a href="#" title="">4</a>
            <a href="#" title="">5</a>
            <a href="javascript:void(0)" class="more">...</a>
            <a href="#" title="">106</a>
            <a href="#" title="">107</a>
            <a href="#" title="">108</a>
            <a href="#" title="">109</a>
            <a href="#" title="">110</a>
            <a href="#" title="后一页" class="end">>></a>
    </td>
    </tr>
</tfoot>--%>
</table>

<%--<div class="opt_action">
    将选中书归入：
    <select name="select" id="select">
      <option value="0">C++</option>
      <option value="1">VC++</option>
    </select>
    
    <input type="submit" name="button" id="button" value="提交" />
</div>--%>

</form>
 
 <script type="text/javascript">
 var s = document.getElementById("data_body").getElementsByTagName("tr");
 for( var i=0; i< s.length; i++) s[i].style.background = i%2==0?"#DDF5D9":"#fff";
 </script>
</asp:Content>
