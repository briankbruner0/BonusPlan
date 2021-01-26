using AtriaEM;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace BonusPlan
{
    public class SecurityOverrideBO : AtriaSecurity.AuthenticateBO
    {
        private DataSet UserDetail { get; set; }

        public string EffectiveDT
        { get; set; }

        #region Private Properties

        //Application
        private string _ApplicationID;

        private string _ApplicationName;
        private string _ApplicationPath;
        private string _ApplicationPhysicalPath;

        //Asset
        private string _FeatureID;

        private string _FeatureName;
        private string _FeaturePath;
        private string _VirtualPath;
        private string _WebPath;
        private string _TempPath;

        //Page
        private string _Title;

        private string _Body;

        //Community
        private string _CommunityID;

        private string _CommunityMessage;

        //Region
        private Boolean _RegionAccessFlg;

        //Division
        private Boolean _DivisionAccessFlg;

        //Company
        private Boolean _CompanyAccessFlg;

        //User
        private string _CreateFlg;

        private string _ModifyFlg;
        private string _DeleteFlg;
        private string _PendingFlg;

        //Site Login URL (Used when redirecting to login page)
        private string _SiteLoginURL
        {
            get { return ConfigurationManager.AppSettings["SiteLoginURL"].ToString().WhenNullOrEmpty(string.Empty); }
        }

        private string _UniquePath;
        private AtriaSecurity.CommunityDTO _Community = new AtriaSecurity.CommunityDTO();

        #endregion Private Properties

        #region Public Properties

        public string FeatureName
        {
            get
            {
                return _FeatureName;
            }
            set
            {
                _FeatureName = value;
            }
        }

        public string FeatureID
        {
            get
            {
                return _FeatureID;
            }
            set
            {
                _FeatureID = value;
            }
        }

        public string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public string ApplicationPath
        {
            get
            {
                return _ApplicationPath;
            }
            set
            {
                _ApplicationPath = value;
            }
        }

        public string ApplicationPhysicalPath
        {
            get
            {
                return _ApplicationPhysicalPath;
            }
            set
            {
                _ApplicationPhysicalPath = value;
            }
        }

        public string ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
            set
            {
                _ApplicationID = value;
            }
        }

        public string CreateFlg
        {
            get
            {
                return _CreateFlg;
            }
            set
            {
                _CreateFlg = value;
            }
        }

        public string DeleteFlg
        {
            get
            {
                return _DeleteFlg;
            }
            set
            {
                _DeleteFlg = value;
            }
        }

        public string ModifyFlg
        {
            get
            {
                return _ModifyFlg;
            }
            set
            {
                _ModifyFlg = value;
            }
        }

        public string PendingFlg
        {
            get
            {
                return _PendingFlg;
            }
            set
            {
                _PendingFlg = value;
            }
        }

        public string FeaturePath
        {
            get
            {
                return _FeaturePath;
            }
            set
            {
                _FeaturePath = value;
            }
        }

        public string VirtualPath
        {
            get
            {
                return _VirtualPath;
            }
            set
            {
                _VirtualPath = value;
            }
        }

        public string WebPath
        {
            get
            {
                return _WebPath;
            }
            set
            {
                _WebPath = value;
            }
        }

        public string TempPath
        {
            get
            {
                return _TempPath;
            }
            set
            {
                _TempPath = value;
            }
        }

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                _Body = value;
            }
        }

        public string CommunityID
        {
            get
            {
                return _CommunityID;
            }
            set
            {
                _CommunityID = value;
            }
        }

        public string CommunityMessage
        {
            get
            {
                return _CommunityMessage;
            }
            set
            {
                _CommunityMessage = value;
            }
        }

        public Boolean RegionAccessFlg
        {
            get
            {
                return _RegionAccessFlg;
            }
            set
            {
                _RegionAccessFlg = value;
            }
        }

        public Boolean DivisionAccessFlg
        {
            get
            {
                return _DivisionAccessFlg;
            }
            set
            {
                _DivisionAccessFlg = value;
            }
        }

        public Boolean CompanyAccessFlg
        {
            get
            {
                return _CompanyAccessFlg;
            }
            set
            {
                _CompanyAccessFlg = value;
            }
        }

        public string UniquePath
        {
            get
            {
                return _UniquePath.WhenNullOrEmpty("");
            }
            set
            {
                _UniquePath = value;
            }
        }

        public AtriaSecurity.CommunityDTO Community
        {
            get
            {
                if (_Community == null)
                {
                    _Community = new AtriaSecurity.CommunityDTO();
                }
                return _Community;
            }
            set
            {
                _Community = value;
            }
        }

        #endregion Public Properties

        public SecurityOverrideBO(string UserName, string UserPassword)
        {
            Username = UserName;
            Password = UserPassword;

            UserDetail = base.UserDetailGet();
        }

        public SecurityOverrideBO()
        {
            //Verify The Site ID
            if (!SiteIDVerify())
            {
                HttpContext.Current.Response.Redirect(_SiteLoginURL);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                return;
            }

            if (!UserNameVerify())
            {
                if (String.IsNullOrEmpty(Username))
                {
                    //HttpContext.Current.Response.Redirect("../../application/portal/login.aspx");
                    HttpContext.Current.Response.Redirect(_SiteLoginURL);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    return;
                }
            }
            else
            {
                if (!LoginTimeVerify())
                {
                    //HttpContext.Current.Response.Redirect("../../application/portal/login.aspx");
                    HttpContext.Current.Response.Redirect(_SiteLoginURL);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    return;
                }
                if (!BonusPlanCommunityVerify())
                {
                    //HttpContext.Current.Response.Redirect("../../application/portal/login.aspx");
                    HttpContext.Current.Response.Redirect(_SiteLoginURL);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    return;
                }
            }

            //If this is a valid user, obtain the detail information.
            UserDetail = base.UserDetailGet();
        }

        private bool SiteIDVerify()
        {
            bool SiteIDVerifyFlg = false;

            //Pull SiteID Cookie value
            string SiteIDCookie = string.Empty;

            if (HttpContext.Current.Request.Cookies["SITEID"] != null)
            {
                SiteIDCookie = HttpContext.Current.Request.Cookies["SITEID"].Value.ToString().WhenNullOrEmpty(string.Empty);
            }

            //Set RedirectURL if SiteIDs don't match
            if (SiteID == SiteIDCookie)
            {
                SiteIDVerifyFlg = true;
            }

            return SiteIDVerifyFlg;
        }

        /// <summary>
        /// Three steps:
        /// 1. Verify to see if the user has application acces.
        /// 2. Verify to see if the user has feature access.
        /// 3. Get the detail of the feature.
        /// </summary>
        public bool AccessVerify(string tmpApplicationID, string tmpFeatureID)
        {
            this._ApplicationID = tmpApplicationID.ToString();
            this._FeatureID = tmpFeatureID.ToString();

            // 1. Verify to see if the user has application acces.
            if (ApplicationAccessVerify())
            {
                // 2. Verify to see if the user has feature access.
                if (!AssetAccessVerify())
                {
                    HttpContext.Current.Response.Write("You do not have access to this page.");
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    return false;
                }
            }
            else
            {
                HttpContext.Current.Response.Write("You do not have access to this application.");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                return false;
            }
            // 3. Get the detailed access and string attributes of the feature.
            AssetDetailGet();
            return true;
        }

        private void PageRefresh()
        {
            string UrlPath = System.Web.HttpContext.Current.Request.ServerVariables["Script_name"].ToString();
            string CurrentQueryString = System.Web.HttpContext.Current.Request.QueryString.ToString();
            StringBuilder sb = new StringBuilder();

            NameValueCollection objQueryStringVariableCollection = HttpContext.Current.Request.QueryString;

            if (objQueryStringVariableCollection.HasKeys())
            {
                string[] Keys = objQueryStringVariableCollection.AllKeys;

                // WHEN THERE IS A BUILDING ID, ENSURE THAT THE BUILDING ID QUERYSTRING IS REWRITTEN APPROPRIATELY
                if (!string.IsNullOrEmpty(Community.Campus.Building.BuildingID))
                {
                    UrlPath = UrlPath + "?CommunityNumber=" + CommunityNumber;
                    for (int myCount = 0; myCount <= Keys.GetUpperBound(0); myCount++)
                    {
                        if (Keys[myCount].ToString() != "CommunityNumber")
                        {
                            // SET THE BUILDING ID QUERYSTRING TO THE CORRECT VALUE
                            if (Keys[myCount].ToString().Equals("BuildingID"))
                            {
                                UrlPath = UrlPath + "&" + Keys[myCount].ToString() + "=" + Community.Campus.Building.BuildingID;
                            }
                            else
                            {
                                UrlPath = UrlPath + "&" + Keys[myCount].ToString() + "=" + HttpContext.Current.Request.QueryString[Keys[myCount].ToString()].ToString();
                            }
                        }
                    }
                }
                else
                {
                    UrlPath = UrlPath + "?CommunityNumber=" + CommunityNumber;
                    for (int myCount = 0; myCount <= Keys.GetUpperBound(0); myCount++)
                    {
                        if (Keys[myCount].ToString() != "CommunityNumber")
                        {
                            UrlPath = UrlPath + "&" + Keys[myCount].ToString() + "=" + HttpContext.Current.Request.QueryString[Keys[myCount].ToString()].ToString();
                        }
                    }
                }
            }
            else
            {
                UrlPath = UrlPath + "?CommunityNumber=" + CommunityNumber;
            }

            HttpContext.Current.Response.Redirect(UrlPath);
        }

        private bool UserNameVerify()
        {
            if (HttpContext.Current.Request.Cookies["atria_name"] != null)
            {
                Username = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["atria_name"].Value);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LoginTimeVerify()
        {
            string LoggedInTimeCookie = null;

            DateTime LogInTime = DateTime.Now;
            DateTime CurrentTime = DateTime.Now;

            if ((HttpContext.Current.Request.Cookies["loggedin"] == null))
            {
                return false;
            }
            else
            {
                LoggedInTimeCookie = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["loggedin"].Value);
                // Replace() call below is used for backward compaitibility to the ColdFusion cookies.
                LogInTime = Convert.ToDateTime(LoggedInTimeCookie.Replace("ts ", "").Replace("{'", "").Replace("'}", ""));

                if (LoggedInTimeCookie != string.Empty)
                {
                    if (LogInTime.ToShortDateString() != CurrentTime.ToShortDateString())
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        private bool BonusPlanCommunityVerify()
        {
            string CommunityNumberCookie = String.Empty;
            string CurrentQueryString = String.Empty;
            DataSet DataSet = new DataSet();
            DataView DataView = new DataView();

            // Get the community access list for the user
            DataSet = BonusPlanCommunityUserAccessGet();

            //HANDLE QUERYSTRING COMMUNITY NUMBER
            if (HttpContext.Current.Request.QueryString["CommunityNumber"] != null)
            {
                //There is a querystring CommunityNumber
                if (HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value.ToString()) != HttpContext.Current.Request.QueryString["CommunityNumber"].ToString())
                {
                    if (DataSet.Tables.Count > 0)
                    {
                        if (DataSet.Tables[1].Rows.Count > 0)
                        {
                            DataView.Table = DataSet.Tables[1];
                            DataView.RowFilter = "CommunityNumber ='" + HttpContext.Current.Request.QueryString["CommunityNumber"].ToString().Trim() + "'";
                            if ((DataView.Count > 0))
                            {
                                //User has access to CommunityNumber
                                CommunityNumber = DataView[0].Row["CommunityNumber"].ToString();
                                CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                                //PageRefresh();
                                return true;
                            }
                            else
                            {
                                //Error - No Access to the Community or QueryString value is malformed
                                HttpContext.Current.Response.Write("You do not have access to the Community (" + HttpContext.Current.Request.QueryString["CommunityNumber"].ToString() + ") you are attempting to view.");
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.End();
                                return false;
                            }
                        }
                    }
                }
            }
            //HANDLE NO QUERYSTRING COMMUNITY NUMBER TEST THE COMMUNITY NUMBER COOKIE
            else
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value.ToString())))
                {
                    // If No COMMUNITY NUMBER COOKIE, CREATE ONE FROM THE DEFAULT COMMUNITY NUMBER IN THE USER COMMUNITY ACCESS LIST
                    if (DataSet.Tables.Count > 0)
                    {
                        if (DataSet.Tables[1].Rows.Count > 0)
                        {
                            CommunityNumber = DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString();
                            CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                            PageRefresh();
                        }
                    }
                }
                else
                //IF COMMUNITY COOKIE EXISTS VERIFY IT AGAINST THE USER COMMUNITY ACCESS LIST
                {
                    if (DataSet.Tables.Count > 0)
                    {
                        if (DataSet.Tables[1].Rows.Count > 0)
                        {
                            DataView.Table = DataSet.Tables[1];
                            DataView.RowFilter = "CommunityNumber ='" + HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value.ToString()) + "'";
                            if ((DataView.Count > 0))
                            {
                                CommunityNumber = DataView[0].Row["CommunityNumber"].ToString();
                            }
                            else
                            {
                                // If No COMMUNITY NUMBER COOKIE, CREATE ONE FROM THE DEFAULT COMMUNITY NUMBER IN THE USER COMMUNITY ACCESS LIST
                                if (DataSet.Tables.Count > 0)
                                {
                                    if (DataSet.Tables[1].Rows.Count > 0)
                                    {
                                        CommunityNumber = DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString();
                                        CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                                        PageRefresh();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //FINALLY, CHECK THE COMMUNITY NUMBER COOKIE ONCE MORE
            if (string.IsNullOrEmpty(HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value.ToString())))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Verify that the user has access to the asset and get the access flags for the user
        /// </summary>
        /// <returns></returns>
        private bool AssetAccessVerify()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            string functionReturnValue = String.Empty;

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.AssetAccessVerify", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;
                DataSet DataSet = new DataSet();

                command.Parameters.Add("@FeatureID", SqlDbType.VarChar);
                command.Parameters["@FeatureID"].Value = FeatureID;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = Username;

                command.Parameters.Add("@Url", SqlDbType.VarChar);
                command.Parameters["@Url"].Value = HttpContext.Current.Request.Url.ToString();

                command.Parameters.Add("@AssetAccessFlg", SqlDbType.Int);
                command.Parameters["@AssetAccessFlg"].Direction = ParameterDirection.Output;

                DataAdapter.Fill(DataSet);

                if (DataSet.Tables.Count > 0)
                {
                    if (DataSet.Tables[0].Rows.Count > 0)
                    {
                        //Security Access Contraints
                        CreateFlg = DataSet.Tables[0].Rows[0]["CreateFlg"].WhenNullOrEmpty("0");
                        DeleteFlg = DataSet.Tables[0].Rows[0]["DeleteFlg"].WhenNullOrEmpty("0");
                        ModifyFlg = DataSet.Tables[0].Rows[0]["ModifyFlg"].WhenNullOrEmpty("0");
                        PendingFlg = DataSet.Tables[0].Rows[0]["PendingFlg"].WhenNullOrEmpty("0");
                    }
                }

                functionReturnValue = command.Parameters["@AssetAccessFlg"].Value.ToString();
            }

            return functionReturnValue == "1";
        }

        /// <summary>
        /// Verify that the user has access to the application
        /// </summary>
        /// <returns></returns>
        private bool ApplicationAccessVerify()
        {
            bool functionReturnValue = false;

            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.ApplicationAccessVerify", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@ApplicationID", SqlDbType.VarChar);
                command.Parameters["@ApplicationID"].Value = ApplicationID;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = Username;

                command.Parameters.Add("@ApplicationAccessFlg", SqlDbType.Int);
                command.Parameters["@ApplicationAccessFlg"].Direction = ParameterDirection.Output;

                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.ExecuteNonQuery();

                functionReturnValue = Convert.ToBoolean(command.Parameters["@ApplicationAccessFlg"].Value);//.ToString();
            }

            return functionReturnValue;
        }

        private void AssetDetailGet()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            DataSet DataSet = new DataSet();

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.AssetDetailGet", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@FeatureID", SqlDbType.Int);
                command.Parameters["@FeatureID"].Value = FeatureID;

                command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                DataAdapter.Fill(DataSet);

                if (DataSet.Tables.Count > 0)
                {
                    if (DataSet.Tables[0].Rows.Count > 0)
                    {
                        Title = DataSet.Tables[0].Rows[0]["Title"].WhenNullOrEmpty(String.Empty);
                        Body = DataSet.Tables[0].Rows[0]["Body"].WhenNullOrEmpty(String.Empty);
                        FeaturePath = DataSet.Tables[0].Rows[0]["FeaturePath"].WhenNullOrEmpty(String.Empty);
                        VirtualPath = DataSet.Tables[0].Rows[0]["VirtualPath"].WhenNullOrEmpty(String.Empty);
                        WebPath = DataSet.Tables[0].Rows[0]["WebPath"].WhenNullOrEmpty(String.Empty);
                        TempPath = DataSet.Tables[0].Rows[0]["TempPath"].WhenNullOrEmpty(String.Empty);
                        ApplicationPath = DataSet.Tables[0].Rows[0]["ApplicationPath"].WhenNullOrEmpty(String.Empty);
                        ApplicationPhysicalPath = DataSet.Tables[0].Rows[0]["ApplicationPhysicalPath"].WhenNullOrEmpty(String.Empty);
                        ApplicationName = DataSet.Tables[0].Rows[0]["ApplicationName"].WhenNullOrEmpty(String.Empty);
                        CommunityID = DataSet.Tables[0].Rows[0]["CommunityID"].WhenNullOrEmpty(String.Empty);
                        CommunityMessage = DataSet.Tables[0].Rows[0]["CommunityMessage"].WhenNullOrEmpty(String.Empty);
                    }
                }

                //09-06-2012 Amanda Marburger no longer needed because stored procedure returns the required data

                ////rename
                //command = new SqlCommand("Portal.FeatureContentGet", connection);
                //command.CommandType = CommandType.StoredProcedure;
                //DataAdapter.SelectCommand = command;

                //command.Parameters.Add("@FeatureID", SqlDbType.Int);
                //command.Parameters["@FeatureID"].Value = FeatureID;

                //command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                //command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                //DataAdapter.Fill(DataSet);

                //if (DataSet.Tables.Count > 0)
                //{
                //    if (DataSet.Tables[0].Rows.Count > 0)
                //    {
                //        CommunityID = HttpUtility.HtmlDecode(DataSet.Tables[0].Rows[0]["CommunityID"].WhenNullOrEmpty(string.Empty));
                //        CommunityMessage = HttpUtility.HtmlDecode(DataSet.Tables[0].Rows[0]["CommunityMessage"].WhenNullOrEmpty(string.Empty));
                //    }
                //}
            }
        }

        /// <summary>
        /// Populates path attributes for the passed Attribute
        /// </summary>
        public void AssetPathGet(Int32 FeatureID)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            DataSet DataSet = new DataSet();

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.AssetContentGet", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@FeatureID", SqlDbType.Int);
                command.Parameters["@FeatureID"].Value = FeatureID;

                DataAdapter.Fill(DataSet);
            }

            if (DataSet.Tables.Count > 0)
            {
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    FeaturePath = DataSet.Tables[0].Rows[0]["FeaturePath"].WhenNullOrEmpty(String.Empty);
                    VirtualPath = DataSet.Tables[0].Rows[0]["VirtualPath"].WhenNullOrEmpty(String.Empty);
                    WebPath = DataSet.Tables[0].Rows[0]["WebPath"].WhenNullOrEmpty(String.Empty);
                    TempPath = DataSet.Tables[0].Rows[0]["TempPath"].WhenNullOrEmpty(String.Empty);
                }
            }
        }

        /// <summary>
        /// Returns HTML for the MenuBar at the top of a panel, given that the
        /// ApplicationID and FeatureID are set in the instance of the object.
        /// This may be overridden at the application level.
        /// Change Control: 11/11/2011 Amanda Marburger - Changed all methods from private to public so they can be called from another class
        /// </summary>
        /// <returns></returns>
        //public virtual string MenuBar()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //*****************************************************
        //    // Build Menu Navigation Bars for Intranet 2.0
        //    //*****************************************************
        //    //sb.AppendLine(MenuNavigationContent());

        //    sb.AppendLine("<table border=0 cellpadding=0 cellspacing=0 style='margin:0px;width:100%;height:25px'>");
        //    sb.AppendLine(" <tr>\n");
        //    sb.AppendLine("     <td style='height:20px;width:760px;background-color:#DA5BF5' background='../../../../../images/bgToolbar.gif'>");
        //    sb.AppendLine("         <table border=0 cellpadding=0 cellspacing=0 style='height:25px;width:760px;'>");
        //    sb.AppendLine("             <tr>");
        //    sb.AppendLine("                 <td style='vertical-align:middle;width:10px;height:25px'>&nbsp;</td>");

        //    //*****************************************************
        //    // Build Primary Menu Navigation
        //    //*****************************************************
        //    sb.AppendLine(MenuNavigationPrimary());

        //    //*********************************
        //    //Build Sub Menu Navigation
        //    //*********************************
        //    sb.AppendLine(MenuNavigationSecondary());

        //    //*********************************
        //    //Build Community Navigation
        //    //*********************************
        //    sb.AppendLine(MenuNavigationCommunity(ApplicationPath));

        //    //*********************************
        //    //Build Region Navigation
        //    //*********************************
        //    //sb.AppendLine(MenuNavigationRegion());

        //    //*********************************
        //    //Build Division Navigation
        //    //*********************************
        //    //sb.AppendLine(MenuNavigationDivision());

        //    //*********************************
        //    //Build Company Navigation
        //    //*********************************
        //    //sb.AppendLine(MenuNavigationCompany());

        //    sb.AppendLine("                 <td STYLE=\"PADDING-RIGHT:3px;VERTICAL-ALIGN:middle;Text-align:right;HEIGHT:20px\">&nbsp;</td>");
        //    sb.AppendLine("             </tr>");
        //    sb.AppendLine("         </table>");
        //    sb.AppendLine("     </td>");
        //    sb.AppendLine(" </tr>");
        //    sb.AppendLine("</table>");

        //    //*************************************************************************************************
        //    //This is a place holder for the new employee, community and content search
        //    //*************************************************************************************************
        //    //sb.AppendLine(MenuSearchBar());

        //    //*************************************************************************************************
        //    //This is a display area for Date and time and User Name of the logged in person.
        //    //*************************************************************************************************
        //    //sb.AppendLine(MenuUserProfileBar());

        //    return sb.ToString();
        //}

        #region Menu Bar Overrides

        /// <summary>
        /// Returns HTML for the MenuBar at the top of a panel, given that the
        /// ApplicationID and FeatureID are set in the instance of the object.
        /// This may be overridden at the application level.
        /// Change Control: 11/11/2011 Amanda Marburger - Changed all methods from private to public so they can be called from another class
        /// </summary>
        /// <returns></returns>
        public string MenuBar()
        {
            StringBuilder sb = new StringBuilder();

            //*****************************************************
            // Build Menu Navigation Bars for Intranet 2.0
            //*****************************************************
            //sb.AppendLine(MenuNavigationContent());

            sb.AppendLine("<table border=0 cellpadding=0 cellspacing=0 style='margin:0px;width:100%;height:25px'>");
            sb.AppendLine(" <tr>\n");
            sb.AppendLine("     <td style='height:20px;width:760px;background-color:#DA5BF5' background='../../../../../images/bgToolbar.gif'>");
            sb.AppendLine("         <table border=0 cellpadding=0 cellspacing=0 style='height:25px;width:760px;'>");
            sb.AppendLine("             <tr>");
            sb.AppendLine("                 <td style='vertical-align:middle;width:10px;height:25px'>&nbsp;</td>");

            //*****************************************************
            // Build Primary Menu Navigation
            //*****************************************************
            sb.AppendLine(MenuNavigationPrimary());

            //*********************************
            //Build Sub Menu Navigation
            //*********************************
            sb.AppendLine(MenuNavigationSecondary());

            //*********************************
            //Build Community Navigation
            //*********************************
            sb.AppendLine(BonusPlanMenuNavigationCommunity(ApplicationPath));

            //*********************************
            //Build Region Navigation
            //*********************************
            //sb.AppendLine(MenuNavigationRegion());

            //*********************************
            //Build Division Navigation
            //*********************************
            //sb.AppendLine(MenuNavigationDivision());

            //*********************************
            //Build Company Navigation
            //*********************************
            //sb.AppendLine(MenuNavigationCompany());

            sb.AppendLine("                 <td STYLE=\"PADDING-RIGHT:3px;VERTICAL-ALIGN:middle;Text-align:right;HEIGHT:20px\">&nbsp;</td>");
            sb.AppendLine("             </tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");

            //*************************************************************************************************
            //This is a place holder for the new employee, community and content search
            //*************************************************************************************************
            //sb.AppendLine(MenuSearchBar());

            //*************************************************************************************************
            //This is a display area for Date and time and User Name of the logged in person.
            //*************************************************************************************************
            //sb.AppendLine(MenuUserProfileBar());

            return sb.ToString();
        }

        /// <summary>
        /// Community DropDown for navigation.
        /// </summary>
        /// <param name="Path">Current path so that the user is not redirected to a home page.</param>
        /// <returns></returns>
        ///
        protected string BonusPlanMenuNavigationCommunity(string Path)
        {
            string urlPath = HttpContext.Current.Request.ServerVariables["Script_name"];
            string currentQueryString = HttpContext.Current.Request.QueryString.ToString();
            //05-08-2012 Amanda Marburger
            //Added UniquePath Property to the SecurityBO to allow for us to control the page redirection when the community is selected from the menu bar
            string applicationPath = "";

            if (UniquePath.Length > 0)
            {
                applicationPath = Path + UniquePath;
            }
            else
            {
                applicationPath = Path;
            }

            DataSet DataSet = BonusPlanCommunityUserAccessGet();
            StringBuilder sb = new StringBuilder();

            if (!(CommunityNumber.Length > 0))
            {
                if (DataSet.Tables.Count > 0)
                {
                    if (DataSet.Tables[1].Rows.Count > 0)
                    {
                        DataRow row = DataSet.Tables[1].Rows[0];
                        CommunityNumber = row["CommunityNumber"].ToString();

                        if (currentQueryString != String.Empty)
                        {
                            string CommunityNumberQueryString = HttpContext.Current.Request.QueryString["CommunityNumber"];
                            if (!string.IsNullOrEmpty(CommunityNumberQueryString))
                            {
                                urlPath = urlPath + "?" + currentQueryString + "&CommunityNumber=" + CommunityNumberQueryString;
                            }
                            else
                            {
                                urlPath = urlPath + "?" + currentQueryString + "&CommunityNumber=" + CommunityNumber;
                            }
                        }
                        else
                        {
                            urlPath = urlPath + "?CommunityNumber=" + CommunityNumber;
                        }

                        HttpContext.Current.Response.Redirect(urlPath);
                    }
                }
            }

            sb.AppendLine("<td style='vertical-align:middle;width:10px;height:25px'>&nbsp;</td>");
            sb.AppendLine("<td style='vertical-align:middle;width:25px;height:20px'>");
            string onChangeText = "if(document.getElementById(this.id).options[document.getElementById(this.id).selectedIndex].value.length > 0){window.top.location.href='" + applicationPath + "?CommunityNumber=' + document.getElementById(this.id).options[document.getElementById(this.id).selectedIndex].value + '&EffectiveDt=" + EffectiveDT + "';}";
            sb.AppendLine("<select id=\"MenuBarCommunityNumber\" name=\"MenuBarCommunityNumber\" onchange=\"" + onChangeText + "\">");
            sb.AppendLine("<option selected>Select a Community...</option>");

            if (DataSet.Tables.Count > 0)
            {
                if (DataSet.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow rowItem in DataSet.Tables[1].Rows)
                    {
                        if (CommunityNumber.Length > 0)
                        {
                            if (CommunityNumber == rowItem["CommunityNumber"].ToString())
                            {
                                sb.AppendFormat("<option value='{0}' selected>{1}</option>", rowItem["CommunityNumber"], rowItem["CommunityName"]);
                            }
                            else
                            {
                                sb.AppendFormat("<option value='{0}'>{1}</option>", rowItem["CommunityNumber"], rowItem["CommunityName"]);
                            }
                        }
                        else
                        {
                            sb.AppendFormat("<option value='{0}'>{1}</option>", rowItem["CommunityNumber"], rowItem["CommunityName"]);
                        }
                    }
                }
            }
            sb.AppendLine("</select>");
            sb.AppendLine("</td>");

            return sb.ToString();
        }

        public DataSet BonusPlanCommunityUserAccessGet()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            DataSet DataSet = new DataSet();
            DataView DataView = new DataView();

            using (connection)
            {
                SqlCommand command = new SqlCommand("BonusPlan.CommunityUserAccessGet", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = Username;

                //command.Parameters.Add("@EffectiveDt", SqlDbType.VarChar);
                //command.Parameters["@EffectiveDt"].Value = EffectiveDT;

                using (connection)
                {
                    DataAdapter.Fill(DataSet);
                    if (DataSet.Tables[1].Rows.Count == 0)
                    {
                        HttpContext.Current.Response.Write("<font color=red><b>Your Username is not assigned to a community.  <br>Please contact the Help Desk at 1-866-289-4251 and have them assign your Username to your community. <b></font>");
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }

                    if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["CommunityNumber"]))
                    {
                        if (DataSet.Tables.Count > 0)
                        {
                            CommunityNumber = HttpContext.Current.Request.QueryString["CommunityNumber"].ToString();

                            // SET THE COMMUNITY COMPLEX FLG
                            DataView.Table = DataSet.Tables[1];
                            DataView.RowFilter = "CommunityNumber ='" + CommunityNumber + "'";

                            if ((DataView.Count > 0))
                            {
                                Community.ComplexFlg = DataView[0].Row["ComplexFlg"].WhenNullOrEmpty("0");
                                CommunityNumber = HttpContext.Current.Request.QueryString["CommunityNumber"].ToString();
                            }
                            else
                            {
                                Community.ComplexFlg = "0";
                                CommunityNumber = DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString();
                            }
                        }

                        CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                    }
                    else if (HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"] == null)
                    {
                        if (DataSet.Tables.Count > 0)
                        {
                            if (DataSet.Tables[1].Rows.Count > 0)
                            {
                                CommunityNumber = DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString();

                                // SET THE COMMUNITY COMPLEX FLG
                                DataView.Table = DataSet.Tables[1];
                                DataView.RowFilter = "CommunityNumber ='" + CommunityNumber + "'";

                                if ((DataView.Count > 0))
                                {
                                    Community.ComplexFlg = DataView[0].Row["ComplexFlg"].WhenNullOrEmpty("0");
                                }
                                else
                                {
                                    Community.ComplexFlg = "0";
                                }
                            }
                        }
                        CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                    }
                    else if (HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"] != null)
                    {
                        if (DataSet.Tables.Count > 0)
                        {
                            if (DataSet.Tables[1].Rows.Count > 0)
                            {
                                if (HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value != DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString())
                                {
                                    if (String.IsNullOrEmpty(HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value))
                                    {
                                        CommunityNumber = DataSet.Tables[1].Rows[0]["CommunityNumber"].ToString();
                                        CreateCookie("COMMUNITYNUMBER", CommunityNumber);
                                    }
                                    else
                                    {
                                        CommunityNumber = System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value);
                                    }

                                    // SET THE COMMUNITY COMPLEX FLG
                                    DataView.Table = DataSet.Tables[1];
                                    DataView.RowFilter = "CommunityNumber ='" + CommunityNumber + "'";

                                    if ((DataView.Count > 0))
                                    {
                                        Community.ComplexFlg = DataView[0].Row["ComplexFlg"].WhenNullOrEmpty("0");
                                    }
                                    else
                                    {
                                        Community.ComplexFlg = "0";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        CommunityNumber = System.Web.HttpContext.Current.Server.UrlDecode(System.Web.HttpContext.Current.Request.Cookies["COMMUNITYNUMBER"].Value);

                        // SET THE COMMUNITY COMPLEX FLG
                        DataView.Table = DataSet.Tables[1];
                        DataView.RowFilter = "CommunityNumber ='" + CommunityNumber + "'";

                        if ((DataView.Count > 0))
                        {
                            Community.ComplexFlg = DataView[0].Row["ComplexFlg"].WhenNullOrEmpty("0");
                        }
                        else
                        {
                            Community.ComplexFlg = "0";
                        }
                    }
                }
            }
            return DataSet;
        }

        #endregion Menu Bar Overrides

        #region MenuBar Methods

        /// <summary>
        /// Primary Navigation drop down for the MenuBar
        /// </summary>
        /// <returns></returns>
        protected string MenuNavigationPrimary()
        {
            string applicationPath;
            string urlPath = HttpContext.Current.Request.ServerVariables["Script_name"];
            string currentQueryString = HttpContext.Current.Request.QueryString.ToString();

            DataSet DataSet = NavigationAssetByApplicationIDGet();
            StringBuilder sb = new StringBuilder();

            if (DataSet.Tables.Count > 0)
            {
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    applicationPath = DataSet.Tables[0].Rows[0]["ApplicationLink"].WhenNullOrEmpty("../../../../../.." + urlPath);
                    if (applicationPath.Substring(applicationPath.LastIndexOf("/") + 1) == string.Empty)
                    {
                        //applicationPath += "Default.aspx";
                    }
                }
                else
                {
                    applicationPath = "../../../../../.." + urlPath;
                }
            }
            else
            {
                applicationPath = "../../../../../.." + urlPath;
            }

            applicationPath = applicationPath.Replace("\\", "/");

            sb.AppendLine("<td style='padding-right:3px;vertical-align:middle;width:20px;height:20px'>");
            sb.AppendLine("<select onchange=\"window.top.location=document.getElementById(this.id).options[document.getElementById(this.id).selectedIndex].value;\" id=\"navigation\">");
            sb.AppendLine("<option selected>Go To...</option>");

            foreach (DataRow itemRow in DataSet.Tables[0].Rows)
            {
                if (itemRow["FeatureTypeID"].ToString() == "3")
                {
                    sb.AppendFormat("<option value='{0}?CommunityNumber={1}'>{2}</option>", itemRow["FeaturePath"], CommunityNumber, itemRow["FeatureName"]);
                }
            }

            sb.AppendLine("</select>");
            sb.AppendLine("</td>");

            return sb.ToString();
        }

        /// <summary>
        /// Subnavigation drop down for the MenuBar
        /// </summary>
        /// <returns></returns>
        protected string MenuNavigationSecondary()
        {
            DataSet DataSet = SubNavigationAssetByApplicationIDGet();
            StringBuilder sb = new StringBuilder();

            if (DataSet.Tables.Count > 0)
            {
                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    sb.AppendLine("<td style='vertical-align:middle;width:20px;height:20px'>");
                    sb.AppendLine("<select onchange=\"window.top.location=document.getElementById(this.id).options[document.getElementById(this.id).selectedIndex].value\" ID=\"subnavigation\">");
                    sb.AppendLine("<option selected>I Want To...</option>");
                    foreach (DataRow rowItem in DataSet.Tables[0].Rows)
                    {
                        sb.AppendFormat("<option value='{0}?CommunityNumber={1}'>{2}</option>", rowItem["FeaturePath"].WhenNullOrEmpty(String.Empty), CommunityNumber, rowItem["FeatureName"].WhenNullOrEmpty(String.Empty));
                    }
                    sb.AppendLine("</select>");
                    sb.AppendLine("</td>");
                }
            }
            return sb.ToString();
        }

        #endregion MenuBar Methods

        private DataSet NavigationAssetByApplicationIDGet()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["webapplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            DataSet DataSet = new DataSet();

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.NavigationAssetByApplicationIDGet", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = Username;

                command.Parameters.Add("@ApplicationID", SqlDbType.VarChar);
                command.Parameters["@ApplicationID"].Value = ApplicationID;

                DataAdapter.Fill(DataSet);
                if (DataSet.Tables.Count > 0)
                {
                    if (DataSet.Tables[0].Rows.Count > 0)
                    {
                        if (DataSet.Tables[0].Rows[0]["FeatureTypeID"].WhenNullOrEmpty(String.Empty) == "2")
                        {
                            FeatureName = DataSet.Tables[0].Rows[0]["FeatureName"].WhenNullOrEmpty(String.Empty);
                        }
                    }
                }
            }

            return DataSet;
        }

        private DataSet SubNavigationAssetByApplicationIDGet()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["webapplication"]);
            SqlDataAdapter DataAdapter = new SqlDataAdapter();
            DataSet DataSet = new DataSet();

            using (connection)
            {
                SqlCommand command = new SqlCommand("Portal.SubNavigationAssetByApplicationIDGet", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataAdapter.SelectCommand = command;

                command.Parameters.Add("@Username", SqlDbType.VarChar);
                command.Parameters["@Username"].Value = Username;

                command.Parameters.Add("@ApplicationID", SqlDbType.Int);
                command.Parameters["@ApplicationID"].Value = ApplicationID;

                command.Parameters.Add("@ParentFeatureID", SqlDbType.Int);
                command.Parameters["@ParentFeatureID"].Value = FeatureID;

                DataAdapter.Fill(DataSet);
            }
            return DataSet;
        }
    }
}