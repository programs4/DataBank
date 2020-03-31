<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="List_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 40px" class="modal fade" id="myModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-header" style="background-color: #363B3F">
                                    <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" style="color: white"><b><span id="popTitle">Yeni əlavə</span></b></h4>
                                </div>
                                <div class="modal-body" style="background-color: #F5F5F5" id="ModalContentID">
                                    <asp:Panel ID="PnlContentRoot" ScrollBars="Vertical" runat="server">
                                        <div class="ContentRoot">
                                            <asp:Panel ID="PnlSehadet" runat="server" GroupingText="Doğum şəhadətnaməsi" CssClass="PnlItems">
                                                Doğum şəhadətnaməsinin seriyası və nömrəsi<br />
                                                <asp:DropDownList ID="DlistPassportTypeDs" runat="server" CssClass="form-control" DataTextField="Series" DataValueField="ID" Width="220px">
                                                </asp:DropDownList>
                                                &nbsp; &nbsp;<asp:TextBox ID="TxtDocNumberDs" runat="server" CssClass="form-control" placeholder="Yalnız rəqəm" Width="200px" MaxLength="8"></asp:TextBox>
                                                <br />
                                            </asp:Panel>
                                            <asp:Panel ID="PnlPassport" runat="server" GroupingText="Digər sənəd" CssClass="PnlItems">
                                                Sənədin nömrəsi<br />
                                                <asp:DropDownList ID="DlistPassportTypesOther" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="220px">
                                                </asp:DropDownList>
                                                &nbsp; &nbsp;<asp:TextBox ID="TxtDocNumberOther" runat="server" CssClass="form-control" Width="200px" MaxLength="8"></asp:TextBox>
                                                <br />
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
                                                <asp:DropDownList ID="DListGender" runat="server" CssClass="form-control" Width="250px">
                                                    <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                                    <asp:ListItem Value="True" Text="Oğlan"></asp:ListItem>
                                                    <asp:ListItem Value="False" Text="Qız"></asp:ListItem>
                                                </asp:DropDownList><br />
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
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            Ölkə<br />
                                                            <asp:DropDownList ID="DlistCountry" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%" OnSelectedIndexChanged="DlistCountry_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <br />

                                                            Şəhər/rayon<br />
                                                            <asp:DropDownList ID="DlistCity" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                                                            </asp:DropDownList>
                                                            <br />
                                                            <br />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    Ünvan<br />
                                                    <asp:TextBox ID="txtUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </asp:Panel>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlYasadUnvan" runat="server" GroupingText="Yaşadığı ünvan" CssClass="PnlItems">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        Ölkə<br />
                                                        <asp:DropDownList ID="DlistCountry2" runat="server" CssClass="form-control .col-md-6" Enabled="False" Width="100%" DataTextField="Name" DataValueField="ID">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                Şəhər/rayon<br />
                                                <asp:DropDownList ID="DlistCity2" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                Ünvan<br />
                                                <asp:TextBox ID="txtUnvan2" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="modal-footer" style="background-color: #363B3F">
                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-default" Text="Təmizlə" Font-Bold="True" OnClick="BtnClear_Click" OnClientClick="return confirm('Anket təmizlənsin?');"></asp:Button>
                                    <asp:Button ID="BtnSavePersons" runat="server" CssClass="btn btn-default" Text="Yadda saxla" Font-Bold="True" OnClick="BtnSavePersons_Click" OnClientClick="this.style.display='none';"></asp:Button>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <asp:Panel ID="PnlFilter" DefaultButton="BtnFilter" runat="server">

                <ul class="FilterSearch">
                    <li>Sənədin növü:
                            <br />
                        <asp:DropDownList ID="DListFltrDoctType" CssClass="form-control" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrDoctType_SelectedIndexChanged">
                            <asp:ListItem Text="--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Şəxsiyyət vəsiqəsi" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Doğum şəhadətnaməsi" Value="20,30,40"></asp:ListItem>
                            <asp:ListItem Text="Müvəqqəti yaşayış icazəsi" Value="50"></asp:ListItem>
                            <asp:ListItem Text="Daimi yaşayış icazəsi" Value="60"></asp:ListItem>
                            <asp:ListItem Text="Əcnəbi vətəndaşın xarici pasportu" Value="70"></asp:ListItem>
                            <asp:ListItem Text="Digər" Value="100"></asp:ListItem>
                        </asp:DropDownList></li>
                    <li>Sənədin nömrəsi:
                            <br />
                        <asp:TextBox ID="TxtFltrDoctNumber" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                    <li>FİN:
                            <br />
                        <asp:TextBox ID="TxtFltrFin" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                    <li>Soyadı:
                            <br />
                        <asp:TextBox ID="TxtFltrSurname" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                    <li>Adı:
                            <br />
                        <asp:TextBox ID="TxtFltrName" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                    <li>Atasının adı:<br />
                        <asp:TextBox ID="TxtFltrPatronymic" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>

                    <li>Qeydiyyatda olduğu şəhər/rayon:
                            <br />
                        <asp:DropDownList ID="DListFltrRegions" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>
                   
                    <li>Status:
                            <br />
                        <asp:DropDownList ID="DlistStatus" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>
                </ul>
                <asp:Panel ID="PnlImgBtn" runat="server" hspace="5" alt="Ətraflı axtarış" data-toggle="tooltip" title="Ətraflı axtarış">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:LinkButton ID="LnkDetailFilter" runat="server" OnClick="LnkDetailFilter_Click"><img class="alignMiddle" src="/pics/more.png" /> daha ətraflı</asp:LinkButton>
                    <br />
                    <br />
                </asp:Panel>
                <asp:Panel ID="PnlDetailSearch" Visible="false" runat="server">
                    <ul class="FilterSearch">
                        <li>Doğum tarixi (il / ay / gün)<br />
                            <asp:DropDownList ID="DListFltrDogY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DListFltrDogM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="DListFltrDogD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                            </asp:DropDownList>
                        </li>
                        <li>Cinsi:
                            <br />
                            <asp:DropDownList ID="DListFltrGender" runat="server" CssClass="form-control" Width="250px">
                                <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Oğlan"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Qız"></asp:ListItem>
                            </asp:DropDownList></li>

                        <asp:UpdatePanel ID="UptHeyatStatusu" runat="server">
                            <ContentTemplate>
                                <li>Həyat statusu:
                                    <br />
                                    <asp:DropDownList ID="DListFltrIsOlum" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrIsOlum_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Həyatda olanlar"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Vəfat edənlər"></asp:ListItem>
                                    </asp:DropDownList></li>

                                <asp:MultiView ID="MltOlum" runat="server">
                                    <asp:View ID="View2" runat="server">
                                        <li class="backgroundSilver">Vəfat tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrOlumY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrOlumM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrOlumD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptHimaye" runat="server">
                            <ContentTemplate>
                                <li class="himayeBackColor">Himayə:
                            <br />
                                    <asp:DropDownList ID="DListFltrHimaye" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrHimaye_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Himayədə olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Himayədə olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltHimaye" runat="server">
                                    <asp:View ID="View18" runat="server">

                                        <li class="himayeBackColor">Himayəyə götürən:
											<br />
                                            <asp:DropDownList ID="DListFltrHimayeTeshkilatOrShex" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                                <asp:ListItem Value="-1" Text="--"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Təşkilat tərəfindən himayəyə götürülənlər"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Şəxs tərəfindən himayəyə götürülənlər"></asp:ListItem>
                                            </asp:DropDownList></li>

                                        <li class="himayeBackColor">Məhrum olunma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrHimayeMehrumY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHimayeMehrumM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHimayeMehrumD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptAliment" runat="server">
                            <ContentTemplate>
                                <li class="alimentBackColor">Aliment:
                            <br />
                                    <asp:DropDownList ID="DListFltrAliment" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrAliment_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Aliment alanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Aliment almayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltAliment" runat="server">
                                    <asp:View ID="View10" runat="server">
                                        <li class="alimentBackColor">Təyin olunma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrAlimentY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAlimentM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAlimentD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptMuavinat" runat="server">
                            <ContentTemplate>
                                <li class="muavinatBackColor">Müavinat:
                            <br />
                                    <asp:DropDownList ID="DListFltrMuavinat" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrMuavinat_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Müavinat alanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Müavinat almayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltMuavinat" runat="server">
                                    <asp:View ID="View9" runat="server">
                                        <li class="muavinatBackColor">Təyin olunma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrMuavinatY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMuavinatM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMuavinatD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptSehiyyeQeydiyyat" runat="server">
                            <ContentTemplate>
                                <li class="sehiyyeBackColor">Səhiyyə qeydiyyatı:
                            <br />
                                    <asp:DropDownList ID="DListFltrSehiyyeQeydiyyat" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrSehiyyeQeydiyyat_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Səhiyyə qeydiyyatı olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Səhiyyə qeydiyyatı olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltSehiyyeQeydiyyat" runat="server">
                                    <asp:View ID="View17" runat="server">
                                        <li class="sehiyyeBackColor">Qeydiyyatda olduğu şəhər/rayon:
											<br />
                                            <asp:DropDownList ID="DListFltrSehiyyeQeydiyyatRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptSehiyyeMualice" runat="server">
                            <ContentTemplate>
                                <li class="mualiceBackColor">Müalicə aldığı səhiyyə müəssisəsi:
                            <br />
                                    <asp:DropDownList ID="DListFltrSehiyyeMualice" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrSehiyyeMualice_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Müalicə alanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Müalicə almayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltSehiyyeMualice" runat="server">
                                    <asp:View ID="View15" runat="server">

                                        <li class="mualiceBackColor">Müalicə aldığı şəhər/rayon:<br />
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>

                                        <li class="mualiceBackColor">Müalice aldığı tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>

                                        <li class="mualiceBackColor">Monitoring tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceMonitoringY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceMonitoringM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSehiyyeMualiceMonitoringD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>

                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptSagamligTarixce" runat="server">
                            <ContentTemplate>
                                <li class="saglamliqBackColor">Sağlamlıq tarixcəsi:
                                    <br />
                                    <asp:DropDownList ID="DListFltrSagamligTarixce" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrSagamligTarixce_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Sağlamlıq tarixcəsi olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sağlamlıq tarixcəsi olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltSagamligTarixce" runat="server">
                                    <asp:View ID="View16" runat="server">

                                        <li class="saglamliqBackColor">Sağlamlıq tarixcəsinin növü:
											<br />
                                            <asp:DropDownList ID="DListFltrSagamligTarixceNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptIdman" runat="server">
                            <ContentTemplate>
                                <li class="idmanBackColor">İdman nailiyyəti:
                            <br />
                                    <asp:DropDownList ID="DListFltrIdmanNailiyyet" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrIdmanNailiyyet_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="İdman nailiyyəti olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="İdman nailiyyəti olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltIdmanNailiyyet" runat="server">
                                    <asp:View ID="View12" runat="server">
                                        <li class="idmanBackColor">Yarışın keçirildiyi şəhər/rayon:<br />
                                            <asp:DropDownList ID="DListFltrIdmanNailiyyetRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="idmanBackColor">İdman növü<br />
                                            <asp:DropDownList ID="DListFltrIdmanNailiyyetNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="idmanBackColor">Yarışın keçirildiyi tarix (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrIdmanNailiyyetY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrIdmanNailiyyetM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrIdmanNailiyyetD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>

                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptTehsilAlan" runat="server">
                            <ContentTemplate>
                                <li class="tehsilAlanBackColor">Təhsil alanlar:
                                    <br />
                                    <asp:DropDownList ID="DListFltrTehsil" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrTehsil_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Təhsil alanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Təhsil almayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltTehsil" runat="server">
                                    <asp:View ID="View14" runat="server">
                                        <li class="tehsilAlanBackColor">Təhsil aldığı şəhər/rayon:<br />
                                            <asp:DropDownList ID="DListFltrTehsilRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="tehsilAlanBackColor">Təhsil aldığı yer<br />
                                            <asp:DropDownList ID="DListFltrTehsilYer" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                                <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Evdə təhsil alanlar"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Təhsil müəssisəsində təhsil alanlar"></asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptTehsil" runat="server">
                            <ContentTemplate>
                                <li class="tehsilNailiyyetBackColor">Təhsil nailiyyeti:
                            <br />
                                    <asp:DropDownList ID="DListFltrTehsilNailiyyet" runat="server" CssClass="form-control" Width="250px" OnSelectedIndexChanged="DListFltrTehsilNailiyyet_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Təhsil nailiyyeti olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Təhsil nailiyyeti olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltTehsilNailiyyet" runat="server">
                                    <asp:View ID="View13" runat="server">
                                        <li class="tehsilNailiyyetBackColor">Yarışın keçirildiyi şəhər/rayon:<br />
                                            <asp:DropDownList ID="DListFltrTehsilNailiyyetRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="tehsilNailiyyetBackColor">Fənn növü<br />
                                            <asp:DropDownList ID="DListFltrTehsilNailiyyetNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="tehsilNailiyyetBackColor">Yarışın keçirildiyi tarix (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrTehsilNailiyyetY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTehsilNailiyyetM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTehsilNailiyyetD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>

                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptYetkinlik" runat="server">
                            <ContentTemplate>
                                <li class="dunyayaKorpeBackColor">Yetkinlik yaşına çatmayan qız:
                            <br />
                                    <asp:DropDownList ID="DListFltrDunyayaKorpe" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrDunyayaKorpe_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Dünyaya körpə gətirənlər"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Dünyaya körpə gətirməyənlər"></asp:ListItem>
                                    </asp:DropDownList></li>

                                <asp:MultiView ID="MltDunyayaKorpe" runat="server">
                                    <asp:View ID="View1" runat="server">
                                        <li class="dunyayaKorpeBackColor">Doğulduğu şəhər/rayon:
                                    <br />
                                            <asp:DropDownList ID="DListFltrDunyayaKorpeRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="dunyayaKorpeBackColor">Doğulduğu tarix (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrDunyayaKorpeY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrDunyayaKorpeM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrDunyayaKorpeD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptHuquqQarsi" runat="server">
                            <ContentTemplate>
                                <li class="huquqQarsiBackColor">Uşağa qarşı hüquqpozmaları:
                            <br />
                                    <asp:DropDownList ID="DListFltrHuquqPozmaQarsi" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrHuquqPozmaQarsi_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Hüququ pozulanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hüququ pozulmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltHuquqPozmaQarsi" runat="server">
                                    <asp:View ID="View3" runat="server">
                                        <li class="huquqQarsiBackColor">Maddə:
                                    <br />
                                            <asp:TextBox ID="TxtFltrHuquqPozmaQarsiMadde" CssClass="form-control" runat="server" Width="250px"></asp:TextBox></li>
                                        <li class="huquqQarsiBackColor">Hüquq p-nun tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaQarsiY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaQarsiM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaQarsiD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptHuquq" runat="server">
                            <ContentTemplate>
                                <li>Hüquq pozma:
                                    <br />
                                    <asp:DropDownList ID="DListFltrHuquqPozma" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrHuquqPozma_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Hüquq qaydalarını pozanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hüquq qaydalarını pozmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltHuquqPozma" runat="server">
                                    <asp:View ID="View5" runat="server">
                                        <li class="backgroundSilver">Cinayət əməli və ya inzibati xəta:
                                    <br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaMecelle" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="backgroundSilver">Tədbiq edilmiş cəza növü:
                                    <br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaCezaNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="backgroundSilver">Сəza çəkdiyi şəhər/rayon:
                                    <br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>

                                        <li class="backgroundSilver">Monitorinq tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaMonitoringY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaMonitoringM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaMonitoringD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="backgroundSilver">Cəzanın bitmə tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrHuquqPozmaY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrHuquqPozmaD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptTerbiyeviTedbir" runat="server">
                            <ContentTemplate>
                                <li>Uşağa keçirilmiş tərbiyəvi tədbirlər:
                            <br />
                                    <asp:DropDownList ID="DListFltrTerbiyeviTedbir" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrTerbiyeviTedbir_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Tərbiyəvi tədbir keçirilənlər"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Tərbiyəvi tədbir keçirilməyənlər"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltTerbiyeviTedbir" runat="server">
                                    <asp:View ID="View4" runat="server">
                                        <li class="backgroundSilver">Doğulduğu şəhər/rayon:
                                    <br />
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="backgroundSilver">Tədbirin tətbiq tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="backgroundSilver">Tədbirin monitorinq tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirMonitoringY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirMonitoringM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrTerbiyeviTedbirMonitoringD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptMeshgulluq" runat="server">
                            <ContentTemplate>
                                <li>Uşağın məşğulluq vəziyyəti:
                            <br />
                                    <asp:DropDownList ID="DListFltrMeshgulluq" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrMeshgulluq_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Məşğul olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Məşğul olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltMeshgulluq" runat="server">
                                    <asp:View ID="View6" runat="server">
                                        <li class="backgroundSilver">Üzv olma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrMeshQeydY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMeshQeydM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMeshQeydD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                    <asp:View ID="View7" runat="server">
                                        <li class="backgroundSilver">Ayrılma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrMeshCixmaY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMeshCixmaM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrMeshCixmaD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="uptSosialYardim" runat="server">
                            <ContentTemplate>
                                <li>Sosial yardım:
                            <br />
                                    <asp:DropDownList ID="DListFltrSosialYardım" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrSosialYardım_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Sosial yardım təyin edilənlər"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sosial yardım təyin edilməyənlər"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltSosialYardım" runat="server">
                                    <asp:View ID="View8" runat="server">
                                        <li class="backgroundSilver">Təyin olunma tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrSosialYardımY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSosialYardımM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSosialYardımD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptSosialXidmet" runat="server">
                            <ContentTemplate>
                                <li>Sosial xidmət:
                            <br />
                                    <asp:DropDownList ID="DListFltrSosialXidmet" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrSosialXidmet_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Sosial xidmət alanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Sosial xidmət almayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltSosialXidmet" runat="server">
                                    <asp:View ID="View19" runat="server">
                                        <li class="backgroundSilver">Sosial xidmətin növü:
											<br />
                                            <asp:DropDownList ID="DListFltrSosialXidmetNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>

                                        <li class="backgroundSilver">Xidmətin başlama tarixi (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrSosialXidmetY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSosialXidmetM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrSosialXidmetD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UptDuserge" runat="server">
                            <ContentTemplate>
                                <li>İstirafət düşərgəsi:
                                    <br />
                                    <asp:DropDownList ID="DListFltrAsudeDusherge" runat="server" CssClass="form-control" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListFltrAsudeDusherge_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="İstirafət düşərgəsində olanlar"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="İstirafət düşərgəsində olmayanlar"></asp:ListItem>
                                    </asp:DropDownList></li>
                                <asp:MultiView ID="MltAsudeDusherge" runat="server">
                                    <asp:View ID="View11" runat="server">

                                        <li class="backgroundSilver">Düşərgənin yerləşdiyi şəhər/rayon:
                                    <br />
                                            <asp:DropDownList ID="DListFltrAsudeDushergeRegion" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                                            </asp:DropDownList></li>
                                        <li class="backgroundSilver">Düşərgədən gəldiyi tarix (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGeldiyiY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGeldiyiM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGeldiyiD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>
                                        <li class="backgroundSilver">Düşərgəyə getdiyi tarix (il / ay / gün)<br />
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGetdiyiY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGetdiyiM" runat="server" CssClass="form-control" DataTextField="ID" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DListFltrAsudeDushergeGetdiyiD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="70px">
                                            </asp:DropDownList>
                                        </li>

                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </ul>
                </asp:Panel>
                <ul class="FilterSearch">
                    <li class="NoStyle">
                        <asp:ImageButton ID="BtnFilter" runat="server" ImageUrl="/pics/btn_search.png" OnClick="BtnFilter_Click" OnClientClick="this.style.display='none';" />
                        <asp:ImageButton ID="BtnFilterClear" runat="server" ImageUrl="/pics/btn_clear.png" OnClick="BtnFilterClear_Click" OnClientClick="this.style.display='none';" />
                    </li>
                </ul>
            </asp:Panel>
            <asp:Panel ID="PnlAdd" runat="server" Style="padding: 10px 10px 10px 0px; height: 50px; float: left; margin-top: 30px; margin-bottom: 5px">
                <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
            </asp:Panel>
            <div style="padding: 10px 10px 10px 0px; height: 50px; float: right; margin-top: 30px;">
                <asp:Label ID="LblCount" runat="server" Font-Bold="False" Text="--"></asp:Label>
            </div>
            <asp:GridView ID="GrdList" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="99%" CssClass="boxShadow" Font-Bold="True" DataKeyNames="ID">
                <Columns>
                    <asp:BoundField DataField="Ss" HeaderText="S/s">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="FIN" HeaderText="FİN">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Soyad" HeaderText="Soyadı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ad" HeaderText="Adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Ata" HeaderText="Atasının adı">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="DogumTarixi" HeaderText="Doğum tarixi">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Qeydiyyatda olduğu ünvan">
                        <ItemTemplate>
                            <%# Eval("CountriesName") %>, <%# Eval("RegionsName") %><br />
                            <span class="addressSub"><%# Eval("YasadigiUnvan") %></span>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="/history/?i=<%#Eval("ID").QueryIdEncrypt() %>"><span class="glyphicon glyphicon-time" aria-hidden="true"></span>Tarixçə</a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="/detail/?i=<%#Eval("ID").QueryIdEncrypt() %>">
                                <img src="/pics/edit.png">
                                Ətraflı</a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <EmptyDataTemplate>
                    <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                        Məlumat tapılmadı.
                    </div>
                </EmptyDataTemplate>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle Font-Bold="True" BackColor="#19797C" ForeColor="White" Height="40px" />
                <PagerSettings PageButtonCount="20" />
                <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
                <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
                <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <div align="left" style="margin-bottom: 50px; margin-top: 20px; padding-left: 0px; clear: left; float: left;" id="MoreButton">
                &nbsp;<img src="/Pics/LoadingLittle.gif" style="display: none;" id="Load" /><asp:LinkButton ID="LnkOtherChild" runat="server" Font-Size="12pt" CommandArgument="0" OnClientClick="this.style.display='none'; document.getElementById('Load').style.display='';" Font-Bold="true" Font-Strikeout="False" Font-Underline="False" OnClick="LnkOtherChild_Click"><img class="alignMiddle" src="/pics/more.png" /> davamı</asp:LinkButton>
                &nbsp;<br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

