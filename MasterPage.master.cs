using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PnlLeft.Style.Remove("display");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LeftPanelFixSize", "SetLeftBlackPanel();", true);

        LtrHistory.Visible = DALC._IsPermissionType(30);

        if (!IsPostBack)
        {
            LtrUsersName.Text = DALC._GetUsersLogin.Fullname.ToUpper();
            LtrOrganizationName.Text = DALC._GetUsersLogin.OrganizationsName;
            LblNotify.Text = DALC.GetUsersHistoryCount();
            LblNotify.Visible = LblNotify.Text != "0";
        }
    }
}
