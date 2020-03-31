using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class E_Services_Adoption_Default : System.Web.UI.Page
{
    string TableName = "AdoptionPersons";
    string[] LoginInfo;
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();
    private void BindList()
    {
        DataTable DtContent = DALC.GetDataTable("*", "Contents", "Where ID=1");
        LtrContentTitle.Text = DtContent._Rows("Title");
        LtrContentText.Text = DtContent._Rows("ContentText");

        DListOrganizations.DataSource = DALC.GetDataTable("ID,Name", "AdoptionOrganizations", "Where AdoptionOrganizationsTypesID=20");
        DListOrganizations.DataBind();
        DListOrganizations.Items.Insert(0, new ListItem("- İcra Hakimiyyəti -", "-1"));

        DListEyeColor.DataSource = DALC.GetDataTable("ID,Name", "Colors", "");
        DListEyeColor.DataBind();
        DListEyeColor.Items.Insert(0, new ListItem("- Göz rəngi -", "-1"));

        DListHairColor.DataSource = DALC.GetDataTable("ID,Name", "Colors", "");
        DListHairColor.DataBind();
        DListHairColor.Items.Insert(0, new ListItem("- Saç rəngi -", "-1"));
    }
    //hansi filterler doldurulubsa onlari goturmek loglama ucun
    private void CheckControls()
    {
        string SearchFilters = "";
        foreach (Control item in PnlSearch.Controls)
        {
            if (item is TextBox)
            {
                if (!string.IsNullOrEmpty((item as TextBox).Text))
                {
                    SearchFilters += "Tam adı:" + (item as TextBox).Text;
                }
            }
            if (item is DropDownList)
            {
                if ((item as DropDownList).SelectedValue != "-1")
                {
                    string DListTitle = "";
                    if ((item as DropDownList).ID == "DListGender")
                    {
                        DListTitle = "Cinsi:";
                    }
                    if ((item as DropDownList).ID == "DListEyeColor")
                    {
                        DListTitle = "Göz rəngi:";
                    }
                    if ((item as DropDownList).ID == "DListHairColor")
                    {
                        DListTitle = "Saç rəngi:";
                    }
                    if ((item as DropDownList).ID == "DListAgeRange")
                    {
                        DListTitle = "Yaş aralığı:";
                    }
                    if ((item as DropDownList).ID == "DListBrotherSister")
                    {
                        DListTitle = "Qardaş, Bacı mövcudluğu:";
                    }
                    SearchFilters += DListTitle + (item as DropDownList).SelectedItem.Text + ",";
                }
            }
        }

        if (string.IsNullOrEmpty(SearchFilters))
        {
            SearchFilters = "Ümumi axtarış edildi.";
        }
        if (Session["IsSearch"] == null && !string.IsNullOrEmpty(Config._GetQueryString("pn")))
        {
            DALC_Adoption.SearchLogInsert(LoginInfo[0], LoginInfo[1], DALC_Adoption._GetUsersLogin.AdoptionOrganizationsID._ToString(), DALC_Adoption._GetUsersLogin.RegisterNo._ToString(), DALC_Adoption._GetUsersLogin.RegisterDate, "<b>Axtarış edildi</b> | " + SearchFilters.TrimEnd(','));
            Session["IsSearch"] = true;
        }
    }

    private void BindData()
    {
        RptChilds.DataSource = null;
        RptChilds.DataBind();

        PnlSearch.BindControls(FilterDictionary, TableName);

        FilterDictionary = new Dictionary<string, object>()
        {
             {"Gender",DListGender.SelectedValue},
             {"GozColorsID",int.Parse(DListEyeColor.SelectedValue)},
             {"SachColorsID",int.Parse(DListHairColor.SelectedValue)},
             {"FLOOR(DATEDIFF(DAY , DogumTarixi ,GETDATE()) / 365.25)(BETWEEN)",DListAgeRange.SelectedValue}
        };

        if (DListBrotherSister.SelectedIndex != 0)
        {
            FilterDictionary.Add("IsBrotherSister", Convert.ToByte(DListBrotherSister.SelectedValue));
        }

        if (TxtFullname.Text.Length > 0)
        {
            FilterDictionary.Add("CONCAT(Soyad,' ',Ad,' ',Ata)(LIKE)", TxtFullname.Text);
        }

        int PageNum;
        int RowNumber = 3;

        if (!int.TryParse(Config._GetQueryString("pn"), out PageNum))
        {
            PageNum = 1;
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult AdoptionPersonsResult = DALC_Adoption.GetAdoptionPersons(FilterDictionary, PageNum, RowNumber);

        if (AdoptionPersonsResult.Count == -1 || AdoptionPersonsResult.Dt == null)
        {
            Config.RedirectError();
            return;
        }

        if (AdoptionPersonsResult.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.Redirect(string.Format("/e-services/adoption/?p=adoptionsite&pn={0}", PageNum - 1));
        }

        LblCount.Text = string.Format("Tapılıb: {0} anket", AdoptionPersonsResult.Count.ToString());

        int Total_Count = AdoptionPersonsResult.Count % RowNumber > 0 ? (AdoptionPersonsResult.Count / RowNumber) + 1 : AdoptionPersonsResult.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = AdoptionPersonsResult.Count > RowNumber;

        RptChilds.DataSource = AdoptionPersonsResult.Dt;
        RptChilds.DataBind();

        CheckControls();

    }
    public void ErrorMessages(TextBox T, string Text)
    {
        if (T != null)
        {
            T.Focus();
            T.BorderColor = System.Drawing.Color.FromArgb(244, 67, 54);
        }
        PnlError.Visible = true;
        LtrError.Text = Text;
        return;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "JqueryModal", "CallDatePicker();", true);

        //Session kontrol!
        if (Session["EgovLogin"] == null)
        {
            Config.RedirectError();
            return;
        }
        if (MultiView1.ActiveViewIndex == 0)
        {
            Session["UsersLogin"] = null;
        }

        if (!IsPostBack)
        {
            LoginInfo = (string[])Session["EgovLogin"];

            if (string.IsNullOrEmpty(Config._GetQueryString("pn")) && DALC_Adoption._GetUsersLogin == null)
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;

            }
            BindList();
            BindData();
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PnlSearch.BindControls(FilterDictionary, TableName, true);
        Session["IsSearch"] = null;
        Cache.Remove(TableName._ToString());
        Config.Redirect("/e-services/adoption/?pn=1");
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        #region Validator Inputs
        DListOrganizations.BorderColor = TxtRegisterNo.BorderColor = TxtRegisterDt.BorderColor = System.Drawing.Color.Empty;
        if (DListOrganizations.SelectedValue == "-1")
        {
            DListOrganizations.Focus();
            DListOrganizations.BorderColor = System.Drawing.Color.FromArgb(244, 67, 54);
            PnlError.Visible = true;
            LtrError.Text = "İcra hakimiyyətini seçin.";
            return;
        }
        if (string.IsNullOrEmpty(TxtRegisterNo.Text))
        {
            ErrorMessages(TxtRegisterNo, "Uçot nömrəsi qeyd olunmayıb.");
            return;
        }
        if (!Config.IsNumeric(TxtRegisterNo.Text))
        {
            ErrorMessages(TxtRegisterNo, "Uçot nömrəsi düzgün formatda doldurulmayıb.");
            return;
        }
        if (string.IsNullOrEmpty(TxtRegisterDt.Text))
        {
            ErrorMessages(TxtRegisterDt, "Uçot tarixi qeyd olunmayıb.");
            return;
        }
        if (Config.DateFormatClear(TxtRegisterDt.Text) == null)
        {
            ErrorMessages(TxtRegisterDt, "Uçot tarixi düzgün formatda qeyd olunmayıb.");
            return;
        }

        #endregion

        LoginInfo = (string[])Session["EgovLogin"];
        string OrganizationsID = DListOrganizations.SelectedValue;
        DALC_Adoption.UsersInfo UsersInfo = new DALC_Adoption.UsersInfo();
        UsersInfo.AdoptionOrganizationsID = int.Parse(OrganizationsID);
        UsersInfo.RegisterNo = int.Parse(TxtRegisterNo.Text);
        UsersInfo.RegisterDate = (DateTime)Config.DateFormatClear(TxtRegisterDt.Text);

        DALC_Adoption.SearchLogInsert(LoginInfo[0], LoginInfo[1], OrganizationsID, TxtRegisterNo.Text, UsersInfo.RegisterDate, "<span style=\"color:green;font-weight:bold\">Xidmətə giriş edildi.</span>");
        Session["UsersLogin"] = UsersInfo;
        Config.Redirect("/e-services/adoption/?pn=1");
    }
}