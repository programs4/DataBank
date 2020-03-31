using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetAdministratorsLogin == null)
        {
            Config.Redirect("/adminn/?return=" + Request.Url.ToString());
            return;
        }
        string UserControlName = "main";

        if (Config._GetQueryString("p").Length > 0)
            UserControlName = Config._GetQueryString("p");
        try
        {
            PanelControl.Controls.Add(Page.LoadControl(UserControlName + ".ascx"));
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("Admin - User control tapilmadi: Catch error: " + er.Message);
            Response.Write("Xəta: " + er.Message);
            Response.End();
        }
    }

    protected void LnkChangePage_Click(object sender, EventArgs e)
    {
        Config.Redirect("?p=" + ((sender as LinkButton).CommandArgument));
        return;
    }

    protected void LinkButtonExit_Click(object sender, EventArgs e)
    {
        Config.Redirect("/exit");
    }
}