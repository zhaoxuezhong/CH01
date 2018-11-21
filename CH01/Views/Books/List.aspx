<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<BookShop.Models.Book>>" %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<meta name="Robots" content="index,follow" />
<title>图书列表 - "第三波书店"-网上书店</title>
<link href=<%=Url.Content("~/CSS/global.css") %> rel="stylesheet" type="text/css" />
<link href=<%=Url.Content("~/CSS/channel.css") %> rel="stylesheet" type="text/css" />
<script type="text/javascript" src=<%=Url.Content("~/js/global.js") %>></script>
    <style>
        .list_area img {
            margin-right: 0;
            width: auto;
        }

    </style>
</head>

<body>
<div id="top">
	<div class="status">您好，欢迎来到第三波书店^_^   
        <%-- %><span> <a href="#">【登录】</a><a href="#">【免费注册】</a></span> --%>
        <%= Session["loginUser"]==null
            ?string.Format("<span><a href='{0}'>【登录】</a><a href='#'>【免费注册】</a></span>",Url.Content("~/user/login"))
            :string.Format("<span>您好，{0}<a href='{1}'>【注销】</a></span>",
                            (Session["loginUser"] as BookShop.Models.User).Name,Url.Content("~/user/loginout"))
             %>
	</div>
    <div class="member">
    	<ul>
        	<li><a href="#"><img src=<%=Url.Content("~/Images/payVIP.gif") %> height="18" alt="开通VIP" /></a></li>
            <li><a href="#"><img src=<%=Url.Content("~/Images/payCoin.gif") %> height="18" alt="学币中心" /></a></li>
        </ul>
    </div>
</div>

<div id="header">
	<div id="logo"><img src=<%=Url.Content("~/Images/logo.gif") %> alt="第三波书店" /></div>
    <div id="nav">
    	<div id="a_b_01"><img src=<%=Url.Content("~/images/a_b_01.gif") %> />[<img src=<%=Url.Content("~/images/taobao.gif") %> /> <a href="#">分站</a>]</div>
        <ul id="mainnav">
        	<li class="current"><a href="<%=Url.Content("~/home/index") %>">首页</a></li>
            <li><a href="#">登录</a></li>
            <li><a href="#">注册</a></li>
            <li><a href="#">商讯</a></li>
            <li><a href="#">购物流程</a></li>
            <li><a href="#">在线客服</a></li>
            <li><a href="#">积分兑换</a></li>
            <li><a href="#">书籍求购</a></li>
            <li><a href="#">帮助</a></li>
        </ul>
    </div>
</div>

<div id="a_b_04"><img src=<%=Url.Content("~/images/a_b_04.jpg")%> alt="" /></div>

<div id="breadcrumb">您现在的位置： 首页  >   <%=Model!=null&&Model.Count>0?Model[0].Category.Name:""
                                     %>   >   <a href="#" class="red">言情（1890种）</a></div>

