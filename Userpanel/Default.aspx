<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Userpanel_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <ul id="myTab" class="nav nav-pills" role="tablist" style="background-color: #F9F9F9; padding: 5px; border: 1px solid #E4E4E4; height: 53px;">
                <li role="presentation" class="active">
                    <a href="#rec" id="rec-tab" role="tab" data-toggle="tab" aria-controls="rec" aria-expanded="false"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span>&nbsp;<b>Şəxsi məlumatlar</b></a></li>

                <li role="presentation" class="">
                    <a href="#send" role="tab" id="send-tab" data-toggle="tab" aria-controls="send" aria-expanded="true"><span class="glyphicon glyphicon-lock" aria-hidden="true"></span>&nbsp;<b>Şifrəni dəyiş</b></a>
                </li>
            </ul>

            <div id="myTabContent" class="tab-content" style="padding: 10px 10px 10px 3px; margin-top: 5px">
                <div role="tabpanel" class="tab-pane fade active in" id="rec" aria-labelledby="rec-tab">
                    <div class="userPanelFont">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                Qurumun adı:<br />
                                <asp:TextBox ID="TxtOrganizations" runat="server" CssClass="form-control" Width="450px" Height="35px" Enabled="false"></asp:TextBox>
                                <br />
                                <br />
                                Şəxsiyyət vəsiqəsinin nömrəsi:<br />
                                <asp:TextBox ID="TxtPassportNumber" CssClass="form-control" Enabled="false" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                <br />
                                Şəxsiyyət vəsiqəsinin Fin kodu:<br />
                                <asp:TextBox ID="TxtFin" CssClass="form-control" Enabled="false" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                <br />
                                Soyadı, adı və atasının adı:<br />
                                <asp:TextBox ID="TxtFullname" CssClass="form-control" Enabled="false" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                <br />
                                Elektron poçt ünvanı:<br />
                                <asp:TextBox ID="TxtEmail" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                <br />
                                Telefon nömrəsi:<br />
                                <asp:TextBox ID="TxtContacts" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                <br />
                                <asp:Button ID="BtnEdit" runat="server" CssClass="btn btn-default" Font-Bold="True" OnClick="BtnEdit_Click" Text="Yadda saxla" Width="150px" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane fade" id="send" aria-labelledby="send-tab">
                    <div class="userPanelFont">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                Cari Şifrə<br />
                                <asp:TextBox ID="TxtPassword" runat="server" CssClass="txtboxfont form-control tbPassword" Width="450px" Height="35px"></asp:TextBox>
                                <br />
                                <br />
                                Yeni şifrə<br />
                                <asp:TextBox ID="TxtnewPass" runat="server" CssClass="txtboxfont form-control txtnewPass " Width="450px" Height="35px" TextMode="Password"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label2" runat="server" Font-Size="8pt" Text="- minimum 4, maksimum 20 simvol"></asp:Label>
                                <br />
                                <br />
                                Şifrə təkrar<br />
                                <asp:TextBox ID="TxtRepeatPass" runat="server" CssClass="txtboxfont form-control txtRepeatPass" Width="450px" Height="35px" TextMode="Password"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="BtnChangePass" runat="server" CssClass="btn btn-default" Font-Bold="True" OnClick="BtnChangePass_Click" Text="Şifrəni yenilə" Width="150px" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

