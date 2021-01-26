using AtriaEM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class PaymentScheduleBO : PaymentScheduleDTO
    {
        #region PaymentSchedule

        public DataSet PaymentScheduleGet()
        {
            DataSet dsPayment = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentScheduleGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayment);
                    }
                }
            }

            return dsPayment;
        }

        public DataSet PaymentScheduleDetailGet()
        {
            DataSet dsPayment = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentScheduleDetailGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@PaymentScheduleID", SqlDbType.Int);
                    Command.Parameters["@PaymentScheduleID"].Value = PaymentScheduleID;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayment);

                        if (dsPayment.HasRows())
                        {
                            PaymentScheduleID = dsPayment.Tables[0].Rows[0]["PaymentScheduleID"].ToString();
                            EmailAlertDt = dsPayment.Tables[0].Rows[0]["EmailAlertDt"].ToString();
                            ProcessDt = dsPayment.Tables[0].Rows[0]["ProcessDt"].ToString();
                            PayrollDt = dsPayment.Tables[0].Rows[0]["PayrollDt"].ToString();
                            Audit.CreateBy = dsPayment.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsPayment.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsPayment.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsPayment.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsPayment.Tables[0].Rows[0]["ActiveFlg"].ToString();
                            FinalNotificationDt = dsPayment.Tables[0].Rows[0]["FinalNotificationDt"].ToString();
                            BonusPlanID = dsPayment.Tables[0].Rows[0]["BonusPlanID"].ToString();
                        }
                    }
                }
            }

            return dsPayment;
        }

        public void PaymentScheduleInsert()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentScheduleInsert", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@EmailAlertDt", SqlDbType.VarChar);
                    Command.Parameters["@EmailAlertDt"].Value = EmailAlertDt.NullIfEmpty();

                    Command.Parameters.Add("@ProcessDt", SqlDbType.VarChar);
                    Command.Parameters["@ProcessDt"].Value = ProcessDt.NullIfEmpty();

                    Command.Parameters.Add("@PayrollDt", SqlDbType.VarChar);
                    Command.Parameters["@PayrollDt"].Value = PayrollDt.NullIfEmpty();

                    Command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    Command.Parameters["@CreateBy"].Value = Audit.CreateBy;

                    Command.Parameters.Add("@FinalNotificationDt", SqlDbType.VarChar);
                    Command.Parameters["@FinalNotificationDt"].Value = FinalNotificationDt.NullIfEmpty();

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public void PaymentScheduleUpdate()
        {
            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentScheduleUpdate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@PaymentScheduleID", SqlDbType.Int);
                    Command.Parameters["@PaymentScheduleID"].Value = PaymentScheduleID;

                    Command.Parameters.Add("@EmailAlertDt", SqlDbType.VarChar);
                    Command.Parameters["@EmailAlertDt"].Value = EmailAlertDt;

                    Command.Parameters.Add("@ProcessDt", SqlDbType.VarChar);
                    Command.Parameters["@ProcessDt"].Value = ProcessDt.NullIfEmpty();

                    Command.Parameters.Add("@PayrollDt", SqlDbType.VarChar);
                    Command.Parameters["@PayrollDt"].Value = PayrollDt.NullIfEmpty();

                    Command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    Command.Parameters["@ModifyBy"].Value = Audit.ModifyBy;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    Command.Parameters.Add("@FinalNotificationDt", SqlDbType.VarChar);
                    Command.Parameters["@FinalNotificationDt"].Value = FinalNotificationDt;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.VarChar);
                    Command.Parameters["@BonusPlanID"].Value = BonusPlanID.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                }
            }
        }

        public string PaymentScheduleGrid()
        {
            StringBuilder sbPayment = new StringBuilder();
            DataSet dsPayment = PaymentScheduleGet();

            sbPayment.AppendLine("<table id='PaymentScheduleGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 740px;'>");
            sbPayment.AppendLine("  <thead>");
            sbPayment.AppendLine("    <tr>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">&nbsp;</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">ID</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">Bonus Plan</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:150px;\">Approval Notification Dt</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:150px;\">Final Notification Dt</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:100px;\">Process Date</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:100px;\">Payroll Date</td>");
            sbPayment.AppendLine("        <th class=\"columnHeader\" style=\"width:50px;\">Active</td>");
            sbPayment.AppendLine("    </tr>");
            sbPayment.AppendLine("  </thead>");
            sbPayment.AppendLine("  <tbody>");

            if (dsPayment.HasRows())
            {
                Dictionary<string, string> paymentSchedulePlanFields = new Dictionary<string, string>();
                paymentSchedulePlanFields.Add("Schedule ID", "PaymentScheduleID");
                paymentSchedulePlanFields.Add("Bonus Plan", "BonusPlan");
                paymentSchedulePlanFields.Add("Approval Notification Dt", "EmailAlertDt");
                paymentSchedulePlanFields.Add("Final Notification Dt", "EmailAlertDt");
                paymentSchedulePlanFields.Add("Process Date", "ProcessDt");
                paymentSchedulePlanFields.Add("Payroll Date", "PayrollDt");
                paymentSchedulePlanFields.Add("Create By", "CreateBy");
                paymentSchedulePlanFields.Add("Create Date", "CreateDt");
                paymentSchedulePlanFields.Add("Modify By", "ModifyBy");
                paymentSchedulePlanFields.Add("Modify Date", "ModifyDt");
                paymentSchedulePlanFields.Add("Active", "ActiveFlgImg");

                string[,] paymentSchedulePopupStyle = new string[,]
                {
                    {"border-bottom: solid 1px Black;", "border-bottom: solid 1px Black;"},
                    {"", ""},
                    {"", ""},
                    {"", ""},
                    {"border-bottom: solid 1px Black;", "border-bottom: solid 1px Black;"},
                    {"", ""},
                    {"", ""},
                    {"", ""},
                    {"", ""},
                    {"", ""}
                };

                foreach (DataRow row in dsPayment.Tables[0].Rows)
                {
                    sbPayment.AppendFormat("<tr onMouseOver=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='#6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='PaymentSchedule.aspx?PaymentScheduleID={0}'\">", row["PaymentScheduleID"].WhenNullOrEmpty(""));
                    sbPayment.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, paymentSchedulePlanFields, "", paymentSchedulePopupStyle));
                    sbPayment.AppendFormat("<td>{0}</td>", row["PaymentScheduleID"]);
                    sbPayment.AppendFormat("<td>{0}</td>", row["BonusPlan"]);
                    sbPayment.AppendFormat("<td>{0}</td>", row["EmailAlertDt"]);
                    sbPayment.AppendFormat("<td>{0}</td>", row["FinalNotificationDt"]);
                    sbPayment.AppendFormat("<td>{0}</td>", row["ProcessDt"]);
                    sbPayment.AppendFormat("<td>{0}</td>", row["PayrollDt"]);
                    sbPayment.AppendFormat("<td><img src='{0}' /></td>", row["ActiveFlg"].ToString() == "1" ? "../../../../images/icon_check.gif" : "../../../../images/icon_check_open.gif");
                    sbPayment.AppendLine("</tr>");
                }
            }
            else
            {
                sbPayment.AppendLine("<tr><td colspan='6'>No data to display</td></tr>");
            }

            sbPayment.AppendLine("  </tbody>");
            sbPayment.AppendLine("</table>");

            return sbPayment.ToString();
        }

        #endregion PaymentSchedule
    }
}