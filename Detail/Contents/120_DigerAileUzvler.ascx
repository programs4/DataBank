<%@ Control Language="C#" AutoEventWireup="true" CodeFile="120_DigerAileUzvler.ascx.cs" Inherits="Detail_Contents_120_DigerAileUzvler" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlAdd" runat="server" Style="padding: 15px; padding-left: 0px;" HorizontalAlign="Left" Width="100%">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PnlRoot" runat="server" Visible="false" CssClass="ContentRoot">
            <asp:Panel ID="PnlSAA" runat="server" GroupingText="Adı, soyadı və atasının adı" CssClass="PnlItems">
                Qohumluq dərəcəsi<br />
                <asp:DropDownList ID="DListRelativTypes" runat="server" CssClass="form-control" Width="250px">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="30">Qardaşı</asp:ListItem>
                    <asp:ListItem Value="40">Bacısı</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Fərdi identifikasiya nömrəsi<br />
                <asp:TextBox ID="TxtPin" runat="server" CssClass="form-control" placeholder="Hərf və rəqəmlərdən ibarət kod" Width="100%" MaxLength="7"></asp:TextBox>
                <br />
                <br />

                Adı
                 <br />
                <asp:TextBox ID="TxtAd" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Soyadı<br />
                <asp:TextBox ID="TxtSoyad" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının adı<br />
                <asp:TextBox ID="TxtAta" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <br />
                <br />

                Ailə vəziyyəti<br />
                <asp:DropDownList ID="DListMarriage" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control" Width="250px"></asp:DropDownList>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess1" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess1_Click" CommandArgument="1"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LnkCancel1" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkCancel1_Click"><img border="0" src="/pics/CancelButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>

            <asp:Panel ID="PnlYasadigiUnvan" runat="server" CssClass="PnlItems" GroupingText="Yaşadığı ünvan">
                Ölkə<br />
                <asp:DropDownList ID="DlistCountry" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DlistCountry_SelectedIndexChanged">
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
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess2" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess1_Click" CommandArgument="2"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LnkCancel2" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkCancel1_Click"><img border="0" src="/pics/CancelButton.png" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <br />
        </asp:Panel>

        <asp:GridView ID="GrdList" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="GrdList_RowDeleting" OnSelectedIndexChanged="GrdList_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Ss" HeaderText="№">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="TypeName" />
                <asp:BoundField DataField="FIN" HeaderText="FİN">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Ad" HeaderText="Adi">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Soyad" HeaderText="Soyadı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Ata" HeaderText="Ata adı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Marriage" HeaderText="Ailə vəziyyəti">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Qeydiyyatda olduğu ünvan">
                    <ItemTemplate>
                        <%# Eval("CountriesName") %>, <%# Eval("RegionsName") %><br />
                        <span class="addressSub"><%# Eval("YasadigiUnvan") %></span>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:BoundField DataField="Description" HeaderText="Qeyd">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:CommandField SelectText="&lt;span class=&quot;glyphicon glyphicon-pencil&quot; aria-hidden=&quot;true&quot;&gt;&lt;/span&gt; Ətraflı" ShowSelectButton="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField DeleteText="&lt;span class=&quot;glyphicon glyphicon-trash&quot; aria-hidden=&quot;true&quot;&gt;&lt;/span&gt; Sil" ShowDeleteButton="True">
                    <ControlStyle ForeColor="#CC0000" />
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>

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

    </ContentTemplate>
</asp:UpdatePanel>
