using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_210_AsudeDuserge : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsAsudeDuserge";

    string[] _SqlSelectColumns =
                               {
                                   "Adi",
                                   Config.SqlAddressColumnsFormat("CountriesID", "RegionsID", "Unvan", "Unvan"),
                                   Config.SqlDateTimeFormat("GeldiyiTarix"),
                                   Config.SqlDateTimeFormat("GetdiyiTarix"),
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
        DListGetdiyiTarixD.DataSource = DListGeldiyiTarixD.DataSource = Config.NumericList(1, 31);
        DListGetdiyiTarixD.DataBind();
        DListGeldiyiTarixD.DataBind();

        DListGetdiyiTarixM.DataSource = DListGeldiyiTarixM.DataSource = Config.MonthList();
        DListGetdiyiTarixM.DataBind();
        DListGeldiyiTarixM.DataBind();

        DListGetdiyiTarixY.DataSource = DListGeldiyiTarixY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListGetdiyiTarixY.DataBind();
        DListGeldiyiTarixY.DataBind();
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
        if (!DALC._IsPermissionTypeEdit(330))
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
        TxtDusergeAdi.Text = TxtUnvan.Text = TxtDescription.Text = "";
        DlistCountry.SelectedValue = DlistCity.SelectedValue = "0";
        DListGeldiyiTarixD.SelectedValue = DListGeldiyiTarixM.SelectedValue = DListGetdiyiTarixD.SelectedValue = DListGetdiyiTarixM.SelectedValue = "00";
        DListGetdiyiTarixY.SelectedValue = DListGeldiyiTarixY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtDusergeAdi.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Düşərgə adı mütləq qeyd olunmalıdır.", Page);
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
            LogText = "Asudə düşərgə üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Asudə düşərgə üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Adi", TxtDusergeAdi.Text.Trim() },
            { "CountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "RegionsID", DlistCity.SelectedValue.NullConvert() },
            { "Unvan", TxtUnvan.Text.Trim() },
            { "GeldiyiTarix", (DListGeldiyiTarixY.SelectedValue + DListGeldiyiTarixM.SelectedValue + DListGeldiyiTarixD.SelectedValue).NullConvert() },
            { "GetdiyiTarix", (DListGetdiyiTarixY.SelectedValue + DListGetdiyiTarixM.SelectedValue + DListGetdiyiTarixD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 330, string.Format(LogText, ID), _PersonsID);
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

        TxtDusergeAdi.Text = Dt._Rows("Adi");
        DlistCountry.SelectedValue = Dt._Rows("CountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("RegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("Unvan");
        DListGeldiyiTarixD.SelectedValue = Dt._RowsObject("GeldiyiTarix").IntDateDay();
        DListGeldiyiTarixM.SelectedValue = Dt._RowsObject("GeldiyiTarix").IntDateMonth();
        DListGeldiyiTarixY.SelectedValue = Dt._RowsObject("GeldiyiTarix").IntDateYear();
        DListGetdiyiTarixD.SelectedValue = Dt._RowsObject("GetdiyiTarix").IntDateDay();
        DListGetdiyiTarixM.SelectedValue = Dt._RowsObject("GetdiyiTarix").IntDateMonth();
        DListGetdiyiTarixY.SelectedValue = Dt._RowsObject("GetdiyiTarix").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 330, "Asudə düşərgə üzrə məlumat silindi. № " + ID._ToString(), _PersonsID);
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