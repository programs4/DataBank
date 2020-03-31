using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_List : System.Web.UI.UserControl
{
    public void Bind()
    {
        DataTable Dt = DALC.GetSoragcaByTableName(DListTableName.SelectedValue,"Order By ID desc");
        GrdList.DataSource = Dt;
        GrdList.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void DListTableNames_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }

    protected void BtnInsert_Click(object sender, EventArgs e)
    {
        if (DListTableName.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Əlavə edəcəyiniz növü seçin.", Page);
            return;
        }
        if (TxtName.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Əlavə edəcəyiniz məlumatı daxil edin.", Page);
            return;
        }
        int CheckInsert = DALC.InsertSoragca(DListTableName.SelectedValue, TxtName.Text);
        if (CheckInsert < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListTableNames_SelectedIndexChanged(null, null);
        TxtName.Text = "";
        DALC.AdministratorsHistoryInsert(DListTableName.SelectedItem.Text + " üçün yeni soğça növü əlavə edildi. Adı: " + TxtName.Text.Trim());
    }

}