/********************************************************************
-- ATRIA SENIOR LIVING GROUP CONFIDENTIAL.  For authorized use only.
-- Except for as expressly authorized by Atria Senior Living Group,
-- do not disclose, copy, reproduce, distribute, or modify.
-- TM 2012 Atria Senior Living, LLC
*********************************************************************
PURPOSE:		Business Object for Menu
AUTHOR:			Amanda.Marburger
DATE:			12/16/2012
NOTES:
CHANGE CONTROL:
********************************************************************/

using AtriaEM;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BonusPlan
{
    public class MenuBarBO : Atria.MenuBar.MenuBO
    {
        #region EffectiveDT MenuBar

        private DataSet MenuBarMonthGet()
        {
            DataSet dsMonth = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MenuBarMonthGet", Connection) { CommandType = CommandType.StoredProcedure })
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dsMonth);
                    }
                }
            }

            return dsMonth;
        }

        /// <summary>
        /// Renders the Month Menu Bar (DatePicker)
        /// </summary>
        /// <returns></returns>
        public override string MenuBarMonth()
        {
            EffectiveDTVerify();

            DataSet dsMenuBarMonth = MenuBarMonthGet();
            StringBuilder sbMonth = new StringBuilder();

            string UrlPath = System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            string CurrentQueryString = System.Web.HttpContext.Current.Request.QueryString.ToString();
            string StyleSelect = "";
            DateTime CurrentDT = Convert.ToDateTime(EffectiveDT);

            if (dsMenuBarMonth.HasRows())
            {
                sbMonth.AppendLine("<table border=0 cellpadding=0 cellspacing=0>");
                sbMonth.AppendLine(" <tr>");
                //moved 'width:auto;' for IE9 compatibility
                sbMonth.AppendLine("     <td style='height:25px; width:auto;' class='MenuBarMonth'>");
                sbMonth.AppendLine("         <table border=0 cellpadding=0 cellspacing=0 style='height:25px; margin-left: 7px;margin-right: 7px;'>");
                sbMonth.AppendLine("             <tr>");

                DataTable months = dsMenuBarMonth.Tables["Table"];

                var query =
                           from month in months.AsEnumerable()
                           where month.Field<int>("MonthGroup") == 1
                           select new
                           {
                               DateName = month.Field<string>("DateName"),
                               MonthGroup = month.Field<int>("MonthGroup"),
                               EffectiveDT = month.Field<DateTime>("EffectiveDT")
                           };

                if (query.Count() != 0)
                {
                    foreach (var month in query)
                    {
                        UrlPath = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"] + "?CommunityNumber=" + CommunityNumber + "&EffectiveDT=" + string.Format("{0:MM/dd/yyyy}", month.EffectiveDT.ToShortDateString());
                        if (Convert.ToDateTime(CurrentDT).Month == Convert.ToDateTime(month.EffectiveDT.ToString()).Month
                            && Convert.ToDateTime(CurrentDT).Year == Convert.ToDateTime(month.EffectiveDT.ToString()).Year
                            )
                        {
                            StyleSelect = "style=\"font-weight:bold;background-color:#FBFAE2;\"";
                        }
                        else
                        {
                            StyleSelect = "";
                        }

                        sbMonth.AppendFormat("                <td class=\"MenuBarMonthButton2\" {0}>", StyleSelect.ToString());
                        sbMonth.AppendFormat("                  <a href=\"{0}\" alt=\"{1} button\" {2}>{1}</a>", UrlPath, month.DateName.WhenNullOrEmpty("&nbsp;"), StyleSelect);
                        sbMonth.AppendLine("                 </td>");

                        UrlPath = "";
                    }
                }

                sbMonth.AppendLine("              </tr>");
                sbMonth.AppendLine("         </table>");
                sbMonth.AppendLine("     </td>");
                sbMonth.AppendLine("     <td style='height:25px;' class='MenuBarMonth2'>");
                sbMonth.AppendLine("         <table border=0 cellpadding=0 cellspacing=0 style='height:25px; margin-left: 7px;'>");
                sbMonth.AppendLine("             <tr>");
                query =
                          from month in months.AsEnumerable()
                          where month.Field<int>("MonthGroup") == 2
                          select new
                          {
                              DateName = month.Field<string>("DateName"),
                              MonthGroup = month.Field<int>("MonthGroup"),
                              EffectiveDT = month.Field<DateTime>("EffectiveDT")
                          };
                if (query.Count() != 0)
                {
                    foreach (var month in query)
                    {
                        UrlPath = System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"] + "?CommunityNumber=" + CommunityNumber + "&EffectiveDT=" + string.Format("{0:MM/dd/yyyy}", month.EffectiveDT.ToShortDateString());
                        if (Convert.ToDateTime(EffectiveDT).Month == Convert.ToDateTime(month.EffectiveDT.ToString()).Month
                            && Convert.ToDateTime(EffectiveDT).Year == Convert.ToDateTime(month.EffectiveDT.ToString()).Year
                            )
                        {
                            StyleSelect = "style=\"font-weight:bold;background-color:#333;\"";
                        }
                        else
                        {
                            StyleSelect = "";
                        }
                        sbMonth.AppendFormat("               <td class=\"MenuBarMonthButton2\" {0}>", StyleSelect.ToString());
                        sbMonth.AppendFormat("                  <a href=\"{0}\" alt=\"{1} button\" {2}>{1}</a>", UrlPath, month.DateName.WhenNullOrEmpty("&nbsp;"), StyleSelect);
                        sbMonth.AppendLine("                 </td>");

                        UrlPath = "";
                    }
                }

                sbMonth.AppendLine("              </tr>");
                sbMonth.AppendLine("         </table>");
                sbMonth.AppendLine("        </td>");
                sbMonth.AppendLine("    </tr>");
                sbMonth.AppendLine("</table>");
            }

            return sbMonth.ToString();
        }

        #endregion EffectiveDT MenuBar
    }
}