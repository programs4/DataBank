<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError().GetBaseException();
        DALC.ErrorLogsInsert("DataBank - Global.asax sehv verdi: " + ex.Message + "  |  Source: " + ex.Source + "  |  Url: " + HttpContext.Current.Request.Url.ToString(), true);

        //Master Page-də səhv çıxan da 
        if (Request.Url.ToString().ToLower().IndexOf("/error") > -1)
        {
            Response.Write("Xəta baş verdi ...");
            Response.End();
            return;
        }

        Config.RedirectError();
        return;
    }

</script>
