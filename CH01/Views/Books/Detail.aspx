<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Common.Master" 
    Inherits="System.Web.Mvc.ViewPage<BookShop.Models.Book>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Title %> - 图书详情
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="book_view">
    	<h1 class="b_title"><%=Model.Title %></h1>
        <div class="b_exa">
        	<span class="book_group">丛书名：<%=Model.Title %></span>
            <span class="book_status">正在阅读（150 人），已经阅读（<%=Model.Clicks %> 人） <span><a href="#">放入书架</a></span> <a href="#">什么是书架？</a></span>
        </div>
        
        <!--book basic start-->
        <dl class="put_book">
        	<dt>
            	<img src=<%=Url.Content("~/Images/books.jpg") %> alt="<%=Model.Title %>">
            	<div class="chakan"><img src=<%=Url.Content("~/images/zoom.gif") %>> <a class="gray878787a" href="#" name="bigpicture">点击查看大图</a></div>
            </dt>
            <dd>
            	<div id="book_editor">作　　者： <%=Model.Author %> 著<br>出 版 社： <%=Model.Publisher.Name %></div>
                <ul id="book_attribute">
                    <li>出版时间： <%=Model.PublishDate %></li>
                    <li>字　　数： </li>
                    <li>版　　次： 1</li>

                    <li>页　　数： 540</li>
                    <li>印刷时间： 2009-5-1</li>
                    <li>开　　本： 16开</li>
                    <li>印　　次： 1</li>
                    <li>纸　　张： 胶版纸</li>
                    <li>I S B N    ： <%=Model.ISBN %></li>

                    <li>包　　装： 平装</li>
                </ul>
                
                <div id="book_categroy">所属分类： 
                    <a href="#" target="_blank" class="blue12a"><%=Model.Category.Name %></a> &gt;&gt; 
                    <a href="#" target="_blank" class="blue12a">保健/心理健康</a> &gt;&gt; 
                    <a href="#" target="_blank" class="blue12a">健康百科</a></div>
                
                <div id="book_price">
                    <span class="gray87">定价：<span class="del">￥<%=Model.UnitPrice %></span></span>
                    <span class="red">当当价：￥<b>35.40</b></span>   折扣：<span class="redc30">59折</span>   节省：￥24.60
                </div> 
                
                <div id="book_point">
                	<span>送积分：<span id="pointsTag">354</span></span>　<a target="_blank" href="#2">积分说明</a> <br>
                	<a href="#"><img src=<%= Url.Content("~/Images/btn_goumai.gif")%> 
                        onmouseover="this.src='<%= Url.Content("~/Images/btn_goumai_click.gif")%> '" 
                        onmouseout="this.src='<%= Url.Content("~/Images/btn_goumai.gif")%> '"></a>
                    <a href="#"><img src=<%= Url.Content("~/Images/btn_zancun.gif") %> 
                        onmouseover="this.src='<%= Url.Content("~/Images/btn_zancun_click.gif")%> '" 
                        onmouseout="this.src='<%= Url.Content("~/Images/btn_zancun.gif")%> '"></a>
                </div> 
                
                <div id="book_count">
                	顾客评分：<span id="book_id_15">
                        <img src=<%=Url.Content("~/images/star_red.gif") %>>
                        <img src=<%=Url.Content("~/images/star_red.gif") %>>
                        <img src=<%=Url.Content("~/images/star_red.gif") %>>
                        <img src=<%=Url.Content("~/images/star_red.gif") %>>
                        <img src=<%=Url.Content("~/images/star_red.gif") %>></span>
                    共有商品评论0条  <a href="#">查看评论摘要</a>
                </div>
				<script type="text/javascript">
				    var book_score = parseInt($("book_id_15").innerHTML);
				    var book_score_str = "";

				    for (var m = 0; m < book_score; m++) {
				        book_score_str += "<img src='~/images/star_red.gif' />";
				    }
				    $("book_id_15").innerHTML = book_score_str;
                </script>

            </dd>
        </dl>
        <!--book basic end-->
        <!--book intro start-->
        <dl class="book_intro">
        	<dt>编辑推荐</dt>
            <dd>中国健康类图书第一品牌“国医健康绝学系列”2009年重磅新品
