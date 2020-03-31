<%@ Control Language="C#" AutoEventWireup="true" CodeFile="270_Huquqpozma.ascx.cs" Inherits="Detail_Contents_270_Huquqpozma" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlAdd" runat="server" Style="padding: 15px; padding-left: 0px;" HorizontalAlign="Left" Width="100%">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PnlEdit" Visible="false" runat="server" CssClass="ContentRoot">
            <div class="PnlItems">
                Cinayət əməli və ya inzibati xəta<br />
                <asp:DropDownList ID="DListMecelle" runat="server" CssClass="form-control .col-md-6" DataValueField="ID" DataTextField="Name"  Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                İşə baxmış məhkəmənin adı<br />
                <asp:TextBox ID="TxtMehkemeAd" CssClass="form-control .col-md-6" Width="100%" runat="server"></asp:TextBox>
                <br />
                <br />
                Maddə<br />
                <asp:TextBox ID="TxtMadde" CssClass="form-control .col-md-6" Width="100%" runat="server"></asp:TextBox>
                <br />
                <br />
                Məhkəmə qərarı<br />
                <asp:TextBox ID="TxtMehkemeQerari" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Məhkəmə qərarının sayı<br />
                <asp:TextBox ID="TxtMehkemeQerarSayi" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Məhkəmə qərarının tarixi<br />
                <asp:DropDownList ID="DListQerarD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListQerarM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListQerarY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Tədbiq edilmiş cəza növü<br />
                <asp:DropDownList ID="DListCezaNov" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataValueField="ID" DataTextField="Name" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Cəza çəkdiyi müəssisənin adı<br />
                <asp:TextBox ID="TxtCezaMuessise" CssClass="form-control .col-md-6" Width="100%" runat="server"></asp:TextBox>
                <br />
                <br />
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
                <asp:TextBox ID="TxtUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Cəza çəkdiyi müəssisədə keçirilmiş monitorinq tarixi<br />
                <asp:DropDownList ID="DListMonitoringD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListMonitoringM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListMonitoringY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Monitorinq nəticəsi<br />
                <asp:TextBox ID="TxtMonitoringNeticesi" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Tərbiyəvi xarakterli məcburi tədbirlər<br />
                <asp:DropDownList ID="DListTerbiyeviTedbir" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataValueField="ID" DataTextField="Name"  Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Tətbiq edilmiş inzibati tənbeh növü<br />
                <asp:DropDownList ID="DListTenbehNov" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataValueField="ID" DataTextField="Name"  Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Cəzadan azad edilibsə səbəbi<br />
                <asp:DropDownList ID="DListCezaAzad" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataValueField="ID" DataTextField="Name"  Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Cəzanın bitmə tarixi<br />
                <asp:DropDownList ID="DListCezaBitmeD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListCezaBitmeM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListCezaBitmeY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
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
                <asp:BoundField DataField="Madde" HeaderText="Maddə" />
                <asp:BoundField DataField="MehkemeQerari" HeaderText="Məhkəmə qərarı" />
                <asp:BoundField DataField="Unvan" HeaderText="Müəssisənin ünvanı" HtmlEncode="False" />
                <asp:BoundField DataField="CezaninBitdiyiTarix" HeaderText="Cəzanın bitdiyi tarix"></asp:BoundField>
                <asp:BoundField DataField="MonitorinqTarixi" HeaderText="Monitorinq tarixi">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MonitorinqNeticesi" HeaderText="Monitorinqin nəticəsi" />
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
