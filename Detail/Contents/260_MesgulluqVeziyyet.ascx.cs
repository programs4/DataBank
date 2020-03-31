using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_260_MesgulluqVeziyyet : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsMesgulluqVeziyyet";

    string[] _SqlSelectColumns =
                               {
                                   "IsIsh",
                                   "IsYeri",
                                   Config.SqlDateTimeFormat("QeydiyyatTarixi"),
                                   Config.SqlDateTimeFormat("IsdenCixmaTarixi"),
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
        DListQeydiyyatTarixiD.DataSource = DListIshdenCixmaD.DataSource = Config.NumericList(1, 31);
        DListQeydiyyatTarixiD.DataBind();
        DListIshdenCixmaD.DataBind();

        DListQeydiyyatTarixiM.DataSource = DListIshdenCixmaM.DataSource = Config.MonthList();
        DListQeydiyyatTarixiM.DataBind();
        DListIshdenCixmaM.DataBind();

        DListQeydiyyatTarixiY.DataSource = DListIshdenCixmaY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListQeydiyyatTarixiY.DataBind();
        DListIshdenCixmaY.DataBind();
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
        if (!DALC._IsPermissionTypeEdit(380))
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
        DListIsh.SelectedValue = "0";
        TxtIshYeri.Text = TxtDescription.Text = "";
        DListQeydiyyatTarixiD.SelectedValue = DListQeydiyyatTarixiM.SelectedValue = DListIshdenCixmaD.SelectedValue = DListIshdenCixmaM.SelectedValue = "00";
        DListQeydiyyatTarixiY.SelectedValue = DListIshdenCixmaY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DListIsh.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Məşğulluq vəziyyəti mütləq qeyd olunmalıdır.", Page);
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
            LogText = "15 yaşından yuxarı uşağın məşğulluq vəziyyəti haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "15 yaşından yuxarı uşağın məşğulluq vəziyyəti haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "IsIsh", DListIsh.SelectedValue.NullConvert() },
            { "IsYeri", TxtIshYeri.Text.Trim() },
            { "QeydiyyatTarixi", (DListQeydiyyatTarixiY.SelectedValue + DListQeydiyyatTarixiM.SelectedValue + DListQeydiyyatTarixiD.SelectedValue).NullConvert() },
            { "IsdenCixmaTarixi", (DListIshdenCixmaY.SelectedValue + DListIshdenCixmaM.SelectedValue + DListIshdenCixmaD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 380, string.Format(LogText, ID), _PersonsID);
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

        DListIsh.SelectedValue = Dt._Rows("IsIsh").IsEmptyReplaceNoul();
        TxtIshYeri.Text = Dt._Rows("IsYeri");
        DListQeydiyyatTarixiD.SelectedValue = Dt._RowsObject("QeydiyyatTarixi").IntDateDay();
        DListQeydiyyatTarixiM.SelectedValue = Dt._RowsObject("QeydiyyatTarixi").IntDateMonth();
        DListQeydiyyatTarixiY.SelectedValue = Dt._RowsObject("QeydiyyatTarixi").IntDateYear();

        DListIshdenCixmaD.SelectedValue = Dt._RowsObject("IsdenCixmaTarixi").IntDateDay();
        DListIshdenCixmaM.SelectedValue = Dt._RowsObject("IsdenCixmaTarixi").IntDateMonth();
        DListIshdenCixmaY.SelectedValue = Dt._RowsObject("IsdenCixmaTarixi").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 380, string.Format("15 yaşından yuxarı uşağın məşğulluq vəziyyəti haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}