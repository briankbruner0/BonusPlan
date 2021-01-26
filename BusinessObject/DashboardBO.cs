using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BonusPlan
{
    public class DashboardBO : DashboardDTO
    {
        #region SQL

        public DataSet MoveInRevenueActivityGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.MoveInRevenueActivityGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@OperationClusterID", SqlDbType.Int);
                    command.Parameters["@OperationClusterID"].Value = OperationClusterID.NullIfEmpty();

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpMoveInRevenueActivityGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpMoveInRevenueActivityGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet MoveInRevenueActivityByCommunityGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.MoveInRevenueActivityByCommunityGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpApprovalByUsernameGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpApprovalByUsernameGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpApprovalByUsernameDetailGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpApprovalByUsernameDetailGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpAdjustmentBankGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpAdjustmentBankGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpAdjustmentBankDetailGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpAdjustmentBankDetailGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpPotentialCommissionGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpPotentialCommissionGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpPotentialCommissionDetailGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpPotentialCommissionDetailGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        public DataSet snpNetCommissionGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpNetCommissionGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        //public DataSet snpRespiteResidentGet()
        //{
        //    DataSet dsApproval = new DataSet();
        //    using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand("BonusPlan.snpRespiteResidentGet", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
        //            command.Parameters["@CommunityNumber"].Value = CommunityNumber;

        //            command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
        //            command.Parameters["@EffectiveDT"].Value = EffectiveDT.NullIfEmpty();

        //            SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
        //            DataAdapter.Fill(dsApproval);
        //        }
        //    }
        //    return dsApproval;
        //}

        public DataSet snpPaymentStatusGet()
        {
            DataSet dsApproval = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BonusPlan.snpPaymentStatusGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600;

                    command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    command.Parameters["@UserName"].Value = Audit.UserName;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(command);
                    DataAdapter.Fill(dsApproval);
                }
            }
            return dsApproval;
        }

        #endregion SQL

        #region Grids

        public string snpMoveInRevenueActivity()
        {
            StringBuilder sbMoveInRevenueActivity = new StringBuilder();
            DataSet dsMoveInRevenueActivity = snpMoveInRevenueActivityGet();

            sbMoveInRevenueActivity.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:900px;\">");
            sbMoveInRevenueActivity.AppendLine("<tr>");
            sbMoveInRevenueActivity.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbMoveInRevenueActivity.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Move In Revenue Activity</div>");

            sbMoveInRevenueActivity.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbMoveInRevenueActivity.AppendLine("    <tbody>");
            sbMoveInRevenueActivity.AppendLine("        <tr>");
            sbMoveInRevenueActivity.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");
            sbMoveInRevenueActivity.AppendLine("        <table id='MoveInRevenueStatusGrid' cellpadding='0' cellspacing='0' border='0' style='width: 900px;'>");
            sbMoveInRevenueActivity.AppendLine("            <tr>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:50px;padding-right:5px;padding-left:5px;\">Res.&nbsp;Agr.</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:120px;padding-right:5px;\">Name</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:60px;padding-right:5px;\">Customer&nbsp;ID</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:85px;padding-right:5px;\">Move&nbsp;In&nbsp;Dt</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:85px;padding-right:5px;\">Move&nbsp;Out&nbsp;Dt</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:45px;padding-right:5px;\">Stay</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:180px;padding-right:5px;\">Description</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:85px;padding-right:5px;\">Move&nbsp;In</td>");
            sbMoveInRevenueActivity.AppendLine("                <td class=\"PortalheaderUnderline\" style=\"width:85px;padding-right:5px;\">Look&nbsp;Back</td>");
            sbMoveInRevenueActivity.AppendLine("            </tr>");

            if (dsMoveInRevenueActivity.HasRows())
            {
                var distinctRows = (from DataRow dRow in dsMoveInRevenueActivity.Tables[0].Rows
                                    select new
                                    {
                                        CustomerID = dRow["CustomerID"],
                                        EventHistoryID = dRow["EventHistoryID"],
                                        ResidentName = dRow["ResidentName"],
                                        MoveInDT = dRow["MoveInDT"],
                                        MoveOutDT = dRow["MoveOutDt"],
                                        PaidDT = dRow["PaidDT"],
                                        MoveInRevenueStatus = dRow["MoveInRevenueStatus"],
                                        MoveInRevenueStatusID = dRow["MoveInRevenueStatusID"],
                                        BillingID = dRow["BillingID"],
                                        ResidencyAgreementFlg = dRow["ResidencyAgreementFlg"],
                                        DayOfResidence = dRow["DayOfResidence"],
                                        AmountAtMaturityTotal = dRow["AmountAtMaturityTotal"],
                                        AmountAtLookBackTotal = dRow["AmountAtLookBackTotal"],
                                    }
                                    ).Distinct();

                foreach (var distinctRow in distinctRows)
                {
                    string cssMoveInRevenueStatusHeader = "", cssMoveInRevenueStatusBody = "";

                    switch (distinctRow.MoveInRevenueStatusID.ToString())
                    {
                        case "1":
                            cssMoveInRevenueStatusHeader = "style='background-color: #65BA62;'";
                            cssMoveInRevenueStatusBody = "style='background-color: #B2DEB1;'";
                            break;

                        case "2":
                            cssMoveInRevenueStatusHeader = "style='font-weight: bold; background-color: #B6B6B7;'";
                            cssMoveInRevenueStatusBody = "style='background-color: #DDDEE0;'";
                            break;

                        case "3":
                            cssMoveInRevenueStatusHeader = "style='background-color: #99CCFF;'";
                            cssMoveInRevenueStatusBody = "style='background-color: #D0E4FC;'";
                            break;

                        case "4":
                            cssMoveInRevenueStatusHeader = "style='font-weight: bold; background-color: #F8F384;'";
                            cssMoveInRevenueStatusBody = "style='background-color: #FBFAE2;'";
                            break;
                    }

                    sbMoveInRevenueActivity.AppendFormat("<tr><td {0} colspan='9' style=\"padding-left:5px\">{1}</td></tr>", cssMoveInRevenueStatusHeader, distinctRow.MoveInRevenueStatus.ToString());
                    sbMoveInRevenueActivity.AppendFormat("<tr {0}><td style=\"text-align:center\"><img src='{1}' /></td>", cssMoveInRevenueStatusBody, distinctRow.ResidencyAgreementFlg.ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", distinctRow.ResidentName.ToString());
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right\">{0}</td>", distinctRow.CustomerID.ToString());
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right\">{0}</td>", distinctRow.MoveInDT.ToString());
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right\">{0}</td>", distinctRow.MoveOutDT.ToString().WhenNullOrEmpty("&nbsp;"));
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right;padding-right:5px\">{0}</td>", distinctRow.DayOfResidence.ToString());
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right;\">{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right;\">{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendLine("</tr>");

                    var RevenueDetail = (from DataRow dRow in dsMoveInRevenueActivity.Tables[0].Rows
                                         where dRow["CustomerID"].ToString() == distinctRow.CustomerID.ToString()
                                         && dRow["EventHistoryID"].ToString() == distinctRow.EventHistoryID.ToString()
                                         select new
                                         {
                                             CustomerID = dRow["CustomerID"],
                                             EventHistoryID = dRow["EventHistoryID"],
                                             ResidentName = dRow["ResidentName"],
                                             MoveInDT = dRow["MoveInDT"],
                                             BillingID = dRow["BillingID"],
                                             Description = dRow["Description"],
                                             DayOfResidence = dRow["DayOfResidence"],
                                             AmountAtMaturity = dRow["AmountAtMaturity"],
                                             AmountAtLookBack = dRow["AmountAtLookBack"],
                                         }).Distinct();

                    foreach (var revenueRow in RevenueDetail)
                    {
                        sbMoveInRevenueActivity.AppendFormat("<tr {0}>", cssMoveInRevenueStatusBody);
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                        sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", revenueRow.Description.ToString());
                        sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right;padding-right:5px;\">{0}</td>", revenueRow.AmountAtMaturity.ToString().MoneyFormat());
                        sbMoveInRevenueActivity.AppendFormat("<td style=\"text-align:right;padding-right:5px;\">{0}</td>", revenueRow.AmountAtLookBack.ToString().MoneyFormat());
                        sbMoveInRevenueActivity.AppendLine("</tr>");
                    }
                    //Total Revenue
                    sbMoveInRevenueActivity.AppendFormat("<tr {0}>", cssMoveInRevenueStatusBody);
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"border-top:1px solid black;\">{0}</td>", "Total Revenue Potential");
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"border-top:1px solid black;text-align:right;padding-right:5px;\">{0}</td>", distinctRow.AmountAtMaturityTotal.ToString().MoneyFormat());
                    sbMoveInRevenueActivity.AppendFormat("<td style=\"border-top:1px solid black;text-align:right;padding-right:5px;\">{0}</td>", distinctRow.AmountAtLookBackTotal.ToString().MoneyFormat());
                    sbMoveInRevenueActivity.AppendLine("</tr>");
                    //Percentage Row
                    sbMoveInRevenueActivity.AppendFormat("<tr {0} colspan='9'>", cssMoveInRevenueStatusBody);
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendFormat("<td>{0}</td>", "&nbsp;");
                    sbMoveInRevenueActivity.AppendLine("</tr>");
                }
            }
            else
            {
                sbMoveInRevenueActivity.AppendLine("<tr><td colspan='9'>No data available for this community</td></tr>");
            }

            sbMoveInRevenueActivity.AppendLine("</table>");

            sbMoveInRevenueActivity.AppendLine("            </td>");
            sbMoveInRevenueActivity.AppendLine("        </tr>");
            sbMoveInRevenueActivity.AppendLine("    </tbody>");
            sbMoveInRevenueActivity.AppendLine("    </table>");

            sbMoveInRevenueActivity.AppendLine("    </td>");
            sbMoveInRevenueActivity.AppendLine("</tr>");
            sbMoveInRevenueActivity.AppendLine("</table>");
            return sbMoveInRevenueActivity.ToString();
        }

        public string snpAdjustmentBank()
        {
            StringBuilder sbAdjustment = new StringBuilder();
            DataSet dsAdjustment = snpAdjustmentBankGet();
            DataSet dsAdjustmentDetail = snpAdjustmentBankDetailGet();

            sbAdjustment.AppendLine("<script type=\"text/javascript\">");
            sbAdjustment.AppendLine("       $(document).ready(function () {");
            sbAdjustment.AppendLine("           $(\".AdjustmentBankDetail\").hide();");
            sbAdjustment.AppendLine("           $(\".AdjustmentBank\").click(function(){");
            sbAdjustment.AppendLine("               $(\".AdjustmentBankDetail\").toggle();");
            sbAdjustment.AppendLine("           });");
            sbAdjustment.AppendLine("           $(\".AdjustmentBank\").css('cursor', 'pointer');");
            sbAdjustment.AppendLine("       });");
            sbAdjustment.AppendLine("</script>");

            sbAdjustment.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:220px;\">");
            sbAdjustment.AppendLine("<tr>");
            sbAdjustment.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbAdjustment.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Adjustment Bank</div>");

            sbAdjustment.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbAdjustment.AppendLine("    <tbody>");
            sbAdjustment.AppendLine("        <tr>");
            sbAdjustment.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            sbAdjustment.AppendLine("           <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
            sbAdjustment.AppendLine("               <tr class=\"AdjustmentBank\">");
            sbAdjustment.AppendLine("                   <td>");
            sbAdjustment.AppendLine("                       $");
            sbAdjustment.AppendLine("                   </td>");

            if (dsAdjustment.HasRows())
            {
                sbAdjustment.AppendFormat("                 <td style='text-align: right;'>{0}</td>", dsAdjustment.Tables[0].Rows[0]["AdjustmentBankAmount"].ToString().WhenNullOrEmpty("0.00").AccountingFormat());
            }
            else
            {
                sbAdjustment.AppendLine("                   <td style='text-align: right;'>0.00</td>");
            }

            sbAdjustment.AppendLine("               </tr>");

            if (dsAdjustmentDetail.HasRows())
            {
                sbAdjustment.AppendLine("               <tr class=\"AdjustmentBankDetail\">");
                sbAdjustment.AppendLine("                   <td colspan='2'>");
                sbAdjustment.AppendLine("                       <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
                sbAdjustment.AppendLine("                           <tr>");
                sbAdjustment.AppendLine("                               <td>&nbsp;</td>");
                sbAdjustment.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbAdjustment.AppendLine("                                   Customer");
                sbAdjustment.AppendLine("                               </td>");
                sbAdjustment.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbAdjustment.AppendLine("                                   Type");
                sbAdjustment.AppendLine("                               </td>");
                sbAdjustment.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbAdjustment.AppendLine("                                   Amount");
                sbAdjustment.AppendLine("                               </td>");
                sbAdjustment.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbAdjustment.AppendLine("                                   &nbsp;");
                sbAdjustment.AppendLine("                               </td>");
                sbAdjustment.AppendLine("                           </tr>");

                Dictionary<string, string> revenueFields = new Dictionary<string, string>();
                revenueFields.Add("Customer ID", "CustomerID");
                revenueFields.Add("Move In Date", "MoveInDT");
                revenueFields.Add("Revenue ID", "MoveInRevenueID");

                Dictionary<string, string> noteField = new Dictionary<string, string>();
                noteField.Add("Note", "Note");

                foreach (DataRow row in dsAdjustmentDetail.Tables[0].Rows)
                {
                    sbAdjustment.AppendLine("                           <tr>");
                    sbAdjustment.AppendFormat("                               {0}", AtriaBase.UI.Popup.InformationIconGet(row, revenueFields));
                    sbAdjustment.AppendFormat("                               <td>{0}</td>", row["Resident"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbAdjustment.AppendFormat("                               <td>{0}</td>", row["Type"].ToString());
                    sbAdjustment.AppendFormat("                               <td style='text-align:right;padding-right:5px;'>{0}</td>", row["Amount"].ToString().AccountingFormat());
                    sbAdjustment.AppendFormat("                               {0}", row["Note"].ToString().Length > 0 ? AtriaBase.UI.Popup.InformationIconGet(row, noteField, "../../../../images/icon_note.gif") : "<td>&nbsp;</td>");
                    sbAdjustment.AppendLine("                           </tr>");
                }
                sbAdjustment.AppendLine("                       </table>");
                sbAdjustment.AppendLine("                   </td>");
                sbAdjustment.AppendLine("               </tr>");
            }

            sbAdjustment.AppendLine("               </table>");
            sbAdjustment.AppendLine("            </td>");
            sbAdjustment.AppendLine("        </tr>");
            sbAdjustment.AppendLine("    </tbody>");
            sbAdjustment.AppendLine("    </table>");

            sbAdjustment.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">");
            sbAdjustment.AppendLine("       M = Manual Transaction<br/>");
            sbAdjustment.AppendLine("       LB = Lookback System Transaction</div>");
            sbAdjustment.AppendLine("    </td>");
            sbAdjustment.AppendLine("</tr>");
            sbAdjustment.AppendLine("</table>");

            return sbAdjustment.ToString();
        }

        public string snpPotentialComission()
        {
            StringBuilder sbPotential = new StringBuilder();
            DataSet dsPotential = snpPotentialCommissionGet();
            DataSet dsPotentialDetail = snpPotentialCommissionDetailGet();

            sbPotential.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:220px;\">");
            sbPotential.AppendLine("<tr>");
            sbPotential.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            // Changed 'Potential Commision' to 'Potential Bonus' - 1/29/13 - Frank Baker
            sbPotential.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Potential Bonus</div>");

            sbPotential.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbPotential.AppendLine("    <tbody>");
            sbPotential.AppendLine("        <tr>");
            sbPotential.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            sbPotential.AppendLine("           <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
            sbPotential.AppendLine("               <tr>");
            sbPotential.AppendLine("                   <td>");
            sbPotential.AppendLine("                   $");
            sbPotential.AppendLine("                   </td>");
            sbPotential.AppendLine("                   <td style='text-align: right;'>");

            if (dsPotential.HasRows())
            {
                sbPotential.AppendFormat("                       {0}", dsPotential.Tables[0].Rows[0]["PotentialCommission"].ToString().WhenNullOrEmpty("0.00").AccountingFormat());
            }
            else
            {
                sbPotential.AppendLine("                      0.00");
            }

            sbPotential.AppendLine("                   </td>");
            sbPotential.AppendLine("               </tr>");

            if (dsPotentialDetail.HasRows())
            {
                sbPotential.AppendLine("               <tr>");
                sbPotential.AppendLine("                   <td colspan='2'>");
                sbPotential.AppendLine("                       <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
                sbPotential.AppendLine("                           <tr>");
                sbPotential.AppendLine("                               <td>&nbsp;</td>");
                sbPotential.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbPotential.AppendLine("                                   Customer");
                sbPotential.AppendLine("                               </td>");
                sbPotential.AppendLine("                               <td class=\"PortalheaderUnderline\">");
                sbPotential.AppendLine("                                   Amount");
                sbPotential.AppendLine("                               </td>");
                sbPotential.AppendLine("                           </tr>");

                Dictionary<string, string> revenueFields = new Dictionary<string, string>();
                revenueFields.Add("Customer ID", "CustomerID");
                revenueFields.Add("Move In Date", "MoveInDt");
                revenueFields.Add("Revenue ID", "MoveInRevenueID");

                foreach (DataRow row in dsPotentialDetail.Tables[0].Rows)
                {
                    sbPotential.AppendLine("                           <tr>");
                    sbPotential.AppendFormat("                               {0}", AtriaBase.UI.Popup.InformationIconGet(row, revenueFields));
                    sbPotential.AppendFormat("                               <td>{0}</td>", row["Resident"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPotential.AppendFormat("                               <td style='text-align:right'>{0}</td>", row["Amount"].ToString().AccountingFormat());
                    sbPotential.AppendLine("                           </tr>");
                }
                sbPotential.AppendLine("                       </table>");
                sbPotential.AppendLine("                   </td>");
                sbPotential.AppendLine("               </tr>");
            }

            sbPotential.AppendLine("           </table>");

            sbPotential.AppendLine("            </td>");
            sbPotential.AppendLine("        </tr>");
            sbPotential.AppendLine("    </tbody>");
            sbPotential.AppendLine("    </table>");

            sbPotential.AppendLine("    </td>");
            sbPotential.AppendLine("</tr>");
            sbPotential.AppendLine("</table>");

            return sbPotential.ToString();
        }

        public string snpNetCommission()
        {
            StringBuilder sbNetCommission = new StringBuilder();
            DataSet dsNetCommission = snpNetCommissionGet();

            sbNetCommission.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:220px;\">");
            sbNetCommission.AppendLine("<tr>");
            sbNetCommission.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            // Changed 'Net Commision' to 'Net Bonus' - 1/29/13 - Frank Baker
            sbNetCommission.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Total Bonus</div>");

            sbNetCommission.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbNetCommission.AppendLine("    <tbody>");
            sbNetCommission.AppendLine("        <tr>");
            sbNetCommission.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            sbNetCommission.AppendLine("           <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
            sbNetCommission.AppendLine("               <tr>");
            sbNetCommission.AppendLine("                   <td>");
            sbNetCommission.AppendLine("                   $");
            sbNetCommission.AppendLine("                   </td>");
            sbNetCommission.AppendLine("                   <td style='text-align: right;'>");

            if (dsNetCommission.HasRows())
            {
                sbNetCommission.AppendFormat("                       {0}", dsNetCommission.Tables[0].Rows[0]["NetCommission"].ToString().AccountingFormat().WhenNullOrEmpty("0.00").AccountingFormat());
            }
            else
            {
                sbNetCommission.AppendLine("                       0.00");
            }

            sbNetCommission.AppendLine("                   </td>");
            sbNetCommission.AppendLine("               </tr>");
            sbNetCommission.AppendLine("           </table>");

            sbNetCommission.AppendLine("            </td>");
            sbNetCommission.AppendLine("        </tr>");
            sbNetCommission.AppendLine("    </tbody>");
            sbNetCommission.AppendLine("    </table>");

            sbNetCommission.AppendLine("    </td>");
            sbNetCommission.AppendLine("</tr>");
            sbNetCommission.AppendLine("</table>");

            return sbNetCommission.ToString();
        }

        public string snpApprovalByUserName()
        {
            StringBuilder sbApproval = new StringBuilder();
            DataSet dsApproval = snpApprovalByUsernameGet();
            DataSet dsApprovalDetail = snpApprovalByUsernameDetailGet();

            //sbApproval.AppendLine("<script type=\"text/javascript\">");
            //sbApproval.AppendLine("       $(document).ready(function () {");
            //sbApproval.AppendLine("           $(\".ApprovalDetail\").hide();");
            //sbApproval.AppendLine("           $(\".Approval\").click(function(){");
            //sbApproval.AppendLine("               $(this).next(\"tr\").next(\"tr\").toggle();");
            //sbApproval.AppendLine("           });");
            //sbApproval.AppendLine("           $(\".Approval\").css('cursor', 'pointer');");
            //sbApproval.AppendLine("       });");
            //sbApproval.AppendLine("</script>");

            sbApproval.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:220px;\">");
            sbApproval.AppendLine("<tr>");
            sbApproval.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbApproval.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Approvals</div>");

            sbApproval.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbApproval.AppendLine("    <tbody>");
            sbApproval.AppendLine("        <tr>");
            sbApproval.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            if (dsApproval.HasRows())
            {
                foreach (DataRow row in dsApproval.Tables[0].Rows)
                {
                    sbApproval.AppendLine("           <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
                    sbApproval.AppendLine("               <tr class='Approval'>");
                    sbApproval.AppendFormat("                   <td class='PortalheaderUnderline'>{0}</td>", row["UserName"].ToString());
                    sbApproval.AppendFormat("                   <td style='text-align: right;border-bottom:solid 1px #F0F0F0;'>${0}</td>", row["Amount"].ToString().AccountingFormat());
                    sbApproval.AppendLine("               </tr>");
                    sbApproval.AppendLine("               <tr class='Approval'>");
                    sbApproval.AppendLine("                   <td colspan='2'>");
                    sbApproval.AppendLine("                       <table cellpadding='0' cellspacing='0' border='0' style='width: 190px;margin-left:15px;'>");
                    sbApproval.AppendLine("                         <tr>");
                    sbApproval.AppendFormat("                           <td>{0}</td>", row["Approver"].ToString());
                    sbApproval.AppendLine("                             <td>");
                    sbApproval.AppendFormat("                               <a onclick=\"ApprovePayment('{0}');\"><img src='../../../../images/icon_approve.gif' border='0' alt='Approve Payment' /></a>", row["PaymentToApprovalID"]);
                    sbApproval.AppendLine("                             </td>");
                    //sbApproval.AppendLine("                             <td>&nbsp;</td>");
                    //sbApproval.AppendLine("                             <td>");
                    //sbApproval.AppendFormat("                               <a onclick=\"DenyPayment('{0}');\"><img src='../../../../images/icon_deny.gif' border='0' alt='Deny Payment' /></a>", row["PaymentToApprovalID"]);
                    //sbApproval.AppendLine("                             </td>");
                    sbApproval.AppendLine("                             <td>&nbsp;</td>");
                    sbApproval.AppendLine("                             <td>");
                    sbApproval.AppendFormat("                               <a onclick=\"DisplayExceptionNote('{0}');\"><img src='../../../../images/icon_late.gif' border='0' alt='Create an Exception' /></a>", row["PaymentToApprovalID"]);
                    sbApproval.AppendLine("                             </td>");
                    sbApproval.AppendLine("                         </tr>");
                    sbApproval.AppendLine("                         <tr>");
                    sbApproval.AppendLine("                             <td colspan='4'>");
                    sbApproval.AppendFormat("                               <div style='display:none;' name='divExceptionNote{0}' id='divExceptionNote{0}'>", row["PaymentToApprovalID"]);
                    sbApproval.AppendFormat("                                     <textarea rows='4' cols='33'  name='exceptionNote{0}' id='exceptionNote{0}'></textarea>", row["PaymentToApprovalID"]);
                    sbApproval.AppendFormat("                                   <input type='button' value='Submit' onclick=\"CreateException('{0}');\" />", row["PaymentToApprovalID"]);
                    sbApproval.AppendLine("                                 </div>");
                    sbApproval.AppendLine("                             </td>");
                    sbApproval.AppendLine("                         </tr>");
                    sbApproval.AppendLine("                       </table>");
                    sbApproval.AppendLine("                   </td>");
                    sbApproval.AppendLine("               </tr>");

                    if (dsApprovalDetail.HasRows())
                    {
                        Dictionary<string, string> approvalFields = new Dictionary<string, string>();
                        approvalFields.Add("Customer ID", "CustomerID");
                        approvalFields.Add("Move In Date", "MoveInDt");
                        approvalFields.Add("Community Number", "CommunityNumber");

                        sbApproval.AppendLine("               <tr class='ApprovalDetail'>");
                        sbApproval.AppendLine("                   <td colspan='2'>");
                        sbApproval.AppendLine("                       <table cellpadding='0' cellspacing='0' border='0' style='width: 200px;'>");
                        sbApproval.AppendLine("                         <tr>");
                        sbApproval.AppendLine("                             <td>&nbsp</td>");
                        sbApproval.AppendLine("                             <td class='PortalheaderUnderline'>Customer</td>");
                        sbApproval.AppendLine("                             <td class='PortalheaderUnderline'>Amount</td>");
                        sbApproval.AppendLine("                             <td class='PortalheaderUnderline'>&nbsp;</td>");
                        sbApproval.AppendLine("                         </tr>");

                        foreach (DataRow detail in dsApprovalDetail.Tables[0].Rows)
                        {
                            Dictionary<string, string> noteFields = new Dictionary<string, string>();
                            noteFields.Add("Note", "Note");
                            noteFields.Add("Create By", "CreateBy");

                            if (row["ApprovalGUID"].ToString() == detail["ApprovalGUID"].ToString())
                            {
                                sbApproval.AppendLine("                         <tr>");
                                sbApproval.AppendFormat("                           {0}", AtriaBase.UI.Popup.InformationIconGet(detail, approvalFields));
                                sbApproval.AppendFormat("                           <td>{0}</td>", detail["Resident"].ToString().WhenNullOrEmpty("&nbsp;"));
                                sbApproval.AppendFormat("                           <td style='text-align:right;padding-right:5px;'>{0}</td>", detail["Amount"].ToString());
                                sbApproval.AppendFormat("                           {0}", detail["Note"].ToString().Length > 0 ? AtriaBase.UI.Popup.InformationIconGet(detail, noteFields, "../../../../images/icon_note.gif") : "<td>&nbsp;</td>");
                                sbApproval.AppendLine("                         </tr>");
                            }
                        }

                        sbApproval.AppendLine("                        </table>");
                        sbApproval.AppendLine("                   </td>");
                        sbApproval.AppendLine("               </tr>");
                    }
                    sbApproval.AppendLine("           </table>");
                    sbApproval.AppendLine("<br/>");
                }
            }
            else
            {
                sbApproval.AppendLine("           <table cellpadding='0' cellspacing='0' border='0' style='width: 200px'>");
                sbApproval.AppendLine("               <tr>");
                sbApproval.AppendLine("                   <td>No approvals</td>");
                sbApproval.AppendLine("                 </tr>");
                sbApproval.AppendLine("             </table>");
            }

            sbApproval.AppendLine("            </td>");
            sbApproval.AppendLine("        </tr>");
            sbApproval.AppendLine("    </tbody>");
            sbApproval.AppendLine("    </table>");

            sbApproval.AppendLine("    </td>");
            sbApproval.AppendLine("</tr>");
            sbApproval.AppendLine("</table>");

            return sbApproval.ToString();
        }

        //public string snpRespitResident()
        //{
        //    StringBuilder sbRespite = new StringBuilder();
        //    DataSet dsRespite = snpRespiteResidentGet();

        //    sbRespite.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
        //    sbRespite.AppendLine("<tr>");
        //    sbRespite.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
        //    sbRespite.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Respite Residents</div>");

        //    sbRespite.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
        //    sbRespite.AppendLine("    <tbody>");
        //    sbRespite.AppendLine("        <tr>");
        //    sbRespite.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

        //    sbRespite.AppendLine("          <table cellpadding='0' cellspacing='0' border='0' style='width: 490px'>");
        //    sbRespite.AppendLine("               <tr>");
        //    sbRespite.AppendLine("                   <td class='PortalheaderUnderline'>Name</td>");
        //    sbRespite.AppendLine("                   <td class='PortalheaderUnderline'>Customer ID</td>");
        //    sbRespite.AppendLine("                   <td class='PortalheaderUnderline'>Repsite Date</td>");
        //    sbRespite.AppendLine("               </tr>");

        //    if (dsRespite.HasRows())
        //    {
        //        foreach (DataRow row in dsRespite.Tables[0].Rows)
        //        {
        //            sbRespite.AppendLine("               <tr>");
        //            sbRespite.AppendFormat("                   <td>{0}, {1}</td>", row["LastName"].ToString().WhenNullOrEmpty("&nbsp;"), row["FirstName"].ToString().WhenNullOrEmpty("&nbsp;"));
        //            sbRespite.AppendFormat("                   <td>{0}</td>", row["CustomerID"].ToString().WhenNullOrEmpty("&nbsp;"));
        //            sbRespite.AppendFormat("                   <td>{0}</td>", row["MoveInDate"].ToString().WhenNullOrEmpty("&nbsp;"));
        //            sbRespite.AppendLine("               </tr>");
        //        }
        //    }
        //    else
        //    {
        //        sbRespite.AppendLine("               <tr><td colspan='3'>No data to display</td></tr>");
        //    }

        //    sbRespite.AppendLine("          </table>");

        //    sbRespite.AppendLine("            </td>");
        //    sbRespite.AppendLine("        </tr>");
        //    sbRespite.AppendLine("    </tbody>");
        //    sbRespite.AppendLine("    </table>");

        //    sbRespite.AppendLine("    </td>");
        //    sbRespite.AppendLine("</tr>");
        //    sbRespite.AppendLine("</table>");

        //    return sbRespite.ToString();
        //}

        public string snpPaymentStatus()
        {
            StringBuilder sbPayment = new StringBuilder();
            DataSet dsPayment = snpPaymentStatusGet();

            sbPayment.AppendLine("<table border=\"1\" cellspacing=\"0\" cellpadding=\"3\" style=\"width:500px;\">");
            sbPayment.AppendLine("<tr>");
            sbPayment.AppendLine("    <td style=\"background-color:#f7f7f7;border:3px solid #efefef;\">");
            sbPayment.AppendLine("    <div style=\"color: #4a6d94; padding-bottom: 0px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; font-weight: bold;\">Approval Status</div>");

            sbPayment.AppendLine("    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:5px;\">");
            sbPayment.AppendLine("    <tbody>");
            sbPayment.AppendLine("        <tr>");
            sbPayment.AppendLine("          <td style=\"background-color:#FFFFFF;padding:5px;border-top:#e5e5e5 1px solid;border-bottom:#d2d2d2 1px solid;border-left:#dbdbdb 1px solid;border-right:#dbdbdb 1px solid;;\"> ");

            sbPayment.AppendLine("          <table cellpadding='0' cellspacing='0' border='0' style='width: 490px'>");
            sbPayment.AppendLine("               <tr>");
            sbPayment.AppendLine("                   <td class='PortalheaderUnderline'>Amount</td>");
            sbPayment.AppendLine("                   <td class='PortalheaderUnderline'>Job Role</td>");
            sbPayment.AppendLine("                   <td class='PortalheaderUnderline'>UserName</td>");
            //sbPayment.AppendLine("                   <td class='PortalheaderUnderline'>Age</td>");  // Removed per christina - 02/04/13 - Tony
            sbPayment.AppendLine("               </tr>");

            if (dsPayment.HasRows())
            {
                foreach (DataRow row in dsPayment.Tables[0].Rows)
                {
                    sbPayment.AppendLine("               <tr>");
                    sbPayment.AppendFormat("                   <td>{0}</td>", row["Amount"].ToString().AccountingFormat().WhenNullOrEmpty("&nbsp;"));
                    sbPayment.AppendFormat("                   <td>{0}</td>", row["JobCategoryName"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayment.AppendFormat("                   <td>{0}</td>", row["UserName"].ToString().WhenNullOrEmpty("&nbsp;"));
                    //sbPayment.AppendFormat("                   <td>{0}</td>", row["Age"].ToString().WhenNullOrEmpty("&nbsp;"));   // Removed per christina - 02/04/13 - Tony
                    sbPayment.AppendLine("               </tr>");
                }
            }
            else
            {
                sbPayment.AppendLine("               <tr><td colspan='3'>No data to display</td></tr>");
            }

            sbPayment.AppendLine("            </td>");
            sbPayment.AppendLine("        </tr>");
            sbPayment.AppendLine("    </tbody>");
            sbPayment.AppendLine("    </table>");

            sbPayment.AppendLine("    </td>");
            sbPayment.AppendLine("</tr>");
            sbPayment.AppendLine("</table>");

            return sbPayment.ToString();
        }

        #endregion Grids

        #region flash charts

        public string BedOccupancyChartGet(DataSet dsBedOccupancy)
        {
            StringBuilder sbBedOccupancyBuild = new StringBuilder();
            StringBuilder sbBedOccupancy = new StringBuilder();

            if (dsBedOccupancy.HasRows())
            {
                sbBedOccupancyBuild.AppendLine("<chart palette='2' pieSliceDepth='20' SubCaption='Beds' showBorder='0' showPercentValues='1' showLabels='0' ");
                sbBedOccupancyBuild.AppendLine("startingAngle='30' showValues='0' pieRadius='60' enableRotation='1' showZeroPies='0'>");

                foreach (DataRow bedOccupancy in dsBedOccupancy.Tables[0].Rows)
                {
                    sbBedOccupancyBuild.AppendLine("<set label='Occupied Beds' value='" + (Convert.ToInt32(bedOccupancy["OccupiedBedPercentage"]) > 100 ? 100 : bedOccupancy["OccupiedBedPercentage"]) + "' isSliced='0' color='333399' />");
                    sbBedOccupancyBuild.AppendLine("<set label='Unoccupied Beds' value='" + (100 - Convert.ToInt32(bedOccupancy["OccupiedBedPercentage"])) + "' isSliced='1' color='BBE0E3' />");

                    sbBedOccupancyBuild.AppendLine("<styles>");
                    sbBedOccupancyBuild.AppendLine("<definition>");
                    sbBedOccupancyBuild.AppendLine("<style type='font' name='CaptionFont' size='12' color='666666' bold='1' />");
                    sbBedOccupancyBuild.AppendLine("<style type='font' name='SubCaptionFont' bold='1' size='12' />");
                    sbBedOccupancyBuild.AppendLine("</definition>");
                    sbBedOccupancyBuild.AppendLine("<application>");
                    sbBedOccupancyBuild.AppendLine("<apply toObject='caption' styles='CaptionFont' /> ");
                    sbBedOccupancyBuild.AppendLine("<apply toObject='SubCaption' styles='SubCaptionFont' />");
                    sbBedOccupancyBuild.AppendLine("</application>");
                    sbBedOccupancyBuild.AppendLine("</styles>");
                }

                sbBedOccupancyBuild.AppendLine("</chart>");
                sbBedOccupancy.AppendLine(RenderChartHTML("../../../Charts/Pie3D.swf", sbBedOccupancyBuild.ToString(), "BedOccupancyChart", "150", "110", false));
            }
            else
            {
                sbBedOccupancyBuild.AppendLine("No data available for this community");
                sbBedOccupancy.AppendLine(sbBedOccupancyBuild.ToString());
            }
            return sbBedOccupancy.ToString();
        }

        public string RoomOccupancyChartGet(DataSet dsCensus)
        {
            StringBuilder sbCensusBuild = new StringBuilder();
            StringBuilder sbCensus = new StringBuilder();

            if (dsCensus.HasRows())
            {
                sbCensusBuild.AppendLine("<chart palette='2' SubCaption='Units' pieSliceDepth='20'  pieRadius='60' showBorder='0'showLabels='0' ");
                sbCensusBuild.AppendLine("showPercentValues='1' startingAngle='45' animation='1' showValues='0'enableRotation='1' showZeroPies='0'  >");

                foreach (DataRow occupancy in dsCensus.Tables[0].Rows)
                {
                    sbCensusBuild.AppendLine("<set label='Occupied Units' value='" + (Convert.ToInt32(occupancy["OccupiedUnitPercentage"]) > 100 ? 100 : occupancy["OccupiedUnitPercentage"]) + "' isSliced='0' color='333399' />");
                    sbCensusBuild.AppendLine("<set label='Unoccupied Units' value='" + (100 - Convert.ToInt32(occupancy["OccupiedUnitPercentage"])) + "' isSliced='1' color='BBE0E3' />");
                    sbCensusBuild.AppendLine("<styles>");
                    sbCensusBuild.AppendLine("<definition>");
                    sbCensusBuild.AppendLine("<style type='font' name='CaptionFont' size='12' color='666666' bold='1' />");
                    sbCensusBuild.AppendLine("<style type='font' name='SubCaptionFont' bold='1' size='12' />");
                    sbCensusBuild.AppendLine("</definition>");
                    sbCensusBuild.AppendLine("<application>");
                    sbCensusBuild.AppendLine("<apply toObject='caption' styles='CaptionFont' /> ");
                    sbCensusBuild.AppendLine("<apply toObject='SubCaption' styles='SubCaptionFont' />");
                    sbCensusBuild.AppendLine("</application>");
                    sbCensusBuild.AppendLine("</styles>");
                }

                sbCensusBuild.AppendLine("</chart>");
                sbCensus.AppendLine(RenderChartHTML("../../../Charts/Pie3D.swf", sbCensusBuild.ToString(), "UnitOccupancyChart", "150", "110", false));
            }
            else
            {
                sbCensusBuild.AppendLine("No data available for this community");
                sbCensus.AppendLine(sbCensusBuild.ToString());
            }

            return sbCensus.ToString();
        }

        public static string RenderChartHTML(string chartSWF, string strXML, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            //Generate the FlashVars string based on whether dataURL has been provided
            StringBuilder strFlashVars = new StringBuilder();
            string DataFile = string.Empty;
            DataFile = string.Format("&chartWidth={0}&chartHeight={1}&debugMode={2}&dataXML={3}", chartWidth, chartHeight, debugMode == false ? "0" : "1", strXML);

            var _with1 = strFlashVars;
            _with1.AppendFormat(("<!-- START Code Block for Chart {0} -->\n" + Environment.NewLine), chartId);

            _with1.AppendFormat(("<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" width=\"{0}\" height=\"{1}\" name=\"{2}\"\">\n" + Environment.NewLine), chartWidth, chartHeight, chartId);
            _with1.AppendLine("<param name=\"allowScriptAccess\" value=\"always\" />" + Environment.NewLine);
            _with1.AppendLine("<param name=\"wMode\" value=\"opaque\" />" + Environment.NewLine);
            _with1.AppendFormat(("<param name=\"movie\" value=\"{0}\"/>\n" + Environment.NewLine), chartSWF);
            _with1.AppendFormat(("<param name=\"FlashVars\" value=\"{0}\" />\n" + Environment.NewLine), DataFile);
            _with1.AppendLine("<param name=\"quality\" value=\"high\" />" + Environment.NewLine);
            _with1.AppendFormat(("<embed src=\"{0}\" FlashVars=\"{1}\" quality=\"high\" width=\"{2}\" height=\"{3}\" name=\"{4}\" allowScriptAccess=\"always\" type=\"application/x-shockwave-flash\" />\n" + Environment.NewLine), chartSWF, DataFile, chartWidth, chartHeight, chartId);
            _with1.AppendLine("</object>" + Environment.NewLine);

            _with1.AppendFormat(("<!-- END Code Block for Chart {0} -->\n" + Environment.NewLine), chartId);

            return strFlashVars.ToString();
        }

        #endregion flash charts
    }
}