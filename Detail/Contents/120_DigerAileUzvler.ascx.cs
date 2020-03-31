using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_120_DigerAileUzvler : System.Web.UI.UserControl
{
    string _TableName = "PersonsRelatives";
    int _PersonsID = 0;

    void GridBind()
    {
        GrdList.DataSource = DALC.GetPersonsRelativesByPersonsID(_PersonsID);
        GrdList.DataBind();

        for (int i = 0; i < GrdList.Rows.Count; i++)
        {
            ((LinkButton)GrdList.Rows[i].Cells[GrdList.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Əminsiniz?');";
        }
    }

    #region Soragcalar
    //Sorgacalar

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

    public void BindMarriage()
    {
        DListMarriage.DataSource = DALC.GetMarriage();
        DListMarriage.DataBind();
        DListMarriage.Items.Insert(0, new ListItem("--", "0"));
    }
    #endregion

    public void PermissionSet(Panel P, LinkButton L, int PermissionID)
    {
        if (DALC._IsPermissionType(PermissionID))
        {
            P.Visible = true;

            //Edit imkani
            if (!DALC._IsPermissionTypeEdit(PermissionID))
            {
                P.Enabled = false;
                L.Visible = false;
            }
        }
        else
        {
            P.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        PermissionSet(PnlSAA, LnkSuccess1, 180);
        PermissionSet(PnlSAA, LnkAdd, 180);

        if (LnkAdd.Visible == false)
            GrdList.Columns[GrdList.Columns.Count - 1].Visible = false;

        PermissionSet(PnlYasadigiUnvan, LnkSuccess2, 190);
        PermissionSet(PnlYasadigiUnvan, LnkCancel2, 190);

        if (!IsPostBack)
        {
            GridBind();
            BindMarriage();
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        Countries();

        // Təmizləmə işləri
        TxtPin.Text = TxtAd.Text = TxtSoyad.Text = TxtAta.Text = txtUnvan.Text = TxtDescription.Text = "";
        DlistCity.SelectedValue = DlistCountry.SelectedValue = DListRelativTypes.SelectedValue = DListMarriage.SelectedValue = "0";

        GrdList.SelectedIndex = -1;
        PnlRoot.Visible = true;
        LnkCancel1.Visible = LnkSuccess1.Visible;
        LnkCancel2.Visible = LnkSuccess2.Visible;
    }

    protected void LnkSuccess1_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;
        string Lnk = (sender as LinkButton).CommandArgument;

        if (DListRelativTypes.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Qohumluq dərəcəsi mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (Lnk == "1")
        {
            if (TxtAd.Text.Trim().Length < 3)
            {
                Config.MsgBoxAjax("Ad mütləq qeyd olunmalıdır.", Page);
                return;
            }
            if (TxtSoyad.Text.Trim().Length < 3)
            {
                Config.MsgBoxAjax("Soyad mütləq qeyd olunmalıdır.", Page);
                return;
            }
        }
        else
        {
            if (DlistCountry.SelectedValue == "0" && DlistCity.SelectedValue == "0" && string.IsNullOrEmpty(txtUnvan.Text.Trim()))
            {
                Config.MsgBoxAjax("Yaşadığı ünvan haqqında məlumatlardan ən az biri mütləq qeyd olunmalıdır.", Page);
                return;
            }
        }

        if (GrdList.SelectedIndex == -1)
        {
            int ChekID = DALC.NewBlankRelatives(_PersonsID, int.Parse(DListRelativTypes.SelectedValue));
            if (ChekID < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            ID = ChekID._ToString();
            LogText = "Digər ailə üzvlərinin {0} məlumatları əlavə edildi. № {1}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Digər ailə üzvlərinin {0} məlumatlarında düzəliş edildi. № {1}";
            HistoryStatus = 30;
        }

        int UsersPermissionTypesID = 0;
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        if (Lnk == "1")
        {
            UsersPermissionTypesID = 180;
            Dictionary.Add("FIN", TxtPin.Text.Trim());
            Dictionary.Add("Ad", TxtAd.Text.Trim());
            Dictionary.Add("Soyad", TxtSoyad.Text.Trim());
            Dictionary.Add("Ata", TxtAta.Text.Trim());
            Dictionary.Add("PersonsRelativesTypesID", DListRelativTypes.SelectedValue.NullConvert());
            Dictionary.Add("PersonsRelativesMarriageID", DListMarriage.SelectedValue.NullConvert());
            Dictionary.Add("Description", TxtDescription.Text.Trim());
        }
        else
        {
            UsersPermissionTypesID = 190;
            Dictionary.Add("YasadigiUnvanCountriesID", DlistCountry.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvanRegionsID", DlistCity.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvan", txtUnvan.Text.Trim());
        }

        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        Dictionary.Add("WhereID", int.Parse(ID));

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        LnkAdd.CommandArgument = "";

        DALC.UsersHistoryInsert(HistoryStatus, UsersPermissionTypesID, string.Format(LogText, UsersPermissionTypesID == 180 ? "fərdi" : "yaşadığı ünvan", ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlRoot.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlRoot.Visible = true;
        Countries();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtPin.Text = Dt._Rows("FIN");
        DListRelativTypes.SelectedValue = Dt._Rows("PersonsRelativesTypesID");
        TxtAd.Text = Dt._Rows("Ad");
        TxtSoyad.Text = Dt._Rows("Soyad");
        TxtAta.Text = Dt._Rows("Ata");
        DListMarriage.SelectedValue = Dt._Rows("PersonsRelativesMarriageID").IsEmptyReplaceNoul(); ;
        TxtDescription.Text = Dt._Rows("Description");

        DlistCountry.SelectedValue = Dt._Rows("YasadigiUnvanCountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("YasadigiUnvanRegionsID").IsEmptyReplaceNoul();
        txtUnvan.Text = Dt._Rows("YasadigiUnvan");

        PnlRoot.Visible = true;
        LnkCancel1.Visible = LnkSuccess1.Visible;
        LnkCancel2.Visible = LnkSuccess2.Visible;
    }

    protected void GrdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = GrdList.DataKeys[e.RowIndex]["ID"]._ToInt32();
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("IsDeleted", 1);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        Dictionary.Add("WhereID", ID);

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        PnlRoot.Visible = false;
        GrdList.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 180, string.Format("Digər ailə üzvləri haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    protected void LnkCancel1_Click(object sender, EventArgs e)
    {
        PnlRoot.Visible = false;
    }
}