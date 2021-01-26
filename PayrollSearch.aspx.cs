using Atria.Framework;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class PayrollSearch : System.Web.UI.Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();

        #region local vars

        private string userName;
        private string paymentDate;

        #endregion local vars

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2397";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            userName = objSecurity.Username;

            objMenuBar.Username = objSecurity.Username;
            objMenuBar.ApplicationName = objSecurity.ApplicationName;

            if (Request.QueryString.HasValue("EffectiveDt", typeof(DateTime)))
            {
                objSecurity.EffectiveDT = Request.QueryString["EffectiveDt"].ToString();
            }
            else
            {
                objSecurity.EffectiveDT = DateTime.Today.ToShortDateString();
            }

            SecurityMenu.Text = objSecurity.MenuBar();
            SecurityMenu.Text += objMenuBar.MenuUserProfile();
            SecurityTitle.Text = objSecurity.Title;
            SecurityBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br />", RegexOptions.Multiline);

            if (!Page.IsPostBack)
            {
                FormInitialize();
                DataToFormBind();
                if (PaymentDt.Items.Count > 0)
                {
                    Search();
                }
                else
                {
                    //put something in the box
                    PaymentDt.Items.Add("No Dates Found");
                    SearchSubmit.Enabled = false;
                }
            }
        }

        #region button events

        protected void SearchSubmit_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PayrollSearch.aspx");
        }

        #endregion button events

        public void Search()
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                DataSet dsPayroll = PayrollByUserPayrollDateGet();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Payroll Date", paymentDate);

                int rowCount = dsPayroll.HasRows() ? dsPayroll.Tables[0].Rows.Count : 0;

                PaymentSearchCriteriaGrid.Text = AtriaBase.UI.Search.SearchCriteriaGrid(parameters, rowCount, 750, 200);

                if (Excel.Checked)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>
                    {
                        {"PaymentID", "Payment ID"},
                        {"PaymentDt", "Payroll Date"},
                        {"Community", "Community Name"},
                        {"Amount", "Amount"},
                        {"CustomerID", "Customer ID"},
                        {"CustomerName", "Customer"},
                        {"PaymentType", "Type"},
                    };

                    AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsPayroll, "Payroll Search List", fields, AtriaBase.UI.OfficeExportType.Excel);
                }
                else
                {
                    PayrollSearchGrid.Text = PayrollSearchGridGet(dsPayroll);
                }
            }
            else
            {
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            AtriaBase.UI.ListControl.ListPopulate(PaymentDt, "PaymentDt", "PaymentDt", PayrollDateByEmployeeIDGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.Nothing);
        }

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
            paymentDate = PaymentDt.SelectedValue;
        }

        public bool ServerValidate(ref string errors)
        {
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        #region data calls

        public DataSet PayrollDateByEmployeeIDGet()
        {
            DataSet dsPayroll = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PayrollDateByEmployeeIDGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    Command.Parameters["@UserName"].Value = userName.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayroll);
                    }
                }
            }

            return dsPayroll;
        }

        public DataSet PayrollByUserPayrollDateGet()
        {
            DataSet dsPayroll = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PayrollByUserPayrollDateGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@UserName", SqlDbType.VarChar);
                    Command.Parameters["@UserName"].Value = userName.NullIfEmpty();

                    Command.Parameters.Add("@PayrollDt", SqlDbType.VarChar);
                    Command.Parameters["@PayrollDt"].Value = paymentDate.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayroll);
                    }
                }
            }

            return dsPayroll;
        }

        #endregion data calls

        #region grid display

        public string PayrollSearchGridGet(DataSet dsPayroll)
        {
            StringBuilder sbPayroll = new StringBuilder();

            sbPayroll.AppendLine("<table id='PayrollSearchGrid' border='0' class='tablesorter' cellpadding='0' cellspacing='0' style='width:1024px;'>");
            sbPayroll.AppendLine("   <thead>");
            sbPayroll.AppendLine("      <tr>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:20px;'>&nbsp</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Payment ID</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:200px;'>Community</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Amount</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Type</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Customer</th>");
            sbPayroll.AppendLine("      </tr>");
            sbPayroll.AppendLine("   </thead>");
            sbPayroll.AppendLine("   <tbody>");

            if (dsPayroll.HasRows())
            {
                Dictionary<string, string> payrollFields = new Dictionary<string, string>();
                payrollFields.Add("Payment ID", "PaymentID");
                payrollFields.Add("Community ID", "CommunityID");
                payrollFields.Add("Customer ID", "CustomerID");

                foreach (DataRow row in dsPayroll.Tables[0].Rows)
                {
                    sbPayroll.AppendLine("   <tr>");
                    sbPayroll.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, payrollFields));
                    sbPayroll.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["PaymentID"].ToString());
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["Community"].ToString());
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["Amount"].ToString());
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["PaymentType"].ToString());
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["CustomerName"].ToString());
                    sbPayroll.AppendLine("   </tr>");
                }
            }
            else
            {
                sbPayroll.AppendLine("   <tr><td colspan='6'>No Data to Display</td></tr>");
            }

            sbPayroll.AppendLine("   </tbody>");
            sbPayroll.AppendLine("</table>");

            return sbPayroll.ToString();
        }
    }

    #endregion grid display
}