using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportsList_Controls_Hesabat3 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GrdList.DataSource = DALC.GetReportsInfo();
            GrdList.DataBind();
        }
    }
}