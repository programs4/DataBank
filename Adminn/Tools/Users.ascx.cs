using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_Users : UserControl
{
    void ShowModal()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#myModal').modal('show');", true);
    }

    void BindIp(int UsersID)
    {
        DataTable Dt = DALC.GetUsersPermissionIPList(UsersID);

        GrdIP.DataSource = Dt;
        GrdIP.DataBind();

        if (GrdIP.Rows.Count > 0)
        {
            for (int i = 0; i < GrdIP.Rows.Count; i++)
            {
                ((LinkButton)GrdIP.Rows[i].Cells[GrdIP.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Silmək istədiyinizə əminsinizmi?');";
            }
        }
    }

    void BindRegions()
    {
        DataTable Dt = DALC.GetRegionsList();
        GrdRegions.DataSource = Dt;
        GrdRegions.DataBind();
    }

    void BindTypes()
    {
        DataTable Dt = DALC.GetUsersPermissionTypesList();
        GrdTypes.DataSource = Dt;
        GrdTypes.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PnlPermission.Height = Unit.Pixel(700);
            PnlDiscussion.Height = Unit.Pixel(700);

            DListOrganization.DataSource = DALC.GetOrganizations();
            DListOrganization.DataBind();
            DListOrganization.Items.Insert(0, new ListItem("--", "0"));

            DListFilterOrganizations.DataSource = DALC.GetOrganizations();
            DListFilterOrganizations.DataBind();
            DListFilterOrganizations.Items.Insert(0, new ListItem("--", "0"));

            LnkOtherApp_Click(null, null);
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        LnkOtherApp.CommandArgument = "0";
        LnkOtherApp_Click(null, null);
    }

    protected void LnkOtherApp_Click(object sender, EventArgs e)
    {
        GrdList.DataSource = null;
        GrdList.DataBind();

        LnkOtherApp.Visible = false;
        LblCount.Text = "Axtarış üzrə: 0      Səhifə üzrə: 0";
        LnkOtherApp.CommandArgument = (int.Parse(LnkOtherApp.CommandArgument) + 20)._ToString();

        string PassNumber = "0";

        if (TxtFilterPassportNumber.Text.Trim().IsNumeric())
            PassNumber = TxtFilterPassportNumber.Text.Trim();

        DALC.DataTableResult UsersList = DALC.GetUsersList(
        LnkOtherApp.CommandArgument,
        DListFilterOrganizations.SelectedValue,
        TxtFilterUsername.Text,
        PassNumber,
        TxtFilterFullname.Text.Trim(),
        DListFilterStatus.SelectedValue);

        if (UsersList.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdList.DataSource = UsersList.Dt;
        GrdList.DataBind();

        LblCount.Text = "Axtarış üzrə: " + UsersList.Count + "      Səhifə üzrə: " + GrdList.Rows.Count._ToString();

        if (UsersList.Dt.Rows.Count > 0)
            LnkOtherApp.Visible = (GrdList.Rows.Count < UsersList.Count);
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        LtrModalHeader1.Text = "Yeni istifadəçi";
        BtnSaveUsers.CommandArgument = "";
        PnlResetPassword.Visible = false;
        TxtUsername.Enabled = true;
        PnlPassword.Visible = true;
        DListOrganization.SelectedValue = "0";
        TxtUsername.Text = TxtPass.Text = TxtPassportNumber.Text = TxtFin.Text = TxtFullname.Text = TxtEmail.Text = TxtContacts.Text = TxtDescription.Text = "";
        ShowModal();
    }

    protected void BtnSaveUsers_Click(object sender, EventArgs e)
    {
        if (DListOrganization.SelectedValue == "0")
        {
            ShowModal();
            Config.MsgBoxAjax("Qurumun adını seçin.", Page);
            return;
        }

        // Əgər yeni istifadəçi əlavə edilirsə login passüordu yoxlayaq
        if (string.IsNullOrEmpty(BtnSaveUsers.CommandArgument))
        {
            TxtUsername.Text = TxtUsername.Text.Trim();

            if (TxtUsername.Text.Length < 4)
            {
                ShowModal();
                TxtUsername.Focus();
                Config.MsgBoxAjax("İstifadəçi adı minimum 4 simvoldan ibarət olmalıdır.", Page);
                return;
            }

            if (TxtUsername.Text.Length > 25)
            {
                ShowModal();
                TxtUsername.Focus();
                Config.MsgBoxAjax("İstifadəçi adı maksimum 25 simvoldan ibarət olmalıdır.", Page);
                return;
            }

            string LoginAllowChars = "qazwsxedcrfvtgbyhnujmikolpQAZWSXEDCRFVTGBYHNUJMIKOLP0123456789._";
            for (int i = 0; i < TxtUsername.Text.Length; i++)
            {
                //İlk və son simvollar ancaq rəqəm hərf olsun.
                if (i == 0)
                {
                    if (TxtUsername.Text.Substring(i, 1) == "." || TxtUsername.Text.Substring(i, 1) == "_")
                    {
                        ShowModal();
                        TxtUsername.Focus();
                        Config.MsgBoxAjax("İstifadəçi adının ilk simvolu rəqəm və ya hərf tipli olmalıdır.", Page);
                        return;
                    }
                }

                if (LoginAllowChars.IndexOf(TxtUsername.Text.Substring(i, 1)) < 0)
                {
                    ShowModal();
                    TxtUsername.Focus();
                    Config.MsgBoxAjax("İstifadəçi adının tərkibində yalnız rəqəm (0-9), hərf (a-z), nöqtə (.) və alt xətt (_) qəbul edilir.", Page);
                    return;
                }

                if (i == (TxtUsername.Text.Length - 1))
                {
                    if (TxtUsername.Text.Substring(i, 1) == "." || TxtUsername.Text.Substring(i, 1) == "_")
                    {
                        ShowModal();
                        TxtUsername.Focus();
                        Config.MsgBoxAjax("İstifadəçi adının son simvolu rəqəm və ya hərf tipli olmalıdır.", Page);
                        return;
                    }
                }
            }

            //Login varmı:
            string LoginCount = DALC.GetDbSingleValuesParams("Count(*)", "Users", "IsActive=1 and Username", TxtUsername.Text, "", "-1");

            //Əgər mənfi bir olarsa serverdə yüklənmə var.
            if (LoginCount == "-1")
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            //Əgər mənfi bir olmasa keçir bura və əgər sıfır deyilsə deməli bazada var 1 yada daha çoxdu
            if (LoginCount != "0")
            {
                ShowModal();
                Config.MsgBoxAjax("Bu istifadəçi adı artıq qeydiyyatdan keçirilib.", Page);
                return;
            }

            if (TxtPass.Text.Length < 4)
            {
                ShowModal();
                Config.MsgBoxAjax("İstifadəçi şifrəsi ən az 4 simvoldan ibarət olmalıdır.", Page);
                return;
            }
            if (TxtPass.Text.Length > 20)
            {
                ShowModal();
                Config.MsgBoxAjax("İstifadəçi şifrəsi maksimum 20 simvoldan ibarət olmalıdır.", Page);
                return;
            }
        }

        if (TxtPassportNumber.Text.Length < 1)
        {
            ShowModal();
            Config.MsgBoxAjax("Şəxsiyyət vəsiqəsinin nömrəsini daxil edin.", Page);
            return;
        }

        if (!TxtPassportNumber.Text.IsNumeric())
        {
            ShowModal();
            Config.MsgBoxAjax("Şəxsiyyət vəsiqəsinin nömrəsi rəqəm tipli olmalıdır.", Page);
            return;
        }

        if (TxtFin.Text.Length != 7)
        {
            ShowModal();
            Config.MsgBoxAjax("Şəxsiyyət vəsiqəsinin FİN kodu 7 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (TxtFullname.Text.Length < 1)
        {
            ShowModal();
            Config.MsgBoxAjax("Soyadı, adı və atasının adını daxil edin.", Page);
            return;
        }

        if (!TxtEmail.Text.IsEmail())
        {
            ShowModal();
            Config.MsgBoxAjax("Elektron poçt ünvanını düzgün formatda daxil edin.", Page);
            return;
        }

        if (TxtContacts.Text.Trim().Length < 1)
        {
            ShowModal();
            Config.MsgBoxAjax("Telefon nömrəsini daxil edin.", Page);
            return;
        }

        if (!string.IsNullOrEmpty(BtnSaveUsers.CommandArgument))
        {
            Dictionary<string, object> DictUpdate = new Dictionary<string, object>();
            DictUpdate.Add("OrganizationsID", int.Parse(DListOrganization.SelectedValue));
            DictUpdate.Add("PassportNumber", int.Parse(TxtPassportNumber.Text.Trim()));
            DictUpdate.Add("Pin", TxtFin.Text.Trim());
            DictUpdate.Add("Fullname", TxtFullname.Text.Trim());
            DictUpdate.Add("Email", TxtEmail.Text.Trim());
            DictUpdate.Add("Contacts", TxtContacts.Text.Trim());
            DictUpdate.Add("Description", TxtDescription.Text.Trim());
            DictUpdate.Add("Add_Dt", DateTime.Now);
            DictUpdate.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            DictUpdate.Add("WhereID", int.Parse(BtnSaveUsers.CommandArgument));

            int Chek = DALC.UpdateDatabase("Users", DictUpdate);
            if (Chek < 1)
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            DALC.AdministratorsHistoryInsert("İstifadəçinin məlumatları yeniləndi. UsersID: " + BtnSaveUsers.CommandArgument);
        }
        else
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            Dictionary.Add("OrganizationsID", int.Parse(DListOrganization.SelectedValue));
            Dictionary.Add("Username", TxtUsername.Text.Trim());
            Dictionary.Add("Password", Config.SHA1Special(TxtPass.Text));
            Dictionary.Add("PassportNumber", int.Parse(TxtPassportNumber.Text.Trim()));
            Dictionary.Add("Pin", TxtFin.Text.Trim());
            Dictionary.Add("Fullname", TxtFullname.Text.Trim());
            Dictionary.Add("Email", TxtEmail.Text.Trim());
            Dictionary.Add("Contacts", TxtContacts.Text.Trim());
            Dictionary.Add("Description", TxtDescription.Text.Trim());
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            int UsersID = DALC.InsertDatabase("Users", Dictionary);
            if (UsersID < 1)
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            DALC.AdministratorsHistoryInsert("Yeni istifadəçi əlavə edildi. UsersID: " + UsersID.ToString());
        }

        LnkOtherApp_Click(null, null);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yertirildi.", Page, true);
    }

    protected void BtnResetPassword_Click(object sender, EventArgs e)
    {
        string ResetPassword = Config.Key(6);
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Password", ResetPassword.SHA1Special());
        Dictionary.Add("WhereID", int.Parse(BtnSaveUsers.CommandArgument));

        int Chek = DALC.UpdateDatabase("Users", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.AdministratorsHistoryInsert(BtnSaveUsers.CommandArgument + " № li İstifadəçinin şifərsi dəyişdirildi(Reset Password). Yeni şifrə: " + ResetPassword.Substring(0, 3) + "***");
        Config.MsgBoxAjax("İstfifadəçi şifrəsi yeniləndi. ", Page, true);
        LblPass.Text = "Yeni şifrə: <b>" + ResetPassword + "</b>";
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        ShowModal();
        LtrModalHeader1.Text = "Düzəliş";
        PnlResetPassword.Visible = true;
        string UsersID = BtnSaveUsers.CommandArgument = (sender as LinkButton).CommandArgument;
        DataTable Dt = DALC.GetUsersByID(UsersID);

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Məlumat tapılmadı.", Page);
            return;
        }

        TxtUsername.Enabled = false;
        PnlPassword.Visible = false;
        DListOrganization.SelectedValue = Dt._Rows("OrganizationsID");
        TxtUsername.Text = Dt._Rows("Username");
        TxtPassportNumber.Text = Dt._Rows("PassportNumber");
        TxtFin.Text = Dt._Rows("Pin");
        TxtFullname.Text = Dt._Rows("Fullname");
        TxtEmail.Text = Dt._Rows("Email");
        TxtContacts.Text = Dt._Rows("Contacts");
        TxtDescription.Text = Dt._Rows("Description");
        LblPass.Text = "";
    }

    protected void LnkPermission_Click(object sender, EventArgs e)
    {
        TxtIp.Text = "";

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#PermissionModal').modal('show');", true);

        int UsersID = (sender as LinkButton).CommandArgument._ToInt16();

        //Save də istifadə edək.
        BtnAllPermissionSave.CommandArgument = UsersID.ToString();

        DataTable DtUsersInfo = DALC.GetDataTableParams("*,(Select Name From Organizations Where ID=OrganizationsID) as OrganizationsName", "Users", "ID", UsersID, "");

        ChkRegions.Checked = !(bool)DtUsersInfo.Rows[0]["IsPermissionRegions"];
        ChkTypes.Checked = !(bool)DtUsersInfo.Rows[0]["IsPermissionTypes"];
        ChkIp.Checked = !(bool)DtUsersInfo.Rows[0]["IsPermissionIP"];

        LtrUserFullName.Text = DtUsersInfo.Rows[0]["FullName"]._ToString();
        LtrOrganizationsName.Text = DtUsersInfo.Rows[0]["OrganizationsName"]._ToString();

        ChkRegions_CheckedChanged(null, null);
        ChkTypes_CheckedChanged(null, null);
        ChkIp_CheckedChanged(null, null);

        BindRegions();
        BindTypes();
        BindIp(UsersID);

        string UsersPermissionID = "," + DALC.GetUsersPermissionTypesID(UsersID);
        string UsersPermissionIsEdit = "," + DALC.GetUsersPermissionTypesIDForEdit(UsersID);

        if (UsersPermissionID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        if (UsersPermissionIsEdit == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        for (int i = 0; i < GrdTypes.Rows.Count; i++)
        {
            DropDownList D = (DropDownList)GrdTypes.Rows[i].Cells[1].Controls[1];

            if (GrdTypes.DataKeys[i]["ID"]._ToInt16() < 100)
            {
                D.Items[1].Enabled = false;
                D.Items[2].Text = "İcazə ver";
            }

            if (UsersPermissionID.IndexOf("," + GrdTypes.DataKeys[i]["ID"]._ToString() + ",") > -1)
            {
                D.SelectedValue = "1";

                if (UsersPermissionIsEdit.IndexOf("," + GrdTypes.DataKeys[i]["ID"]._ToString() + ",") > -1)
                {
                    D.SelectedValue = "2";
                }
            }
        }

        string UsersPermissionRegions = "," + DALC.GetUsersPermissionRegions(UsersID);
        if (UsersPermissionRegions == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        for (int i = 0; i < GrdRegions.Rows.Count; i++)
        {
            if (UsersPermissionRegions.IndexOf("," + GrdRegions.DataKeys[i]["ID"]._ToString() + ",") > -1)
            {
                ((CheckBox)GrdRegions.Rows[i].Cells[1].Controls[1]).Checked = true;
            }
        }
    }

    protected void BtnIpAdd_Click(object sender, EventArgs e)
    {
        if (TxtIp.Text.Trim().Length < 1)
            return;

        if (TxtIp.Text.Trim().Length > 20)
        {
            Config.MsgBoxAjax("Ip 20 simvoldan böyük olmamalıdır.", Page);
            return;
        }

        string IpCount = DALC.GetDbSingleValuesMultiParams("Count(*)", "UsersPermissionIP", new string[] { "UsersID", "Ip" }, new object[] { BtnAllPermissionSave.CommandArgument, TxtIp.Text.Trim() }, "", "-1");
        if (IpCount == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        if (IpCount != "0")
        {
            Config.MsgBoxAjax("İstifadəçiyə bu Ip hüququ verilib.", Page);
            return;
        }
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("UsersID", int.Parse(BtnAllPermissionSave.CommandArgument));
        Dictionary.Add("Ip", TxtIp.Text.Trim());
        Dictionary.Add("Add_Dt", DateTime.Now);

        int ChekInsert = DALC.InsertDatabase("UsersPermissionIP", Dictionary);

        if (ChekInsert < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
        }
        else
        {
            Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
            BindIp(int.Parse(BtnAllPermissionSave.CommandArgument));
            DALC.AdministratorsHistoryInsert(BtnAllPermissionSave.CommandArgument + " № li istifadəçiyə " + TxtIp.Text.Trim() + " № li Ip üçün icazə verildi.");
        }
    }

    protected void ChkIp_CheckedChanged(object sender, EventArgs e)
    {
        PnlIp.Visible = !ChkIp.Checked;
    }

    protected void ChkRegions_CheckedChanged(object sender, EventArgs e)
    {
        PnlRegions.Visible = !ChkRegions.Checked;
    }

    protected void ChkTypes_CheckedChanged(object sender, EventArgs e)
    {
        PnlTypes.Visible = !ChkTypes.Checked;
    }

    protected void GrdIP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ChekDelete = DALC.DeleteUsersPermissionIP(GrdIP.DataKeys[e.RowIndex]["ID"]._ToInt32());
        if (ChekDelete < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        GrdIP.Rows[e.RowIndex].Visible = false;
        DALC.AdministratorsHistoryInsert(BtnAllPermissionSave.CommandArgument + " № li istifadəçinin " + GrdIP.DataKeys[e.RowIndex]["Ip"]._ToString() + " № li Ip-si ləğv edildi.");
    }

    public void BtnPermisSave_Click()
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("UsersID", typeof(int));
        Dt.Columns.Add("UsersPermissionTypesID", typeof(int));
        Dt.Columns.Add("IsEdit", typeof(bool));
        Dt.Columns.Add("Add_Dt", typeof(DateTime));
        Dt.Columns.Add("Add_Ip", typeof(int));

        for (int i = 0; i < GrdTypes.Rows.Count; i++)
        {
            DropDownList D = (DropDownList)GrdTypes.Rows[i].Cells[1].Controls[1];

            if (D.SelectedValue != "0")
            {
                Dt.Rows.Add(
                    int.Parse(BtnAllPermissionSave.CommandArgument),
                    GrdTypes.DataKeys[i]["ID"]._ToInt16(),
                    (D.SelectedValue == "2"),
                    DateTime.Now,
                    Request.UserHostAddress.IPToInteger());
            }
        }

        int ChekDelete = DALC.DeleteUsersPermission(false, int.Parse(BtnAllPermissionSave.CommandArgument));
        if (ChekDelete < 0)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count > 0)
        {
            int InsertChek = DALC.InsertBulk("UsersPermission", Dt);
            if (InsertChek < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
        }
    }

    public void BtnRegionsSave_Click()
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("UsersID", typeof(int));
        Dt.Columns.Add("RegionsID", typeof(int));
        Dt.Columns.Add("Add_Dt", typeof(DateTime));
        Dt.Columns.Add("Add_Ip", typeof(int));

        for (int i = 0; i < GrdRegions.Rows.Count; i++)
        {
            if (((CheckBox)GrdRegions.Rows[i].Cells[1].Controls[1]).Checked)
            {
                Dt.Rows.Add(
                    int.Parse(BtnAllPermissionSave.CommandArgument),
                    GrdRegions.DataKeys[i]["ID"]._ToInt32(),
                    DateTime.Now,
                    Request.UserHostAddress.IPToInteger());
            }
        }

        int ChekDelete = DALC.DeleteUsersPermission(true, int.Parse(BtnAllPermissionSave.CommandArgument));
        if (ChekDelete < 0)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        if (Dt.Rows.Count > 0)
        {
            int InsertChek = DALC.InsertBulk("UsersPermissionRegions", Dt);
            if (InsertChek < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
        }
    }

    protected void BtnAllPermissionSave_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("IsPermissionIP", !ChkIp.Checked);
        Dictionary.Add("IsPermissionRegions", !ChkRegions.Checked);
        Dictionary.Add("IsPermissionTypes", !ChkTypes.Checked);
        Dictionary.Add("WhereID", int.Parse(BtnAllPermissionSave.CommandArgument));
        int UpdateChek = DALC.UpdateDatabase("Users", Dictionary);
        if (UpdateChek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        BtnRegionsSave_Click();
        BtnPermisSave_Click();
        DALC.AdministratorsHistoryInsert(BtnAllPermissionSave.CommandArgument + " № li istifadəçinin hüquqlarında dəyişiklik edildi.");
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
    }
}