<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Users.ascx.cs" Inherits="Adminn_Tools_Users" %>
<div class="panel panel-default">
    <div class="panel-heading">İstifadəçilər</div>
</div>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <div style="margin-top: 40px" class="modal fade" id="myModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #363B3F">
                        <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" style="color: white"><b><span id="popTitle">
                            <asp:Literal ID="LtrModalHeader1" runat="server"></asp:Literal></span></b></h4>
                    </div>
                    <div class="modal-body" style="background-color: #F5F5F5" id="ModalContentID">
                        <asp:Panel Style="padding: 10px" ID="PnlDiscussion" runat="server" ScrollBars="Vertical">
                            Qurumun adı:<br />
                            <asp:DropDownList ID="DListOrganization" CssClass="form-control" Width="450px" Height="35px" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList><br />
                            <br />
                            İstifadəçi adı:<br />
                            <asp:TextBox ID="TxtUsername" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                            <br />
                            <asp:Panel ID="PnlPassword" runat="server">
                                İstifadəçi şifrəsi:<br />
                                <asp:TextBox ID="TxtPass" CssClass="form-control" Width="450px" Height="35px" runat="server" TextMode="Password"></asp:TextBox><br />
                                <br />
                            </asp:Panel>
                            Şəxsiyyət vəsiqəsinin nömrəsi:<br />
                            <asp:TextBox ID="TxtPassportNumber" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
                            <br />
                            FİN:<br />
                            <asp:TextBox ID="TxtFin" CssClass="form-control" Width="450px" Height="35px" runat="server"></asp:TextBox><br />
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
                            Qeyd:<br />
                            <asp:TextBox ID="TxtDescription" CssClass="form-control" Width="450px" Height="45px" TextMode="MultiLine" runat="server"></asp:TextBox>
                            <asp:Panel ID="PnlResetPassword" runat="server">
                                <br />
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnResetPassword" CssClass="btn btn-danger" runat="server" Text="Reset Password" OnClick="BtnResetPassword_Click" OnClientClick="return confirm('Əminsiniz?');" />
                                        &nbsp;<asp:Label ID="LblPass" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <div class="modal-footer" style="background-color: #363B3F">
                      <%--  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>--%>
                                <asp:Button ID="BtnSaveUsers" runat="server" CssClass="btn btn-default" Text="Yadda saxla" Font-Bold="True" OnClick="BtnSaveUsers_Click" OnClientClick="this.style.display='none';"></asp:Button>
                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 40px" class="modal fade" id="PermissionModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #363B3F">
                        <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" style="color: white"><b><span id="Span1">Hüquqlar</span></b></h4>
                    </div>
                    <div class="modal-body" style="background-color: #F5F5F5" id="PermissionModalContentID">
                        <asp:Panel Style="padding: 10px" ID="PnlPermission" runat="server" ScrollBars="Vertical">
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="inner-left-content">
                                        <div style="margin-top: 10px; margin-bottom: 10px">
                                            <div class="bs-callout">
                                                <h4>
                                                    <asp:Literal ID="LtrUserFullName" runat="server">WebProject - FİN all information də cərimələrə baxmaq.</asp:Literal>
                                                </h4>
                                                <p>
                                                    <asp:Literal ID="LtrOrganizationsName" runat="server">WebProject - FİN all information də cərimələrə baxmaq.</asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                        <asp:CheckBox ID="ChkIp" CssClass="Chekbx" runat="server" AutoPostBack="True" OnCheckedChanged="ChkIp_CheckedChanged"></asp:CheckBox>
                                        İstənilən IP üzrə giriş hüququ<br />
                                        <br />
                                        <asp:CheckBox ID="ChkRegions" CssClass="Chekbx" runat="server" AutoPostBack="True" OnCheckedChanged="ChkRegions_CheckedChanged"></asp:CheckBox>
                                        Bütün bölgələrə baxış hüququ<br />
                                        <br />
                                        <asp:CheckBox ID="ChkTypes" CssClass="Chekbx" runat="server" AutoPostBack="True" OnCheckedChanged="ChkTypes_CheckedChanged"></asp:CheckBox>
                                        Bütün imkanlardan istifadə hüququ<br />
                                        <br />
                                        <ul class="accordion">
                                            <asp:Panel ID="PnlIp" runat="server">
                                                <li>
                                                    <h4><span class="dot"></span>Ip hüquqlar
                                                    <img class="ac-arrow" src="/Pics/ico13.png" /></h4>
                                                    <div class="acc-cont" style="padding: 0px">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>

                                                                <br />

                                                                <asp:TextBox ID="TxtIp" CssClass="form-control" Width="85%" Height="35px" runat="server"></asp:TextBox>
                                                                &nbsp;
                                                        <asp:Button ID="BtnIpAdd" CssClass="btn btn-default" runat="server" Text="ƏLAVƏ ET" OnClick="BtnIpAdd_Click" Width="13%" />
                                                                <br />
                                                                <br />
                                                                <asp:GridView ID="GrdIP" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" ShowHeader="False" DataKeyNames="ID,Ip" OnRowDeleting="GrdIP_RowDeleting">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Ip">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>

                                                                        <asp:CommandField DeleteText="&lt;span class=&quot;glyphicon glyphicon-trash&quot; aria-hidden=&quot;true&quot;&gt;&lt;/span&gt; Sil" ShowDeleteButton="True">
                                                                            <ControlStyle ForeColor="#CC0000" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="121px" />
                                                                        </asp:CommandField>

                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <EmptyDataTemplate>
                                                                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                                                                            Məlumat tapılmadı.
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" Height="40px" />
                                                                    <PagerSettings PageButtonCount="20" />
                                                                    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                                                                    <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
                                                                    <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </li>
                                            </asp:Panel>

                                            <asp:Panel ID="PnlRegions" runat="server">
                                                <li>
                                                    <h4><span class="dot"></span>Bölgə hüquqları
                                                    <img class="ac-arrow" src="/Pics/ico13.png" /></h4>
                                                    <div class="acc-cont" style="padding: 3px">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GrdRegions" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Name">
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>

                                                                        <asp:TemplateField HeaderText="İcazə">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="ChkRegions" CssClass="Chekbx" runat="server"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                            <ItemStyle Width="100px" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <EmptyDataTemplate>
                                                                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                                                                            Məlumat tapılmadı.
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" Height="40px" />
                                                                    <PagerSettings PageButtonCount="20" />
                                                                    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                                                                    <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
                                                                    <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </li>
                                            </asp:Panel>

                                            <asp:Panel ID="PnlTypes" runat="server">
                                                <li>
                                                    <h4><span class="dot"></span>Düzəliş və baxış hüquqları 
                                                    <img class="ac-arrow" src="/Pics/ico13.png" /></h4>
                                                    <div class="acc-cont" style="padding: 3px">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="GrdTypes" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID">
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <%# Eval("Name") %><br />
                                                                                <span class="addressSub"><%# Eval("Description") %></span>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="DlistPermission" runat="server" CssClass="form-control" Width="100%">
                                                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Baxış</asp:ListItem>
                                                                                    <asp:ListItem Value="2">Düzəliş</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <EmptyDataTemplate>
                                                                        <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                                                                            Məlumat tapılmadı.
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle Font-Bold="True" ForeColor="Black" Height="40px" />
                                                                    <PagerSettings PageButtonCount="20" />
                                                                    <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                                                                    <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
                                                                    <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </li>
                                            </asp:Panel>
                                        </ul>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div class="modal-footer" style="background-color: #363B3F">
                        <asp:Button ID="BtnAllPermissionSave" runat="server" CssClass="btn btn-default" Font-Bold="True" OnClick="BtnAllPermissionSave_Click" OnClientClick="this.style.display='none';" Text="Dəyişiklikləri yadda saxla" />
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div style="padding: 10px">
            <asp:Panel ID="PnlFilter" DefaultButton="BtnFilter" runat="server">
                <ul class="FilterSearch">
                    <li>Qurum:
                            <br />
                        <asp:DropDownList ID="DListFilterOrganizations" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>
                    <li>İstifadəçi adı:
                            <br />
                        <asp:TextBox ID="TxtFilterUsername" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                    <li>Şəxsiyyət vəsiqəsinin nömrəsi:
                            <br />
                        <asp:TextBox ID="TxtFilterPassportNumber" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
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
            <div style="padding: 10px 10px 10px 0px; margin-top: 15px">
                <div style="float: left;">
                    <asp:LinkButton ID="LnkAdd" runat="server" ToolTip="Yeni istifadəçi əlavə et" OnClick="LnkAdd_Click"><img src="/pics/new.png" class="alignMiddle" /> YENİ İSTİFADƏÇİ</asp:LinkButton>
                </div>
            </div>
            <div style="float: right; padding: 5px;">
                <asp:Label ID="LblCount" runat="server" Font-Bold="False" Text="--"></asp:Label>
            </div>
            <asp:GridView ID="GrdList" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                <Columns>
                    <asp:BoundField DataField="Ss" HeaderText="S/s">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="OrganizationsName" HeaderText="Qurumun adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Username" HeaderText="İstifadəçi adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="PassportNumber" HeaderText="Ş/v nömrəsi">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Pin" HeaderText="FİN">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>

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

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkPermission" CommandArgument='<%#Eval("ID") %>' runat="server" OnClick="LnkPermission_Click"><span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span>&nbsp;Hüquqları</asp:LinkButton>
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
    </ContentTemplate>
</asp:UpdatePanel>
