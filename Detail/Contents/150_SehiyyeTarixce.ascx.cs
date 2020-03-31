using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Detail_Contents_150_SehiyyeTarixce : UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsSehiyyeTarixce";

    string[] _SqlSelectColumns =
                               {
                                   "Adi",
                                   "SaglamliqImkanlari",
                                   "Description"
                               };


    public void Grid1Bind()
    {
        GrdList1.DataSource = DALC.GetSehiyyeTarixce(_PersonsID, _TableName, _SqlSelectColumns, "20");
        GrdList1.DataBind();

        for (int i = 0; i < GrdList1.Rows.Count; i++)
        {
            ((LinkButton)GrdList1.Rows[i].Cells[GrdList1.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Əminsiniz?');";
        }
    }

    public void Grid2Bind()
    {
        GrdList2.DataSource = DALC.GetSehiyyeTarixce(_PersonsID, _TableName, _SqlSelectColumns, "10");
        GrdList2.DataBind();

        for (int i = 0; i < GrdList2.Rows.Count; i++)
        {
            ((LinkButton)GrdList2.Rows[i].Cells[GrdList2.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Əminsiniz?');";
        }
    }

    void BindTarixceNov()
    {
        DListTarixceNov.DataSource = DALC.GetDataTable("ID,Name", "PersonsSehiyyeTarixceNov", "");
        DListTarixceNov.DataBind();
        DListTarixceNov.Items.Insert(0, new ListItem("--", "0"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        if (!IsPostBack)
        {
            Grid1Bind();
            Grid2Bind();
            BindTarixceNov();
        }

        //Silinmə əməliyyatına icazə varmı?
        if (!DALC._IsPermissionTypeEdit(270))
        {
            LnkAdd.Visible = false;
            PnlEdit.Visible = false;

            GrdList1.Columns[GrdList1.Columns.Count - 1].Visible = false;
            GrdList1.Columns[GrdList1.Columns.Count - 2].Visible = false;

            GrdList2.Columns[GrdList2.Columns.Count - 1].Visible = false;
            GrdList2.Columns[GrdList2.Columns.Count - 2].Visible = false;
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        // Təmizləmə işləri
        TxtAdi.Text = TxtSaglamlıq.Text = TxtDescription.Text = "";
        DListTarixceNov.SelectedValue = "0";
        DListTarixceNov.Enabled = true;

        GrdList1.SelectedIndex = -1;
        GrdList2.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        if (LnkSuccess.CommandArgument == "2")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", "$(document).ready(function(){$('#history-tab').click();});", true);

        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DListTarixceNov.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Növü mütləq qeyd olunmalıdır.", Page);
            return;
        }
        if (TxtAdi.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Adı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (GrdList1.SelectedIndex == -1 && GrdList2.SelectedIndex == -1)
        {
            ID = DALC.NewBlankInsert(_TableName, _PersonsID).ToString();
            if (int.Parse(ID) < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            LogText = "Sağlamlıq tarixçəsi haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            if (LnkSuccess.CommandArgument == "1")
                ID = GrdList1.SelectedDataKey["ID"]._ToString();
            else
                ID = GrdList2.SelectedDataKey["ID"]._ToString();


            LogText = "Sağlamlıq tarixçəsi haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "PersonsSehiyyeTarixceNovID", DListTarixceNov.SelectedValue.NullConvert() },
            { "Adi", TxtAdi.Text.Trim() },
            { "SaglamliqImkanlari", TxtSaglamlıq.Text.Trim() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 270, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;

        Grid1Bind();
        Grid2Bind();
    }

    protected void GrdList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        DListTarixceNov.Enabled = false;
        PnlSaglamlıq.Visible = true;
        LnkSuccess.CommandArgument = "1";

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList1.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListTarixceNov.SelectedValue = Dt._Rows("PersonsSehiyyeTarixceNovID").IsEmptyReplaceNoul();
        TxtAdi.Text = Dt._Rows("Adi");
        TxtSaglamlıq.Text = Dt._Rows("SaglamliqImkanlari");
        TxtDescription.Text = Dt._Rows("Description");

        PnlEdit.Visible = true;
    }

    protected void GrdList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", "$(document).ready(function(){$('#history-tab').click();});", true);
        PnlEdit.Visible = true;
        DListTarixceNov.Enabled = false;
        PnlSaglamlıq.Visible = false;
        LnkSuccess.CommandArgument = "2";
        DataTable Dt = new DataTable();
        Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList2.SelectedDataKey["ID"]._ToString(), "");
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListTarixceNov.SelectedValue = Dt._Rows("PersonsSehiyyeTarixceNovID").IsEmptyReplaceNoul();
        TxtAdi.Text = Dt._Rows("Adi");
        TxtSaglamlıq.Text = Dt._Rows("SaglamliqImkanlari");
        TxtDescription.Text = Dt._Rows("Description");

        PnlEdit.Visible = true;
    }

    protected void GrdList1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = GrdList1.DataKeys[e.RowIndex]["ID"]._ToInt32();
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
        PnlEdit.Visible = false;
        GrdList1.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 270, string.Format("Sağlamlıq tarixçəsi haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void GrdList2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", "$(document).ready(function(){$('#history-tab').click();});", true);
        int ID = GrdList2.DataKeys[e.RowIndex]["ID"]._ToInt32();
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
        PnlEdit.Visible = false;
        GrdList2.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 270, string.Format("Sağlamlıq tarixçəsi haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        if (LnkSuccess.CommandArgument == "2")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", "$(document).ready(function(){$('#history-tab').click();});", true);

        PnlEdit.Visible = false;
    }

    protected void DListTarixceNov_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlSaglamlıq.Visible = DListTarixceNov.SelectedValue == "20";
        if (DListTarixceNov.SelectedValue == "10")
            LnkSuccess.CommandArgument = "2";
    }
}