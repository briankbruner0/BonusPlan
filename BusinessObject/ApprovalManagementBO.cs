using AtriaEM;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class ApprovalManagementBO : ApprovalManagementDTO
    {
        public DataSet ApprovalManagementGet()
        {
            DataSet dsApproval = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalManagementGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentID", SqlDbType.Int);
                    Command.Parameters["@PaymentID"].Value = PaymentID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsApproval);

                        if (dsApproval.HasRows())
                        {
                            BonusPlanID = dsApproval.Tables[0].Rows[0]["BonusPlanID"].ToString();
                        }
                    }
                }
            }

            return dsApproval;
        }

        public DataSet ApprovalManagementDetailGet()
        {
            DataSet dsApproval = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalManagementDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentToApprovalID", SqlDbType.Int);
                    Command.Parameters["@PaymentToApprovalID"].Value = PaymentToApprovalID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsApproval);

                        if (dsApproval.HasRows())
                        {
                            PaymentToApprovalID = dsApproval.Tables[0].Rows[0]["PaymentToApprovalID"].ToString();
                            BonusPlanID = dsApproval.Tables[0].Rows[0]["BonusPlanID"].ToString();
                            JobCategoryID = dsApproval.Tables[0].Rows[0]["JobCategoryID"].ToString();
                            JobCategoryIDAlternate = dsApproval.Tables[0].Rows[0]["JobCategoryIDAlternate"].ToString();
                            UsernameAlternate = dsApproval.Tables[0].Rows[0]["UsernameAlternate"].ToString();
                            //CriteriaValue = dsApproval.Tables[0].Rows[0]["CriteriaValue"].ToString();
                            Sort = dsApproval.Tables[0].Rows[0]["Sort"].ToString();
                            ApproveFlg = dsApproval.Tables[0].Rows[0]["ApproveFlg"].ToString();
                            DenyFlg = dsApproval.Tables[0].Rows[0]["DenyFlg"].ToString();
                            ExceptionFlg = dsApproval.Tables[0].Rows[0]["ExceptionFlg"].ToString();
                            OverrideFlg = dsApproval.Tables[0].Rows[0]["OverrideFlg"].ToString();
                            ExceptionNote = dsApproval.Tables[0].Rows[0]["ExceptionNote"].ToString();
                            AdministrationNote = dsApproval.Tables[0].Rows[0]["AdministrationNote"].ToString();
                            Audit.CreateBy = dsApproval.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsApproval.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsApproval.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsApproval.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsApproval.Tables[0].Rows[0]["ActiveFlg"].ToString();
                        }
                    }
                }
            }

            return dsApproval;
        }

        public void ApprovalManagementUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalManagementUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentToApprovalID", SqlDbType.Int);
                    Command.Parameters["@PaymentToApprovalID"].Value = PaymentToApprovalID.NullIfEmpty();

                    Command.Parameters.Add("@UsernameAlternate", SqlDbType.VarChar);
                    Command.Parameters["@UsernameAlternate"].Value = UsernameAlternate.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryID", SqlDbType.Int);
                    Command.Parameters["@JobCategoryID"].Value = JobCategoryID.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryIDAlternate", SqlDbType.Int);
                    Command.Parameters["@JobCategoryIDAlternate"].Value = JobCategoryIDAlternate.NullIfEmpty();

                    Command.Parameters.Add("@Sort", SqlDbType.Int);
                    Command.Parameters["@Sort"].Value = Sort.NullIfEmpty();

                    Command.Parameters.Add("@ApproveFlg", SqlDbType.Int);
                    Command.Parameters["@ApproveFlg"].Value = ApproveFlg.NullIfEmpty();

                    Command.Parameters.Add("@DenyFlg", SqlDbType.Int);
                    Command.Parameters["@DenyFlg"].Value = DenyFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionFlg", SqlDbType.Int);
                    Command.Parameters["@ExceptionFlg"].Value = ExceptionFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionNote", SqlDbType.VarChar);
                    Command.Parameters["@ExceptionNote"].Value = ExceptionNote.NullIfEmpty();

                    Command.Parameters.Add("@AdministrationNote", SqlDbType.VarChar);
                    Command.Parameters["@AdministrationNote"].Value = AdministrationNote.NullIfEmpty();

                    Command.Parameters.Add("@OverrideFlg", SqlDbType.Int);
                    Command.Parameters["@OverrideFlg"].Value = OverrideFlg.NullIfEmpty();

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Audit.ModifyBy.NullIfEmpty();

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public void ApprovalManagementInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalManagementInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentID", SqlDbType.Int);
                    Command.Parameters["@PaymentID"].Value = PaymentID.NullIfEmpty();

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    Command.Parameters.Add("@UsernameAlternate", SqlDbType.VarChar);
                    Command.Parameters["@UsernameAlternate"].Value = UsernameAlternate.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryID", SqlDbType.Int);
                    Command.Parameters["@JobCategoryID"].Value = JobCategoryID.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryIDAlternate", SqlDbType.Int);
                    Command.Parameters["@JobCategoryIDAlternate"].Value = JobCategoryIDAlternate.NullIfEmpty();

                    Command.Parameters.Add("@Sort", SqlDbType.Int);
                    Command.Parameters["@Sort"].Value = Sort.NullIfEmpty();

                    Command.Parameters.Add("@ApproveFlg", SqlDbType.Int);
                    Command.Parameters["@ApproveFlg"].Value = ApproveFlg.NullIfEmpty();

                    Command.Parameters.Add("@DenyFlg", SqlDbType.Int);
                    Command.Parameters["@DenyFlg"].Value = DenyFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionFlg", SqlDbType.Int);
                    Command.Parameters["@ExceptionFlg"].Value = ExceptionFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionNote", SqlDbType.VarChar);
                    Command.Parameters["@ExceptionNote"].Value = ExceptionNote.NullIfEmpty();

                    Command.Parameters.Add("@AdministrationNote", SqlDbType.VarChar);
                    Command.Parameters["@AdministrationNote"].Value = AdministrationNote.NullIfEmpty();

                    Command.Parameters.Add("@OverrideFlg", SqlDbType.Int);
                    Command.Parameters["@OverrideFlg"].Value = OverrideFlg.NullIfEmpty();

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public DataSet ApprovalManagementPaymentByApprovalIDGet()
        {
            DataSet dsApproval = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalManagementPaymentByApprovalIDGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentID", SqlDbType.Int);
                    Command.Parameters["@PaymentID"].Value = PaymentID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsApproval);
                    }
                }
            }

            return dsApproval;
        }

        public string ApprovalManagementInformationBlock()
        {
            StringBuilder sbApproval = new StringBuilder();
            DataSet dsApproval = ApprovalManagementGet();

            sbApproval.AppendLine("<table id=\"InformationBlock\" class=\"InformationBlock\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:750px;border-collapse:collapse;margin:7px;\">");
            sbApproval.AppendLine(" <tr class=\"InformationBlockTabHeaderContainerRow\">");
            sbApproval.AppendLine("     <td class=\"InformationBlockTabHeaderContainerCell\">");
            sbApproval.AppendLine("         <table id=\"InformationBlockTabHeaderTable\" class=\"InformationBlockTabHeader\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"border-collapse:collapse;\">");
            sbApproval.AppendLine("             <tr id=\"InformationBlockTabHeaderRow\" class=\"InformationBlockTabHeaderRow\">");
            sbApproval.AppendLine("                 <td class=\"InformationBlockTabHeaderCellFirst\" nowrap=\"1\" style=\"width:80px;white-space:nowrap;\"><span>Approval</span></td>");
            sbApproval.AppendLine("                 <td class=\"InformationBlockTabHeaderFolderIconFirst\"><div class=\"InformationBlockTabHeaderFolderIconPad\">&nbsp;</div></td>");
            sbApproval.AppendLine("                 <td class=\"InformationBlockTabHeaderBufferCell\"></td>");
            sbApproval.AppendLine("             </tr>");
            sbApproval.AppendLine("         </table>");
            sbApproval.AppendLine("     </td>");
            sbApproval.AppendLine(" </tr>");
            sbApproval.AppendLine(" <tr class=\"InformationBlockTabContainerRow\">");
            sbApproval.AppendLine("     <td class=\"InformationBlockTabContainerCell\">");
            sbApproval.AppendLine("         <table class=\"InformationBlockTabContainerTable\" border=\"0\">");
            sbApproval.AppendLine("             <tr class=\"InformationBlockTabContainerTR\">");
            sbApproval.AppendLine("                 <td class=\"InformationBlockTabContainerTD\">");
            sbApproval.AppendLine("                     <table id=\"ApprovalTab\" class=\"InformationBlockTab\" border=\"0\" style=\"border-width:0px;display:block;\">");
            sbApproval.AppendLine("                         <tr class=\"InformationBlockSectionRootContainerRow\">");
            sbApproval.AppendLine("                             <td class=\"InformationBlockSectionRootContainerCell\" colspan=\"3\">");
            sbApproval.AppendLine("                                 <table class=\"InformationBlockSectionInnerContainer\" border=\"0\">");
            sbApproval.AppendLine("                                     <tr class=\"InformationBlockSectionInnerContainerRow\">");
            sbApproval.AppendLine("                                         <td class=\"InformationBlockSectionInnerContainerCell\" style=\"width:100%;\">");
            sbApproval.AppendLine("                                             <table class=\"InformationBlockSectionContainer\" border=\"0\">");
            sbApproval.AppendLine("                                                 <tr class=\"InformationBlockSectionContainerRow\">");
            sbApproval.AppendLine("                                                     <td class=\"InformationBlockSectionContainerCell\">");
            sbApproval.AppendLine("                                                         <table id=\"SectionResidentTab\" class=\"InformationBlockSection\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"border-width:0px;border-collapse:collapse;\">");
            sbApproval.AppendLine("                                                             <tr class=\"InformationBlockSectionRow\">");
            sbApproval.AppendLine("                                                                 <td class=\"InformationBlockSectionValue\" colspan=\"2\">");

            if (dsApproval.HasRows())
            {
                sbApproval.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 700px;\">");
                sbApproval.AppendLine("  <tr>");
                sbApproval.AppendLine("      <td>");
                sbApproval.AppendLine("        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" >");
                sbApproval.AppendLine("          <tr>");
                sbApproval.AppendLine("             <td class=\"Label\" style=\"white-space:nowrap;\" nowrap=\"nowrap\">Community:</td>");
                sbApproval.AppendFormat("           <td class=\"Data\">{0}</td>", dsApproval.Tables[0].Rows[0]["Community"].ToString());
                sbApproval.AppendLine("          </tr>");
                sbApproval.AppendLine("          <tr>");
                sbApproval.AppendLine("             <td class=\"Label\" style=\"white-space:nowrap;\" nowrap=\"nowrap\">Employee ID:</td>");
                sbApproval.AppendFormat("           <td class=\"Data\">{0}</td>", dsApproval.Tables[0].Rows[0]["EmployeeID"].ToString());
                sbApproval.AppendLine("          </tr>");
                sbApproval.AppendLine("          <tr>");
                sbApproval.AppendLine("             <td class=\"Label\" style=\"white-space:nowrap;\" nowrap=\"nowrap\">Employee Name:</td>");
                sbApproval.AppendFormat("           <td class=\"Data\">{0}</td>", dsApproval.Tables[0].Rows[0]["UserName"].ToString());
                sbApproval.AppendLine("          </tr>");
                sbApproval.AppendLine("          <tr>");
                sbApproval.AppendLine("             <td class=\"Label\" style=\"white-space:nowrap;\" nowrap=\"nowrap\">Amount:</td>");
                sbApproval.AppendFormat("           <td class=\"Data\">{0}</td>", dsApproval.Tables[0].Rows[0]["Amount"].ToString());
                sbApproval.AppendLine("          </tr>");
                sbApproval.AppendLine(ApprovalManagementPaymentByApprovalIDGrid());
                sbApproval.AppendLine("          </table>");
                sbApproval.AppendLine("      </td>");
                sbApproval.AppendLine("  </tr>");
                sbApproval.AppendLine("</table>");
            }

            sbApproval.AppendLine("                                                                     </td>");
            sbApproval.AppendLine("                                                                  </tr>");
            sbApproval.AppendLine("                                                              </table>");
            sbApproval.AppendLine("                                                         </td>");
            sbApproval.AppendLine("                                                     </tr>");
            sbApproval.AppendLine("                                                 </table>");
            sbApproval.AppendLine("                                             </td>");
            sbApproval.AppendLine("                                         </tr>");
            sbApproval.AppendLine("                                     </table>");
            sbApproval.AppendLine("                                 </td>");
            sbApproval.AppendLine("                             </tr>");
            sbApproval.AppendLine("                         </table>");
            sbApproval.AppendLine("                     </td>");
            sbApproval.AppendLine("                 </tr>");
            sbApproval.AppendLine("           </table>");
            sbApproval.AppendLine("        </td>");
            sbApproval.AppendLine("     </tr>");
            sbApproval.AppendLine("</table>");

            return sbApproval.ToString();
        }

        public string ApprovalManagementGrid()
        {
            StringBuilder sbApproval = new StringBuilder();
            DataSet dsApproval = ApprovalManagementGet();

            sbApproval.AppendLine("<table id='ApprovalWorkflowGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 750px;'>");
            sbApproval.AppendLine("  <thead>");
            sbApproval.AppendLine("    <tr>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">ID</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:100px;\">Job Role</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:100px;\">Job Role Alternate</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:100px;\">User Alternate</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:50px; writing-mode: tb-rl;\">Criteria</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">Sort</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px; writing-mode: tb-rl;\">Approved</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;  writing-mode: tb-rl;\">Denied</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px; writing-mode: tb-rl;\">Exception</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">Note</th>");
            sbApproval.AppendLine("        <th class=\"columnHeader\" style=\"width:20px; writing-mode: tb-rl;\">Override</th>");
            sbApproval.AppendLine("\t\t\t\t<th class=\"columnHeader\" style=\"width:20px;\">Note</th>");
            sbApproval.AppendLine("    </tr>");
            sbApproval.AppendLine("  </thead>");
            sbApproval.AppendLine("  <tbody>");

            if (dsApproval.HasRows())
            {
                foreach (DataRow row in dsApproval.Tables[0].Rows)
                {
                    sbApproval.AppendFormat("<tr onmouseover=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='#6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='ApprovalAdministration.aspx?CommunityNumber={0}&PaymentID={1}&PaymentToApprovalID={2}'\">", CommunityNumber, row["PaymentID"].ToString(), row["PaymentToApprovalID"].ToString());
                    sbApproval.AppendFormat("<td class=\"columnData\">{0}</td>", row["PaymentToApprovalID"].ToString());
                    sbApproval.AppendFormat("<td class=\"columnData\">{0}</td>", row["JobCategory"].ToString().nbspIfNull());
                    sbApproval.AppendFormat("<td class=\"columnData\">{0}</td>", row["JobCategoryAlternate"].ToString().nbspIfNull());
                    sbApproval.AppendFormat("<td class=\"columnData\">{0}</td>", row["UserNameAlternate"].ToString().nbspIfNull());
                    sbApproval.AppendFormat("<td class=\"columnData\" style='text-align:right'>{0}</td>", row["CriteriaValue"].ToString().nbspIfNull());
                    sbApproval.AppendFormat("<td class=\"columnData\" style='text-align:right'>{0}</td>", row["Sort"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbApproval.AppendFormat("<td class=\"columnData\"><img src='{0}' /></td>", row["ApproveFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");
                    sbApproval.AppendFormat("<td class=\"columnData\"><img src='{0}' /></td>", row["DenyFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");
                    sbApproval.AppendFormat("<td class=\"columnData\"><img src='{0}' /></td>", row["ExceptionFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");

                    if (row["ExceptionNote"].ToString().Length > 0)
                    {
                        sbApproval.AppendFormat("<td class=\"columnData\"><img src='../../../../images/icon_note.gif'></td>");
                    }
                    else
                    {
                        sbApproval.AppendLine("<td>&nbsp;</td>");
                    }

                    sbApproval.AppendFormat("<td class=\"columnData\"><img src='{0}' /></td>", row["OverrideFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");

                    if (row["OverrideNote"].ToString().Length > 0)
                    {
                        sbApproval.AppendFormat("<td class=\"columnData\"><img src='../../../../images/icon_note.gif'></td>");
                    }
                    else
                    {
                        sbApproval.AppendLine("<td>&nbsp;</td>");
                    }
                    sbApproval.AppendLine("</tr>");
                }
            }
            else
            {
                sbApproval.AppendLine("<tr><td colspan='5'>No data to display</td></tr>");
            }

            sbApproval.AppendLine("  </tbody>");
            sbApproval.AppendLine("</table>");

            return sbApproval.ToString();
        }

        public string ApprovalManagementPaymentByApprovalIDGrid()
        {
            DataSet dsApprovalGUIDRow = ApprovalManagementPaymentByApprovalIDGet();

            StringBuilder sbApprovalGUIDGrid = new StringBuilder();

            sbApprovalGUIDGrid.AppendLine("  <table id='ApprovalGUIDGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 100%;'>");
            sbApprovalGUIDGrid.AppendLine("    <thead>");
            sbApprovalGUIDGrid.AppendLine("      <tr>");
            sbApprovalGUIDGrid.AppendLine("         <th class='columnHeader' style='width:35px; text-align:center'>Payroll ID</th>");
            sbApprovalGUIDGrid.AppendLine("         <th class='columnHeader' style='width:150px; text-align:center'>Resident</th>");
            sbApprovalGUIDGrid.AppendLine("         <th class='columnHeader' style='width:100px; text-align:center'>Customer ID</th>");
            sbApprovalGUIDGrid.AppendLine("         <th class='columnHeader' style='width:100px; text-align:center'>Amount</th>");
            sbApprovalGUIDGrid.AppendLine("         <th class='columnHeader' style='width:75px; text-align:center'>Payment Date</th>");
            sbApprovalGUIDGrid.AppendLine("      </tr>");
            sbApprovalGUIDGrid.AppendLine("    </thead>");
            sbApprovalGUIDGrid.AppendLine("      <tbody>");

            if (dsApprovalGUIDRow.HasRows())
            {
                foreach (DataRow row in dsApprovalGUIDRow.Tables[0].Rows)
                {
                    sbApprovalGUIDGrid.AppendLine("    <tr>");
                    sbApprovalGUIDGrid.AppendFormat("        <td class='columnData' style='width:25px; text-align:right'>{0}</td>", row["PayrollID"].ToString());
                    sbApprovalGUIDGrid.AppendFormat("        <td class='columnData' style='width:150px;'>{0}</td>", row["ResidentName"].ToString());
                    sbApprovalGUIDGrid.AppendFormat("        <td class='columnData' style='width:100px;  text-align:right'>{0}</td>", row["CustomerID"].ToString());
                    sbApprovalGUIDGrid.AppendFormat("        <td class='columnData' style='width:100px;  text-align:right'>{0}</td>", row["Amount"].ToString());
                    sbApprovalGUIDGrid.AppendFormat("        <td class='columnData' style='width:75px;'>{0}</td>", row["PaymentDT"].ToString());
                    sbApprovalGUIDGrid.AppendLine("    </tr>");
                }
            }
            else
            {
                sbApprovalGUIDGrid.AppendLine("      <tr>");
                sbApprovalGUIDGrid.AppendLine("        <td colspan='5'>No Data to Display.</td>");
                sbApprovalGUIDGrid.AppendLine("      </tr>");
            }

            sbApprovalGUIDGrid.AppendLine("      </tbody>");
            sbApprovalGUIDGrid.AppendLine("    </table>");

            return sbApprovalGUIDGrid.ToString();
        }

        public void ApprovalUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.ApprovalUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@PaymentToApprovalID", SqlDbType.Int);
                    Command.Parameters["@PaymentToApprovalID"].Value = PaymentToApprovalID;

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Audit.UserName;

                    Command.Parameters.Add("@ApproveFlg", SqlDbType.Int);
                    Command.Parameters["@ApproveFlg"].Value = ApproveFlg.NullIfEmpty();

                    Command.Parameters.Add("@DenyFlg", SqlDbType.Int);
                    Command.Parameters["@DenyFlg"].Value = DenyFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionFlg", SqlDbType.Int);
                    Command.Parameters["@ExceptionFlg"].Value = ExceptionFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExceptionNote", SqlDbType.VarChar);
                    Command.Parameters["@ExceptionNote"].Value = ExceptionNote.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }
    }
}