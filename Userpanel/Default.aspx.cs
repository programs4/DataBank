using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class Userpanel_Default : System.Web.UI.Page
{
    enum EPersonsInfo
    {
        Email = 0,
        PhoneNumber = 1,
    }
    void BindUsersInfo()
    {
        DataTable Dt = DALC.GetUsersByID(DALC._GetUsersLogin.ID._ToString());

        TxtOrganizations.Text = Dt._Rows("Orgname");
        TxtPassportNumber.Text = Dt._Rows("PassportNumber");
        TxtFin.Text = Dt._Rows("Pin");
        TxtFullname.Text = Dt._Rows("Fullname");
        TxtEmail.Text = Dt._Rows("Email");
        TxtContacts.Text = Dt._Rows("Contacts");

        // Məlumatlar dəyişdirildikdə log tutmaq ucun ilkin məlumatlari alaq
        string[] PersonsInfo = { Dt._Rows("Email"), Dt._Rows("Contacts") };
        ViewState["UsersInfo"] = PersonsInfo;

    }

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
            BindUsersInfo();

            if (Config._GetQueryString("tab") == "1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", @" $(document).ready(function () {document.getElementById('rec-tab').click();});", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", @" $(document).ready(function () {document.getElementById('send-tab').click();});", true);
            }
        }
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {

        if (!Config.IsEmail(TxtEmail.Text))
        {
            Config.MsgBoxAjax("Elektron poçt ünvanını düzgün formatda daxil edin.", Page);
            return;
        }

        if (TxtContacts.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Telefon nömrəsini daxil edin.", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Email", TxtEmail.Text.Trim());
        Dictionary.Add("Contacts", TxtContacts.Text.Trim());
        Dictionary.Add("WhereID", DALC._GetUsersLogin.ID);

        int ChekUpdate = DALC.UpdateDatabase("Users", Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        else
        {
            // Məlumatlar dəyişdirildikdə log tutaq
            string[] PersonsInfo = ViewState["UsersInfo"] as string[];

            if (PersonsInfo[Convert.ToInt32(EPersonsInfo.Email)] != TxtEmail.Text)
            {
                DALC.UsersHistoryInsert(30, 0, "Şəxsi kabinetdən, email dəyişdirildi. Köhnə email: " + PersonsInfo[Convert.ToInt32(EPersonsInfo.Email)]);
            }

            if (PersonsInfo[Convert.ToInt32(EPersonsInfo.PhoneNumber)] != TxtContacts.Text)
            {
                DALC.UsersHistoryInsert(30, 0, "Şəxsi kabinetdən, telefon nömrəsi dəyişdirildi. Köhnə nömrə: " + PersonsInfo[Convert.ToInt32(EPersonsInfo.PhoneNumber)]);
            }

            BindUsersInfo();
            Config.MsgBoxAjax("Məlumatlar uğurla dəyişdirildi", Page, true);
        }

    }

    protected void BtnChangePass_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", @"$(document).ready(function(){$('#send-tab').click();});", true);

        if (TxtPassword.Text.Length < 4)
        {
            Config.MsgBoxAjax("Cari şifrənizi daxil edin.", Page);
            return;

        }

        if (TxtnewPass.Text.Length < 4)
        {
            Config.MsgBoxAjax("Yeni şifrəniz minimum 4 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (TxtnewPass.Text.Length > 20)
        {
            Config.MsgBoxAjax("Yeni şifrəniz maksimum 20 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (TxtnewPass.Text != TxtRepeatPass.Text)
        {
            Config.MsgBoxAjax("Yeni şifrəniz uyğun gəlmir.", Page);
            return;
        }

        string Password = DALC.GetDbSingleValuesParams("Password", "Users", "ID", DALC._GetUsersLogin.ID, "", "-1");
        if (Password.Length < 10)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Password != Config.SHA1Special(TxtPassword.Text))
        {
            Config.MsgBoxAjax("Cari şifrəniz doğru deyil.", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Password", Config.SHA1Special(TxtnewPass.Text));
        Dictionary.Add("WhereID", DALC._GetUsersLogin.ID);

        int ChekUpdate = DALC.UpdateDatabase("Users", Dictionary);

        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        else
        {
            Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
            DALC.UsersHistoryInsert(30, 0, "Şifrə dəyişdirildi. Köhnə şifrə: " + TxtPassword.Text);
        }
    }
}