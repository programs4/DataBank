<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="E_Services_AdoptionForeign_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DataBank E-Services-Foreign</title>

    <!-- CSS -->
    <link href="/Css/Normalize.css" rel="stylesheet" />
    <link href="/Css/Bootstrap.css" rel="stylesheet" />
    <link href="/Css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="/Css/styles.css?V8" rel="stylesheet" />

    <script type="text/javascript" src="/Js/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="/Js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.bootpag.js"></script>
    <script type="text/javascript" src="/Js/custom.js"></script>
    <script type="text/javascript" src="/js/bootstrap-datetimepicker.js"></script>
    <script src='https://www.google.com/recaptcha/api.js'></script>

    <script>

        //No iframe
        if (window == top) {
            top.location.replace('/error');
        }

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
            CheckLang();
        });

        function CheckLang() {
            var pathname = window.location.href.split("?")[1].substring(5, 7);
            $(".lang-holder .lang").each(function () {
                $(this).removeClass("active");
                if ($(this).attr("data-value") === pathname) {
                    $(this).addClass("active");
                }
            })
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="lang-holder">
                            <a href="/e-services/adoptionforeign/?lang=az" data-value="az" class="lang active">AZ</a>
                            <a href="/e-services/adoptionforeign/?lang=ru" data-value="ru" class="lang">RU</a>
                            <a href="/e-services/adoptionforeign/?lang=en" data-value="en" class="lang">EN</a>
                        </div>
                        <div class="content-form">
                            <div class="row">
                                <asp:Panel ID="PnlAlert" CssClass="error-message" Visible="false" runat="server" Style="width: 97%; margin-left: 14px; padding: 10px;">
                                    <div class="col-md-12">
                                        <asp:Literal ID="LtrAlert" runat="server"></asp:Literal>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-6">
                                    <asp:Label ID="LblName" CssClass="input-title" runat="server"> Ad:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtName" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblSurname" CssClass="input-title" runat="server"> Soyad:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtSurname" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblPatronymic" CssClass="input-title" runat="server"> Ata adı:</asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtPatronymic" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblGender" CssClass="input-title" runat="server"> Cins:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListGender" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text=" -- " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Qadın" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Kişi" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblBirthDate" CssClass="input-title" runat="server"> Doğum tarixi:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtBirthDate" runat="server" CssClass="form-control form_datetime form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblBirthPlace" CssClass="input-title" runat="server"> Doğum yeri:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListBirthPlace" CssClass="form-control form-inputs" runat="server" DataTextField="Name" DataValueField="ID" Height="35px"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblNationality" CssClass="input-title" runat="server"> Vətəndaşlıq:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListNationality" CssClass="form-control form-inputs" runat="server" DataTextField="Name" DataValueField="ID" Height="35px"></asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblRegisteredAddress" CssClass="input-title" runat="server"> Daimi yaşayış və qeydiyyat ünvanı:</asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtRegisteredAddress" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblCurrentResidence" CssClass="input-title" runat="server"> Faktiki yaşayış ünvanı:</asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtCurrentResidence" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblPhoneNumber" CssClass="input-title" runat="server"> Ev telefon nömrəsi:</asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtPhoneNumber" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblMobileNumber" CssClass="input-title" runat="server"> Mobil telefon nömrəsi:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtMobileNumber" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblEmail" CssClass="input-title" runat="server"> Elektron ünvan:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control form-inputs" Height="35px"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblEducation" CssClass="input-title" runat="server"> Təhsil:</asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListEducation" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text=" -- " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Ali" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Natamam ali" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="Orta" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="Orta ixtisas" Value="40"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="LblMarriedStatus" CssClass="input-title" runat="server"> Ailə vəziyyəti:</asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListMarriedStatus" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text="Subay" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Evli" Value="20"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <h3 style="margin-top: 20px; margin-bottom: 20px;">
                                    <asp:Literal ID="LtrSubTitle" runat="server">Övladlığa götürməyə arzu edilən uşağın əlamətləri</asp:Literal></h3>
                                <div class="col-md-4">
                                    <asp:Label ID="LblChildAge" CssClass="input-title" runat="server"> Yaşı:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListChildAge" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text=" -- " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="0-3 yaş" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="3-5 yaş" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="5-18 yaş" Value="30"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="LblChildGender" CssClass="input-title" runat="server"> Cinsi:<span style="color: red;">*</span></asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListChildGender" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text=" -- " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Qadın" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Kişi" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="LblHealthStatus" CssClass="input-title" runat="server"> Sağlamlıq vəziyyəti:</asp:Label><br />
                                    <br />
                                    <asp:DropDownList ID="DListHealthStatus" runat="server" Height="35px" CssClass="form-control input-filter form-inputs">
                                        <asp:ListItem Text="Sağlam" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Sağlamlıq imkanları məhdud" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="LblCause" CssClass="input-title" runat="server"> Övladlığa uşaq götürmənin səbəbi:</asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TxtCause" runat="server" CssClass="form-control form-inputs" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <br />


                                <div class="col-md-12">
                                    <asp:Panel ID="PnlDocID" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="LblDocIDTitle" runat="server" Text="Şəxsiyyət vəsiqəsinin surəti" Font-Bold="True"></asp:Label><br />
                                        <asp:FileUpload ID="FileUploadPassport" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="Label3" runat="server" Text="Qısa tərcümeyi hal" Font-Bold="True"></asp:Label><br />
                                        <asp:FileUpload ID="FileUploadTercumeHal" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel2" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="Label4" runat="server" Text="Övladlığa götürməyə imkan verməyən xəstəliklərin olmaması barədə tibbi arayış" Font-Bold="True"></asp:Label><br />
                                        <asp:FileUpload ID="FileUploadXestelik" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel3" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="Label5" runat="server" Text="Ümumi səhhəti barədə tibbi rəy" Font-Bold="True"></asp:Label><br />
                                        <asp:FileUpload ID="FileUploadTibb" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>  <div class="col-md-12">
                                    <asp:Panel ID="Panel4" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="Label6" runat="server" Text="Yaşayış sahəsi barədə məlumat" Font-Bold="True"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label7" runat="server" Font-Size="10pt" ForeColor="#666666" Text="(yaşayış sahəsinə mülkiyyət hüququnu təsdiqləyən sənəd )"></asp:Label>
                                        <br />
                                        <asp:FileUpload ID="FileUploadSahe" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="PnlMaritalStatusDoc" CssClass="pnl-fileupload" runat="server">
                                        <asp:Literal ID="LblMaritalStatusTitle" runat="server" Text="&lt;b&gt;Ailə vəziyyəti barədə məlumatı təsdiq edən sənəd&lt;/b&gt;"></asp:Literal>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="#666666" Text=" (nikah haqqında şəhadətnamənin və ya nikahın pozulmasi haqqında şəhadətnamənin surəti)"></asp:Label>
                                        <br />

                                        <asp:FileUpload ID="FileUploadAile" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="PnlDocWork" CssClass="pnl-fileupload" runat="server">
                                        <asp:Label ID="LblWorkTitle" runat="server" Text="İş yerini təsdiq edən sənəd" Font-Bold="True"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label2" runat="server" Font-Size="10pt" ForeColor="#666666" Text="(iş yerini, vəzifəsini, əmək haqqı və başqa gəlirlərini göstərən sənəd)"></asp:Label>
                                        <br />

                                        <asp:FileUpload ID="FileUploadIsYeri" CssClass="fileupload-section" runat="server" />
                                    </asp:Panel>
                                </div>



                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="g-recaptcha" data-sitekey="6LfCU0QUAAAAABsWDo8wFyxDogH7kGk0vrWaZeyX" style="margin-top: 25px; margin-left: 15px;"></div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <asp:Button ID="BtnConfirm" Text="TƏSDİQLƏ" Style="float: right" runat="server" CssClass="btn btn-default btn-appeal-confirm" OnClick="BtnConfirm_Click" OnClientClick="this.style.display='none';" />
                                </div>
                            </div>
                    </div>
                </div>
            </div>

        </div>
        </div>
    </form>
    <script>

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
