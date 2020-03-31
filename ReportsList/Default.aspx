<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ReportsList_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="reports">
                <ul>
                    <li><a href="/reportslist/?p=hesabat1">Bölgələr üzrə uşaqların sayı</a></li>
                    <li><a href="/reportslist/?p=hesabat2">Bölgələr üzrə təhsil nailliyyəti</a></li>
                    <li><a href="/reportslist/?p=hesabat3">Bölgələr üzrə idman nailliyyəti olanlar</a></li>
                </ul>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="reportPrint">
                <span class="glyphicon glyphicon-print" aria-hidden="true"></span>
                <a onclick="window.print();" href="#print">Siyahını çap et</a>
            </div>
            <asp:Panel ID="PanelControl" runat="server">
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>

