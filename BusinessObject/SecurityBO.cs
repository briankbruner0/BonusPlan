using AtriaEM;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace BonusPlan
{
    public class SecurityBO : AtriaSecurity.SecurityBO
    {
        public string EffectiveDT
        { get; set; }

        #region Community Modal

        public string CommunityUserAccessMenuModal()
        {
            DataSet DataSet = CommunityUserAccessGet();
            StringBuilder HTMLOutput = new StringBuilder();
            string urlPath = HttpContext.Current.Request.ServerVariables["Script_name"];
            string currentQueryString = HttpContext.Current.Request.QueryString.ToString();

            //Added UniquePath Property to the SecurityBO to allow for us to control the page redirection when the community is selected from the menu bar
            string applicationPath = "";

            if (UniquePath.Length > 0)
            {
                applicationPath = ApplicationPath + UniquePath;
            }
            else
            {
                applicationPath = ApplicationPath;
            }

            if (DataSet.Tables.Count > 0)
            {
                HTMLOutput.AppendLine("<script type=\"text/javascript\">");
                HTMLOutput.AppendLine("$(document).ready(function() {");
                HTMLOutput.AppendLine("$(\"#communityFilter\").val(\"\");");
                HTMLOutput.AppendLine("$(\"#communityFilter\").trigger(\"keyup\");");//trigger the key up function after clearing any previous values from search
                HTMLOutput.AppendLine(" $(\"#communityFilter\").keyup(function () {");
                HTMLOutput.AppendLine("     // Retrieve the input field text and reset the count to zero");
                HTMLOutput.AppendLine("     var filter = $(this).val().toLowerCase();");
                HTMLOutput.AppendLine("     if (filter.length > 2) {");
                HTMLOutput.AppendLine("         // Loop through the comment list  ");
                HTMLOutput.AppendLine("         $(\".community\").each(function () {");
                HTMLOutput.AppendLine("             //If the list item does not contain the text phrase fade it out");
                HTMLOutput.AppendLine("             if ($(this).text().toLowerCase().search(new RegExp(filter, \"i\")) < 0) {");
                HTMLOutput.AppendLine("                 $(this).closest('div').closest('div').closest('a').fadeOut()");
                HTMLOutput.AppendLine("                 // Show the list item if the phrase matches and increase the count by 1");
                HTMLOutput.AppendLine("             } else {");
                HTMLOutput.AppendLine("                 $(this).closest('div').closest('div').closest('a').show()");
                HTMLOutput.AppendLine("             }");
                HTMLOutput.AppendLine("         });");
                HTMLOutput.AppendLine("     }");
                HTMLOutput.AppendLine("     else {");
                HTMLOutput.AppendLine("         $(\".community-modal-action\").show();");
                HTMLOutput.AppendLine("     }");
                HTMLOutput.AppendLine(" });");
                HTMLOutput.AppendLine("  $(document).on('click', '.community-modal-action', function (e) {");
                HTMLOutput.AppendLine("     var href = $(this).attr('data-href');");
                HTMLOutput.AppendLine("     window.location = href;");
                HTMLOutput.AppendLine("   });");
                HTMLOutput.AppendLine("});");
                HTMLOutput.AppendLine("</script>");

                if (DataSet.Tables[0].Rows.Count > 0)
                {
                    HTMLOutput.AppendLine("<div class=\"modal fade\" id=\"communityModal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myCommunityLabel\" aria-hidden=\"true\">");
                    HTMLOutput.AppendLine(" <div class=\"modal-dialog\">");
                    HTMLOutput.AppendLine("     <div class=\"modal-content\">");
                    HTMLOutput.AppendLine("         <div class=\"modal-header\">");
                    HTMLOutput.AppendLine("             <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>");
                    HTMLOutput.AppendLine("             <h4 class=\"modal-title\" id=\"myModalLabel\">Please Select a Community</h4>");
                    HTMLOutput.AppendLine("         </div>");
                    HTMLOutput.AppendLine("         <div class=\"modal-body\">");
                    HTMLOutput.AppendLine("                 <div class=\"input-group\">");
                    HTMLOutput.AppendLine("                     <span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-search\"></span></span>");
                    HTMLOutput.AppendLine("                     <input type=\"text\" class=\"form-control\" placeholder=\"Search\" id=\"communityFilter\">");
                    HTMLOutput.AppendLine("                 </div>");
                    HTMLOutput.AppendLine("                 <br />");

                    foreach (DataRow rowItem in DataSet.Tables[0].Rows)
                    {
                        HTMLOutput.AppendFormat("                   <a style=\"text-decoration:none;\" class=\"community-modal-action\" data-href=\"/Application/BonusPlan/default.aspx?CommunityNumber={0}\">", rowItem["CommunityNumber"], Environment.NewLine);
                        HTMLOutput.AppendFormat("                       <div class=\"thumbnail\" style=\"margin:0px 0px 7px 0px;cursor:pointer;cursor:hand;\">", Environment.NewLine);
                        HTMLOutput.AppendFormat("                           <div>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                               <table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"padding:3px;\" class=\"caption communityTable\">", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                   <tbody>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                       <tr>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                           <td colspan=\"2\" style=\"padding: 0px 7px 0px 7px; width: 114px; min-width: 114px; max-width: 114px; vertical-align: top; \">", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                               <img src=\"../../../../../../../images/Community/{0}0.jpg\" height=\"70\" width=\"100\">", rowItem["CommunityNumber"], Environment.NewLine);
                        HTMLOutput.AppendFormat("                                           </td>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                           <td class=\"community\" style=\"padding: 0px 7px 0px 7px;\">", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                               <nobr><strong style=\"color: #707070;\">{0} </strong></nobr><br>", rowItem["Community"], Environment.NewLine);
                        HTMLOutput.AppendFormat("                                               <address style=\"font-size:12px;margin:0px;\">", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span> &#8226;", rowItem["Address1"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span> &#8226;", rowItem["City"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span> &#8226;", rowItem["State"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span> &#8226;", rowItem["Country"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span><br>", rowItem["PostalCode"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070;\">{0}</span><br>", rowItem["PhoneNumber1"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070; \">{0}</span> &#8226;", rowItem["rptDivision"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                                   <span style=\"color: #707070; \">{0}</span>", rowItem["rptRegion"].WhenNullOrEmpty("&nbsp;"), Environment.NewLine);
                        HTMLOutput.AppendFormat("                                               </address>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                           </td>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                       </tr>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                                   </tbody>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                               </table>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                           </div>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                       </div>", Environment.NewLine);
                        HTMLOutput.AppendFormat("                   </a>", Environment.NewLine);
                    }

                    HTMLOutput.AppendLine("             </div>");
                    HTMLOutput.AppendLine("         </div>");
                    HTMLOutput.AppendLine("     </div>");
                    HTMLOutput.AppendLine("</div>");
                }
            }

            return HTMLOutput.ToString();
        }

        #endregion Community Modal

        /// <summary>
        /// Returns HTML for the MenuBar at the top of a panel, given that the
        /// ApplicationID and FeatureID are set in the instance of the object.
        /// This may be overridden at the application level.
        /// Change Control: 11/11/2011 Amanda Marburger - Changed all methods from private to public so they can be called from another class
        /// </summary>
        /// <returns></returns>
        public override string MenuBar()
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
                            if (CommunityNumberQueryString.Trim().Length > 0)
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
                command.CommandTimeout = 600;

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
    }
}