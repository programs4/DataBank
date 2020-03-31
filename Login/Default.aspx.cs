using System;
using System.Data;

public partial class Login_Default : System.Web.UI.Page
{
    string Type = "";
    string Username = "";
    string Password = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AdminLogin"] = null;
        Session["UsersLogin"] = null;
        Session.Clear();
        Session.RemoveAll();
        Application.Clear();

        Type = Config._GetQueryString("t");

        DlistType.Visible = (Type == "a");

        if (Type == "a")
        {
            //Static key yoxluyaq.
            if (Config._GetQueryString("k") != "pJXazBZXqhfrQAnJNmjfFfphT2QZKsDuKsMsFgPdVyMrYTeGGk")
            {
                Config.RedirectError();
                return;
            }
        }

        if (!IsPostBack)
            TxtUsername.Focus();
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        Username = TxtUsername.Text;
        Password = TxtPassword.Text;

        if (string.IsNullOrEmpty(Username))
        {
            Config.MsgBoxAjax("İstifadəçi adı daxil edin.", Page);
            TxtUsername.Focus();
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            Config.MsgBoxAjax("Şifrə daxil edni.", Page);
            TxtPassword.Focus();
            return;
        }

        if (Type == "a")
        {
            if (DlistType.SelectedValue == "1")
            {
                AdministratorCheck();
            }
            else
            {
                AdoptionAdministratorCheck();
            }
        }
        else
        {
            UsersCheck();
        }
    }

    public void AdoptionAdministratorCheck()
    {
        DataTable Dt = DALC.GetDataTableMultiParams("*", "AdoptionAdministrators", new string[] { "Username", "Password", "IsActive" }, new object[] { Username, Password.SHA1Special(), true }, "");

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count != 1)
        {
            Config.MsgBoxAjax("Giriş baş tutmadı!", Page);
            return;
        }

        if (Dt._Rows("PermissionIP") != "*")
        {
            if (Dt._Rows("PermissionIP").IndexOf(Request.UserHostAddress) < 0)
            {
                Config.MsgBoxAjax("Mövcud IP üzrə sistemə giriş hüququnuz yoxdur.", Page);
                return;
            }
        }

        //Success
        DALC_Adoption.AdoptionAdministratorsInfoClass AdoptionAdministratorsInfoClass = new DALC_Adoption.AdoptionAdministratorsInfoClass();
        AdoptionAdministratorsInfoClass.ID = Dt._RowsObject("ID")._ToInt16();
        AdoptionAdministratorsInfoClass.Fullname = Dt._Rows("Fullname");

        Session["AdoptionAdminLogin"] = AdoptionAdministratorsInfoClass;
        DALC_Adoption.AdoptionAdministratorsHistoryInsert("Sistemə giriş etdi.");

        if (Config._GetQueryString("return").Length > 0)
            Config.Redirect(Config._GetQueryString("return"));
        else
            Config.Redirect("/adoptionadminn/tools/");
    }

    public void AdministratorCheck()
    {
        DataTable Dt = DALC.GetDataTableMultiParams("*", "Administrators", new string[] { "Username", "Password", "IsActive" }, new object[] { Username, Password.SHA1Special(), true }, "");

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count != 1)
        {
            Config.MsgBoxAjax("Giriş baş tutmadı!", Page);
            return;
        }

        bool IsPermissionIP = (bool)Dt._RowsObject("IsPermissionIP");
        if (IsPermissionIP)
        {
            string CountAllowIp = DALC.GetDbSingleValuesMultiParams("COUNT(*)", "AdministratorsPermissionIP", new string[] { "AdministratorsID", "Ip" }, new object[] { Dt._Rows("ID"), Request.UserHostAddress }, "", "-1");
            if (CountAllowIp == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            if (string.IsNullOrEmpty(CountAllowIp) || CountAllowIp == "0")
            {
                Config.MsgBoxAjax("Giriş baş tutmadı.", Page);
                return;
            }
        }

        //Success
        DALC.AdministratorsInfoClass AdminLoginInformation = new DALC.AdministratorsInfoClass();
        AdminLoginInformation.ID = Dt._RowsObject("ID")._ToInt16();
        AdminLoginInformation.Fullname = Dt._Rows("Fullname");
        AdminLoginInformation.Status = Dt._RowsObject("AdministratorsStatusID")._ToInt16();

        Session["AdminLogin"] = AdminLoginInformation;
        DALC.AdministratorsHistoryInsert("Sistemə giriş etdi.");

        if (Config._GetQueryString("return").Length > 0)
            Config.Redirect(Config._GetQueryString("return"));
        else
            Config.Redirect("/adminn/tools/");
    }

    public void UsersCheck()
    {
        DataTable Dt = DALC.GetDataTableMultiParams("*,(Select Name from Organizations Where ID=OrganizationsID) as OrganizationsName", "Users", new string[] { "Username", "Password", "IsActive" }, new object[] { Username, Password.SHA1Special(), true }, "");

        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count != 1)
        {
            Config.MsgBoxAjax("Giriş baş tutmadı!", Page);
            return;
        }

        bool IsPermissionIP = (bool)Dt._RowsObject("IsPermissionIP");
        bool IsPermissionRegions = (bool)Dt._RowsObject("IsPermissionRegions");
        bool IsPermissionTypes = (bool)Dt._RowsObject("IsPermissionTypes");

        if (IsPermissionIP)
        {
            string CountAllowIp = DALC.GetDbSingleValuesMultiParams("COUNT(*)", "UsersPermissionIP", new string[] { "UsersID", "Ip" }, new object[] { Dt._Rows("ID"), Request.UserHostAddress }, "", "-1");
            if (CountAllowIp == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            if (string.IsNullOrEmpty(CountAllowIp) || CountAllowIp == "0")
            {
                Config.MsgBoxAjax("Giriş baş tutmadı.", Page);
                return;
            }
        }

        DALC.UsersInfoClass UserInfo = new DALC.UsersInfoClass();
        UserInfo.ID = Dt._RowsObject("ID")._ToInt16();
        UserInfo.OrganizationsID = Dt._RowsObject("OrganizationsID")._ToInt16();
        UserInfo.OrganizationsName = Dt._Rows("OrganizationsName");
        UserInfo.Fullname = Dt._Rows("Fullname");
        UserInfo.LoginKey = Config.Key(20);

        if (IsPermissionRegions)
        {
            //Xususi icazeler
            UserInfo.PermissionRegions = "," + DALC.GetUsersPermissionRegions(UserInfo.ID);
            if (UserInfo.PermissionRegions == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            //Əgər hüquq xüsusi quş qoyub heçnə seçmiyibsə
            if (UserInfo.PermissionRegions.Length < 2)
            {
                UserInfo.PermissionRegions = ",0,";
                return;
            }
        }
        else
        {
            //Full icaze
            UserInfo.PermissionRegions = "*";
        }

        if (IsPermissionTypes)
        {
            //Xususi icazeler
            UserInfo.PermissionTypes = "," + DALC.GetUsersPermissionTypesID(UserInfo.ID);
            if (UserInfo.PermissionTypes == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            //Əgər hüquq xüsusi quş qoyub heçnə seçmiyibsə
            if (UserInfo.PermissionTypes.Length < 2)
            {
                UserInfo.PermissionTypes = ",0,";
                return;
            }

            //Editler
            UserInfo.PermissionTypesEdit = "," + DALC.GetUsersPermissionTypesIDForEdit(UserInfo.ID);
            if (UserInfo.PermissionTypesEdit == "-1")
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            //Əgər hüquq xüsusi quş qoyub heçnə seçmiyibsə
            if (UserInfo.PermissionTypesEdit.Length < 2)
            {
                UserInfo.PermissionTypesEdit = ",0,";
                return;
            }
        }
        else
        {
            //Full icaze
            UserInfo.PermissionTypes = "*";
            UserInfo.PermissionTypesEdit = "*";
        }

        Session["UsersLogin"] = UserInfo;
        DALC.UsersHistoryInsert(10, 0, "Sistemə giriş etdi.");

        if (Config._GetQueryString("return").Length > 0)
            Config.Redirect(Config._GetQueryString("return"));
        else
            Config.Redirect("/main/?" + Config.Key(50));
    }
}