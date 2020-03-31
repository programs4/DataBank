using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdoptionAdminn_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PnlLeft.Style.Remove("display");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "LeftPanelFixSize", "SetLeftBlackPanel();", true);

        if (!IsPostBack)
        {

        }
    }
}
