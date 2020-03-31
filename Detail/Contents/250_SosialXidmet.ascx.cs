using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_250_SosialXidmet : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsSosialXidmet";

    string[] _SqlSelectColumns =
                               {
                                   "Adi",
                                   "Sebebi",
                                   "(Select Name From PersonsSosialXidmetNov Where ID=PersonsSosialXidmetNovID) as XidmetNovu",
                                   "MuessiseAdi",
                                   Config.SqlAddressColumnsFormat("MuessiseCountriesID", "MuessiseRegionsID", "MuessiseUnvan", "Unvan"),
                                   Config.SqlDateTimeFormat("MonitorinqTarixi"),
                                   "MonitorinqNeticesi",
                                   Config.SqlDateTimeFormat("XidmetBaslamaTarix"),
                                   Config.SqlDateTimeFormat("XidmetBitmeTarix"),
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
        DListMonitoringD.DataSource = DListXidmetBaslamaD.DataSource = DListXidmetBitmeD.DataSource = Config.NumericList(1, 31);
        DListMonitoringD.DataBind();
        DListXidmetBaslamaD.DataBind();
        DListXidmetBitmeD.DataBind();

        DListMonitoringM.DataSource = DListXidmetBaslamaM.DataSource = DListXidmetBitmeM.DataSource = Config.MonthList();
        DListMonitoringM.DataBind();
        DListXidmetBaslamaM.DataBind();
        DListXidmetBitmeM.DataBind();

        DListMonitoringY.DataSource = DListXidmetBaslamaY.DataSource = DListXidmetBitmeY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMonitoringY.DataBind();
        DListXidmetBaslamaY.DataBind();
        DListXidmetBitmeY.DataBind();
    }

    public void BindXidmetNov()
    {
        DListXidmetNov.DataSource = DALC.GetSoragcaByTableName("PersonsSosialXidmetNov");
        DListXidmetNov.DataBind();
        DListXidmetNov.Items.Insert(0, new ListItem("--", "0"));
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
        if (!DALC._IsPermissionTypeEdit(370))
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
        BindXidmetNov();

        // Təmizləmə işləri
        TxtSosialAdi.Text = TxtSebeb.Text = TxtMuessiseAdi.Text = TxtUnvan.Text = TxtUnvan.Text = TxtMonitoringNeticesi.Text = TxtDescription.Text = "";
        DlistCountry.SelectedValue = DlistCity.SelectedValue = DListXidmetNov.SelectedValue = "0";
        DListXidmetBaslamaD.SelectedValue = DListXidmetBaslamaM.SelectedValue = DListXidmetBitmeD.SelectedValue = DListXidmetBitmeM.SelectedValue = DListMonitoringD.SelectedValue = DListMonitoringM.SelectedValue = "00";
        DListMonitoringY.SelectedValue = DListXidmetBaslamaY.SelectedValue = DListXidmetBitmeY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtSosialAdi.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Sosial xidmətin adı mütləq qeyd olunmalıdır.", Page);
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

            LogText = "Uşağa və onun valideynlərinə yönləndirilmiş sosial xidmətlər üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Uşağa və onun valideynlərinə yönləndirilmiş sosial xidmətlər üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Adi", TxtSosialAdi.Text.Trim() },
            { "Sebebi", TxtSebeb.Text.Trim() },
            { "PersonsSosialXidmetNovID", DListXidmetNov.SelectedValue.NullConvert() },
            { "MuessiseAdi", TxtMuessiseAdi.Text.Trim() },
            { "MuessiseCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "MuessiseRegionsID", DlistCity.SelectedValue.NullConvert() },
            { "MuessiseUnvan", TxtUnvan.Text.Trim() },
            { "MonitorinqTarixi", (DListMonitoringY.SelectedValue + DListMonitoringM.SelectedValue + DListMonitoringD.SelectedValue).NullConvert() },
            { "MonitorinqNeticesi", TxtMonitoringNeticesi.Text.Trim() },
            { "XidmetBaslamaTarix", (DListXidmetBaslamaY.SelectedValue + DListXidmetBaslamaM.SelectedValue + DListXidmetBaslamaD.SelectedValue).NullConvert() },
            { "XidmetBitmeTarix", (DListXidmetBitmeY.SelectedValue + DListXidmetBitmeM.SelectedValue + DListXidmetBitmeD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 370, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();
        Countries();
        BindXidmetNov();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtSosialAdi.Text = Dt._Rows("Adi");
        TxtSebeb.Text = Dt._Rows("Sebebi");
        DListXidmetNov.SelectedValue = Dt._Rows("PersonsSosialXidmetNovID").IsEmptyReplaceNoul();
        TxtMuessiseAdi.Text = Dt._Rows("MuessiseAdi");
        DlistCountry.SelectedValue = Dt._Rows("MuessiseCountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("MuessiseRegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("MuessiseUnvan");
        DListMonitoringD.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateDay();
        DListMonitoringM.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateMonth();
        DListMonitoringY.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateYear();
        TxtMonitoringNeticesi.Text = Dt._Rows("MonitorinqNeticesi");
        DListXidmetBaslamaD.SelectedValue = Dt._RowsObject("XidmetBaslamaTarix").IntDateDay();
        DListXidmetBaslamaM.SelectedValue = Dt._RowsObject("XidmetBaslamaTarix").IntDateMonth();
        DListXidmetBaslamaY.SelectedValue = Dt._RowsObject("XidmetBaslamaTarix").IntDateYear();
        DListXidmetBitmeD.SelectedValue = Dt._RowsObject("XidmetBitmeTarix").IntDateDay();
        DListXidmetBitmeM.SelectedValue = Dt._RowsObject("XidmetBitmeTarix").IntDateMonth();
        DListXidmetBitmeY.SelectedValue = Dt._RowsObject("XidmetBitmeTarix").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 370, string.Format("Uşağa və onun valideynlərinə yönləndirilmiş sosial xidmətlər üzrə məlumat silindi. № {0}", ID), _PersonsID);
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