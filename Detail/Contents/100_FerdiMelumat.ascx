<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100_FerdiMelumat.ascx.cs" Inherits="Detail_Contents_100_FerdiMelumat" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:Panel ID="PnlRoot" runat="server" CssClass="ContentRoot">
            <asp:Panel ID="PnlStatus" runat="server" GroupingText="Status" CssClass="PnlItems">
                Statusu
                 <br />
                <asp:DropDownList ID="DlistStatus" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="220px">
                </asp:DropDownList>&nbsp; &nbsp;
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccessStatus" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccessStatus_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlSehadet" runat="server" GroupingText="Doğum şəhadətnaməsi" CssClass="PnlItems">
                Sənədin seriyası və nömrəsi
                 <br />
                <asp:DropDownList ID="DlistPassportTypeDs" runat="server" CssClass="form-control" DataTextField="Series" DataValueField="ID" Width="220px">
                </asp:DropDownList>&nbsp; &nbsp;
                <asp:TextBox ID="TxtDocNumberDs" runat="server" CssClass="form-control" placeholder="Yalnız rəqəm" Width="200px" MaxLength="8"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess1" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess1_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlPassport" runat="server" GroupingText="Digər sənəd" CssClass="PnlItems">
                Sənədin nömrəsi<br />
                <asp:DropDownList ID="DlistPassportTypesOther" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="220px">
                </asp:DropDownList>
                &nbsp; &nbsp;
                    <asp:TextBox ID="TxtDocNumberOther" runat="server" CssClass="form-control" Width="200px" MaxLength="8"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess2" runat="server" OnClick="LnkSuccess2_Click" OnClientClick="SubmitLoading(this);"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlInfo" runat="server" GroupingText="Fərdi məlumatlar" CssClass="PnlItems">
                Fərdi identifikasiya nömrəsi<br />
                <asp:TextBox ID="txtPin" runat="server" CssClass="form-control" placeholder="Hərf və rəqəmlərdən ibarət kod" Width="100%" MaxLength="7"></asp:TextBox>
                <br />
                <br />
                Uşağın adı<br />
                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Uşağın soyadı<br />
                <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Uşağın atasının adı<br />
                <asp:TextBox ID="txtAta" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Vətəndaşlıq vəziyyəti<br />
                <asp:DropDownList ID="DlistVetendasliq" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Cinsi<br />
                <asp:DropDownList ID="DlistGender" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                    <asp:ListItem Value="0" Text="--"></asp:ListItem>
                    <asp:ListItem Value="True" Text="Oğlan"></asp:ListItem>
                    <asp:ListItem Value="False" Text="Qız"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Doğum tarixi<br />
                <asp:DropDownList ID="DlistDogumD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DlistDogumY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>

                <br />
                <asp:Panel ID="PnlDogUnvan" runat="server" CssClass="PnlItemsSub" GroupingText="Uşağın doğumunun qeydiyyata alındığı yer">
                    Ölkə<br />
                    <asp:DropDownList ID="DlistCountry" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DlistCountry_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Şəhər/rayon<br />
                    <asp:DropDownList ID="DlistCity" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Ünvan<br />
                    <asp:TextBox ID="txtUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    <br />
                    <br />
                </asp:Panel>
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess3" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess3_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="PnlYasadUnvan" runat="server" GroupingText="Yaşadığı ünvan" CssClass="PnlItems">
                Ölkə<br />
                <asp:DropDownList ID="DlistCountry2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Enabled="False" Width="100%">
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
                <br />
                <asp:CheckBox ID="ChkUnvan" CssClass="Chekbx" runat="server" AutoPostBack="True" OnCheckedChanged="ChkUnvan_CheckedChanged" />Hal hazırda yaşadığı ünvan fərqlidir
                <asp:Panel ID="PnlMuveqqetiYasadUnvan" Visible="false" runat="server">
                    <br />
                    <br />
                    Ölkə<br />
                    <asp:DropDownList ID="DlistCountry3" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DlistCountry3_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Şəhər/rayon<br />
                    <asp:DropDownList ID="DlistCity3" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Ünvan<br />
                    <asp:TextBox ID="txtUnvan3" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </asp:Panel>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess4" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess4_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
