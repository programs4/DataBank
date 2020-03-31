<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdoptionPersonOperations.ascx.cs" Inherits="AdoptionAdminn_Tools_AdoptionPersonOperations" %>


<div class="panel panel-default">
    <div class="panel-heading">
        <asp:Literal ID="LtrTitle" runat="server">Yeni</asp:Literal>
    </div>
</div>

<div class="row" style="margin-left: 0; margin-right: 0;">
    <div class="col-xs-12 col-sm-12 col-md-8">
        <asp:RadioButtonList ID="RListAdoptionPersonsStatus" runat="server" DataValueField="ID" DataTextField="Name" CssClass="radio-list" RepeatDirection="Horizontal"></asp:RadioButtonList>
    </div>
    <div class="col-xs-12 col-sm-12 col-md-4 img-panel" style="text-align: right;">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <asp:Image ID="ImgMain" ImageUrl="/Pics/profile-img.png" CssClass="child-img main-img" runat="server" />
        </div>
        <div class="col-xs-12 col-sm-12 col-md-12">
            <div class="fileupload-section">
                <asp:FileUpload ID="FileUploadFiles" runat="server" ClientIDMode="Static" Style="display: none;" />
                <a href="#open-file" onclick="document.getElementById('FileUploadFiles').click();" id="fileUploadDoc">
                    <img class="icon-add" src="/Pics/AddPlus.png" />
                    <asp:Literal ID="LtrFileUpload" runat="server" Text="Şəkli dəyiş"></asp:Literal>
                </a>
                <div style="display: none" id="loadingDoc">
                    <img style="width: 40px; height: 40px;" src="/Pics/load.gif" />
                    <asp:Literal ID="LtrLoading" runat="server"><span  style="position:relative;top:-12px;">Yüklənir...</span></asp:Literal>
                </div>

            </div>

        </div>

    </div>
</div>

