using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_290_HuquqpozmaQarsi : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsHuquqpozmaQarsi";

    string[] _SqlSelectColumns =
                               {
                                   "(Select Name From PersonsHuquqpozmaQarsiOrganNov Where ID=PersonsHuquqpozmaQarsiOrganNovID) as OrganNov",
                                   Config.SqlAddressColumnsFormat("OrganCountriesID", "OrganRegionsID", "OrganUnvan", "Unvan"),
                                   Config.SqlDateTimeFormat("QeydealinmaTarixi"),
                                   "(Select Name From PersonsHuquqpozmaQarsiToredenSexsNov Where ID=PersonsHuquqpozmaQarsiToredenSexsNovID) as ToredenSexsNov",
                                   "(Select Name From PersonsHuquqpozmaMecelleNov Where ID=PersonsHuquqpozmaMecelleNovID) as MecelleNov",
                                   "MehkemeAdi",
                                   "Madde",
                                   "MehkemeQerari",
                                   "MehkemeQerarininSayi",
                                   Config.SqlDateTimeFormat("MehkemeQerarininTarixi"),
                                   "(Select Name From PersonsHuquqpozmaQarsiToredenSexsCezaNov Where ID=PersonsHuquqpozmaQarsiToredenSexsCezaNovID) as ToredenSexsCezaNov",
                                   "ToredeneQarsiTedbirler",
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
        DListQeydealinmaTarixiD.DataSource = DListQerarD.DataSource = Config.NumericList(1, 31);
        DListQeydealinmaTarixiD.DataBind();
        DListQerarD.DataBind();

        DListQeydealinmaTarixiM.DataSource = DListQerarM.DataSource = Config.MonthList();
        DListQeydealinmaTarixiM.DataBind();
        DListQerarM.DataBind();

        DListQeydealinmaTarixiY.DataSource = DListQerarY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListQeydealinmaTarixiY.DataBind();
        DListQerarY.DataBind();
    }

    public void BindTypes()
    {
        DListOrgan.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaQarsiOrganNov");
        DListOrgan.DataBind();
        DListOrgan.Items.Insert(0, new ListItem("--", "0"));

        DListToreden.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaQarsiToredenSexsNov");
        DListToreden.DataBind();
        DListToreden.Items.Insert(0, new ListItem("--", "0"));

        DListMecelle.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaMecelleNov");
        DListMecelle.DataBind();
        DListMecelle.Items.Insert(0, new ListItem("--", "0"));

        DListCezaNov.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaQarsiToredenSexsCezaNov");
        DListCezaNov.DataBind();
        DListCezaNov.Items.Insert(0, new ListItem("--", "0"));
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
        if (!DALC._IsPermissionTypeEdit(410))
        {
            LnkAdd.Visible = false;
            PnlEdit.Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 1].Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 2].Visible = false;
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        BindTypes();
        Countries();
        Aylar();

        // Təmizləmə işləri
        TxtUnvan.Text = TxtMehkemeAdi.Text = TxtMadde.Text = TxtMehkemeQerari.Text = TxtMehkemeQerarSayi.Text = TxtToredeneQarsiTedbirler.Text = TxtDescription.Text = "";
        DListOrgan.SelectedValue = DlistCountry.SelectedValue = DlistCity.SelectedValue = DListToreden.SelectedValue = DListMecelle.SelectedValue = DListCezaNov.SelectedValue = "0";
        DListQeydealinmaTarixiD.SelectedValue = DListQeydealinmaTarixiM.SelectedValue = DListQerarD.SelectedValue = DListQerarM.SelectedValue = "00";
        DListQeydealinmaTarixiY.SelectedValue = DListQerarY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DListOrgan.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Hüquq mühafizə orqanı mütləq qeyd olunmalıdır.", Page);
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

            LogText = "Uşağa qarşı hüquqpozmalar üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Uşağa qarşı hüquqpozmalar üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "PersonsHuquqpozmaQarsiOrganNovID", DListOrgan.SelectedValue.NullConvert() },
            { "OrganCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "OrganRegionsID", DlistCity.SelectedValue.NullConvert()},
            { "OrganUnvan", TxtUnvan.Text.Trim() },
            { "QeydealinmaTarixi", (DListQeydealinmaTarixiY.SelectedValue + DListQeydealinmaTarixiM.SelectedValue + DListQeydealinmaTarixiD.SelectedValue).NullConvert() },
            { "PersonsHuquqpozmaQarsiToredenSexsNovID", DListToreden.SelectedValue.NullConvert() },
            { "PersonsHuquqpozmaMecelleNovID", DListMecelle.SelectedValue.NullConvert() },
            { "MehkemeAdi", TxtMehkemeAdi.Text.Trim() },
            { "Madde", TxtMadde.Text.Trim() },
            { "MehkemeQerari", TxtMehkemeQerari.Text.Trim() },
            { "MehkemeQerarininSayi", TxtMehkemeQerarSayi.Text.Trim() },
            { "MehkemeQerarininTarixi", (DListQerarY.SelectedValue + DListQerarM.SelectedValue + DListQerarD.SelectedValue).NullConvert() },
            { "PersonsHuquqpozmaQarsiToredenSexsCezaNovID", DListCezaNov.SelectedValue.NullConvert() },
            { "ToredeneQarsiTedbirler", TxtToredeneQarsiTedbirler.Text.Trim() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 410, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        BindTypes();
        Countries();
        Aylar();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListOrgan.SelectedValue = Dt._Rows("PersonsHuquqpozmaQarsiOrganNovID").IsEmptyReplaceNoul();
        DlistCountry.SelectedValue = Dt._Rows("OrganCountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("OrganRegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("OrganUnvan");
        DListQeydealinmaTarixiD.SelectedValue = Dt._RowsObject("QeydealinmaTarixi").IntDateDay();
        DListQeydealinmaTarixiM.SelectedValue = Dt._RowsObject("QeydealinmaTarixi").IntDateMonth();
        DListQeydealinmaTarixiY.SelectedValue = Dt._RowsObject("QeydealinmaTarixi").IntDateYear();
        DListToreden.SelectedValue = Dt._Rows("PersonsHuquqpozmaQarsiToredenSexsNovID").IsEmptyReplaceNoul();
        DListMecelle.SelectedValue = Dt._Rows("PersonsHuquqpozmaMecelleNovID").IsEmptyReplaceNoul();
        TxtMehkemeAdi.Text = Dt._Rows("MehkemeAdi");
        TxtMadde.Text = Dt._Rows("Madde");
        TxtMehkemeQerari.Text = Dt._Rows("MehkemeQerari");
        TxtMehkemeQerarSayi.Text = Dt._Rows("MehkemeQerarininSayi");
        DListQerarD.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateDay();
        DListQerarM.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateMonth();
        DListQerarY.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateYear();
        DListCezaNov.SelectedValue = Dt._Rows("PersonsHuquqpozmaQarsiToredenSexsCezaNovID").IsEmptyReplaceNoul();
        TxtToredeneQarsiTedbirler.Text = Dt._Rows("ToredeneQarsiTedbirler");
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
        DALC.UsersHistoryInsert(40, 410, string.Format("Uşağa qarşı hüquqpozmalar üzrə məlumat silindi. № {0}", ID._ToString()), _PersonsID);
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