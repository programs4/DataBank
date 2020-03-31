<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Adminn_Tools_Main" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>


        <div class="panel panel-default">
            <div class="panel-heading">Tənzimləmələr</div>
        </div>
        <br />
        <ul id="myTab" class="nav nav-pills" role="tablist" style="background-color: #F9F9F9; padding: 5px; border: 1px solid #E4E4E4;">
            <li role="presentation" class="active"><a href="#rec" id="rec-tab" role="tab" data-toggle="tab" aria-controls="rec" aria-expanded="false">
                <span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;Fərdi qeydlər</a></li>

            <li role="presentation" class=""><a href="#password" role="tab" id="password-tab" data-toggle="tab" aria-controls="password" aria-expanded="true">
                <span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Şifrəni dəyiş</a></li>

        </ul>

        <div id="myTabContent" class="tab-content" style="padding: 10px 0px 10px 3px; margin-top: 5px">

            <div role="tabpanel" class="tab-pane fade active in" id="rec" aria-labelledby="rec-tab">
                <asp:Panel ID="Pnl1" runat="server" BackColor="#EFEFEF" CssClass="textBox" DefaultButton="BtnSaveNote">
                    <div class="panel panel-default">
                        <div class="panel-heading">Fərdi qeydlər</div>
                        <div class="panel-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <br />
                                    <asp:TextBox ID="TxtDescription" runat="server" BackColor="#FFFFE1" CssClass="form-control" Height="150px" TextMode="MultiLine" Width="70%"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Button ID="BtnSaveNote" runat="server" CssClass="btn btn-default" Height="35px" OnClick="BtnSaveNote_Click" Text="YADDA SAXLA" Width="160px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div role="tabpanel" class="tab-pane fade" id="password" aria-labelledby="password-tab">
                <asp:Panel ID="Panel1" runat="server" BackColor="#EFEFEF" CssClass="textBox" DefaultButton="BtnConfirm">
                    <div style="padding: 10px">
                        <div class="panel panel-default">
                            <div class="panel-heading">Şifrəni dəyiş</div>
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        Köhnə şifrə:<br />
                                        <asp:TextBox ID="TxtOldPassword" runat="server" CssClass="form-control" Height="35px" Width="350px"></asp:TextBox>
                                        <br />
                                        <br />
                                        Yeni şifrə:<br />
                                        <asp:TextBox ID="TxtNewPassword" runat="server" CssClass="form-control" Height="35px" TextMode="Password" Width="350px"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Font-Size="8pt" Text="- minimum 4, maksimum 20 simvol"></asp:Label>
                                        <br />
                                        <br />
                                        Təkrar yeni şifrə:<br />
                                        <asp:TextBox ID="TxtBackPassword" runat="server" CssClass="form-control" Height="35px" TextMode="Password" Width="350px"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Button ID="BtnConfirm" runat="server" CssClass="btn btn-default" Height="35px" OnClick="BtnConfirm_Click" Text="TƏSDİQ ET" Width="160px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
            </div>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>
