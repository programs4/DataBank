<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Application.Clear();
        Config.Redirect("/");
    }
</script>

