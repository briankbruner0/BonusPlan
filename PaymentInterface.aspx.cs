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
    public partial class PaymentInterface : Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();

        private string reportWrkPayrollID;
        private string reportBonusPlanID;

        public string ReportWrkPayrollID
        {
            get { return reportWrkPayrollID.WhenNullOrEmpty(string.Empty); }
            set { reportWrkPayrollID = value; }
        }

        public string ReportBonusPlanID
        {
            get { return reportBonusPlanID.WhenNullOrEmpty(string.Empty); }
            set { reportBonusPlanID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2409";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            if (Request.QueryString.HasValue("EffectiveDt", typeof(DateTime)))
            {
                objSecurity.EffectiveDT = Request.QueryString["EffectiveDt"].ToString();
            }
            else
            {
                objSecurity.EffectiveDT = DateTime.Today.ToShortDateString();
            }

            SecurityMenu.Text = objSecurity.MenuBar();
            SecurityTitle.Text = objSecurity.Title;
            SecurityBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br/>", RegexOptions.Multiline);

            if (!Page.IsPostBack)
            {
                FormInitialize();
            }
        }

        protected void Search()
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                wrkPayrollGenerate();

                DataSet dsPayroll = wrkPayrollGet();
                PaymentGrid.Text = PaymentSearchResultGrid(dsPayroll);

                if (Excel.Checked)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>
                    {
                        {"PayGroup", "Pay Group"},
                        {"ADP_EmployeeID", "Employee ID"},
                        {"EmployeeName", "Employee Name"},
                        {"Type_Of_Earning", "Earning Type"},
                        {"Amount", "Payable Amount"},
                        {"Community", "Community"},
                    };

                    AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsPayroll, "Payroll List", fields, AtriaBase.UI.OfficeExportType.Excel);
                }

                if (dsPayroll.HasRows())
                {
                    //if payroll grid was generated then we show submit button
                    Submit.Visible = true;
                    Cancel.Visible = true;
                }
            }
        }

        #region IDataInputForm Members

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
            ReportBonusPlanID = BonusPlanID.SelectedValue.ToString();
        }

        public void FormInitialize()
        {
            objBonusPlan.Audit.ActiveFlg = "1";
            AtriaBase.UI.ListControl.ListPopulate(BonusPlanID, "BonusPlan", "BonusPlanID", objBonusPlan.BonusPlanGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectAll);
        }

        public bool ServerValidate(ref string errors)
        {
            // Nothing to validate.
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        #region Events

        protected void Submit_Click(object sender, EventArgs e)
        {
            FormToDataBind();

            //generate payroll
            DataSet dsPayroll = PayrollGenerate();

            if (dsPayroll.HasRows())
            {
                Dictionary<string, string> fields = new Dictionary<string, string>
                {
                    {"PayGroup", "Pay Group"},
                    {"ADP_EmployeeID", "Employee ID"},
                    {"EmployeeName", "Employee Name"},
                    {"Type_Of_Earning", "Earning Type"},
                    {"Amount", "Payable Amount"},
                    {"CommunityNumber", "Community Number"},
                };

                AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsPayroll, "Payroll List", fields, AtriaBase.UI.OfficeExportType.Excel);
            }

            //Response.Redirect("PaymentInterface.aspx");
        }

        protected void GeneratePayroll_Click(object sender, EventArgs e)
        {
            Search();
        }

        #endregion Events

        #region SQL

        public DataSet wrkPayrollGet()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("BonusPlan.wrkPayrollGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 6000;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    DataAdapter.Fill(DataSet);
                }
            }

            return DataSet;
        }

        public DataSet wrkPayrollGenerate()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("BonusPlan.wrkPayrollGenerate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 6000;

                    Command.Parameters.Add("@BonusPlanID", SqlDbType.Int);
                    Command.Parameters["@BonusPlanID"].Value = ReportBonusPlanID.NullIfEmpty();

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    DataAdapter.Fill(DataSet);
                }
            }

            return DataSet;
        }

        public DataSet PayrollGenerate()
        {
            DataSet DataSet = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("BonusPlan.PayrollGenerate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 6000;

                    SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);

                    DataAdapter.Fill(DataSet);
                }
            }

            return DataSet;
        }

        #endregion SQL

        #region Grids

        public string PaymentSearchResultGrid(DataSet dsSearchResult)
        {
            StringBuilder HTMLOutput = new StringBuilder();

            HTMLOutput.AppendLine("<table id='PaymentSearchGrid' border='0' class='tablesorter' cellpadding='0' cellspacing='0' style='width:1024px;'>");
            HTMLOutput.AppendLine("   <thead>");
            HTMLOutput.AppendLine("      <tr>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:100px;'>Payroll Date</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:100px;'>Pay Group</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:100px;'>Employee ID</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:200px;'>Employee Name</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:200px;'>Community</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:80px;'>Earning Type</th>");
            HTMLOutput.AppendLine("         <th class='columnHeader' style='width:100px;'>Payable Amount</th>");
            HTMLOutput.AppendLine("      </tr>");
            HTMLOutput.AppendLine("   </thead>");
            HTMLOutput.AppendLine("   <tbody>");

            if (dsSearchResult.HasRows())
            {
                foreach (DataRow Results in dsSearchResult.Tables[0].Rows)
                {
                    HTMLOutput.AppendLine("      <tr>");
                    HTMLOutput.AppendLine("         <td class='columnData' align='right' >" + Results["PayrollDT"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData'>" + Results["PayGroup"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData' align='right'>" + Results["ADP_EmployeeID"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData'>" + Results["EmployeeName"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData'>" + Results["Community"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData'>" + Results["Type_Of_Earning"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("         <td class='columnData' align='right'>" + Results["Amount"].WhenNullOrEmpty("&nbsp;") + "</td>");
                    HTMLOutput.AppendLine("      </tr>");
                }
            }
            else
            {
                HTMLOutput.AppendLine("      <tr>");
                HTMLOutput.AppendLine("         <td colspan='7' class='columnData' align='center' >No Data To Display</td>");
                HTMLOutput.AppendLine("      </tr>");
            }

            HTMLOutput.AppendLine("   </tbody>");
            HTMLOutput.AppendLine("</table>");
            HTMLOutput.AppendLine("<div style='min-height: 50px;'>");
            HTMLOutput.AppendLine("</div>");

            return HTMLOutput.ToString();
        }

        #endregion Grids
    }
}