<div id="container">
	<!--left content-->
	<div id="intro">
    	 <div class="child_menu">
         	<h2 class="white">浏览下级分类</h2>
            <ul>
                <li><a href="#">言情（1890）</a></li>
                <li><a href="#">家庭/婚姻（3567）</a></li>
                <li><a href="#">女性（897）</a></li>
                <li><a href="#">职场（677）</a></li>
                <li><a href="#">财经（565）</a></li>
                <li><a href="#">近代（2390）</a></li>
                <li><a href="#">武侠（4560）</a></li>
                <li class="more"><a href="#">返回上级>></a></li>
            </ul>
         </div>  
         
         <div class="history">
         	<h2>最近的浏览记录</h2>
            <ul>
                <li><span>&middot;</span><a href="#">暮光之城-月食</a></li>
                <li><span>&middot;</span><a href="#">家小团圆（张爱玲...</a></li>
                <li><span>&middot;</span><a href="#">完全图解哇野外求...</a></li>
                <li><span>&middot;</span><a href="#">近代女性服饰演变...</a></li>
                <li><span>&middot;</span><a href="#">【6折】碧血剑</a></li>
            </ul>
         </div>     
    </div>
    
    <div class="main">
    	<div class="list_asc">
        	<!--choice order type-->
            <div class="type_choice f_left">
            	排序方式
                <select name="order" id="order">
                    <option value="UnitPrice" <%=(ViewBag.order=="UnitPrice")?"selected":""%>>按价格 排序</option>
                    <option value="PublishDate" <%=(ViewBag.order=="PublishDate")?"selected":""%>>按出版日期 排序</option>
                </select>
            </div>
            <!--page no-->
            <div class="turn_area f_right">
            	<span id="turn_pre" style="cursor:pointer" onclick="page(<%=ViewBag.pageIndex<=1?1:ViewBag.pageIndex-1%>)"><<</span>
                <span id="turn_page">第 <%=ViewBag.pageIndex %> 页</span>
                <span id="turn_next" style="cursor:pointer" onclick="page(<%=ViewBag.pageIndex>=ViewBag.pageCount?ViewBag.pageCount:ViewBag.pageIndex+1%>)">>></span>
            </div>
            
        </div>
        
       <%-- <dl class="list_area">
        	<dt><a href="#"><img src="images/books.jpg" width="100" height="100" onload="set_pic_size(this,100,100);" alt="暮光之城" /></a></dt>
            <dd>
            	<h2 class="b_title"><a href="#">CSS网站布局实录：基于Web标准的网站设计指南（第二版）</a></h2>
                <!--将书籍的id 写入span-->
                <div class="b_score">顾客评分：<span id="book_id_18">0</span></div>
                <div class="b_property">作者：李超 编著<br />出版社：科学出版社<br />出版时间：2007年09月</div>
                <h4 class="b_intro">★讲述基于Web标准的应用CSS进行网站布局设计与重构的典范之作！ 　　1、知识全面、完美应用 　　CSS选择器、样式继承、层叠、格式化、XML标签、CSS滤镜等。 文本、图像、超链接、列表、菜单、网站...</h4>
                <div class="b_buy">
                	<span class="gray del">￥35.00</span>　<span class="red">￥26.30</span>　折扣：75折　节省：￥8.70
                    <img src="Images/btn_goumai.gif" onmouseover="this.src='Images/btn_goumai_click.gif'" />
                    <img src="Images/btn_zancun.gif" onmouseover="this.src='Images/btn_zancun_click.gif'" />
                </div>
            </dd>
        </dl>--%>
        <% foreach (var item in Model) { %>
        <dl class="list_area">
        	<dt><a href="<%=Url.Content("~/Books/Detail/"+item.Id) %>" target="_blank"><img src=<%=Url.Content("~/images/books.jpg") %> width="100" height="100" onload="set_pic_size(this,100,100);" alt="<%=item.Title %>" /></a></dt>
            <dd>
            	<h2 class="b_title"><a href="<%=Url.Content("~/Books/Detail/"+item.Id) %>"><%=item.Title %></a></h2>
                <!--将书籍的id 写入span-->
                <div class="b_score">顾客评分：<span id="Span1">0</span></div>
                <div class="b_property">作者：<%=item.Author %> 编著<br />出版社：<%=item.Publisher.Name %><br />出版时间：<%=item.PublishDate %></div>
                <h4 class="b_intro">
                    <%=item.ContentDescription %>
                </h4>
                <div class="b_buy">
                	<span class="gray del">￥<%=item.UnitPrice %></span>　<span class="red">￥26.30</span>　折扣：75折　节省：￥8.70
                    <img src=<%=Url.Content("~/Images/btn_goumai.gif") %> onmouseover="this.src=<%=Url.Content("~/Images/btn_goumai_click.gif") %>" />
                    <img src=<%=Url.Content("~/Images/btn_zancun.gif") %> onmouseover="this.src=<%=Url.Content("~/Images/btn_zancun_click.gif") %>" />
                </div>
            </dd>
        </dl>
        <% } %>

        
        <%--<script type="text/javascript">
            var book_list = new Array("15", "16", "17", "18");    //此处将该页需要显示的 书籍id 写入该数组；

            for (var i = 0; i < book_list.length; i++) {
                var book_score_str = "";
                var book_score = parseInt($("book_id_" + book_list[i]).innerHTML);
                for (var m = 0; m < book_score; m++) {
                    book_score_str += "<img src=\"<%=Url.Content("~/images/star_red.gif") %>\" />";
                }
                if (book_score == 0) book_score_str = "暂无评价";
                $("book_id_" + book_list[i]).innerHTML = book_score_str;
            }
		</script>--%>
        
        <div class="pages">
        	<%-- <a href="#" title="前一页" class="first"><<</a>
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
            <a href="#" title="后一页" class="end">>></a> --%>


            
            <% 
                int pageCount = ViewBag.pageCount;
                int pageIndex = ViewBag.pageIndex;
                int start1=pageIndex<5?1:pageIndex-4;
                int end1 = 0;
                bool flag = false;
                if (start1 + 5 > pageCount)
                {
                    end1 = pageCount;
                    flag = false;
                }
                else
                {
                    end1 = start1 + 5;
                    flag = true;
                }
                if (pageIndex>1) {
                %>
            <a href="javascript:page(<%=pageIndex-1 %>)" title="前一页" class="first"><<</a>
            <%
                }
                for (int i=start1;i<=end1;i++ ) {
             %>
                <a href="javascript:page(<%=i %>)" class="<%=pageIndex==i?"current":"" %>"  title="第<%=i %>页"><%=i %></a>
            <% }
                if (flag) {
                int start2 = 0;
    
                if (pageCount-5<=end1+1)
                {
                    start2 = end1+1;
                }
                else
                {%>
                <a href="javascript:void(0)" class="more">...</a>
            <%
                    start2 = pageCount - 5;

                }
                    for (int j=start2;j<=pageCount;j++) {
            %>
                        <a href="javascript:page(<%=j %>)" class="<%=pageIndex==j?"current":"" %>"  title="第<%=j %>页"><%=j %></a>
            <%
                    }
                }
                if (pageCount>pageIndex)
                {
            %>
                    <a href="javascript:page(<%=pageIndex+1%>)" title="后一页" class="end">>></a>
            <%
                }
            %>

            
        </div>
        	
 	</div>
    
