using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

public class DALC_Adoption
{
    public class AdoptionAdministratorsInfoClass
    {
        public int ID
        {
            get;
            set;
        }
        public string Fullname
        {
            get;
            set;
        }
    }

    public static AdoptionAdministratorsInfoClass _GetAdoptionAdministratorsLogin
    {
        get
        {
            if (HttpContext.Current.Session["AdoptionAdminLogin"] != null)
                return (AdoptionAdministratorsInfoClass)HttpContext.Current.Session["AdoptionAdminLogin"];
            else
                return null;
        }
    }

    public class UsersInfo
    {
        public int AdoptionOrganizationsID
        {
            get;
            set;
        }
        public int RegisterNo
        {
            get;
            set;
        }
        public DateTime RegisterDate
        {
            get;
            set;
        }
    }

    public static UsersInfo _GetUsersLogin
    {
        get
        {
            if (HttpContext.Current.Session["UsersLogin"] != null)
                return (UsersInfo)HttpContext.Current.Session["UsersLogin"];
            else
                return null;
        }
    }

    public static string WebRequestMethod(string URL, string Body, string Method = "POST", string ContentType = "application/json;charset=utf-8")
    {
        try
        {
            WebRequest request = WebRequest.Create(URL);
            request.Method = Method;
            request.ContentType = ContentType;

            if (Method == "POST")
            {
                using (Stream SRequest = request.GetRequestStream())
                {
                    using (StreamWriter SW = new StreamWriter(SRequest))
                        SW.Write(Body);
                }
            }

            //get response-stream, and use a streamReader to read the content
            using (Stream SResponse = request.GetResponse().GetResponseStream())
            {
                using (StreamReader SR = new StreamReader(SResponse))
                {
                    return SR.ReadToEnd();
                }
            }

        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("DALC_Adoption.WebRequestMethod catch error: {0}", er.Message));
            throw new Exception(er.Message);
        }
    }

    public static DALC.DataTableResult GetAdoptionPersons(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DALC.DataTableResult AdoptionPersonsList = new DALC.DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");
        StringBuilder ORWhere = new StringBuilder();
        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Value)) && Convert.ToString(Value) != "-1")
                {

                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and ({0} Between @PDt1{1} and @PDt2{1})", Key.Replace("(BETWEEN)", ""), i.ToString());

                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1];
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and {0} Like '%' + @P{1} + '%'", Key.Replace("(LIKE)", ""), i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value.LikeFormat());
                    }
                    else if (Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.AppendFormat(" and {0} in(Select item from SplitString(@wIn{1},','))", Key.Replace("(IN)", ""), i.ToString());
                        com.Parameters.AddWithValue("@wIn" + i.ToString(), Value);
                    }
                    else if (Key.ToUpper().Contains("(NOTIN)"))
                    {
                        AddWhere.AppendFormat(" and {0} not in(Select item from SplitString(@wNotIn{1},','))", Key.Replace("(NOTIN)", ""), i.ToString());
                        com.Parameters.AddWithValue("@wNotIn" + i.ToString(), Value);
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }

            if (ORWhere.Length > 0)
            {
                AddWhere.AppendFormat(" and ({0})", ORWhere.Remove(ORWhere.ToString().Length - 2, 2));
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() Over (Order By Ap.ID desc) as RowIndex,  
                                     CONCAT(Soyad,' ',Ad,' ',Ata) as Fullname ,  
                                     CONCAT(r.Name,' , ',c.Name) as Location ,
                                     ec.Name as EyeColor,
                                     hc.Name as HairColor,     
                                     AO.Name as AdoptionOrganizations,                                
                                     CONVERT(int,ROUND(DATEDIFF(hour,DogumTarixi,GETDATE())/8766.0,0)) as Yash,                                                                                
                                     Ap.*  From AdoptionPersons as Ap LEFT JOIN 
                                    Colors as ec on ec.ID=Ap.GozColorsID 
                                    LEFT JOIN 
                                    Colors as hc on hc.ID=Ap.SachColorsID
                                    LEFT JOIN 
                                    Countries as c on c.ID=Ap.CountriesID
                                    LEFT JOIN 
                                    Regions as r on r.ID=Ap.RegionsID
                                    LEFT JOIN 
                                    AdoptionOrganizations as AO on AO.ID=AP.AdoptionOrganizationsID {1} ) as Ap {2}";

        com.Connection = DALC.SqlConn;

        com.CommandText = string.Format(QueryCommand, "COUNT(Ap.ID)", AddWhere, "");
        try
        {
            com.Connection.Open();
            AdoptionPersonsList.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("DALC_Adoption.GetAdoptionPersons TableName: {0} count xəta: {1}", "AdoptionPersons", er.Message));
            AdoptionPersonsList.Count = -1;
            AdoptionPersonsList.Dt = null;
            return AdoptionPersonsList;
        }
        finally
        {
            com.Connection.Close();
        }


        string RowIndexWhere = " Where Ap.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "Ap.* ", AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(AdoptionPersonsList.Dt);
            return AdoptionPersonsList;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("DALC_Adoption.GetAdoptionPersons TableName: {0} xəta: {1}", "AdoptionPersons", er.Message));
            AdoptionPersonsList.Count = -1;
            AdoptionPersonsList.Dt = null;
            return AdoptionPersonsList;
        }
    }
    public static DataTable GetAdoptionPersonsStatus()
    {
        return DALC.GetDataTableBySqlCommand("Select * from AdoptionPersonsStatus where ID not in(@ID)", "ID", new object[] { 90 });
    }

    //Log Admin insert
    public static void AdoptionAdministratorsHistoryInsert(string LogText)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("AdoptionAdministratorsID", DALC_Adoption._GetAdoptionAdministratorsLogin.ID);
        Dictionary.Add("LogText", LogText);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        DALC.InsertDatabase("AdoptionAdministratorsHistory", Dictionary);
    }

    //Log Admin insert
    public static void SearchLogInsert(string PIN, string AutentType, string AdoptionOrganizationsID, string RegisterNo, DateTime RegisterDate, string SearchParams)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("PIN", PIN);
        Dictionary.Add("AutentType", AutentType);
        Dictionary.Add("AdoptionOrganizationsID", string.IsNullOrEmpty(AdoptionOrganizationsID) ? 0 : int.Parse(AdoptionOrganizationsID));
        Dictionary.Add("RegisterNo", RegisterNo);
        Dictionary.Add("RegisterDate", RegisterDate);
        Dictionary.Add("SearchParams", SearchParams);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        DALC.InsertDatabase("AdoptionSearchHistory", Dictionary);
    }

    #region Filter method
    public static DALC.DataTableResult GetAdoptionAdministrator(int Top, Dictionary<string, object> Dictionary, string OrderBy = "")
    {
        DALC.DataTableResult AdoptionAdministrator = new DALC.DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };
        StringBuilder AddWhere = new StringBuilder("Where 1=1");
        StringBuilder ORWhere = new StringBuilder();

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key;
                Value = Item.Value;
                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and ({0} Between @PDt1{1} and @PDt2{1})", Key.Replace("(BETWEEN)", ""), i.ToString());

                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and {0} Like '%' + @P{1} + '%'", Key.Replace("(LIKE)", ""), i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value.ToString().Replace("İ", "_").Replace("I", "_"));
                    }
                    else if (Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.AppendFormat(" and {0} in(Select item from SplitString(@wIn{0},','))", Key.Replace("(IN)", ""));
                        com.Parameters.AddWithValue("@wIn" + Key.Replace("(IN)", ""), Value);
                    }

                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                }
            }
            if (ORWhere.Length > 0)
            {
                AddWhere.AppendFormat(" and ({0})", ORWhere.Remove(ORWhere.ToString().Length - 2, 2));
            }
        }


        string QueryCommand = @"Select {0} From AdoptionAdministrators {1} {2}";
        com.Connection = DALC.SqlConn;


        com.CommandText = string.Format(QueryCommand, "COUNT(ID)", AddWhere, OrderBy);
        try
        {
            com.Connection.Open();
            AdoptionAdministrator.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("AdoptionAdministrators count xəta: " + er.Message);
            AdoptionAdministrator.Count = -1;
            AdoptionAdministrator.Dt = null;
            return AdoptionAdministrator;
        }
        finally
        {
            com.Connection.Close();
        }


        com.CommandText = string.Format(QueryCommand, "Top (@Top) *", AddWhere, OrderBy);
        com.Parameters.Add("@Top", SqlDbType.Int).Value = Top;
        try
        {
            new SqlDataAdapter(com).Fill(AdoptionAdministrator.Dt);
            return AdoptionAdministrator;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("AdoptionAdministrators xəta: " + er.Message);
            AdoptionAdministrator.Count = -1;
            AdoptionAdministrator.Dt = null;
            return AdoptionAdministrator;
        }
    }

    public static DALC.DataTableResult GetAdoptionAdministratorHistory(int Top, Dictionary<string, object> Dictionary, string OrderBy = "")
    {
        DALC.DataTableResult AdoptionAdministratorHistory = new DALC.DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };
        StringBuilder AddWhere = new StringBuilder("Where 1=1");
        StringBuilder ORWhere = new StringBuilder();

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;
                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Value)) && Convert.ToString(Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and ({0} Between @PDt1{1} and @PDt2{1})", Key.Replace("(BETWEEN)", ""), i.ToString());

                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and {0} Like '%' + @P{1} + '%'", Key.Replace("(LIKE)", ""), i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.AppendFormat(" and {0} in(Select item from SplitString(@wIn{0},','))", Key.Replace("(IN)", ""));
                        com.Parameters.AddWithValue("@wIn" + Key.Replace("(IN)", ""), Value);
                    }

                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                }
            }
            if (ORWhere.Length > 0)
            {
                AddWhere.AppendFormat(" and ({0})", ORWhere.Remove(ORWhere.ToString().Length - 2, 2));
            }
        }

        string QueryCommand = @"Select {0} From 
                                AdoptionAdministratorsHistory as AAH
                                LEFT JOIN 
                                AdoptionAdministrators as AA on AAH.AdoptionAdministratorsID=AA.ID {1} {2}";
        com.Connection = DALC.SqlConn;


        com.CommandText = string.Format(QueryCommand, "COUNT(AAH.ID)", AddWhere, OrderBy);
        try
        {
            com.Connection.Open();
            AdoptionAdministratorHistory.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("AdoptionAdministratorHistory count xəta: " + er.Message);
            AdoptionAdministratorHistory.Count = -1;
            AdoptionAdministratorHistory.Dt = null;
            return AdoptionAdministratorHistory;
        }
        finally
        {
            com.Connection.Close();
        }


        com.CommandText = String.Format(QueryCommand, @"Top (@Top)  
                                                        AAH.ID,
                                                        AAH.AdoptionAdministratorsID,
                                                        AA.Username as UserName,
                                                        AA.Fullname as FullName,
                                                        AAH.LogText as Logtext,
                                                        FORMAT(AAH.Add_Dt, 'dd.MM.yyyy hh:mm') as Date,
                                                        AAH.Add_Ip as Ip", AddWhere, OrderBy);
        com.Parameters.Add("@Top", SqlDbType.Int).Value = Top;
        try
        {
            new SqlDataAdapter(com).Fill(AdoptionAdministratorHistory.Dt);
            return AdoptionAdministratorHistory;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("AdoptionAdministratorHistory xəta: " + er.Message);
            AdoptionAdministratorHistory.Count = -1;
            AdoptionAdministratorHistory.Dt = null;
            return AdoptionAdministratorHistory;
        }
    }

    public static DALC.DataTableResult GetAdoptionSearchHistory(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DALC.DataTableResult AdoptionSearchHistory = new DALC.DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");
        StringBuilder ORWhere = new StringBuilder();
        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Value)) && Convert.ToString(Value) != "-1")
                {

                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and ({0} Between @PDt1{1} and @PDt2{1})", Key.Replace("(BETWEEN)", ""), i.ToString());

                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1];
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and {0} Like '%' + @P{1} + '%'", Key.Replace("(LIKE)", ""), i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value.LikeFormat());
                    }
                    else if (Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.AppendFormat(" and {0} in(Select item from SplitString(@wIn{1},','))", Key.Replace("(IN)", ""), i.ToString());
                        com.Parameters.AddWithValue("@wIn" + i.ToString(), Value);
                    }
                    else if (Key.ToUpper().Contains("(NOTIN)"))
                    {
                        AddWhere.AppendFormat(" and {0} not in(Select item from SplitString(@wNotIn{1},','))", Key.Replace("(NOTIN)", ""), i.ToString());
                        com.Parameters.AddWithValue("@wNotIn" + i.ToString(), Value);
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }

            if (ORWhere.Length > 0)
            {
                AddWhere.AppendFormat(" and ({0})", ORWhere.Remove(ORWhere.ToString().Length - 2, 2));
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() Over (Order By ASH.ID desc) as RowIndex,                                                                                               
                                     ASH.*,AO.Name as AdoptionOrganizations  From AdoptionSearchHistory as ASH 
                                    LEFT JOIN 
                                    AdoptionOrganizations as AO on AO.ID=ASH.AdoptionOrganizationsID 
                                    {1} ) as ASH {2}";

        com.Connection = DALC.SqlConn;

        com.CommandText = string.Format(QueryCommand, "COUNT(ASH.ID)", AddWhere, "");
        try
        {
            com.Connection.Open();
            AdoptionSearchHistory.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("DALC_Adoption.GetAdoptionSearchHistory TableName: {0} count xəta: {1}", "AdoptionSearchHistory", er.Message));
            AdoptionSearchHistory.Count = -1;
            AdoptionSearchHistory.Dt = null;
            return AdoptionSearchHistory;
        }
        finally
        {
            com.Connection.Close();
        }


        string RowIndexWhere = " Where ASH.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "ASH.* ", AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(AdoptionSearchHistory.Dt);
            return AdoptionSearchHistory;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("DALC_Adoption.GetAdoptionSearchHistory TableName: {0} xəta: {1}", "AdoptionSearchHistory", er.Message));
            AdoptionSearchHistory.Count = -1;
            AdoptionSearchHistory.Dt = null;
            return AdoptionSearchHistory;
        }
    }

    public static DataTable GetAdministratorByID(int AdminID)
    {
        return DALC.GetDataTableParams("Top 1 *", "AdoptionAdministrators", "ID", AdminID, "");
    }
    public static DataTable GetAdoptionAdministratorsStatus()
    {
        return DALC.GetDataTable("*", "AdoptionAdministratorsStatus", "");
    }
    #endregion
}