<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsersHistory.ascx.cs" Inherits="Adminn_Tools_UsersHistory" %>
<div class="panel panel-default">
    <div class="panel-heading">Tarixçə</div>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <ul class="FilterSearch">
                <li>Qurum:
                            <br />
                    <asp:DropDownList ID="DListOrganization" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListOrganization_SelectedIndexChanged"></asp:DropDownList></li>
                <li>İstifadəçi adı:
                            <br />
                    <asp:DropDownList ID="DListUsersName" CssClass="form-control" runat="server" DataTextField="Fullname" DataValueField="ID" Width="250px"></asp:DropDownList></li>
                <li>Əməliyyatın növü:
                            <br />
                    <asp:DropDownList ID="DListPermissionsType" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>

                <li>Tarixçə növü:
                            <br />
                    <asp:DropDownList ID="DListHistoryTypes" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>

                <li class="NoStyle">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-default" Width="100px" Height="35px" Text="AXTAR" Font-Bold="False" OnClick="BtnSearch_Click" OnClientClick="this.style.display='none';" />
                </li>
            </ul>
        <div style="float: right; padding: 5px;">
            <asp:Label ID="LblCount" runat="server" Font-Bold="False" Text="--"></asp:Label>
        </div>
        <asp:GridView ID="GrdHistory" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px">
            <Columns>
                <asp:BoundField DataField="Ss" HeaderText="S/s">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>

                <asp:BoundField DataField="UserName" HeaderText="İstifadəçi adı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>

                <asp:BoundField DataField="Fullname" HeaderText="Uşağın S.A.A">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>

                <asp:BoundField DataField="LogText" HeaderText="Aparılan əməliyyat">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="Date" HeaderText="Tarix">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>

                <asp:BoundField DataField="IP" HeaderText="IP">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                    Məlumat tapılmadı.
                </div>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" BackColor="#364150" ForeColor="White" Height="40px" />
            <PagerSettings PageButtonCount="20" />
            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
            <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <div align="left" style="margin-bottom: 50px; margin-top: 20px; padding-left: 0px; clear: left; float: left;" id="MoreButton">
            &nbsp;<img src="/Pics/LoadingLittle.gif" style="display: none;" id="Load" />
            <asp:LinkButton ID="LnkOtherApp" runat="server" Font-Size="12pt" CommandArgument="0" OnClientClick="this.style.display='none'; document.getElementById('Load').style.display='';" Font-Bold="true" Font-Strikeout="False" Font-Underline="False" OnClick="LnkOtherApp_Click"><img class="alignMiddle" src="/pics/more.png" /> davamı</asp:LinkButton>
            &nbsp;<br />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

