<%@ Control Language="C#" AutoEventWireup="true" CodeFile="140_SehiyyeQeydiyyat.ascx.cs" Inherits="Detail_Contents_140_SehiyyeQeydiyyat" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlAdd" runat="server" Style="padding: 15px; padding-left: 0px;" HorizontalAlign="Left" Width="100%">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PnlEdit" Visible="false" runat="server" CssClass="ContentRoot">
            <div class="PnlItems">
                Müəssisənin adı
            <asp:TextBox ID="TxtMuessiseAdi" CssClass="form-control .col-md-6" Width="100%" runat="server"></asp:TextBox>
                <br />
                <br />
                Sahə həkiminin adı<br />
                <asp:TextBox ID="TxtSaheHekimininAdi" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
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
                <asp:TextBox ID="TxtUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LnkCancel" runat="server" OnClick="LnkCancel_Click" OnClientClick="SubmitLoading(this);"><img border="0" src="/pics/CancelButton.png" /></asp:LinkButton>
                </div>
            </div>
            <br />
            <br />
        </asp:Panel>

        <asp:GridView ID="GrdList" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="GrdList_RowDeleting" OnSelectedIndexChanged="GrdList_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Ss" HeaderText="№">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="Adi" HeaderText="Müəssisənin adı" />
                <asp:BoundField DataField="SaheHekimininAdi" HeaderText="Telefon">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Unvan" HeaderText="Müəssisənin ünvanı" HtmlEncode="false" />
                <asp:BoundField DataField="Description" HeaderText="Qeyd" />
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
