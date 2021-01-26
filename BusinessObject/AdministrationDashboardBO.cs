using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BonusPlan
{
    public class AdministrationDashboardBO : AdministrationDashboardDTO
    {
        public DataSet snpRevenueAcccountsGet()
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand Command = new SqlCommand("BonusPlan.snpRevenueAcccountsGet", connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);
                    DataAdapter.Fill(ds);
                }
            }
            return ds;
        }

        public DataSet snpEmployeeOverrideBonusGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.snpEmployeeOverrideBonusGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@EffectiveDt", SqlDbType.DateTime);
                    Command.Parameters["@EffectiveDt"].Value = EffectiveDT;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet snpPaymentApprovalExceptionGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.snpPaymentApprovalExceptionGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet snpManagePaymentApprovalGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.snpManagePaymentApprovalGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet snpQuickLinksGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.snpQuickLinksGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = "1";

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public string snpQuickLinks()
        {
            StringBuilder sbsnpQuickLinks = new StringBuilder();
            DataSet dsQuickLinks = snpQuickLinksGet();

            sbsnpQuickLinks.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:250px;\">");
            sbsnpQuickLinks.AppendLine("<tr>");
            sbsnpQuickLinks.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbsnpQuickLinks.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Bonus Plans</div>");
            sbsnpQuickLinks.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbsnpQuickLinks.AppendLine("    <tbody>");
            sbsnpQuickLinks.AppendLine("        <tr>");

            sbsnpQuickLinks.AppendLine("            <td style=\"background-color:#FFFFFF; padding:8px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb\"> ");
            sbsnpQuickLinks.AppendLine("            <table id='QuickLinksGrid' cellpadding='0' cellspacing='0' border='0' style='width: 250px;' class=\"tablesorter\">");
            sbsnpQuickLinks.AppendLine("                <thead>");
            sbsnpQuickLinks.AppendLine("                    <tr>");
            sbsnpQuickLinks.AppendLine("                        <th class=\"PortalheaderUnderline\" style=\"width:80px; text-align:left\">ID</th>");
            sbsnpQuickLinks.AppendLine("                        <th class=\"PortalheaderUnderline\" style=\"width:420px; text-align:left\">Name</th>");
            sbsnpQuickLinks.AppendLine("                    </tr>");
            sbsnpQuickLinks.AppendLine("                </thead>");
            sbsnpQuickLinks.AppendLine("                <tbody>");

            if (dsQuickLinks.HasRows())
            {
                foreach (DataRow quickLinks in dsQuickLinks.Tables[0].Rows)
                {
                    sbsnpQuickLinks.AppendLine("<tr>");
                    sbsnpQuickLinks.AppendFormat("<td style=\"text-align:left;\">{0}</td>", quickLinks["BonusPlanID"].ToString());
                    sbsnpQuickLinks.AppendFormat("<td style=\"text-align:left;\"><a href='BonusPlanProfile.aspx?BonusPlanID=" + quickLinks["BonusPlanID"].ToString() + "'>{0}</a></td>", quickLinks["BonusPlan"].ToString());
                    sbsnpQuickLinks.AppendLine("</tr>");
                }
            }
            else
            {
                sbsnpQuickLinks.AppendLine("<tr><td>No data available for this community</td></tr>");
            }

            sbsnpQuickLinks.AppendLine("            </tbody>");
            sbsnpQuickLinks.AppendLine("        </table>");
            sbsnpQuickLinks.AppendLine("        </td>");
            sbsnpQuickLinks.AppendLine("    </tr>");
            sbsnpQuickLinks.AppendLine("</tbody>");
            sbsnpQuickLinks.AppendLine("</table>");

            sbsnpQuickLinks.AppendLine("</td>");
            sbsnpQuickLinks.AppendLine("</tr>");
            sbsnpQuickLinks.AppendLine("</table>");
            return sbsnpQuickLinks.ToString();
        }

        public string snpRevenueAcccounts()
        {
            StringBuilder sbsnpRevenueAcccounts = new StringBuilder();
            DataSet dsRevenueAcccounts = snpRevenueAcccountsGet();

            sbsnpRevenueAcccounts.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
            sbsnpRevenueAcccounts.AppendLine("<tr>");
            sbsnpRevenueAcccounts.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbsnpRevenueAcccounts.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Revenue Acccounts</div>");

            sbsnpRevenueAcccounts.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbsnpRevenueAcccounts.AppendLine("    <tbody>");
            sbsnpRevenueAcccounts.AppendLine("        <tr>");
            sbsnpRevenueAcccounts.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");
            sbsnpRevenueAcccounts.AppendLine("          <div style=\"font-family:Helvetica, Arial, sans-serif;line-height:14px;color:#999999;\">");

            sbsnpRevenueAcccounts.AppendLine("<table id='RevenueAcccountsGrid' class=\"tablesorter\" cellpadding='0' cellspacing='0' border='0' style='width: 500px;'>");
            sbsnpRevenueAcccounts.AppendLine("  <thead>");
            sbsnpRevenueAcccounts.AppendLine("    <tr>");
            sbsnpRevenueAcccounts.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:50px; text-align:left\">Effective&nbsp;Date</th>");
            sbsnpRevenueAcccounts.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:250px; text-align:left\">Ledger&nbsp;Entry</th>");
            sbsnpRevenueAcccounts.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:50px; text-align:left\">Active</th>");
            sbsnpRevenueAcccounts.AppendLine("    </tr>");
            sbsnpRevenueAcccounts.AppendLine("  </thead>");
            sbsnpRevenueAcccounts.AppendLine("  <tbody>");

            if (dsRevenueAcccounts.HasRows())
            {
                foreach (DataRow revenueAcccount in dsRevenueAcccounts.Tables[0].Rows)
                {
                    sbsnpRevenueAcccounts.AppendLine("<tr>");
                    sbsnpRevenueAcccounts.AppendFormat("<td style=\"text-align:left;\">{0}</td>", revenueAcccount["ActivityDate"]);
                    sbsnpRevenueAcccounts.AppendFormat("<td style=\"text-align:left;\">{0}</td>", revenueAcccount["LedgerEntry"]);
                    if (revenueAcccount["ActiveFlg"].ToString() == "1")
                    {
                        sbsnpRevenueAcccounts.AppendLine("      <td style=\"width:50px; text-align:center;\"><img src=\"../../../../../../../../Images/icon_check.gif\" border=0></td>");
                    }
                    else
                    {
                        sbsnpRevenueAcccounts.AppendLine("      <td style=\"width:50px; text-align:center;\"><img src=\"../../../../../../../../Images/icon_check_open.gif\" border=0></td>");
                    }

                    sbsnpRevenueAcccounts.AppendLine("</tr>");
                }
            }
            else
            {
                sbsnpRevenueAcccounts.AppendLine("<tr><td colspan=\"3\">No data available for this community</td></tr>");
            }

            sbsnpRevenueAcccounts.AppendLine("  </tbody>");
            sbsnpRevenueAcccounts.AppendLine("</table>");

            sbsnpRevenueAcccounts.AppendLine("            </td>");
            sbsnpRevenueAcccounts.AppendLine("        </tr>");
            sbsnpRevenueAcccounts.AppendLine("    </tbody>");
            sbsnpRevenueAcccounts.AppendLine("    </table>");

            sbsnpRevenueAcccounts.AppendLine("    </td>");
            sbsnpRevenueAcccounts.AppendLine("</tr>");
            sbsnpRevenueAcccounts.AppendLine("</table>");

            return sbsnpRevenueAcccounts.ToString();
        }

        public string snpPaymentApprovalException()
        {
            StringBuilder sbsnpPaymentApprovalException = new StringBuilder();
            DataSet dsPaymentApprovalException = snpPaymentApprovalExceptionGet();

            sbsnpPaymentApprovalException.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
            sbsnpPaymentApprovalException.AppendLine("<tr>");
            sbsnpPaymentApprovalException.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbsnpPaymentApprovalException.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Commission Approval Exception</div>");

            sbsnpPaymentApprovalException.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbsnpPaymentApprovalException.AppendLine("    <tbody>");
            sbsnpPaymentApprovalException.AppendLine("        <tr>");
            sbsnpPaymentApprovalException.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");
            sbsnpPaymentApprovalException.AppendLine("<table id='PaymentApprovalExceptionGrid' class=\"tablesorter\" cellpadding='0' cellspacing='0' border='0' style='width: 500px;'>");
            sbsnpPaymentApprovalException.AppendLine("  <thead>");
            sbsnpPaymentApprovalException.AppendLine("    <tr>");
            sbsnpPaymentApprovalException.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Employee&nbsp;Name</th>");
            sbsnpPaymentApprovalException.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Community&nbsp;Number</th>");
            sbsnpPaymentApprovalException.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Approver</th>");
            sbsnpPaymentApprovalException.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:50px; text-align:left\">Amount</th>");
            sbsnpPaymentApprovalException.AppendLine("    </tr>");
            sbsnpPaymentApprovalException.AppendLine("  </thead>");
            sbsnpPaymentApprovalException.AppendLine("  <tbody>");

            if (dsPaymentApprovalException.HasRows())
            {
                foreach (DataRow paymentApprovalException in dsPaymentApprovalException.Tables[0].Rows)
                {
                    sbsnpPaymentApprovalException.AppendLine("<tr>");
                    sbsnpPaymentApprovalException.AppendFormat("<td style=\"text-align:left;\">{0}</td>", paymentApprovalException["EmployeeName"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbsnpPaymentApprovalException.AppendFormat("<td style=\"text-align:left;\">{0}</td>", paymentApprovalException["CommunityNumber"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbsnpPaymentApprovalException.AppendFormat("<td style=\"text-align:left;\"><a href='ApprovalAdministration.aspx?PaymentID={0}'>{1}</a></td>", paymentApprovalException["PaymentID"].ToString(), paymentApprovalException["DecisionBy"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbsnpPaymentApprovalException.AppendFormat("<td style=\"text-align:right;\"><a href='PaymentProfile.aspx'>{0}</a></td>", paymentApprovalException["Amount"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbsnpPaymentApprovalException.AppendLine("</tr>");
                }
            }
            else
            {
                sbsnpPaymentApprovalException.AppendLine("<tr><td>No data available for this community</td></tr>");
            }

            sbsnpPaymentApprovalException.AppendLine("  </tbody>");
            sbsnpPaymentApprovalException.AppendLine("</table>");
            sbsnpPaymentApprovalException.AppendLine("            </td>");
            sbsnpPaymentApprovalException.AppendLine("        </tr>");
            sbsnpPaymentApprovalException.AppendLine("    </tbody>");
            sbsnpPaymentApprovalException.AppendLine("    </table>");

            sbsnpPaymentApprovalException.AppendLine("    </td>");
            sbsnpPaymentApprovalException.AppendLine("</tr>");
            sbsnpPaymentApprovalException.AppendLine("</table>");

            return sbsnpPaymentApprovalException.ToString();
        }

        public string snpEmployeeOverride()
        {
            StringBuilder sb = new StringBuilder();
            DataSet dsEmployeeOverride = snpEmployeeOverrideBonusGet();

            sb.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:510px;\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sb.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Employee Override</div>");

            sb.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;width:500px;\">");
            sb.AppendLine("    <tbody>");
            sb.AppendLine("        <tr>");
            sb.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            if (dsEmployeeOverride.HasRows())
            {
                var query = from row in dsEmployeeOverride.Tables[0].AsEnumerable()
                            group row by new
                            {
                                bonusPlanID = row.Field<int>("BonusPlanID"),
                                bonusPlan = row.Field<string>("BonusPlan")
                            } into grp
                            orderby grp.Key.bonusPlanID
                            select new
                            {
                                Key = grp.Key,
                                BonusPlanID = grp.Key.bonusPlanID,
                                BonusPlan = grp.Key.bonusPlan,
                            };
                foreach (var bonusPlan in query)
                {
                    sb.AppendFormat("<div style='text-decoration:underline;font-weight:bold;padding-bottom:5px;'>{0}</div>", bonusPlan.Key.bonusPlan.ToString());

                    sb.AppendLine("<table id='EmployeeOverrideGrid' class=\"tablesorter\" cellpadding='0' cellspacing='0' border='0' style='width: 500px;'>");
                    sb.AppendLine("  <thead>");
                    sb.AppendLine("    <tr>");
                    sb.AppendLine("         <td colspan='3'>&nbsp</th>");
                    sb.AppendLine("         <td class='PortalHeader' colspan='2' style='text-align:center'>Bonus Plan</th>");
                    sb.AppendLine("     </tr>");
                    sb.AppendLine("    <tr>");
                    sb.AppendLine("        <th class='PortalheaderUnderline' style='width:20px; text-align:left' nowrap>&nbsp</th>");
                    sb.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Community Name</th>");
                    sb.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Employee Name</th>");
                    sb.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:60px; text-align:left\">Standard</th>");
                    sb.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:60px; text-align:left\">Override</th>");
                    sb.AppendLine("    </tr>");
                    sb.AppendLine("  </thead>");
                    sb.AppendLine("  <tbody>");

                    var overrideRecs = (from eOverrideItem in dsEmployeeOverride.Tables[0].AsEnumerable()
                                        where eOverrideItem["BonusPlanID"].ToString() == bonusPlan.Key.bonusPlanID.ToString()
                                        select eOverrideItem);

                    //Dictionary<string, string> iconFields = new Dictionary<string, string>();
                    //iconFields.Add("Bonus Plan ID", "BonusPlanID");
                    //iconFields.Add("Community ID", "CustomerID");
                    //iconFields.Add("Effective Date", "EffectiveDT");
                    //iconFields.Add("Resident Name", "ResidentName");
                    //iconFields.Add("Billing ID", "admit_id");
                    //iconFields.Add("Room Number", "RoomNumber");
                    //iconFields.Add("Rate", "Rate");
                    //iconFields.Add("Personal Care", "CareLevel");
                    //iconFields.Add("Ancillary Care", "AncillaryCare");
                    //iconFields.Add("Create By", "CreateBy");
                    //iconFields.Add("Create Date", "CreateDT");
                    //iconFields.Add("Modify By", "ModifyBy");
                    //iconFields.Add("Modify Date", "ModifyDT");

                    string[,] iconStyle = new string[,]
                    {
                        {"", ""},
                        {"border-bottom: solid 1px Black;", "border-bottom: solid 1px Black;"},
                        {"", ""},
                        {"", ""},
                        {"", ""},
                        {"", ""},
                        {"", ""},
                        {"", ""},
                        {"border-bottom: solid 1px Black;", "border-bottom: solid 1px Black;"},
                        {"", ""},
                        {"", ""},
                        {"", ""},
                        {"", ""}
                    };

                    if (overrideRecs.Count() > 0)
                    {
                        foreach (var overrideRec in overrideRecs)
                        {
                            sb.AppendLine("<tr>");
                            sb.AppendLine("<td>&nbsp;</td>"); //JAMES replace this what the below commented out code
                            //sb.AppendLine(AtriaBase.UI.Popup.InformationIconGet(overrideRec, iconFields, "", iconStyle));
                            sb.AppendFormat("<td style=\"text-align:left;\">{0}</td>", overrideRec["Community"].ToString());
                            sb.AppendFormat("<td style=\"text-align:left;\">{0}</td>", overrideRec["EmployeeName"].ToString());
                            sb.AppendFormat("<td style=\"text-align:right;\">{0}</td>", overrideRec["JobCodeBonusPercentage"].ToString());
                            sb.AppendFormat("<td style=\"text-align:right;\">{0}</td>", overrideRec["EmployeeBonusPercentage"].ToString());
                            sb.AppendLine("</tr>");
                        }
                    }
                    else
                    {
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td colspan='5'>No data to display</td>");
                        sb.AppendLine("</tr>");
                    }

                    sb.AppendLine(" </tbody>");
                    sb.AppendLine("</table>");
                }
            }
            else
            {
                sb.AppendLine("No data to display");
            }

            sb.AppendLine("                     </td>");
            sb.AppendLine("                 </tr>");
            sb.AppendLine("             </tbody>");
            sb.AppendLine("         </table>");
            sb.AppendLine("             </div>");
            sb.AppendLine("         </td>");
            sb.AppendLine("     </tr>");
            sb.AppendLine(" </table>");

            return sb.ToString();
        }

        public string snpManagePaymentApproval()
        {
            StringBuilder sbsnpManagePaymentApproval = new StringBuilder();
            DataSet dsManagePaymentApproval = snpManagePaymentApprovalGet();

            sbsnpManagePaymentApproval.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
            sbsnpManagePaymentApproval.AppendLine("<tr>");
            sbsnpManagePaymentApproval.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbsnpManagePaymentApproval.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Manage Payment Approval</div>");
            sbsnpManagePaymentApproval.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbsnpManagePaymentApproval.AppendLine("    <tbody>");
            sbsnpManagePaymentApproval.AppendLine("        <tr>");
            sbsnpManagePaymentApproval.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");
            sbsnpManagePaymentApproval.AppendLine("<table id='ManagePaymentApprovalGrid' cellpadding='0' cellspacing='0' border='0' style='width: 500px;' class=\"tablesorter\">");
            sbsnpManagePaymentApproval.AppendLine("  <thead>");
            sbsnpManagePaymentApproval.AppendLine("    <tr>");
            sbsnpManagePaymentApproval.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Employee&nbsp;Name</th>");
            //sbsnpManagePaymentApproval.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Community</th>");
            sbsnpManagePaymentApproval.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Community&nbsp;Number</th>");
            sbsnpManagePaymentApproval.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Approver</th>");
            sbsnpManagePaymentApproval.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:50px; text-align:left\">Amount</th>");
            sbsnpManagePaymentApproval.AppendLine("    </tr>");
            sbsnpManagePaymentApproval.AppendLine("  </thead>");
            sbsnpManagePaymentApproval.AppendLine("  <tbody>");

            if (dsManagePaymentApproval.HasRows())
            {
                foreach (DataRow paymentApproval in dsManagePaymentApproval.Tables[0].Rows)
                {
                    sbsnpManagePaymentApproval.AppendLine("<tr>");
                    sbsnpManagePaymentApproval.AppendFormat("<td style=\"text-align:left;padding-right:5px;\">{0}</td>", paymentApproval["EmployeeName"].ToString());
                    sbsnpManagePaymentApproval.AppendFormat("<td style=\"text-align:left;padding-right:5px;\">{0}</td>", paymentApproval["CommunityNumber"].ToString());
                    //sbsnpManagePaymentApproval.AppendFormat("<td style=\"text-align:left;padding-right:5px;\">{0}</td>", paymentApproval["Community"].ToString());
                    sbsnpManagePaymentApproval.AppendFormat("<td style=\"text-align:left;\"><a href='ApprovalAdministration.aspx?PaymentID={0}'>{1}</a></td>", paymentApproval["PaymentID"].ToString(), paymentApproval["ApproverName"].ToString());
                    sbsnpManagePaymentApproval.AppendFormat("<td style=\"text-align:right;\"><a href='PaymentProfile.aspx'>{0}</a></td>", paymentApproval["Amount"].ToString());
                    sbsnpManagePaymentApproval.AppendLine("</tr>");
                }
            }
            else
            {
                sbsnpManagePaymentApproval.AppendLine("<tr><td colspan='4'>No data to display</td></tr>");
            }

            sbsnpManagePaymentApproval.AppendLine("  </tbody>");
            sbsnpManagePaymentApproval.AppendLine("</table>");
            sbsnpManagePaymentApproval.AppendLine("            </td>");
            sbsnpManagePaymentApproval.AppendLine("        </tr>");
            sbsnpManagePaymentApproval.AppendLine("    </tbody>");
            sbsnpManagePaymentApproval.AppendLine("    </table>");
            sbsnpManagePaymentApproval.AppendLine("    </td>");
            sbsnpManagePaymentApproval.AppendLine("</tr>");
            sbsnpManagePaymentApproval.AppendLine("</table>");

            return sbsnpManagePaymentApproval.ToString();
        }

        public string snpPayrollSchedule(DataSet dsPayrollSchedule)
        {
            StringBuilder sbsnpPayrollSchedule = new StringBuilder();

            sbsnpPayrollSchedule.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
            sbsnpPayrollSchedule.AppendLine("<tr>");
            sbsnpPayrollSchedule.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbsnpPayrollSchedule.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Payroll Schedule</div>");
            sbsnpPayrollSchedule.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbsnpPayrollSchedule.AppendLine("    <tbody>");
            sbsnpPayrollSchedule.AppendLine("        <tr>");
            sbsnpPayrollSchedule.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");
            sbsnpPayrollSchedule.AppendLine("<table id='PayrollScheduleGrid' cellpadding='0' cellspacing='0' border='0' style='width: 500px;' class=\"tablesorter\">");
            sbsnpPayrollSchedule.AppendLine("  <thead>");
            sbsnpPayrollSchedule.AppendLine("    <tr>");
            sbsnpPayrollSchedule.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Email&nbsp;Date</th>");
            sbsnpPayrollSchedule.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Processed&nbsp;Date</th>");
            sbsnpPayrollSchedule.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:150px; text-align:left\">Payroll&nbsp;Date</th>");
            sbsnpPayrollSchedule.AppendLine("        <th class=\"PortalheaderUnderline\" style=\"width:50px; text-align:left\">Active</th>");
            sbsnpPayrollSchedule.AppendLine("    </tr>");
            sbsnpPayrollSchedule.AppendLine("  </thead>");
            sbsnpPayrollSchedule.AppendLine("  <tbody>");

            if (dsPayrollSchedule.HasRows())
            {
                foreach (DataRow payrollSchedule in dsPayrollSchedule.Tables[0].Rows)
                {
                    sbsnpPayrollSchedule.AppendLine("<tr>");
                    sbsnpPayrollSchedule.AppendFormat("<td style=\"text-align:left;\">{0}</td>", payrollSchedule["EmailAlertDt"]);
                    sbsnpPayrollSchedule.AppendFormat("<td style=\"text-align:left;\">{0}</td>", payrollSchedule["ProcessDt"]);
                    sbsnpPayrollSchedule.AppendFormat("<td style=\"text-align:left;\">{0}</td>", payrollSchedule["PayrollDt"]);

                    if ((int)payrollSchedule["ActiveFlg"] == 1)
                    {
                        sbsnpPayrollSchedule.AppendLine("      <td style=\"width:50px; text-align:center;\"><img src=\"../../../../../../../../Images/icon_check.gif\" border=0></td>");
                    }
                    else
                    {
                        sbsnpPayrollSchedule.AppendLine("      <td style=\"width:50px; text-align:center;\"><img src=\"../../../../../../../../Images/icon_check_open.gif\" border=0></td>");
                    }

                    sbsnpPayrollSchedule.AppendLine("</tr>");
                }
            }
            else
            {
                sbsnpPayrollSchedule.AppendLine("<tr><td colspan=\"4\">No data available for this community</td></tr>");
            }

            sbsnpPayrollSchedule.AppendLine("  </tbody>");
            sbsnpPayrollSchedule.AppendLine("</table>");
            sbsnpPayrollSchedule.AppendLine("            </td>");
            sbsnpPayrollSchedule.AppendLine("        </tr>");
            sbsnpPayrollSchedule.AppendLine("    </tbody>");
            sbsnpPayrollSchedule.AppendLine("    </table>");

            sbsnpPayrollSchedule.AppendLine("    </td>");
            sbsnpPayrollSchedule.AppendLine("</tr>");
            sbsnpPayrollSchedule.AppendLine("</table>");

            return sbsnpPayrollSchedule.ToString();
        }
    }
}