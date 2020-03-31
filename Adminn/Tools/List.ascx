<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Adminn_Tools_List" %>
<div class="panel panel-default">
    <div class="panel-heading">Sorağçalar</div>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="DListTableName" runat="server" OnSelectedIndexChanged="DListTableNames_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Width="250px">
            <asp:ListItem Value="Citizenship">Vətəndaşlıq</asp:ListItem>
            <asp:ListItem Value="PersonsIdmanNov">İdman növləri</asp:ListItem>
            <asp:ListItem Value="PersonsMuavinatNov">Müavinat növləri</asp:ListItem>
            <asp:ListItem Value="PersonsRelativesWorkPositions">İş mövqeyi</asp:ListItem>
            <asp:ListItem Value="PersonsTehsilFennNov">Fənn növləri</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaAzadEdilmeSebebNov">Cəzadan azad edilmə səbəbləri</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaCezaNov">Uşağa tədbiq edilmiş cəza növləri</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaQarsiToredenSexsCezaNov">Huquq pozmani törədən şəxsə tədbiq edilmiş cəza növləri</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaMecburiTedbirNov">Tərbiyəvi xarakterli məcburi tədbirlər</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaQarsiOrganNov">Huquq pozmanı qeydə alan orqan</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaQarsiToredenSexsNov">Uşağa münasibətdə hüquq pozuntusu törədən şəxs</asp:ListItem>
            <asp:ListItem Value="PersonsHuquqpozmaTenbehNov">Tətbiq edilmiş inzibati tənbeh növləri</asp:ListItem>
            <asp:ListItem Value="PersonsSosialXidmetNov">Sosial xidmət növləri</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
        <asp:Button ID="BtnInsert" runat="server" Text="Əlave et" OnClick="BtnInsert_Click" OnClientClick="this.style.display='none';" CssClass="btn btn-default" />
        <asp:GridView ID="GrdList" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="590px" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 25px" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="S/s">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>

                <asp:BoundField DataField="Name" HeaderText="Ad">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="520px" />
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
    </ContentTemplate>
</asp:UpdatePanel>
