<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["EgovLogin"] = null;
        Session.Clear();
        Session.RemoveAll();

        string Token = Request.QueryString["token"]._ToString().Replace(" ", "+");

        //if (Token.Length < 1)
        //{
        //    DALC.ErrorLogsInsert("Token length size", false);
        //    Config.Redirect("~/error/");
        //    return;
        //}

        //string Pin = "19VQAFP";
        //string AutentType = "eimza";

        //EgovToken.TokenService Ts = new EgovToken.TokenService();
        //EgovToken.TgetTokenParamsNewResponseResponse TokenResponse = new EgovToken.TgetTokenParamsNewResponseResponse();
        //try
        //{
        //    TokenResponse = Ts.TgetTokenParamsNew(Token);

        //    //False olduqda səhv yoxdur mənası çıxır.
        //    if (!TokenResponse.faultCode.HasValue)
        //    {
        //        Pin = TokenResponse.token.pin;
        //        AutentType = TokenResponse.token.authenticationTypeName;
        //    }
        //}
        //catch (Exception er)
        //{
        //    DALC.ErrorLogsInsert("Token catch error: " + er.Message + ", Token: " + Token, false);
        //    Config.Redirect("~/error/tokenerror");
        //    return;
        //}

        //****************** 


        //if (Pin.Length != 7)
        //{
        //    string Log = "";

        //    try
        //    {
        //        Log += (" FaultCode: " + TokenResponse.faultCode.Value);
        //        Log += (" FaultString: " + TokenResponse.faultString);
        //        Log += (" Token: " + Token);
        //    }
        //    catch (Exception er)
        //    {
        //        Log += ("Catch error: " + er.Message);
        //    }

        //    DALC.ErrorLogsInsert("Token pin size, error log: " + Log, false);
        //    Config.Redirect("~/error/tokenerror");
        //    return;
        //}

        //Vətəndaş kimi sesiya hazır.
        //Session["EgovLogin"] = new string[] { Pin, AutentType };

        //İcranın hansısa xidmətlərinə baxan admin
        //DALC.AdministratorLogsInsert(0, "Sistemə e-gov.az üzərindən giriş edildi. Autentifikasiya vasitəsi: " + AutentType, false);

        Config.Redirect("/e-services/" + Config._GetQueryString("s") + "/?lang=az" + Token);
    }
</script>
