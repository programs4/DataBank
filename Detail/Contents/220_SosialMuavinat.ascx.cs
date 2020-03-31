using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_220_SosialMuavinat : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsMuavinat";

    string[] _SqlSelectColumns =
                               {
                                   "(Select Name From PersonsMuavinatNov Where ID=PersonsMuavinatNovID) as MuavinatNov",
                                   Config.SqlDateTimeFormat("MuavinatinBaslamaTarixi"),
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
        DListMuavinatD.DataSource = Config.NumericList(1, 31);
        DListMuavinatD.DataBind();

        DListMuavinatM.DataSource = Config.MonthList();
        DListMuavinatM.DataBind();

        DListMuavinatY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMuavinatY.DataBind();

    }

    public void MuavinatNov()
    {
        DListMuavinatNov.DataSource = DALC.GetSoragcaByTableName("PersonsMuavinatNov");
        DListMuavinatNov.DataBind();
        DListMuavinatNov.Items.Insert(0, new ListItem("--", "0"));
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
        if (!DALC._IsPermissionTypeEdit(340))
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
        MuavinatNov();
        // Təmizləmə işləri
        DListMuavinatNov.SelectedValue = "0";
        TxtDescription.Text = "";
        DListMuavinatD.SelectedValue = DListMuavinatM.SelectedValue = "00";
        DListMuavinatY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DListMuavinatNov.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Müavinat növü mütləq qeyd olunmalıdır.", Page);
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
            LogText = "Təyin edilmiş əmək pensiyası və yaxud sosial müavinətlər haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Təyin edilmiş əmək pensiyası və yaxud sosial müavinətlər haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "PersonsMuavinatNovID", DListMuavinatNov.SelectedValue.NullConvert() },
            { "MuavinatinBaslamaTarixi", (DListMuavinatY.SelectedValue + DListMuavinatM.SelectedValue + DListMuavinatD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 340, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();
        MuavinatNov();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListMuavinatNov.SelectedValue = Dt._Rows("PersonsMuavinatNovID").IsEmptyReplaceNoul();
        DListMuavinatD.SelectedValue = Dt._RowsObject("MuavinatinBaslamaTarixi").IntDateDay();
        DListMuavinatM.SelectedValue = Dt._RowsObject("MuavinatinBaslamaTarixi").IntDateMonth();
        DListMuavinatY.SelectedValue = Dt._RowsObject("MuavinatinBaslamaTarixi").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 340, string.Format("Təyin edilmiş əmək pensiyası və yaxud sosial müavinətlər haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}