《求医不如求己家庭医学全书》是一本保佑全家老小平平安安的健康红宝书。里面汇集了将近200种家庭常见疾病的自助调治方案，它们特别简单、特别安全、特别适合家庭使用。严格按照书中的方法去做，每个人身体的绝大多数问题都能得到解决。
　　中里巴人先生健康养生绝学使用说明书，一看就懂，最安全、最有效、最省钱。
本书几大特色：</dd>
        </dl>
        
        <dl class="book_intro">
        	<dt>内容简介</dt>
            <dd>
                <%=Model.ContentDescription %>
            </dd>
        </dl>
        
        <dl class="book_intro">
        	<dt>目录</dt>
            <dd>
                <%=Model.TOC %>
            </dd>
        </dl>
        
        <dl class="book_intro">
        	<dt>书籍插图</dt>
            <dd>中国健康类图书第一品牌“国医健康绝学系列”2009年重磅新品
《求医不如求己家庭医学全书》是一本保佑全家老小平平安安的健康红宝书。里面汇集了将近200种家庭常见疾病的自助调治方案，它们特别简单、特别安全、特别适合家庭使用。严格按照书中的方法去做，每个人身体的绝大多数问题都能得到解决。
　　中里巴人先生健康养生绝学使用说明书，一看就懂，最安全、最有效、最省钱。
本书几大特色：</dd>
        </dl>
        <!--book intro end-->
        
        <!--recommed start-->
        <div class="comm_answer">
			<!--review head start-->
            <div id="div_product_reviews">
                <div class="total_comm">
                    <div class="comm_title">
                        <h2>商品评论 共<em>814</em>条
                        <span class="look_comm"> (<a href="#" name="reviewList" target="_blank">查看所有评论</a>)</span></h2>
                    </div>
        
                    <div class="total_body">
                        <div class="people_average" >
                            <div class="average_left" style="width:160px;"><p>购买过的顾客平均评分</p><span class="a_red28b pd">4</span><span class="red_bold">星半</span>
                                <img src=<%=Url.Content("~/images/star_red.gif") %>>
                                <img src=<%=Url.Content("~/images/star_red.gif") %>>
                                <img src=<%=Url.Content("~/images/star_red.gif") %>>
                                <img src=<%=Url.Content("~/images/star_red.gif") %>>
                                <img src=<%=Url.Content("~/images/star_redgray_big.gif") %>>
                            </div>
                            <span class="span_jt" id="div_window_star"><input class="button_down1" value="" type="button"></span>
                        </div>
                        
                        <div id="div_product_summary">
        
                            <div class="people_heart">心情指数:<em>249</em>人 开心
                                <span id="div_emotion_hover">
                                    <input class="button_down1" type="button">
                                </span>
                            </div>
                            <div id="Div1" class="people_read">阅读场所:<em>180</em>人 床上
                                <span id="div_location_hover">
                                    <input class="button_down1" type="button">
                                </span>
                            </div>
                        </div>
                        
                        <div class="write_comm">
                            <a id="reviewTipa" href="#"><img src=<%=Url.Content("~/images/button_write_comm.gif") %> border="0" title="写评论"></a>
                        </div>
        
        
                    </div>
                </div>
            </div>
            <!--review head end-->
            
            <!--the one reviews start-->
            <div class="comm_list">
                <h3>
                    <img src=<%=Url.Content("~/images/label_1.gif") %> title="精彩评论"><a href="#" target="_blank" name="reviewDetail">非常不错的一本书</a>
                    <span>发表于 2009-04-29 22:46</span>
                </h3>
               
                哦耶，终于拿到书了。&nbsp;<br>实物比照片显示的要漂亮，颜色是看上去很舒服的红色。&nbsp;<br>书很厚，是塑封的，里面有两张挂图，一张标准穴位图，一张足部反射区图，还有一张配套的光盘，用DVD机试了一下，是中里老师讲的。嗯，
            </div>
            <!--the one reviews end-->
            
   		</div>
        <!--recommed end-->
    
    
    </div>

</asp:Content>
