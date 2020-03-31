using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_130_Himaye : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsHimaye";

    string[] _SqlSelectColumns =
                               {
                                   "IsTeshkilat",
                                   "TeskilatOrSexsAdi",
                                   Config.SqlDateTimeFormat("MehrumOlmaTarixi"),
                                   "OvladligaGoturNovbeMelumat",
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
        DListMehrumOlmaTarixiD.DataSource = Config.NumericList(1, 31);
        DListMehrumOlmaTarixiD.DataBind();

        DListMehrumOlmaTarixiM.DataSource = Config.MonthList();
        DListMehrumOlmaTarixiM.DataBind();

        DListMehrumOlmaTarixiY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMehrumOlmaTarixiY.DataBind();
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
        if (!DALC._IsPermissionTypeEdit(250))
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

        // Təmizləmə işləri
        TxtHimayeName.Text = TxtOvladligaGoturNovbeMelumat.Text = TxtDescription.Text = "";
        DListFltrHimayeTeshkilatOrShex.SelectedValue = "0";
        DListMehrumOlmaTarixiD.SelectedValue = DListMehrumOlmaTarixiM.SelectedValue = "00";
        DListMehrumOlmaTarixiY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DListFltrHimayeTeshkilatOrShex.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Himayəyə götürənin təşkilat və ya şəxs olduğu mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (TxtHimayeName.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Himayəyə götürən təşkilatın və ya şəxsin adı mütləq qeyd olunmalıdır.", Page);
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
            LogText = "Himayəyə götürən təşkilat və ya şəxs haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Himayəyə götürən təşkilat və ya şəxs haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "IsTeshkilat", DListFltrHimayeTeshkilatOrShex.SelectedValue.NullConvert() },
            { "TeskilatOrSexsAdi", TxtHimayeName.Text.Trim() },
            { "MehrumOlmaTarixi", (DListMehrumOlmaTarixiY.SelectedValue + DListMehrumOlmaTarixiM.SelectedValue + DListMehrumOlmaTarixiD.SelectedValue).NullConvert() },
            { "OvladligaGoturNovbeMelumat", TxtOvladligaGoturNovbeMelumat.Text.Trim() },
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

        LnkSuccess.CommandArgument = "";

        DALC.UsersHistoryInsert(HistoryStatus, 250, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListFltrHimayeTeshkilatOrShex.SelectedValue = Dt._Rows("IsTeshkilat");
        TxtHimayeName.Text = Dt._Rows("TeskilatOrSexsAdi");
        DListMehrumOlmaTarixiD.SelectedValue = Dt._RowsObject("MehrumOlmaTarixi").IntDateDay();
        DListMehrumOlmaTarixiM.SelectedValue = Dt._RowsObject("MehrumOlmaTarixi").IntDateMonth();
        DListMehrumOlmaTarixiY.SelectedValue = Dt._RowsObject("MehrumOlmaTarixi").IntDateYear();
        TxtOvladligaGoturNovbeMelumat.Text = Dt._Rows("OvladligaGoturNovbeMelumat");
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
        DALC.UsersHistoryInsert(40, 250, string.Format("Himayəyə götürən təşkilat və ya şəxs haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}