using AtriaEM;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class BonusPlanBO : BonusPlanDTO
    {
        #region BonusPlan

        public DataSet BonusPlanGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }

        public DataSet BonusPlanDetailGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);

                        if (dsBonusPlan.HasRows())
                        {
                            BonusPlanID = dsBonusPlan.Tables[0].Rows[0]["BonusPlanID"].ToString();
                            BonusPlan = dsBonusPlan.Tables[0].Rows[0]["BonusPlan"].ToString();
                            ProcessPeriod = dsBonusPlan.Tables[0].Rows[0]["ProcessPeriod"].ToString();
                            PayrollCommunity = dsBonusPlan.Tables[0].Rows[0]["PayrollCommunity"].ToString();
                            ExcludeLookBackFlg = dsBonusPlan.Tables[0].Rows[0]["ExcludeLookBackFlg"].ToString();
                            EmailFlg = dsBonusPlan.Tables[0].Rows[0]["EmailFlg"].ToString();
                            TypeOfEarning = dsBonusPlan.Tables[0].Rows[0]["PS_Type_Of_Earning"].ToString();
                            Audit.CreateBy = dsBonusPlan.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsBonusPlan.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsBonusPlan.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsBonusPlan.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsBonusPlan.Tables[0].Rows[0]["ActiveFlg"].ToString();
                        }
                    }
                }
            }

            return dsBonusPlan;
        }

        public void BonusPlanInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlan", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlan"].Value = BonusPlan;

                    Command.Parameters.Add("@ProcessPeriod", SqlDbType.Int);
                    Command.Parameters["@ProcessPeriod"].Value = ProcessPeriod.NullIfEmpty();

                    Command.Parameters.Add("@ExcludeLookBackFlg", SqlDbType.Int);
                    Command.Parameters["@ExcludeLookBackFlg"].Value = ExcludeLookBackFlg.NullIfEmpty();

                    Command.Parameters.Add("@EmailFlg", SqlDbType.Int);
                    Command.Parameters["@EmailFlg"].Value = EmailFlg.NullIfEmpty();

                    Command.Parameters.Add("@PayrollCommunity", SqlDbType.Int);
                    Command.Parameters["@PayrollCommunity"].Value = PayrollCommunity.NullIfEmpty();

                    Command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    Command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID.NullIfEmpty();

                    Command.Parameters.Add("@LedgerEntryID", SqlDbType.Int);
                    Command.Parameters["@LedgerEntryID"].Value = LedgerEntryID.NullIfEmpty();

                    Command.Parameters.Add("@TypeOfEarning", SqlDbType.VarChar);
                    Command.Parameters["@TypeOfEarning"].Value = TypeOfEarning.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeID", SqlDbType.Int);
                    Command.Parameters["@JobCodeID"].Value = JobCodeID.NullIfEmpty();

                    Command.Parameters.Add("@JobCodePercentage", SqlDbType.VarChar);
                    Command.Parameters["@JobCodePercentage"].Value = JobCodePercentage.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeFlatRate", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeFlatRate"].Value = JobCodeFlatRate.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryID", SqlDbType.Int);
                    Command.Parameters["@JobCategoryID"].Value = JobCategoryID.NullIfEmpty();

                    Command.Parameters.Add("@ApprovalSort", SqlDbType.VarChar);
                    Command.Parameters["@ApprovalSort"].Value = ApprovalSort.NullIfEmpty();

                    Command.Parameters.Add("@ApprovalAmount", SqlDbType.VarChar);
                    Command.Parameters["@ApprovalAmount"].Value = ApprovalAmount.NullIfEmpty();

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public void BonusPlanUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    Command.Parameters.Add("@BonusPlan", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlan"].Value = BonusPlan;

                    Command.Parameters.Add("@PaymentProcessFlg", SqlDbType.Int);
                    Command.Parameters["@PaymentProcessFlg"].Value = PaymentProcessFlg.NullIfEmpty();

                    Command.Parameters.Add("@ExcludeLookBackFlg", SqlDbType.Int);
                    Command.Parameters["@ExcludeLookBackFlg"].Value = ExcludeLookBackFlg.NullIfEmpty();

                    Command.Parameters.Add("@EmailFlg", SqlDbType.Int);
                    Command.Parameters["@EmailFlg"].Value = EmailFlg.NullIfEmpty();

                    Command.Parameters.Add("@RevenueEntryTypeToLedgerEntryID", SqlDbType.VarChar);
                    Command.Parameters["@RevenueEntryTypeToLedgerEntryID"].Value = RevenueEntryTypeToLedgerEntryID.NullIfEmpty();

                    Command.Parameters.Add("@RevenueEntryTypeID", SqlDbType.Int);
                    Command.Parameters["@RevenueEntryTypeID"].Value = RevenueEntryTypeID.NullIfEmpty();

                    Command.Parameters.Add("@LedgerEntryID", SqlDbType.Int);
                    Command.Parameters["@LedgerEntryID"].Value = LedgerEntryID.NullIfEmpty();

                    Command.Parameters.Add("@TypeOfEarning", SqlDbType.VarChar);
                    Command.Parameters["@TypeOfEarning"].Value = TypeOfEarning.NullIfEmpty();

                    Command.Parameters.Add("@BonusPlanToJobCodeID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanToJobCodeID"].Value = BonusPlanToJobCodeID.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeID", SqlDbType.Int);
                    Command.Parameters["@JobCodeID"].Value = JobCodeID.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeCommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeCommunityNumber"].Value = JobCodeCommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeCommissionBase", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeCommissionBase"].Value = JobCodeCommissionBase.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeMultiplier", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeMultiplier"].Value = JobCodeMultiplier.NullIfEmpty();

                    Command.Parameters.Add("@JobCodePercentage", SqlDbType.VarChar);
                    Command.Parameters["@JobCodePercentage"].Value = JobCodePercentage.NullIfEmpty();

                    Command.Parameters.Add("@JobCodeFlatRate", SqlDbType.VarChar);
                    Command.Parameters["@JobCodeFlatRate"].Value = JobCodeFlatRate.NullIfEmpty();

                    Command.Parameters.Add("@JobCategoryID", SqlDbType.Int);
                    Command.Parameters["@JobCategoryID"].Value = JobCategoryID.NullIfEmpty();

                    Command.Parameters.Add("@ApproverManagementID", SqlDbType.VarChar);
                    Command.Parameters["@ApproverManagementID"].Value = ApproverManagementID.NullIfEmpty();

                    Command.Parameters.Add("@ApprovalSort", SqlDbType.VarChar);
                    Command.Parameters["@ApprovalSort"].Value = ApprovalSort.NullIfEmpty();

                    Command.Parameters.Add("@ApprovalAmount", SqlDbType.VarChar);
                    Command.Parameters["@ApprovalAmount"].Value = ApprovalAmount.NullIfEmpty();

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Audit.ModifyBy;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public string BonusPlanQuickLink()
        {
            StringBuilder sbBonusPlan = new StringBuilder();
            DataSet dsBonusPlan = BonusPlanGet();

            sbBonusPlan.AppendLine("<div class='panel panel-default'>");
            sbBonusPlan.AppendLine("    <div class='panel-heading panel-title'>");
            sbBonusPlan.AppendLine("        Bonus Plan Configuration");
            sbBonusPlan.AppendLine("    </div>");
            sbBonusPlan.AppendLine("    <div class='panel-body'>");
            sbBonusPlan.AppendLine("        <div class='col-xs-12 col-sm-6 col-md-4 col-lg-4'>");
            sbBonusPlan.AppendLine("            <table class='table' id='BonusPlanDashboardTable'>");
            sbBonusPlan.AppendLine("                <tbody>");
            if (dsBonusPlan.HasRows())
            {
                foreach (DataRow row in dsBonusPlan.Tables[0].Rows)
                {
                    sbBonusPlan.AppendLine("            <tr>");
                    sbBonusPlan.AppendFormat("              <td><span>{0}</span></td>", row["BonusPlan"].ToString());
                    sbBonusPlan.AppendFormat("              <td><a href='BonusPlanProfile.aspx?BonusPlanID={0}' class='btn btn-primary btn-sm'>View</a></td>", row["BonusPlanID"].ToString());
                    sbBonusPlan.AppendLine("            </tr>");
                }
            }
            else
            {
                sbBonusPlan.AppendLine("No data to display");
            }
            sbBonusPlan.AppendLine("                </tbody>");
            sbBonusPlan.AppendLine("            </table>");
            sbBonusPlan.AppendLine("        </div>");
            sbBonusPlan.AppendLine("    </div>");
            sbBonusPlan.AppendLine("</div>");

            return sbBonusPlan.ToString();
        }

        #endregion BonusPlan

        /// <summary>
        /// Used for type ahead feature on the bonus plan panel
        /// out of standard as far as the name and BO location
        /// </summary>
        /// <returns></returns>
        public DataSet EmployeeSearchGet()
        {
            DataSet dsEmployee = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.EmployeeSearchGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@ADPEmployeeID", SqlDbType.VarChar);
                    Command.Parameters["@ADPEmployeeID"].Value = ADPEmployeeID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsEmployee);
                    }
                }
            }

            return dsEmployee;
        }

        #region Revenue Account

        public DataSet BonusPlanToRevenueDetailGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToRevenueDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);
                    }
                }
            }

            return dsRevenue;
        }

        public string RevenueAccountGrid()
        {
            DataSet dsRevenue = BonusPlanToRevenueDetailGet();
            StringBuilder sbRevenue = new StringBuilder();

            sbRevenue.AppendLine("<div class='table-responsive'>");
            sbRevenue.AppendLine("<table id='RevenueAccountGrid' class='tablesorter table-striped table table-condensed'>");
            sbRevenue.AppendLine("  <thead>");
            sbRevenue.AppendLine("    <tr class='TableRowBorderTop'>");
            sbRevenue.AppendLine("        <th>&nbsp;</th>");
            sbRevenue.AppendLine("        <th>ID</th>");
            sbRevenue.AppendLine("        <th>Event Type</th>");
            sbRevenue.AppendLine("        <th>Ledger Entry</th>");
            sbRevenue.AppendLine("    </tr>");
            sbRevenue.AppendLine("  </thead>");
            sbRevenue.AppendLine("  <tbody>");

            if (dsRevenue.HasRows())
            {
                foreach (DataRow row in dsRevenue.Tables[0].Rows)
                {
                    sbRevenue.AppendLine("<tr>");
                    sbRevenue.AppendFormat("<td><input type='checkbox' id='RevenueAccount{0}' name='RevenueAccount' value='{0}' /></td>", row["RevenueEntryTypeToLedgerEntryID"].ToString());
                    sbRevenue.AppendFormat("<td class='columnData'>{0}</td>", row["RevenueEntryTypeToLedgerEntryID"].ToString());
                    sbRevenue.AppendFormat("<td class='columnData'>{0}</td>", row["RevenueEntryType"].ToString());
                    sbRevenue.AppendFormat("<td class='columnData'>{0}</td>", row["LedgerEntry"].ToString());
                    sbRevenue.AppendLine("</tr>");
                }
            }
            else
            {
                sbRevenue.AppendLine("<tr><td class='columnData' colspan='4'>No data to display</td></tr>");
            }

            sbRevenue.AppendLine("  </tbody>");
            sbRevenue.AppendLine("</table>");
            sbRevenue.AppendLine("</div>");

            return sbRevenue.ToString();
        }

        #endregion Revenue Account

        #region JobCode

        public DataSet BonusPlanToJobCodeEffectiveDateGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToJobCodeEffectiveDateGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }

        public DataSet BonusPlanToJobCodeDetailGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToJobCodeDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);
                    }
                }
            }

            return dsRevenue;
        }

        public string JobCodeGrid()
        {
            DataSet dsJobCode = BonusPlanToJobCodeDetailGet();
            StringBuilder sbJobCode = new StringBuilder();

            sbJobCode.AppendLine("<div class='table-responsive'>");
            sbJobCode.AppendLine("<table id='JobCodeGrid' class='tablesorter table-striped table table-condensed'>");
            sbJobCode.AppendLine("  <thead>");
            sbJobCode.AppendLine("    <tr class='TableRowBorderTop'>");
            sbJobCode.AppendLine("        <th>&nbsp;</th>");
            sbJobCode.AppendLine("        <th class='hidden-xs'>ID</th>");
            sbJobCode.AppendLine("        <th class='hidden-xs'>Job Code</th>");
            sbJobCode.AppendLine("        <th>Job Title</th>");
            sbJobCode.AppendLine("        <th>Community</th>");
            sbJobCode.AppendLine("        <th>Base Comission</th>");
            sbJobCode.AppendLine("        <th>Multiplier</th>");
            sbJobCode.AppendLine("        <th>Percentage</th>");
            sbJobCode.AppendLine("        <th class='hidden-xs'>Flat Rate</th>");
            sbJobCode.AppendLine("    </tr>");
            sbJobCode.AppendLine("  </thead>");
            sbJobCode.AppendLine("  <tbody>");

            if (dsJobCode.HasRows())
            {
                foreach (DataRow row in dsJobCode.Tables[0].Rows)
                {
                    sbJobCode.AppendLine("<tr>");
                    sbJobCode.AppendFormat("<td><input type='checkbox' id='JobCode{0}' name='JobCode' value='{0}' /></td>", row["BonusPlanToJobCodeID"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData hidden-xs'>{0}</td>", row["BonusPlanToJobCodeID"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData hidden-xs'>{0}</td>", row["JobCode"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData'>{0}</td>", row["JobTitle"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbJobCode.AppendFormat("<td class='columnData'>{0}</td>", row["Community"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbJobCode.AppendFormat("<td class='columnData numericShort'>{0}</td>", row["CommissionBase"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbJobCode.AppendFormat("<td class='columnData numericShort'>{0}</td>", row["Multiplier"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbJobCode.AppendFormat("<td class='columnData numericShort'>{0}</td>", row["Percentage"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbJobCode.AppendFormat("<td class='columnData numericShort hidden-xs'>{0}</td>", row["FlatRate"].ToString() == "" ? "&nbsp" : string.Format("{0:N2}", Convert.ToDouble(row["FlatRate"].WhenNullOrEmpty("0"))));
                    sbJobCode.AppendLine("</tr>");
                }
            }
            else
            {
                sbJobCode.AppendLine("<tr><td class='columnData' colspan='6'>No data to display</td></tr>");
            }

            sbJobCode.AppendLine("  </tbody>");
            sbJobCode.AppendLine("</table>");
            sbJobCode.AppendLine("</div>");

            return sbJobCode.ToString();
        }

        public DataSet JobCodeGet()
        {
            DataSet dsJobCode = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.JobCodeGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsJobCode);
                    }
                }
            }

            return dsJobCode;
        }

        #endregion JobCode

        #region User override

        public DataSet BonusPlanToCommunityGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToCommunityGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }

        // RDO 11/30/2016
        // Deletes a community from lnkBonusPlanToCommunity
        public void BonusPlanToCommunityDelete()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToCommunityDelete", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        // RDO 11/30/2016
        // Inserts a community into lnkBonusPlanToCommunity
        public void BonusPlanToCommunityInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToCommunityInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID;

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber;

                    Command.Parameters.Add("@BeginDt", SqlDbType.VarChar);
                    Command.Parameters["@BeginDt"].Value = BeginDt.NullIfEmpty();

                    Command.Parameters.Add("@EndDt", SqlDbType.VarChar);
                    Command.Parameters["@EndDt"].Value = EndDt.NullIfEmpty();

                    Command.Parameters.Add("@Username", SqlDbType.VarChar);
                    Command.Parameters["@Username"].Value = Audit.UserName;

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public DataSet BonusPlanToUserCommunityDetailGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToUserCommunityDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }

        public void BonusPlanToUserCommunityInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToUserCommunityInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    Command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    Command.Parameters["@EmployeeID"].Value = EmployeeID.NullIfEmpty();

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = CommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@Percentage", SqlDbType.VarChar);
                    Command.Parameters["@Percentage"].Value = Percentage.NullIfEmpty();

                    Command.Parameters.Add("@FlatRate", SqlDbType.VarChar);
                    Command.Parameters["@FlatRate"].Value = FlatRate.NullIfEmpty();

                    Command.Parameters.Add("@BeginDt", SqlDbType.VarChar);
                    Command.Parameters["@BeginDt"].Value = BeginDt.NullIfEmpty();

                    Command.Parameters.Add("@EndDt", SqlDbType.VarChar);
                    Command.Parameters["@EndDt"].Value = EndDt.NullIfEmpty();

                    Command.Parameters.Add("@RollforwardFlg", SqlDbType.Int);
                    Command.Parameters["@RollforwardFlg"].Value = RollforwardFlg.NullIfEmpty("0");

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public void BonusPlanToUserCommunityUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToUserCommunityUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanToUserCommunityID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanToUserCommunityID"].Value = BonusPlanToUserCommunityID.NullIfEmpty();

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        #endregion User override

        #region Approval Workflow

        public DataSet BonusPlanToApprovalWorkflowGet()
        {
            DataSet dsBonusPlan = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.BonusPlanToApprovalWorkflowGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsBonusPlan);
                    }
                }
            }

            return dsBonusPlan;
        }

        public string ApprovalWorkflowGrid()
        {
            DataSet dsJobCode = BonusPlanToApprovalWorkflowGet();
            StringBuilder sbJobCode = new StringBuilder();

            sbJobCode.AppendLine("<div class='table-responsive'>");
            sbJobCode.AppendLine("<table id='ApprovalWorkflowGrid' class='tablesorter table-striped table table-condensed'>");
            sbJobCode.AppendLine("  <thead>");
            sbJobCode.AppendLine("    <tr>");
            sbJobCode.AppendLine("        <th colspan='5' class='tableTitle'>Approval Workflow</th>");
            sbJobCode.AppendLine("    </tr>");
            sbJobCode.AppendLine("    <tr>");
            sbJobCode.AppendLine("        <th>&nbsp;</th>");
            sbJobCode.AppendLine("        <th>ID</th>");
            sbJobCode.AppendLine("        <th>Job Role</th>");
            sbJobCode.AppendLine("        <th>Sort</th>");
            sbJobCode.AppendLine("        <th>Amount</th>");
            sbJobCode.AppendLine("    </tr>");
            sbJobCode.AppendLine("  </thead>");
            sbJobCode.AppendLine("  <tbody>");

            if (dsJobCode.HasRows())
            {
                foreach (DataRow row in dsJobCode.Tables[0].Rows)
                {
                    sbJobCode.AppendLine("<tr>");
                    sbJobCode.AppendFormat("<td><input type='checkbox' id='ApprovalWorkflow{0}' name='ApprovalWorkflow' value='{0}' /></td>", row["ApproverManagementID"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData'>{0}</td>", row["ApproverManagementID"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData'>{0}</td>", row["JobCategory"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData' style='text-align:right;'>{0}</td>", row["Sort"].ToString());
                    sbJobCode.AppendFormat("<td class='columnData' style='text-align:right;'>{0}</td>", row["CriteriaValue"].ToString());
                    sbJobCode.AppendLine("</tr>");
                }
            }
            else
            {
                sbJobCode.AppendLine("<tr><td class='columnData' colspan='5'>No data to display</td></tr>");
            }

            sbJobCode.AppendLine("  </tbody>");
            sbJobCode.AppendLine("</table>");
            sbJobCode.AppendLine("</div>");

            return sbJobCode.ToString();
        }

        public DataSet JobCategoryGet()
        {
            DataSet dsJobRole = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.JobCategoryGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsJobRole);
                    }
                }
            }

            return dsJobRole;
        }

        #endregion Approval Workflow
    }
}