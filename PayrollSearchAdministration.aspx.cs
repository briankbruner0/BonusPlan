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
    public partial class PayrollSearchAdministration : System.Web.UI.Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2399";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

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
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            AtriaBase.UI.ListControl.ListPopulate(PaymentDt, "PaymentDt", "PaymentDt", PayrollDateGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
        }

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
        }

        public void Search()
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                if (EmployeeID.Text.Length == 0)
                {
                    EmployeeHiddenID.Value = "";
                }

                DataSet dsPayroll = PayrollByPayrollDateGet();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Payroll Date", PaymentDt.SelectedValue.Length == 0 ? "All" : PaymentDt.SelectedValue.ToString());
                parameters.Add("Employee ID", EmployeeID.Text.Length == 0 ? "All" : EmployeeID.Text);

                int rowCount = dsPayroll.HasRows() ? dsPayroll.Tables[0].Rows.Count : 0;

                PaymentSearchCriteriaGrid.Text = AtriaBase.UI.Search.SearchCriteriaGrid(parameters, rowCount, 750, 200);

                if (Excel.Checked)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>
                    {
                        {"PaymentID", "Payment ID"},
                        {"PaymentDt", "Payroll Date"},
                        {"Employee", "Employee"},
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

        public bool ServerValidate(ref string errors)
        {
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

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

        #region data calls

        public DataSet PayrollDateGet()
        {
            DataSet dsPayroll = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PayrollDateGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayroll);
                    }
                }
            }

            return dsPayroll;
        }

        public DataSet PayrollByPayrollDateGet()
        {
            DataSet dsPayroll = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PayrollByPayrollDateGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@PayrollDt", SqlDbType.VarChar);
                    Command.Parameters["@PayrollDt"].Value = PaymentDt.SelectedValue.NullIfEmpty();

                    Command.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    Command.Parameters["@EmployeeID"].Value = EmployeeHiddenID.Value.NullIfEmpty();

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
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:75px;'>Payment ID</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:200px;'>Employee</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:150px;'>Community</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Amount</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:100px;'>Type</th>");
            sbPayroll.AppendLine("         <th class='columnHeader' style='width:200px;'>Customer</th>");
            sbPayroll.AppendLine("      </tr>");
            sbPayroll.AppendLine("   </thead>");
            sbPayroll.AppendLine("   <tbody>");

            if (dsPayroll.HasRows())
            {
                Dictionary<string, string> payrollFields = new Dictionary<string, string>();
                payrollFields.Add("Payment ID", "PaymentID");
                payrollFields.Add("Community Number", "CommunityNumber");
                payrollFields.Add("Employee ID", "EmployeeID");
                payrollFields.Add("ADP Employee ID", "ADP_EmployeeID");
                payrollFields.Add("Customer ID", "CustomerID");

                foreach (DataRow row in dsPayroll.Tables[0].Rows)
                {
                    sbPayroll.AppendLine("   <tr>");
                    sbPayroll.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, payrollFields));
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["PaymentID"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["Employee"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["Community"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["Amount"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["PaymentType"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendFormat("   <td class='columnData'>{0}</td>", row["CustomerName"].ToString().WhenNullOrEmpty("&nbsp;"));
                    sbPayroll.AppendLine("   </tr>");
                }
            }
            else
            {
                sbPayroll.AppendLine("   <tr><td colspan='7'>No Data to Display</td></tr>");
            }

            sbPayroll.AppendLine("   </tbody>");
            sbPayroll.AppendLine("</table>");

            return sbPayroll.ToString();
        }
    }

    #endregion grid display
}