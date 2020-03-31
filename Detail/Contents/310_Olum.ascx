<%@ Control Language="C#" AutoEventWireup="true" CodeFile="310_Olum.ascx.cs" Inherits="Detail_Contents_310_Olum" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlEdit" runat="server" CssClass="ContentRoot">
            <div class="PnlItems">
                Ölümü<br />
                <asp:DropDownList ID="DListOlum" runat="server" CssClass="form-control" Width="250px">
                    <asp:ListItem Value="False">Qeydə alınmayıb</asp:ListItem>
                    <asp:ListItem Value="True">Qeydə alınıb</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Ölüm qeydiyyatı<br />
                <asp:TextBox ID="TxtOlumQeydiyyat" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Ölüm tarixi<br />
                <asp:DropDownList ID="DListOlumD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListOlumM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListOlumY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                </div>
            </div>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
