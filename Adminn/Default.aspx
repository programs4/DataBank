<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.Redirect("/login/?t=a&k=pJXazBZXqhfrQAnJNmjfFfphT2QZKsDuKsMsFgPdVyMrYTeGGk&return=" + Config._GetQueryString("return"));
    }
    
</script>


