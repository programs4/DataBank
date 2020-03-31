using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Login control
        if (DALC._GetUsersLogin == null)
        {
            Config.Redirect("/?return=" + Request.Url.ToString());
            return;
        }

        if (!IsPostBack)
        {

            LblCount1.Text = DALC.GetDbSingleValues("Count(*)", "Persons","");
            LblCount2.Text = DALC.GetDbSingleValues("Count(*)", "Users", "Where IsActive=1");
            LblCount3.Text = DALC.GetDbSingleValues("Count(*)", "UsersHistory", "where YEAR(Add_Dt)=YEAR(GETDATE()) and MONTH(Add_Dt) = MONTH(GETDATE())");
            LblCount4.Text = DALC.GetDbSingleValues("Count(*)", "UsersHistory", "where  FORMAT(Add_Dt,'ddMMyyyy')=FORMAT(GETDATE(),'ddMMyyyy')");

            DALC.DataTableResult HistoryTypes = DALC.GetHistory("10", DALC._GetUsersLogin.OrganizationsID._ToString(), "0", "0", "0");
            GrdHistory.DataSource = HistoryTypes.Dt;
            GrdHistory.DataBind();
        }
    }
}