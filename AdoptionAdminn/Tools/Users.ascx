<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Users.ascx.cs" Inherits="AdoptionAdminn_Tools_Users" %>

<div class="panel panel-default">
    <div class="panel-heading">İstifadəçilər</div>
</div>
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="margin-top: 40px" class="modal fade" id="myModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="modal-header" style="background-color: #363B3F">
                                <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="color: white"><b><span id="popTitle">
                                    <asp:Literal ID="LtrModalHeader1" runat="server"></asp:Literal></span></b></h4>
                            </div>

                            <div class="modal-body" style="background-color: #F5F5F5" id="ModalContentID">

                                <asp:Panel Style="padding: 10px" ID="PnlAdministrators" runat="server" ScrollBars="Vertical">
                                    İstifadəçi adı:<br />
                                    <asp:TextBox ID="TxtUsername" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    <asp:Panel ID="PnlPassword" runat="server">
                                        İstifadəçi şifrəsi:<br />
                                        <asp:TextBox ID="TxtPass" CssClass="form-control" Width="450px" Height="35px" runat="server" TextMode="Password"></asp:TextBox><br />
                                        <br />
                                    </asp:Panel>
                                    Şöbə:<br />
                                    <asp:TextBox ID="TxtDepartment" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Vəzifə:<br />
                                    <asp:TextBox ID="TxtPosition" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Soyadı, adı və atasının adı:<br />
                                    <asp:TextBox ID="TxtFullname" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Elektron poçt ünvanı:<br />
                                    <asp:TextBox ID="TxtEmail" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Telefon nömrəsi:<br />
                                    <asp:TextBox ID="TxtContacts" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Hüquqlar:<br />
                                    <asp:DropDownList ID="DListPermissions" CssClass="form-control" Width="450px" Height="35px" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList><br />
                                    <br />
                                    İcazə verilən İP-lər:<br />
                                    <asp:TextBox ID="TxtPermissionIP" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                                    <br />
                                    Qeyd:<br />
                                    <asp:TextBox ID="TxtDescription" CssClass="form-control" Width="450px" Height="45px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <br />
                                    <div style="top: 20px; position: relative; left: -3px;">
                                        <asp:CheckBox ID="ChkIsActive" CssClass="Chekbx" runat="server" Checked="true" Enabled="false"></asp:CheckBox>
                                        Aktiv
                                    </div>
                                    <br />

                                    <asp:Panel ID="PnlResetPassword" runat="server">
                                        <br />
                                        <br />

                                        <asp:Button ID="BtnResetPassword" CssClass="btn btn-danger" runat="server" Text="Reset Password" OnClick="BtnResetPassword_Click" OnClientClick="return confirm('Əminsiniz?');" />
                                        &nbsp;<asp:Label ID="LblPass" runat="server"></asp:Label>

                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                            <div class="modal-footer" style="background-color: #363B3F">
                                <asp:Button ID="BtnSaveUsers" runat="server" CssClass="btn btn-default" Text="Yadda saxla" Font-Bold="True" OnClick="BtnSaveUsers_Click" OnClientClick="this.style.display='none';"></asp:Button>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col col-sm-12 col-md-12 col-lg-12">
                <asp:Panel ID="PnlFilter" DefaultButton="BtnFilter" runat="server">
                    <ul class="FilterSearch">
                        <li>İstifadəçi adı:
                            <br />
                            <asp:TextBox ID="TxtFilterUsername" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                        <li>S.A.A:<br />
                            <asp:TextBox ID="TxtFilterFullname" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                        <li>Status:
                            <br />
                            <asp:DropDownList ID="DListFilterStatus" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px">
                                <asp:ListItem Text="Aktiv" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Arxivlənmiş" Value="0"></asp:ListItem>
                            </asp:DropDownList></li>

                        <li class="NoStyle">
                            <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="35px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" OnClientClick="this.style.display='none';" />
                            <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                        <ProgressTemplate>
                            <img src="/Pics/loading.gif" style="width: 30px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                        </li>
                    </ul>
                </asp:Panel>
            </div>
            <div class="col col-sm-12 col-md-12 col-lg-12">
                <div style="float: left; padding: 10px 10px 10px 0px; margin-top: 15px;">
                    <asp:LinkButton ID="LnkAdd" runat="server" ToolTip="Yeni istifadəçi əlavə et" OnClick="LnkAdd_Click"><img src="/pics/new1.png" class="alignMiddle" /> YENİ İSTİFADƏÇİ</asp:LinkButton>
                </div>
                <div style="float: right; padding: 5px;">
                    <asp:Label ID="LblCount" runat="server" Font-Bold="False" Text="--"></asp:Label>
                </div>
                <asp:GridView ID="GrdList" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="S/s">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Username" HeaderText="İstifadəçi adı">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <%-- <asp:BoundField DataField="Position" HeaderText="Vəzifə">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>--%>

                        <asp:BoundField DataField="Fullname" HeaderText="S.A.A">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Email" HeaderText="E-poçt">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Contacts" HeaderText="Telefon">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Description" HeaderText="Qeyd">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID") %>' runat="server" OnClick="LnkEdit_Click"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Düzəlt</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" Font-Size="11pt" />
                        </asp:TemplateField>


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
                    &nbsp;<img src="/Pics/LoadingLittle.gif" style="display: none;" id="Load" /><asp:LinkButton ID="LnkOtherApp" runat="server" Font-Size="12pt" CommandArgument="0" OnClientClick="this.style.display='none'; document.getElementById('Load').style.display='';" Font-Bold="true" Font-Strikeout="False" Font-Underline="False" OnClick="LnkOtherApp_Click"><img class="alignMiddle" src="/pics/more.png" /> davamı</asp:LinkButton>
                    &nbsp;<br />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
