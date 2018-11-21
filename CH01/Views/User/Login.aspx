<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Common.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    登陆
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="action_area">
    	<h2 class="action_type"><img src='<%=Url.Content("~/Images/login_in.gif") %>' alt="会员登录"></h2>
        <p class="state">欢迎光临第三波书店网站，本站为淘宝网旗下专业在线书店！<br>您可以使用第三波书店的用户名，直接登录。</p>
        <%
            string userName = Request.Cookies["userName"] != null ? Request.Cookies["userName"].Value : "";
            string password = Request.Cookies["password"] != null ? Request.Cookies["password"].Value : "";
             %>
        <form action="<%= Url.Content("~/user/dologin") %>" method="post" class="member_form">
        	<p><label>用户名</label><input name="username" type="text" value="<%= userName %>" class="opt_input"></p>
            <p><label>密&nbsp;&nbsp;&nbsp;&nbsp;码</label><input name="password" type="password" value="<%= password %>" class="opt_input"></p>
            <p><label>验证码</label><input name="validateCode" type="text" class="opt_input"> 
                <img src='<%=Url.Content("~/user/SecurityCode") %>' onclick="clickCode(this)"></p>
            <p class="form_sub"><input type="checkbox" name="remanber" checked="checked">  在此计算机上保留我的密码</p>
            <p class="form_sub"><input type="submit" value="登入" class="opt_sub"><a href="#">忘记密码？</a></p>
        </form>
    </div>

    <script>
        function clickCode(val) {
            var date = new Date().getTime();
            val.src = "<%=Url.Content("~/user/SecurityCode")%>?t="+date;
        }

    </script>
</asp:Content>
