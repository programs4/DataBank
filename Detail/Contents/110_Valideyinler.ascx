<%@ Control Language="C#" AutoEventWireup="true" CodeFile="110_Valideyinler.ascx.cs" Inherits="Detail_Contents_110_Valideyinler" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:Panel ID="PnlRoot" runat="server" CssClass="ContentRoot">
            <asp:Panel ID="PnlAta" runat="server" GroupingText="Atasının vətəndaşlığı, doğum və ölüm tarixi" CssClass="PnlItems">
                Fərdi identifikasiya nömrəsi<br />
                <asp:TextBox ID="TxtPin1" runat="server" CssClass="form-control" Width="100%" MaxLength="7"></asp:TextBox>
                <br />
                <br />
                Adı
                 <br />
                <asp:TextBox ID="TxtAd1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Soyadı<br />
                <asp:TextBox ID="TxtSoyad1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının adı<br />
                <asp:TextBox ID="TxtAta1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Vətəndaşlıq vəziyyəti<br />
                <asp:DropDownList ID="DlistVetendasliq1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Doğum tarixi<br />
                <asp:DropDownList ID="DlistDogumD1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumM1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumY1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Ölüm tarixi<br />
                <asp:DropDownList ID="DlistOlumD1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistOlumM1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistOlumY1" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtAtaDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess1" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess1_Click" CommandArgument="10"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlAtaYasadigi" runat="server" CssClass="PnlItems" GroupingText="Atasının yaşadığı ünvan">
                Ölkə<br />
                <asp:DropDownList ID="DlistCountry1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DlistCountry1_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                Şəhər/rayon<br />
                <asp:DropDownList ID="DlistCity1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Ünvan<br />
                <asp:TextBox ID="txtUnvan1" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess2" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess2_Click" CommandArgument="10"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlAtaMesgulluq" runat="server" CssClass="PnlItems" GroupingText="Atasının məşğulluq vəziyyəti">
                İşsiz və ya məşğul olması<br />
                <asp:DropDownList ID="DlistIsh1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="False">İşsiz</asp:ListItem>
                    <asp:ListItem Value="True">İşləyir</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Fəaliyyət növü<br />
                <asp:DropDownList ID="DlistFealiyyet1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess3" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess3_Click" CommandArgument="10"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlAtaMiqrant" runat="server" CssClass="PnlItems" GroupingText="Atasının əmək miqrantı olub-olmaması">
                Əmək miqrantı<br />
                <asp:DropDownList ID="DlistMigrant1" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="True">Miqrantdır</asp:ListItem>
                    <asp:ListItem Value="False">Miqrant deyil</asp:ListItem>
                </asp:DropDownList>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess4" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess4_Click" CommandArgument="10"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlAna" runat="server" GroupingText="Anasının vətəndaşlığı, doğum və ölüm tarixi" CssClass="PnlItems">
                Fərdi identifikasiya nömrəsi<br />
                <asp:TextBox ID="TxtPin2" runat="server" CssClass="form-control" Width="100%" MaxLength="7"></asp:TextBox>
                <br />
                <br />
                Adı
                <br />
                <asp:TextBox ID="TxtAd2" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Soyadı<br />
                <asp:TextBox ID="TxtSoyad2" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının adı<br />
                <asp:TextBox ID="TxtAta2" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Vətəndaşlıq vəziyyəti<br />
                <asp:DropDownList ID="DlistVetendasliq2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Doğum tarixi<br />
                <asp:DropDownList ID="DlistDogumD2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumM2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumY2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Ölüm tarixi<br />
                <asp:DropDownList ID="DlistOlumD2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistOlumM2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistOlumY2" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtAnaDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess5" runat="server" OnClientClick="SubmitLoading(this);" CommandArgument="20" OnClick="LnkSuccess1_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>

            <asp:Panel ID="PnlAnaYasadigi" runat="server" GroupingText="Anasının yaşadığı ünvan" CssClass="PnlItems">
                Ölkə<br />
                <asp:DropDownList ID="DlistCountry2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DlistCountry2_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                Şəhər/rayon<br />
                <asp:DropDownList ID="DlistCity2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Ünvan<br />
                <asp:TextBox ID="txtUnvan2" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess6" runat="server" OnClientClick="SubmitLoading(this);" CommandArgument="20" OnClick="LnkSuccess2_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>

            </asp:Panel>
            <asp:Panel ID="PnlAnaMesgulluq" runat="server" GroupingText="Anasının məşğulluq vəziyyəti" CssClass="PnlItems">
                İşsiz və ya məşğul olması<br />
                <asp:DropDownList ID="DlistIsh2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="False">İşsiz</asp:ListItem>
                    <asp:ListItem Value="True">İşləyir</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Fəaliyyət növü<br />
                <asp:DropDownList ID="DlistFealiyyet2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess7" runat="server" OnClientClick="SubmitLoading(this);" CommandArgument="20" OnClick="LnkSuccess3_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>

            </asp:Panel>
            <asp:Panel ID="PnlAnaMiqrant" runat="server" GroupingText="Anasının əmək miqrantı olub-olmaması" CssClass="PnlItems">
                Əmək miqrantı<br />
                <asp:DropDownList ID="DlistMigrant2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="True">Miqrantdır</asp:ListItem>
                    <asp:ListItem Value="False">Miqrant deyil</asp:ListItem>
                </asp:DropDownList>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess8" runat="server" OnClientClick="SubmitLoading(this);" CommandArgument="20" OnClick="LnkSuccess4_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlNigah" runat="server" GroupingText="Valideynlərinin nikah vəziyyəti" CssClass="PnlItems">
                Ailə vəziyyəti<br />
                <asp:DropDownList ID="DListMarriage" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control" Width="250px">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="True">Evi</asp:ListItem>
                    <asp:ListItem Value="False">Subay</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Nikahın qeydə alındığı tarix<br />
                <asp:DropDownList ID="DlistNigahD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistNigahM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistNigahY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Nigah pozulmuşdursa, pozulma tarixi<br />
                <asp:DropDownList ID="DlistNigahPozD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistNigahPozM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistNigahPozY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtDescriptionNigah" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess9" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess9_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>

        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