</div>


<div id="footer">
	<!--contac us-->
	<div class="telephone">
        <strong>热线</strong> 021-61508168　<strong>传真</strong> 021-61508168-8020　 <br />
        <strong>Q Q</strong>375013071  13483528    562655482  1143735195（技术)<br />
        <strong>MSN</strong> hjservice@hotmail.com   <strong>信箱</strong> shop@hjenglish.com<br />
        <strong>帮助</strong> <a href="/help/help.aspx" target="_blank">银行汇款帐户</a> <a href="/help/help.aspx#help_post" target="_blank">邮局汇款地址</a> 	<a href="/help/help.aspx#help_ship" target="_blank">送货方式及费用</a> <a href="http://www.hjenglish.com/down/faq_2.htm" target="_blank">如何进行下载</a>
    </div>
    <!---->
  <div class="imp_link">
    	<img src=<%=Url.Content("~/Images/alipay.gif") %> alt="支付宝支付" /><img src=<%=Url.Content("~/Images/online_pay.gif") %> alt="在线支付" /><br />
        <a href="http://www.hjenglish.com/about/aboutus.htm" target="_blank">网站介绍</a>　<a href="http://www.hjenglish.com/about/partner.htm" target="_blank">合作伙伴</a>　<a href="#" target="_blank">网站地图</a>　<a href="#" target="_blank">联系我们</a><br />
    <a href="#" target="_blank">增值电信业务经营许可证沪B2-20040503</a> </div>
</div>

<div id="child_site">
	<strong>分站</strong>　 <a href="#" target="_blank">沪江网</a>  <a href="#" target="_blank">听说</a>  <a href="#" target="_blank">口译</a>  <a href="#" target="_blank">CET</a>  <a href="#" target="_blank">考研</a>  <a href="#" target="_blank">雅思</a>  <a href="#" target="_blank">托福</a>  <a href="#" target="_blank">日语</a>  <a href="#" target="_blank">法语</a>  <a href="#" target="_blank">下载</a>  <a href="#" target="_blank">文库</a>  <a href="#" target="_blank">部落</a>  <a href="#" target="_blank">博客</a>  <a href="#" target="_blank">词典</a>  <a href="#" target="_blank">IT新闻</a>  <a href="#" target="_blank">博客园</a>  <a title="新世界日语" href="#" target="_blank">新世界日语</a>  <a title="2010考研书籍推荐专题" href="#" target="_blank">2010考研书籍</a>
</div>

    <script type="text/javascript">
                var ctx ="<%=Url.Content("~/books/list")%>";
                function page(pageIndex) {
    var order = document.getElementById("order").value;
    var url = ctx + "?order=" + order + "&pageIndex="+pageIndex + "&categoryId=" +<%=ViewBag.categoryId%>;
    location.href = url;
}
         </script>
</body>
</html>
    
    
    
