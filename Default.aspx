<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AdminLogin"] = null;
        Session["UsersLogin"] = null;
        Session.Clear();
        Session.RemoveAll();
        Application.Clear();

        Config.Redirect("/login/?k=" + Config.Key(50) + "&return=" + Config._GetQueryString("return"));
    }
</script>

