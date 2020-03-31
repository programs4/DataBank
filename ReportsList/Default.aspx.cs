using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportsList_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Login control
        if (DALC._GetUsersLogin == null)
        {
            Config.Redirect("/?return=" + Request.Url.ToString());
            return;
        }

        string UserControlName = "";

        if (Config._GetQueryString("p").Length > 0)
        {
            UserControlName += Config._GetQueryString("p");
            MultiView1.ActiveViewIndex = 1;

            try
            {
                PanelControl.Controls.Add(Page.LoadControl("controls/" + UserControlName + ".ascx"));
            }
            catch (Exception er)
            {
                DALC.ErrorLogsInsert("Reports - User control tapilmadi: Catch error: " + er.Message);
                Response.Write("Xəta: " + er.Message);
                Response.End();
            }
        }
    }
}