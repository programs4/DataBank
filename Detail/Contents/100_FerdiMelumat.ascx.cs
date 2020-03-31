using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_100_FerdiMelumat : System.Web.UI.UserControl
{
    int _PersonsID = 0;

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

    #region Soragcalar
    //Sorgacalar
    public void PasportTypes()
    {
        DataTable Dt = DALC.GetDocumentTypesChildren();

        DlistPassportTypeDs.DataSource = Dt;
        DlistPassportTypeDs.DataBind();
        DlistPassportTypeDs.Items.Insert(0, new ListItem("--", "0"));

        DataTable DtOther = DALC.GetDocumentTypesOther();

        DlistPassportTypesOther.DataSource = DtOther;
        DlistPassportTypesOther.DataBind();
        DlistPassportTypesOther.Items.Insert(0, new ListItem("--", "0"));

        DlistStatus.DataSource = DALC.GetPersonsStatus();
        DlistStatus.DataBind();
    }

    public void BindCitizenship()
    {
        DlistVetendasliq.DataSource = DALC.GetCitizenship();
        DlistVetendasliq.DataBind();
        DlistVetendasliq.Items.Insert(0, new ListItem("--", "0"));
    }

    public void Aylar()
    {
        DlistDogumD.DataSource = Config.NumericList(1, 31);
        DlistDogumD.DataBind();

        DlistDogumM.DataSource = Config.MonthList();
        DlistDogumM.DataBind();

        DlistDogumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DlistDogumY.DataBind();
    }

    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistCountry.DataSource = Dt;
        DlistCountry.DataBind();
        DlistCountry.Items.Insert(0, new ListItem("--", "0"));

        DlistCountry2.DataSource = Dt;
        DlistCountry2.DataBind();

        DlistCountry3.DataSource = Dt;
        DlistCountry3.DataBind();
        DlistCountry3.Items.Insert(0, new ListItem("--", "0"));
        Regions(DlistCity3, DlistCountry3.SelectedValue);

        //Yaşadığı ünvan daimi Azərbaycan olmalıdır.
        DlistCountry2.SelectedValue = "1";
        Regions(DlistCity2, DlistCountry2.SelectedValue);
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

        //Permission
        PermissionSet(PnlSehadet, LnkSuccess1, 110);
        PermissionSet(PnlPassport, LnkSuccess2, 120);
        PermissionSet(PnlInfo, LnkSuccess3, 100);
        PermissionSet(PnlYasadUnvan, LnkSuccess4, 130);

        //Silinənə access varsa statusla bağlı panel açılsın.
        PnlStatus.Visible = DALC._IsPermissionType(30);

        if (!IsPostBack)
        {
            PasportTypes();
            BindCitizenship();
            Aylar();
            Countries();

            //Bind forms
            DataTable Dt = DALC.GetDataTableParams("TOP 1 *", "Persons", "ID", _PersonsID, "");

            if (Dt == null || Dt.Rows.Count < 1)
            {
                Config.RedirectError();
                return;
            }

            DlistStatus.SelectedValue = Dt._Rows("PersonsStatusID").IsEmptyReplaceNoul();
            txtDescription.Text = Dt._Rows("Description");

            DlistPassportTypeDs.SelectedValue = Dt._Rows("DsDocumentTypesID").IsEmptyReplaceNoul();
            TxtDocNumberDs.Text = Dt._Rows("DsNomresi");

            DlistPassportTypesOther.SelectedValue = Dt._Rows("DocumentTypesID").IsEmptyReplaceNoul();
            TxtDocNumberOther.Text = Dt._Rows("DocumentNumber");

            txtPin.Text = Dt._Rows("FIN");
            txtAd.Text = Dt._Rows("Ad");
            txtSoyad.Text = Dt._Rows("Soyad");
            txtAta.Text = Dt._Rows("Ata");

            DlistVetendasliq.SelectedValue = Dt._Rows("CitizenshipID");

            DlistDogumD.SelectedValue = Dt._Rows("DogumTarixi").IntDateDay();
            DlistDogumM.SelectedValue = Dt._Rows("DogumTarixi").IntDateMonth();
            DlistDogumY.SelectedValue = Dt._Rows("DogumTarixi").IntDateYear();

            DlistGender.SelectedValue = Dt._Rows("Gender");

            DlistCountry.SelectedValue = Dt._Rows("DogumQeydiyyatYeriCountriesID").IsEmptyReplaceNoul();
            DlistCountry_SelectedIndexChanged(null, null);

            DlistCity.SelectedValue = Dt._Rows("DogumQeydiyyatYeriRegionsID").IsEmptyReplaceNoul();
            txtUnvan.Text = Dt._Rows("DogumQeydiyyatYeriUnvan");

            DlistCity2.SelectedValue = Dt._Rows("YasadigiUnvanRegionsID").IsEmptyReplaceNoul();
            txtUnvan2.Text = Dt._Rows("YasadigiUnvan");

            DlistCountry3.SelectedValue = Dt._Rows("MuveqqetiUnvanCountriesID").IsEmptyReplaceNoul(); ;
            Regions(DlistCity3, DlistCountry3.SelectedValue);
            DlistCity3.SelectedValue = Dt._Rows("MuveqqetiUnvanRegionsID").IsEmptyReplaceNoul(); ;
            txtUnvan3.Text = Dt._Rows("MuveqqetiUnvan");

            if (DlistCountry3.SelectedValue != "0" || DlistCity3.SelectedValue != "0" || !string.IsNullOrEmpty(txtUnvan3.Text))
            {
                ChkUnvan.Checked = true;
                PnlMuveqqetiYasadUnvan.Visible = true;
            }
        }
    }

    protected void LnkSuccessStatus_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "PersonsStatusID", DlistStatus.SelectedValue._ToInt16() },
            { "Description", txtDescription.Text },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int ChekUpdate = DALC.UpdateDatabase("Persons", Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        //Count cache clear
        Cache["ListCount"] = "";

        DALC.UsersHistoryInsert(40, 20, string.Format("Uşaqların statusu dəyişdirildi. № {0}", _PersonsID), _PersonsID);
        Config.MsgBoxAjax("Məlumatlar yeniləndi.", Page, true);
    }

    protected void LnkSuccess1_Click(object sender, EventArgs e)
    {
        if (TxtDocNumberDs.Text.Trim().Length > 0 && DlistPassportTypeDs.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Doğum şəhadətnaməsinin seriyasını seçin.", Page);
            return;
        }

        //Ədliyyə Nazirliyi dedi rəqəm olmamalıdı, başlığında X də olanlar var.
        //if (!TxtDocNumberDs.Text.IsNumeric())
        //{
        //    Config.MsgBoxAjax("Doğum şəhadətnaməsinin nömrəsi rəqəm tipdə olmalıdır.", Page);
        //    return;
        //}

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "DsDocumentTypesID", DlistPassportTypeDs.SelectedValue.NullConvert() },
            { "DsNomresi", TxtDocNumberDs.Text },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int Chek = DALC.UpdateDatabase("Persons", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(30, 110, "Doğum şəhadətnaməsinin məlumatları yeniləndi.", _PersonsID);
        Config.MsgBoxAjax("Məlumatlar yeniləndi.", Page, true);
    }

    protected void LnkSuccess2_Click(object sender, EventArgs e)
    {
        if (TxtDocNumberOther.Text.Trim().Length > 0 && DlistPassportTypesOther.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Digər sənəd növünü seçin.", Page);
            return;
        }

        //Ancaq AZE, MYİ, DYİ də rəqəm kontrol.
        if (DlistPassportTypesOther.SelectedValue == "10" || DlistPassportTypesOther.SelectedValue == "50" || DlistPassportTypesOther.SelectedValue == "60")
        {
            if (TxtDocNumberOther.Text.Trim().Length > 0 && !TxtDocNumberOther.Text.IsNumeric())
            {
                Config.MsgBoxAjax("Sənəd nömrəsi rəqəm tipində olmalıdır.", Page);
                return;
            }
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "DocumentTypesID", DlistPassportTypesOther.SelectedValue.NullConvert() },
            { "DocumentNumber", TxtDocNumberOther.Text },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int Chek = DALC.UpdateDatabase("Persons", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(30, 120, "Şəxsiyyət vəsiqəsinin məlumatları yeniləndi.", _PersonsID);
        Config.MsgBoxAjax("Məlumatlar yeniləndi.", Page, true);
    }

    protected void LnkSuccess3_Click(object sender, EventArgs e)
    {
        if (txtPin.Text.Trim().Length < 5)
        {
            Config.MsgBoxAjax("Uşağa məxsus FİN minimum 5 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (txtAd.Text.Trim().Length < 3)
        {
            Config.MsgBoxAjax("Uşağın adı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (txtSoyad.Text.Trim().Length < 3)
        {
            Config.MsgBoxAjax("Uşağın soyadı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (DlistDogumY.SelectedValue == "1000")
        {
            Config.MsgBoxAjax("Doğulduğu il mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (DlistGender.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Cinsi mütləq qeyd olunmalıdır", Page);
            return;
        }

        //FİN üzrə bazada məlumat varmı?
        string Check = DALC.GetSingleValuesBySqlCommand("Select Count(*) From Persons Where FIN=@FIN and ID <> @ID", "FIN,ID", new object[] { txtPin.Text.Trim(), _PersonsID });

        if (Check == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Check != "0")
        {
            Config.MsgBoxAjax("Daxil edilmiş FİN üzrə bazada məlumat mövcuddur.", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "FIN", txtPin.Text.Trim() },
            { "Soyad", txtSoyad.Text.Trim() },
            { "Ad", txtAd.Text.Trim() },
            { "Ata", txtAta.Text.Trim() },
            { "CitizenshipID", DlistVetendasliq.SelectedValue.NullConvert() },
            { "DogumTarixi", (DlistDogumY.SelectedValue + DlistDogumM.SelectedValue + DlistDogumD.SelectedValue).NullConvert() },
            { "Gender", DlistGender.SelectedValue.NullConvert() },
            { "DogumQeydiyyatYeriCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "DogumQeydiyyatYeriRegionsID", DlistCity.SelectedValue.NullConvert() },
            { "DogumQeydiyyatYeriUnvan", txtUnvan.Text.Trim() },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int Chek = DALC.UpdateDatabase("Persons", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(30, 100, "Fərdi məlumatları yeniləndi.", _PersonsID);
        Config.MsgBoxAjax("Məlumatlar yeniləndi.", Page, true);
    }

    protected void LnkSuccess4_Click(object sender, EventArgs e)
    {
        if (!ChkUnvan.Checked)
        {
            DlistCountry3.SelectedValue = "0";
            DlistCity3.SelectedValue = "0";
            txtUnvan3.Text = "";
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "YasadigiUnvanCountriesID", DlistCountry2.SelectedValue.NullConvert() },
            { "YasadigiUnvanRegionsID", DlistCity2.SelectedValue.NullConvert() },
            { "YasadigiUnvan", txtUnvan2.Text.Trim() },
            { "MuveqqetiUnvanCountriesID", DlistCountry3.SelectedValue.NullConvert() },
            { "MuveqqetiUnvanRegionsID", DlistCity3.SelectedValue.NullConvert() },
            { "MuveqqetiUnvan", txtUnvan3.Text.Trim() },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int Chek = DALC.UpdateDatabase("Persons", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(30, 130, "Yaşadığı / Qeydiyyatda olduğu ünvan yeniləndi.", _PersonsID);
        Config.MsgBoxAjax("Məlumatlar yeniləndi.", Page, true);
    }

    protected void ChkUnvan_CheckedChanged(object sender, EventArgs e)
    {
        PnlMuveqqetiYasadUnvan.Visible = ChkUnvan.Checked;
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    protected void DlistCountry3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity3, DlistCountry3.SelectedValue);
    }
}