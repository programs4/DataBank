using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_280_TerbiyeviTedbir : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsTerbiyeviTedbir";

    string[] _SqlSelectColumns =
                               {
                                   "MuessiseAdi",
                                   Config.SqlAddressColumnsFormat("MuessiseCountriesID", "MuessiseRegionsID", "MuessiseUnvan", "Unvan"),
                                   Config.SqlDateTimeFormat("TetbiqTarixi"),
                                   Config.SqlDateTimeFormat("MonitorinqTarixi"),
                                   "MonitorinqNeticesi",
                                   "Description"
                               };


    public void GridBind()
    {
        GrdList.DataSource = DALC.GridBindDataTable(_PersonsID, _TableName, _SqlSelectColumns);
        GrdList.DataBind();

        for (int i = 0; i < GrdList.Rows.Count; i++)
        {
            ((LinkButton)GrdList.Rows[i].Cells[GrdList.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Əminsiniz?');";
        }
    }

    #region Soragcalar
    //Sorgacalar
    public void Aylar()
    {
        DListMonitoringD.DataSource = DListTedbiqD.DataSource = Config.NumericList(1, 31);
        DListMonitoringD.DataBind();
        DListTedbiqD.DataBind();

        DListMonitoringM.DataSource = DListTedbiqM.DataSource = Config.MonthList();
        DListMonitoringM.DataBind();
        DListTedbiqM.DataBind();

        DListMonitoringY.DataSource = DListTedbiqY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMonitoringY.DataBind();
        DListTedbiqY.DataBind();
    }

    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistCountry.DataSource = Dt;
        DlistCountry.DataBind();
        DlistCountry.Items.Insert(0, new ListItem("--", "0"));

        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    public void Regions(DropDownList D, string CountryID)
    {
        if (CountryID == "0")
        {
            D.Enabled = false;
            D.DataSource = null;
            D.DataBind();
            D.Items.Insert(0, new ListItem("--", "0"));
            return;
        }
        else
        {
            D.Enabled = true;
            D.DataSource = DALC.GetRegionsList(CountryID);
            D.DataBind();
            D.Items.Insert(0, new ListItem("--", "0"));
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        if (!IsPostBack)
        {
            GridBind();
        }

        //Silinmə əməliyyatına icazə varmı?
        if (!DALC._IsPermissionTypeEdit(400))
        {
            LnkAdd.Visible = false;
            PnlEdit.Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 1].Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 2].Visible = false;
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        Aylar();
        Countries();

        // Təmizləmə işləri
        TxtMuessiseAdi.Text = TxtUnvan.Text = TxtMonitoringNeticesi.Text = TxtDescription.Text = "";
        DlistCountry.SelectedValue = DlistCity.SelectedValue = "0";
        DListTedbiqD.SelectedValue = DListTedbiqM.SelectedValue = DListMonitoringD.SelectedValue = DListMonitoringM.SelectedValue = "00";
        DListMonitoringY.SelectedValue = DListTedbiqY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtMuessiseAdi.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Müəssisə adı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (GrdList.SelectedIndex == -1)
        {
            ID = DALC.NewBlankInsert(_TableName, _PersonsID).ToString();
            if (int.Parse(ID) < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            LogText = "Tərbiyəvi tədbirlər üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Tərbiyəvi tədbirlər üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "MuessiseAdi", TxtMuessiseAdi.Text.Trim() },
            { "MuessiseCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "MuessiseRegionsID", DlistCity.SelectedValue.NullConvert() },
            { "MuessiseUnvan", TxtUnvan.Text.Trim() },
            { "TetbiqTarixi", (DListTedbiqY.SelectedValue + DListTedbiqM.SelectedValue + DListTedbiqD.SelectedValue).NullConvert() },
            { "MonitorinqTarixi", (DListMonitoringY.SelectedValue + DListMonitoringM.SelectedValue + DListMonitoringD.SelectedValue).NullConvert() },
            { "MonitorinqNeticesi", TxtMonitoringNeticesi.Text.Trim() },
            { "Description", TxtDescription.Text.Trim() },
            { "IsDeleted", 0 },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", int.Parse(ID) }
        };

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(HistoryStatus, 400, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();
        Countries();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtMuessiseAdi.Text = Dt._Rows("MuessiseAdi");
        DlistCountry.SelectedValue = Dt._Rows("MuessiseCountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("MuessiseRegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("MuessiseUnvan");
        DListTedbiqD.SelectedValue = Dt._RowsObject("TetbiqTarixi").IntDateDay();
        DListTedbiqM.SelectedValue = Dt._RowsObject("TetbiqTarixi").IntDateMonth();
        DListTedbiqY.SelectedValue = Dt._RowsObject("TetbiqTarixi").IntDateYear();
        TxtMonitoringNeticesi.Text = Dt._Rows("MonitorinqNeticesi");
        DListMonitoringD.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateDay();
        DListMonitoringM.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateMonth();
        DListMonitoringY.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateYear();
        TxtDescription.Text = Dt._Rows("Description");
        PnlEdit.Visible = true;
    }

    protected void GrdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = GrdList.DataKeys[e.RowIndex]["ID"]._ToInt32();

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "IsDeleted", 1 },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", ID }
        };

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        PnlEdit.Visible = false;
        GrdList.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 400, string.Format("Tərbiyəvi tədbirlər üzrə məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}