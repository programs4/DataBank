<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Detail_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <div class="page-nav printStyle">
        <div style="float: left; width: 50%"><a href="/main">Məlumat bankı</a>  ●  <a href="/list">Uşaqların siyahısı</a>  ●  <a href="#new-win">Düzəliş</a></div>
        <div style="float: left; text-align: right; padding-right: 20px; width: 50%">
            <span class="glyphicon glyphicon-print" aria-hidden="true"></span>
            <a onclick="window.print();" href="#print">Siyahını çap et</a>
          <asp:Literal ID="LtrHistoryIcon" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<span class="glyphicon glyphicon-time" aria-hidden="true"></span></asp:Literal>
                <asp:LinkButton ID="LnkHistory" runat="server" Font-Bold="False" OnClick="LnkHistory_Click">Tarixçə</asp:LinkButton>&nbsp;&nbsp;
        </div>
    </div>

    <div class="src printStyle">
        <input type="text" placeholder="Siyahı üzrə axtar" />
        <img src="/Pics/ico12.png" />
    </div>
    <div class="inner-left-content">

        <div class="ac-tools printStyle">
            <span class="ac-show">
                <img src="/Pics/show.png" /></span>
            <span class="ac-hide" style="margin-left: 10px;">
                <img src="/Pics/hide.png" /></span>
        </div>

        <ul class="accordion">
            <asp:Repeater ID="RptItems" runat="server">
                <ItemTemplate>
                    <li>
                        <h4><span class="dot"></span><%#Eval("Name") %>
                            <span class="searchKeyword" style="display: none"><%#Eval("Name")._ToString().ToLower() %></span>
                            <img class="ac-arrow" src="/Pics/ico13.png" /></h4>
                        <div class="acc-cont" style="padding: 10px">
                            <asp:Panel ID="PnlContent" runat="server" OnLoad="PnlContent_Load" CssClass='<%#Eval("ID")._ToString()+"_"+Eval("Shortname")._ToString() %>'></asp:Panel>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Content>