<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="PnlNotification" runat="server" CssClass="alert alert-success" role="alert" Visible="false">

            <i class="glyphicon glyphicon-ok success-icon"></i><span class="notification-text">Əməliyyat uğurla yerinə yetirildi.</span>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">
                    <i class="glyphicon glyphicon-remove close-icon"></i>
                </span>
            </button>

        </asp:Panel>
        <asp:Panel ID="PnlAlert" Visible="false" CssClass="alert alert-danger alert-dismissible" runat="server" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <asp:Literal ID="LtrError" runat="server"></asp:Literal>
        </asp:Panel>
        <div class="row" style="margin-right: 0;">
            <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                Sənədin növü:<br />
                <asp:DropDownList ID="DListDocType" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                <br />
                <br />

                Sənədin nömrəsi:<span style="color: red;">*</span><br />
                <asp:TextBox ID="TxtDocNumber" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />

                Sənədin verilmə tarixi:<br />
                <asp:TextBox ID="TxtDocGivenDt" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                <br />
                <br />

                FİN:<br />
                <asp:TextBox ID="TxtPIN" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Soyad:<span style="color: red;">*</span><br />
                <asp:TextBox ID="TxtSurname" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Ad:<span style="color: red;">*</span><br />
                <asp:TextBox ID="TxtName" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Ata adı:<span style="color: red;">*</span><br />
                <asp:TextBox ID="TxtPatronymic" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Doğum tarixi:<br />
                <asp:TextBox ID="TxtBirthDt" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                <br />
                <br />
                Cinsiyyət:<br />
                <asp:DropDownList ID="DListGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Qadın" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Kişi" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Göz Rəngi:<br />
                <asp:DropDownList ID="DListEyeColor" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                <br />
                <br />
                Saç Rəngi:<br />
                <asp:DropDownList ID="DListHairColor" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        Ölkə:<br />
                        <asp:DropDownList ID="DListCountries" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListCountries_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        <br />
                        <br />
                        Rayon:<br />
                        <asp:DropDownList ID="DListRegions" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                Uşağın yaşayış yeri:<br />
                <asp:TextBox ID="TxtChildLivePlace" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                Uşağın ilkin uçotunu götürülməsini aparan qəyyumluq və himayə orqanı:<br />
                <%--<asp:TextBox ID="TxtChildFirstRegistrationPlace" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>--%>
                <asp:DropDownList ID="DListChildFirstRegistrationPlace" CssClass="form-control" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                <br />
                <br />
                Uşağın ilkin uçota götürülmə tarixi:<br />
                <asp:TextBox ID="TxtChildFirstRegistraionDt" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                <br />
                <br />
                Məlumat vərəqəsi nömrəsi:<br />
                <asp:TextBox ID="TxtInfoSheetNo" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Məlumat vərəqəsi tarixi:<br />
                <asp:TextBox ID="TxtInfoSheetDt" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        Doğum qeydiyyat yeri olan ölkə:<br />
                        <asp:DropDownList ID="DListBirthRegistraionCountry" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListBirthRegistraionCountry_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        <br />
                        <br />
                        Doğum qeydiyyat yeri olan rayon:<br />
                        <asp:DropDownList ID="DListBirthRegistrationRegion" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                Doğum qeydiyyat ünvanı:<br />
                <asp:TextBox ID="TxtBirthRegistraionAddress" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Uşağın atası barədə məlumat :<br />
                <asp:TextBox ID="TxtInfoFather" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                Uşağın anası barədə məlumat :<br />
                <asp:TextBox ID="TxtInfoMother" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                Uşağın mərkəzləşdirilmiş uçota götürülməsini aparan orqan:<br />
                <asp:TextBox ID="TxtCentralizedRegistrationPlace" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Uşağın səhhəti barədə məlumat:<br />
                <asp:TextBox ID="TxtChildHealthInfo" runat="server" CssClass="form-control" Height="107px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />

            </div>
            <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4">
                Uşağın yaxın qohumları barədə məlumat:<br />
                <asp:TextBox ID="TxtCloseRelativesInfo" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                Valideyn himayəsindən məhrum olma növü:<br />
                <asp:TextBox ID="TxtDeprivedParentCareType" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Uşağın ailəyə verilməsi üçün əsaslar:<br />
                <asp:TextBox ID="TxtReasonGivenToFamily" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                <br />
                <br />
                Uşağın xüsusi əlamətləri:<br />
                <asp:TextBox ID="TxtChildSpecialities" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                Uşağın Azərbaycanlı ailələrinə göstərilməsi barədə məlumatlar:<br />
                <asp:TextBox ID="TxtInfoShownAzeriFamilies" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                AQPDK-da ümumi uçota alınma tarixi:<br />
                <asp:TextBox ID="TxtAQPDKregistrationDt" runat="server" CssClass="form-control form_datetime" Height="35px"></asp:TextBox>
                <br />
                <br />
                Əlavə məlumat:<br />
                <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" Height="108px" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                <br />
                <div style="top: 10px; position: relative; left: -3px; margin-bottom: 2px;">
                    <asp:CheckBox ID="ChkIsWeb" CssClass="Chekbx" runat="server"></asp:CheckBox>
                    Veb-də göstərilsin?                                  
                </div>
                <br />
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4" style="float: left">
                <asp:Panel ID="PnlAddToExisting" runat="server">
                    <div class="panel panel-default add-existing-sister-brother">
                        <div class="panel-heading">
                            Bacı,qardaş əlavə et
                        </div>
                        <div class="panel-body">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding-left: 0">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4">
                                    <label class="row-left">Şəxsi kod:</label><br />
                                    <asp:TextBox ID="TxtChildID" runat="server" CssClass="form-control row-left" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8">
                                    <asp:Button ID="BtnSearch" CssClass="btn btn-default btn-search" OnClick="BtnSearch_Click" Width="100px" Height="35px" runat="server" Text="AXTAR" Font-Bold="False" OnClientClick="this.style.display='none';" />
                                </div>
                            </div>
                            <hr class="hr-style" />
                            <asp:Panel ID="PnlResultSearch" runat="server" Visible="false">
                                <asp:Label ID="LblChildName" CssClass="label label-default lbl-text" runat="server" Text="Soyad Ad Ata adi"></asp:Label>
                                <asp:LinkButton ID="LnkAddBrotherSister" CssClass="btn btn-add" runat="server" Width="100px" OnClick="LnkAddBrotherSister_Click" OnClientClick="return confirm('Bacı(qardaş)lığa əlavə etmək istədiyinizdən əminsinizmi?')"><span class="glyphicon glyphicon-plus"></span> Əlavə et</asp:LinkButton>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="PnlError" runat="server" CssClass="alert alert-danger alert-message" Visible="false">
                                <asp:Literal ID="LtrErrorText" runat="server">Bu istifadəçi artıq başqa istifadəçinin qardaş(bacı) siyahısında mövcuddur.</asp:Literal>
                            </asp:Panel>
                            <asp:Panel ID="PnlSuccess" runat="server" CssClass="alert alert-success alert-message" Visible="false">
                                <asp:Literal ID="Literal1" runat="server">Əməliyyat uğurla yerinə yetirildi.</asp:Literal>
                            </asp:Panel>
                        </div>
                    </div>
                </asp:Panel>


            </div>

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <%--baci qardas siyahisi--%>
                <asp:Panel ID="PnlSisterBrotherList" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        Bacı,qardaş siyahısı
                    </div>
                    <asp:GridView ID="GrdSisterBrotherList" runat="server" OnRowDataBound="GrdSisterBrotherList_RowDataBound" DataKeyNames="ID" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Şəxsi kod">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Fullname" HeaderText="Soyadı, adı və atasının adı">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="PIN" HeaderText="FİN">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Cinsi">
                                <ItemTemplate>
                                    <img src='/Pics/Gender_<%#Eval("Gender")._ToString()=="True"?"1":"0" %>.png' title='<%#Eval("Gender")._ToString()=="True"?"Kişi":"Qadın" %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Address" HeaderText="Ünvan">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                            </asp:BoundField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkRemove" CommandArgument='<%#Eval("ID") %>' OnClick="LnkRemove_Click" OnClientClick="return confirm('Bacı(qardaş)lıqdan çıxarmaq istədiyinizdən əminsinizmi?')" runat="server"><img src="/Pics/icon-remove.png" /></asp:LinkButton>
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
                </asp:Panel>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="col-md-12 FilterSearch NoStyle" style="text-align: right; margin-top: 20px;">
    <asp:Button ID="BtnSave" CssClass="btn btn-default" Width="150px" Height="60px" runat="server" OnClick="BtnSave_Click" Text="YADDA SAXLA" Font-Bold="False" OnClientClick="this.style.display='none';" />
</div>
