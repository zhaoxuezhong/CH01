<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Common.Master" Inherits="System.Web.Mvc.ViewPage<CH01.Models.RegisterModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    注册
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.7.1.js"></script>
    <script src="../../Scripts/jquery.validate.js"></script>
    <script src="../../Scripts/jquery.validate.unobtrusive.js"></script>
<div id="action_area">
    	<h2 class="action_type"><img src="<%=Url.Content("~/Images/register.gif") %>" alt="会员注册"></h2>
        <form action="<%=Url.Content("~/user/doregister") %>" method="post" class="member_form">
        	<p><label><span>*</span>用户名</label>
                <%--<input name="username" type="text" class="opt_input">5-12个字符或数字组成，可用中文名--%>
                <%=Html.TextBox("LoginId", null,new { @class="opt_input"})%>
                <%=Html.ValidationMessage("LoginId") %>
        	</p>
            <p><label><span>*</span>密&nbsp;&nbsp;&nbsp;&nbsp;码</label>
<%--                <input name="password" type="password" class="opt_input">请输入密码--%>
                <%=Html.Password("LoginPwd",null, new { @class="opt_input"})%>
                <%=Html.ValidationMessage("LoginPwd") %>
            </p>
            <p><label><span>*</span>确认密码</label>
                <%--<input name="password" type="password" class="opt_input">请再次输入密码--%>
                <%=Html.Password("rePwd",null, new { @class="opt_input"})%>
                <%=Html.ValidationMessage("rePwd") %>
            </p>
            <p><label><span>*</span>电子邮件</label>
                <%--<input name="password" type="password" class="opt_input">请输入电子邮件--%>
                <%=Html.TextBox("Mail",null, new { @class="opt_input"})%>
                <%=Html.ValidationMessage("Mail") %>
            </p>
            <%--<p><label><span>*</span>性&nbsp;&nbsp;&nbsp;&nbsp;别</label>
                <input name="password" type="password" class="opt_input"></p>
            
            <p><label><span>*</span>验证码</label>
                <input name="checkno" type="text" class="opt_input" style="width:60px;"><img src="Images/checkno.gif"> 请输入验证码</p>
            <p class="form_sub">
                <input type="checkbox" name="" checked="checked">  在此计算机上保留我的密码</p>--%>
            <p class="form_sub">
                <input type="submit" value="确定了，马上提交" class="opt_sub"></p>
            <p class="form_sub">加<span>*</span>的为必填项目</p>
            <p class="form_sub">&gt;<a href="<%=Url.Content("~/user/login") %>">已经有账号，马上登录</a>
                <br>&gt;如果你已经有“第三波书店”社区账号，请<a href="javascript:alert('书店社区暂未开通');">点这里</a>登录升级</p>
        </form>
    </div>

</asp:Content>
