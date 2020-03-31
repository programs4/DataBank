<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="E_Services_Adoption_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataBank E-Services</title>

    <!-- CSS -->
    <link href="/Css/Normalize.css" rel="stylesheet" />
    <link href="/Css/Bootstrap.css" rel="stylesheet" />
    <link href="/Css/styles.css?V7" rel="stylesheet" />

    <script type="text/javascript" src="/Js/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="/Js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.bootpag.js"></script>
    <script type="text/javascript" src="/Js/custom.js"></script>
    <script src="/Js/Bootstrap-datetimepicker.js"></script>
    <script>

        //No iframe
        if (window == top) {
            top.location.replace('/error');
        }

        function Onload() {
            document.getElementById("egovBox").style.display = "block";
        }
        if (window.addEventListener)
            window.addEventListener("load", Onload, false);
        else if (window.attachEvent)
            window.attachEvent("onload", Onload);
        else window.onload = Onload;

    </script>

    <link rel="shortcut icon" href="/Favicon.ico" />
    <meta charset="utf-8" />
    <meta name="description" lang="az" content="" />
    <meta name="keywords" lang="az" content="" />
    <meta name="robots" content="index, follow" />

    <!--  Mobile Viewport Fix -->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body class="site-body">
    <form id="AspnetForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="egovBox" style="min-height: 600px;">

            <div class="site-holder">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewLogin" runat="server">

                                <div class="login-section">
                                    Qeydiyyatda olduğunuz İcra Hakimiyyəti:<span style="color: red;">*</span><br />
                                    <asp:DropDownList ID="DListOrganizations" CssClass="form-control input-filter" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                    <br />
                                    <br />
                                    Uçot nömrəsi:<span style="color: red;">*</span><br />
                                    <asp:TextBox ID="TxtRegisterNo" runat="server" CssClass="form-control" Height="35px"></asp:TextBox>
                                    <br />
                                    <br />
                                    Uçota alınma tarixi:<span style="color: red;">*</span><br />
                                    <asp:TextBox ID="TxtRegisterDt" placeholder="gün.ay.il" runat="server" CssClass="form-control form_datetime" Height="35px" AutoCompleteType="Disabled"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Panel ID="PnlError" CssClass="pnl-error" runat="server" Visible="false">
                                        <asp:Literal ID="LtrError" runat="server"></asp:Literal>
                                    </asp:Panel>
                                    <asp:Button ID="BtnLogin" CssClass="apply-btn" OnClick="BtnLogin_Click" OnClientClick="this.style.display='none'; document.getElementById('Load').style.display='';" runat="server" Text="DAXİL OL" />
                                </div>

                            </asp:View>
                            <asp:View ID="ViewSearch" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="site-content">
                                            <h2 class="content-title text-title">
                                                <asp:Literal ID="LtrContentTitle" runat="server">Əziz valideynlərin nəzərinə!</asp:Literal>
                                            </h2>
                                            <p class="content-text">
                                                <asp:Literal ID="LtrContentText" runat="server">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</asp:Literal>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="filter-panel">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <h2 class="text-title">Məlumat bazasında axtarış</h2>
                                            <asp:Panel ID="PnlSearch" runat="server" CssClass="input-holder">
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:DropDownList ID="DListGender" runat="server" CssClass="form-control input-filter">
                                                        <asp:ListItem Text="- Cinsi -" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Qadın" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Kişi" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:DropDownList ID="DListEyeColor" CssClass="form-control input-filter" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:DropDownList ID="DListHairColor" CssClass="form-control input-filter" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:DropDownList ID="DListAgeRange" CssClass="form-control input-filter" runat="server">
                                                        <asp:ListItem Text="- Yaş Aralığı -" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="0-3 yaş" Value="0&3"></asp:ListItem>
                                                        <asp:ListItem Text="3-6 yaş" Value="3&6"></asp:ListItem>
                                                        <asp:ListItem Text="6-9 yaş" Value="6&9"></asp:ListItem>
                                                        <asp:ListItem Text="9-12 yaş" Value="9&12"></asp:ListItem>
                                                        <asp:ListItem Text="12+ yaş" Value="12&17"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <img src="/Pics/loading-io.gif" class="loading-img" style="display: none;margin-top: 0px;" id="Load" />
                                                    <asp:Button ID="BtnSearch" CssClass="apply-btn" Style="margin-top: 0px;" OnClick="BtnSearch_Click" OnClientClick="this.style.display='none'; document.getElementById('Load').style.display='';" runat="server" Text="Axtar" />
                                                </div>

                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:DropDownList ID="DListBrotherSister" CssClass="form-control input-filter" runat="server" Visible="False">
                                                        <asp:ListItem Text="- Qardaş, Bacı -" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Qardaş, Bacı olmayanlar" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Qardaş, Bacı olanlar" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-xs-12 col-sm-4 col-md-4 col-lg-4 no-padding-left">
                                                    <asp:TextBox ID="TxtFullname" CssClass="form-control input-filter" placeholder="Ad" runat="server" Visible="False"></asp:TextBox>
                                                </div>

                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                                <asp:Panel ID="PnlSearchResult" runat="server" CssClass="search-result" Visible="true">
                                    <span class="result-count">
                                        <asp:Label ID="LblCount" runat="server" Text="500"></asp:Label>
                                    </span>

                                    <asp:Repeater ID="RptChilds" runat="server">
                                        <ItemTemplate>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 info-child">
                                                <div class="col-xs-12 col-sm-5 col-md-4 col-lg-4 profile-img">
                                                    <img src="/Uploads/Adoption/Persons/<%#Eval("ID")%>.jpg?1" />
                                                </div>
                                                <div class="col-xs-12 col-sm-7 col-md-8 col-lg-8 profile-info">
                                                    <h3 class="name-holder"><%#Eval("Fullname")%></h3>
                                                    <p class="content-text">
                                                        Yaşı:
                                            <asp:Literal ID="LtrAge" Text='<%#Eval("Yash")%>' runat="server"></asp:Literal>
                                                    </p>
                                                    <p class="content-text">
                                                        Cinsi:
                                            <asp:Literal ID="LtrGender" Text='<%#Eval("Gender")._ToString()=="True"?"Kişi":"Qadın"%>' runat="server">Kişi</asp:Literal>
                                                    </p>
                                                    <p class="content-text">
                                                        Uşağın ilkin uçotunu götürülməsini aparan qəyyumluq və himayə orqanı:
                                               <asp:Literal ID="LtrChildFirstRegistrationPlace" Text='<%#Eval("AdoptionOrganizations")%>' runat="server">Mavi</asp:Literal>
                                                    </p>
                                                    <p class="content-text">
                                                        Uşağın ilkin uçota götürülmə tarixi:
                                        <asp:Literal ID="LtrChildFirstRegistraionDt" Text='<%#string.IsNullOrEmpty(Eval("IlkinUcotTarix")._ToString())?"":Convert.ToDateTime(Eval("IlkinUcotTarix")).ToString("dd.MM.yyyy")%>' runat="server">Qara</asp:Literal>
                                                    </p>
                                                    <br />
                                                    <p class="content-text" style="color: #27ae60">
                                                        <i class="glyphicon glyphicon-map-marker" style="font-size: 16px"></i>Hara müraciət etməli:<b>
                                       <asp:Literal ID="LtrRegistrationPlace" Text='<%#Eval("AdoptionOrganizations")%>' runat="server"></asp:Literal></b>
                                                    </p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <asp:Panel ID="PnlPager" CssClass="pager_top services-child row pagination-row" Style="text-align: center;" runat="server">
                                        <ul class="pagination bootpag"></ul>
                                    </asp:Panel>

                                    <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <script>

        function CallDatePicker() {
            $(".form_datetime").datetimepicker(
                {
                    format: "dd.mm.yyyy",
                    language: 'en',
                    weekStart: 1,
                    todayBtn: 1,
                    autoclose: 1,
                    todayHighlight: 1,
                    startView: 2,
                    minView: 2,
                    forceParse: 0

                });
        }

        $(document).ready(function () {
            CallDatePicker();
        });


        function GetPagination(t, p) {
            $('.pager_top').bootpag({
                total: t,
                page: p,
                maxVisible: 15,
                leaps: true,
                firstLastUse: true,
                first: '<span aria-hidden="true">&larr;</span>',
                last: '<span aria-hidden="true">&rarr;</span>',
                wrapClass: 'pagination',
                activeClass: 'active',
                disabledClass: 'disabled',
                nextClass: 'next',
                prevClass: 'prev',
                lastClass: 'last',
                firstClass: 'first',

            }).on("page", function (event, num) {
                window.location.href = '/e-services/adoption/?pn=' + num;
            }).find('.pagination');
        }

        $(document).ready(function () {
            GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
            $(".progress-inner").each(function () {
                $(this).animate({ width: $(this).attr('data-percent') + '%' }, 'slow');
            });
        })

        function afterAsyncPostBack() {
            var desiredHeight = document.getElementById('egovBox').offsetHeight + 20;
            var desiredWidth = document.getElementById('egovBox').offsetWidth;
            var message = {
                width: desiredWidth,
                height: desiredHeight
            };
            window.parent.postMessage(JSON.stringify(message), '*');
        }
        afterAsyncPostBack();
        setInterval(function () { afterAsyncPostBack(); }, 100);
    </script>
</body>
</html>
