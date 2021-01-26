/********************************************************************
-- ATRIA SENIOR LIVING GROUP CONFIDENTIAL.  For authorized use only.
-- Except for as expressly authorized by Atria Senior Living,
-- do not disclose, copy, reproduce, distribute, or modify.
-- TM 2012 Atria Senior Living, LLC
*********************************************************************
PURPOSE:		The Business Object for PaymentStatus
AUTHOR:			Tony.Thoman
DATE:			12/16/2012
NOTES:
CHANGE CONTROL:
********************************************************************/

using AtriaEM;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BonusPlan
{
    public class PaymentStatusBO : PaymentStatusDTO
    {
        #region SQL Fetch/Manipulation Methods

        public DataSet PaymentStatusGet()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentStatusGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    Command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    DataAdapter.Fill(DataSet);
                }
            }

            return DataSet;
        }

        public void PaymentStatusDetailGet()
        {
            DataSet dsPaymentStatus = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.PaymentStatusDetailGet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PaymentStatusID", SqlDbType.Int);
                    command.Parameters["@PaymentStatusID"].Value = PaymentStatusID;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dsPaymentStatus);
                        if (dsPaymentStatus.HasRows())
                        {
                            PaymentStatus = dsPaymentStatus.Tables[0].Rows[0]["PaymentStatus"].ToString();
                            PaymentStatusID = dsPaymentStatus.Tables[0].Rows[0]["PaymentStatusID"].ToString();
                            Sort = dsPaymentStatus.Tables[0].Rows[0]["Sort"].ToString();
                            Audit.CreateBy = dsPaymentStatus.Tables[0].Rows[0]["CreateBy"].ToString();
                            Audit.CreateDt = dsPaymentStatus.Tables[0].Rows[0]["CreateDt"].ToString();
                            Audit.ModifyBy = dsPaymentStatus.Tables[0].Rows[0]["ModifyBy"].ToString();
                            Audit.ModifyDt = dsPaymentStatus.Tables[0].Rows[0]["ModifyDt"].ToString();
                            Audit.ActiveFlg = dsPaymentStatus.Tables[0].Rows[0]["ActiveFlg"].ToString();
                        }
                    }
                }
            }
        }

        public void PaymentStatusInsert()
        {
            DataSet dsPaymentStatus = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.PaymentStatusInsert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PaymentStatus", SqlDbType.VarChar);
                    command.Parameters["@PaymentStatus"].Value = PaymentStatus;

                    command.Parameters.Add("@CreateBy", SqlDbType.VarChar);
                    command.Parameters["@CreateBy"].Value = Audit.CreateBy;

                    command.Parameters.Add("@Sort", SqlDbType.Int);
                    command.Parameters["@Sort"].Value = Sort;

                    command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    command.Parameters.Add("@PaymentStatusID", SqlDbType.Int);
                    command.Parameters["@PaymentStatusID"].Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    PaymentStatusID = command.Parameters["@PaymentStatusID"].Value.ToString();
                }
            }
        }

        public void PaymentStatusUpdate()
        {
            DataSet dsPaymentStatus = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand command = new SqlCommand("BonusPlan.PaymentStatusUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PaymentStatusID", SqlDbType.Int);
                    command.Parameters["@PaymentStatusID"].Value = PaymentStatusID;

                    command.Parameters.Add("@PaymentStatus", SqlDbType.VarChar);
                    command.Parameters["@PaymentStatus"].Value = PaymentStatus;

                    command.Parameters.Add("@Sort", SqlDbType.Int);
                    command.Parameters["@Sort"].Value = Sort;

                    command.Parameters.Add("@ModifyBy", SqlDbType.VarChar);
                    command.Parameters["@ModifyBy"].Value = Audit.ModifyBy;

                    command.Parameters.Add("@ActiveFlg", SqlDbType.Int);
                    command.Parameters["@ActiveFlg"].Value = Audit.ActiveFlg;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion SQL Fetch/Manipulation Methods

        #region Grid

        public string PaymentStatusGrid()
        {
            StringBuilder sbHTMLOutput = new StringBuilder();

            sbHTMLOutput.AppendLine("<table id='PaymentStatusGrid' class='tablesorter' cellpadding='0' cellspacing='0' border='0' style='width: 700px;'>");
            sbHTMLOutput.AppendLine("  <thead>");
            sbHTMLOutput.AppendLine("    <tr>");
            sbHTMLOutput.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">&nbsp;</td>");
            sbHTMLOutput.AppendLine("        <th class=\"columnHeader\" style=\"width:20px;\">ID</td>");
            sbHTMLOutput.AppendLine("        <th class=\"columnHeader\" style=\"width:560px;\">PaymentStatus</td>");
            sbHTMLOutput.AppendLine("        <th class=\"columnHeader\" style=\"width:50px;\">Sort</td>");
            sbHTMLOutput.AppendLine("        <th class=\"columnHeader\" style=\"width:50px;\">Active</td>");
            sbHTMLOutput.AppendLine("    </tr>");
            sbHTMLOutput.AppendLine("  </thead>");
            sbHTMLOutput.AppendLine("  <tbody>");

            DataSet dsPaymentStatus = PaymentStatusGet();

            if (dsPaymentStatus.HasRows())
            {
                Dictionary<string, string> paymentStatusFields = new Dictionary<string, string>();
                paymentStatusFields.Add("PaymentStatus", "PaymentStatus");
                paymentStatusFields.Add("Sort", "Sort");
                paymentStatusFields.Add("Created By", "CreatedBy");
                paymentStatusFields.Add("Created Date", "CreatedDate");
                paymentStatusFields.Add("Modify By", "ModifyBy");
                paymentStatusFields.Add("Modify Date", "ModifyDT");

                foreach (DataRow Results in dsPaymentStatus.Tables[0].Rows)
                {
                    sbHTMLOutput.AppendFormat("<tr onmouseover=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='#6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='PaymentStatusProfile.aspx?PaymentStatusID={0}'\">", Results["PaymentStatusID"].ToString());
                    sbHTMLOutput.AppendLine(AtriaBase.UI.Popup.InformationIconGet(Results, paymentStatusFields));
                    sbHTMLOutput.AppendLine("         <td class='columnData' style=\"text-align:right;\" >" + Results["PaymentStatusID"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("         <td class='columnData'>" + Results["PaymentStatus"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("         <td class='columnData' style=\"text-align:right;\">" + Results["Sort"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    sbHTMLOutput.AppendLine("         <td class=\"columnData\" style=\"text-align:center;\" >" + ((Results["ActiveFlg"].WhenNullOrEmpty("0").Equals("0")) ? "<img src='../../../../../../../images/icon_check_open.gif'>" : "<img src='../../../../../../../images/icon_check.gif'>") + "</td>");
                    sbHTMLOutput.AppendLine("      </tr>");
                }
            }
            sbHTMLOutput.AppendLine("   </tbody>");
            sbHTMLOutput.AppendLine("</table>");
            sbHTMLOutput.AppendLine("<div style='min-height: 300px;'>");
            sbHTMLOutput.AppendLine("</div>");

            return sbHTMLOutput.ToString();
        }

        #endregion Grid
    }
}