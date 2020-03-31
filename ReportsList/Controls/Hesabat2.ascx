﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Hesabat2.ascx.cs" Inherits="ReportsList_Controls_Hesabat2" %>
<div class="panel panel-default">
    <div class="panel-heading">Bölgələr üzrə təhsil nailliyyəti</div>
</div>
<asp:GridView ID="GrdList" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Şəhər/rayon" />
        <asp:BoundField DataField="Column1" HeaderText="Riyaziyyat">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Column2" HeaderText="Fizika">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Column3" HeaderText="Kimya">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Column4" HeaderText="Coğrafiya">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Column5" HeaderText="Tarix">
            <HeaderStyle HorizontalAlign="Center" Width="140px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#7C6F57" />
    <EmptyDataTemplate>
        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
            Məlumat tapılmadı.
        </div>
    </EmptyDataTemplate>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <HeaderStyle Font-Bold="True" BackColor="#F6F6F6" ForeColor="Gray" Height="40px" HorizontalAlign="Left" />
    <PagerSettings PageButtonCount="20" />
    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
    <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Left" Font-Bold="False" />
    <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
