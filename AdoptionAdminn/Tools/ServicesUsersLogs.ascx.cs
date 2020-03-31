using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class AdoptionAdminn_Tools_ServicesUsersLogs : System.Web.UI.UserControl
{
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();
    private void BindData()
    {
        GrdUserLogs.DataSource = null;
        GrdUserLogs.DataBind();
        
        FilterDictionary = new Dictionary<string, object>()
        {
             {"PIN",TxtPin.Text},
             {"AdoptionOrganizationsID",DListOrganizations.SelectedValue},
             {"RegisterNo",TxtRegisterNo.Text},
             {"SearchParams(LIKE)",TxtContent.Text}
        };
        
        int PageNum;
        int RowNumber = 20;

        if (!int.TryParse(Config._GetQueryString("pn"), out PageNum))
        {
            PageNum = 1;
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult AdoptionSearchResult = DALC_Adoption.GetAdoptionSearchHistory(FilterDictionary, PageNum, RowNumber);

        if (AdoptionSearchResult.Count == -1)
        {
            return;
        }

        if (AdoptionSearchResult.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.Redirect(string.Format("/adoptionadminn/tools/?p=servicesuserslogs&pn={0}", PageNum - 1));
        }

        LblCount.Text = string.Format("Tapılıb: {0}", AdoptionSearchResult.Count.ToString());

        int Total_Count = AdoptionSearchResult.Count % RowNumber > 0 ? (AdoptionSearchResult.Count / RowNumber) + 1 : AdoptionSearchResult.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = AdoptionSearchResult.Count > RowNumber;

        GrdUserLogs.DataSource = AdoptionSearchResult.Dt;
        GrdUserLogs.DataBind();
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DListOrganizations.DataSource = DALC.GetDataTable("ID,Name", "AdoptionOrganizations", "Where AdoptionOrganizationsTypesID=20");
            DListOrganizations.DataBind();
            DListOrganizations.Items.Insert(0, new ListItem("- İcra Hakimiyyəti -", "-1"));
            BindData();
        }

    }

    
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}