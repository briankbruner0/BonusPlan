using Atria.Framework;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class MoveInRevenueSearch : System.Web.UI.Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected CommunityBO objCommunity = new CommunityBO();

        #region

        private string searchCustomerID;
        private string searchCommunityNumber;
        private string searchBeginEffectiveDt;
        private string searchEndEffectiveDt;
        private string searchFirstName;
        private string searchLastName;

        public string SearchCustomerID
        {
            get { return searchCustomerID.WhenNullOrEmpty(string.Empty); }
            set { searchCustomerID = value; }
        }

        public string SearchCommunityNumber
        {
            get { return searchCommunityNumber.WhenNullOrEmpty(string.Empty); }
            set { searchCommunityNumber = value; }
        }

        public string SearchBeginEffectiveDt
        {
            get { return searchBeginEffectiveDt.WhenNullOrEmpty(string.Empty); }
            set { searchBeginEffectiveDt = value; }
        }

        public string SearchEndEffectiveDt
        {
            get { return searchEndEffectiveDt.WhenNullOrEmpty(string.Empty); }
            set { searchEndEffectiveDt = value; }
        }

        public string SearchFirstName
        {
            get { return searchFirstName.WhenNullOrEmpty(string.Empty); }
            set { searchFirstName = value; }
        }

        public string SearchLastName
        {
            get { return searchLastName.WhenNullOrEmpty(string.Empty); }
            set { searchLastName = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2398";
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
            AtriaBase.UI.ListControl.ListPopulate(CommunityID, "Community", "CommunityNumber", objCommunity.CommunityGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectAll);
        }

        public void Search()
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                DataSet dsRevenue = MoveInRevenueSearchGet();

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("Customer ID", SearchCustomerID.Length == 0 ? "All" : SearchCustomerID);
                parameters.Add("Resident First Name", SearchFirstName.Length == 0 ? "All" : SearchFirstName);
                parameters.Add("Resident Last Name", SearchLastName.Length == 0 ? "All" : SearchLastName);
                parameters.Add("Community", SearchCommunityNumber.Length == 0 ? "All" : AtriaBase.UI.ListControl.ListItemTextGet(CommunityID));
                parameters.Add("Effective Date", SearchBeginEffectiveDt + " - " + SearchEndEffectiveDt);

                int rowCount = dsRevenue.HasRows() ? dsRevenue.Tables[0].Rows.Count : 0;

                RevenueSearchCriteriaGrid.Text = AtriaBase.UI.Search.SearchCriteriaGrid(parameters, rowCount, 750, 200);

                if (Excel.Checked)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>
                    {
                        {"MoveInRevenueID", "ID"},
                        {"CustomerID", "Customer ID"},
                        {"Resident", "Resident"},
                        {"Community", "Community"},
                        {"DayOfResidence", "Days Of Residency"},
                        {"MoveInDt", "Move In Date"},
                        {"MoveOutDt", "Move Out Date"},
                    };

                    AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsRevenue, "Detail Revenue Search List", fields, AtriaBase.UI.OfficeExportType.Excel);
                }
                else
                {
                    RevenueSearchGrid.Text = MoveInRevenueSearchGrid(dsRevenue);
                }
            }
            else
            {
            }
        }

        public void DataToFormBind()
        {
            DateTime dtDate = Convert.ToDateTime(DateTime.Today);
            BeginDt.Value = FirstDayOfMonthGet(DateTime.Now.ToShortTimeString());
            EndDt.Value = LastDayOfMonthGet(DateTime.Now.ToShortTimeString());
        }

        public void FormToDataBind()
        {
            SearchCustomerID = CustomerID.Text;
            SearchFirstName = FirstName.Text;
            SearchLastName = LastName.Text;
            SearchCommunityNumber = AtriaBase.UI.ListControl.ListItemValueGet(CommunityID) == "" ? CommunityID.SelectedValue : AtriaBase.UI.ListControl.ListItemValueGet(CommunityID);
            SearchBeginEffectiveDt = BeginDt.Value;
            SearchEndEffectiveDt = EndDt.Value;
        }

        public bool ServerValidate(ref string errors)
        {
            DateTime BeginDateDT;
            DateTime EndDateDT;

            // If there is a Begin Date, it must be in valid form
            if (this.BeginDt.Value.Length > 0)
            {
                if (!DateTime.TryParse(this.BeginDt.Value, out BeginDateDT))
                {
                    errors += "Please enter a valid date for From Date.<br />";
                    this.BeginDt.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.BeginDt.Attributes.CssStyle.Clear();
                }
            }
            else
            {
                errors += "From Date is a required field.<br />";
                this.BeginDt.Attributes.CssStyle.Add("background-color", "Pink");
            }

            // If there is an End Date, it must be in valid form

            if (this.EndDt.Value.Length > 0)
            {
                if (!DateTime.TryParse(this.EndDt.Value, out EndDateDT))
                {
                    errors += "Please enter a valid date for To Date.<br />";
                    this.EndDt.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.EndDt.Attributes.CssStyle.Clear();
                }
            }
            else
            {
                errors += "To Date is a required field.<br />";
                this.EndDt.Attributes.CssStyle.Add("background-color", "Pink");
            }

            if (this.BeginDt.Value.Length > 0 && this.EndDt.Value.Length > 0)
            {
                // The End Date cannot be less than the Begin Date
                if (Convert.ToDateTime(this.EndDt.Value) < Convert.ToDateTime(this.BeginDt.Value))
                {
                    errors += "The To Date must be later than the From Date.<br />";
                    this.EndDt.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.EndDt.Attributes.CssStyle.Clear();
                }

                if (Convert.ToDateTime(this.BeginDt.Value).AddMonths(6) < Convert.ToDateTime(this.EndDt.Value))
                {
                    errors += "The To Date cannot be more than 6 months greater than the From Date.<br />";
                    this.BeginDt.Attributes.CssStyle.Add("background-color", "Pink");
                    this.EndDt.Attributes.CssStyle.Add("background-color", "Pink");
                }
            }

            ErrorMessage.Visible = true;
            ErrorMessage.Text = errors;
            return errors.Length == 0;
        }

        #endregion

        #region button events

        protected void SearchSubmit_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MoveInRevenueSearch.aspx");
        }

        #endregion

        public DataSet MoveInRevenueSearchGet()
        {
            DataSet dsRevenue = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.MoveInRevenueSearchGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@CustomerID", SqlDbType.Int);
                    Command.Parameters["@CustomerID"].Value = SearchCustomerID.NullIfEmpty();

                    Command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    Command.Parameters["@FirstName"].Value = SearchFirstName.NullIfEmpty();

                    Command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    Command.Parameters["@LastName"].Value = SearchLastName.NullIfEmpty();

                    Command.Parameters.Add("@CommunityNumber", SqlDbType.VarChar);
                    Command.Parameters["@CommunityNumber"].Value = SearchCommunityNumber.NullIfEmpty();

                    Command.Parameters.Add("@BeginEffectiveDt", SqlDbType.DateTime);
                    Command.Parameters["@BeginEffectiveDt"].Value = SearchBeginEffectiveDt.NullIfEmpty();

                    Command.Parameters.Add("@EndEffectiveDt", SqlDbType.DateTime);
                    Command.Parameters["@EndEffectiveDt"].Value = SearchEndEffectiveDt.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsRevenue);
                    }
                }
            }

            return dsRevenue;
        }

        public string MoveInRevenueSearchGrid(DataSet dsRevenue)
        {
            //tblMoveInRevenue.MoveInRevenueID
            //,tblMoveInRevenue.CustomerID
            //,tblMoveInRevenue.MoveInDT
            //,tblMoveInRevenue.MoveOutDT
            //,tblMoveInRevenue.DayOfResidence
            //,tblMoveInRevenue.AmountAtCreate
            //,tblMoveInRevenue.PaidDT
            //,tblMoveInRevenue.LookBackDT
            //,tblMoveInRevenue.ProcessDT
            //,tblMoveInRevenue.PaymentID
            //,tblPatient.first_name
            //,tblPatient.last_name
            //,tblPatient.admit_id

            StringBuilder sbRevenue = new StringBuilder();
            //DataSet dsRevenue = MoveInRevenueSearchGet();

            sbRevenue.AppendLine("<table id='RevenueSearchGrid' border='0' class='tablesorter' cellpadding='0' cellspacing='0' style='width:1024px;'>");
            sbRevenue.AppendLine("   <thead>");
            sbRevenue.AppendLine("      <tr>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:20px;'>&nbsp</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:20px;'>ID</th>");
            //sbRevenue.AppendLine("         <th class='columnHeader' style='width:100px;'>Billing ID</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:100px;'>Customer ID</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:150px;'>Resident</th>");
            //sbRevenue.AppendLine("         <th class='columnHeader' style='width:100px;'>Last Name</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:200px;'>Community</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:150px;'>Day Of Residencey</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:100px;'>Move In Date</th>");
            sbRevenue.AppendLine("         <th class='columnHeader' style='width:100px;'>Move Out Date</th>");
            sbRevenue.AppendLine("      </tr>");
            sbRevenue.AppendLine("   </thead>");
            sbRevenue.AppendLine("   <tbody>");

            if (dsRevenue.HasRows())
            {
                Dictionary<string, string> revenueFields = new Dictionary<string, string>();
                revenueFields.Add("Revenue ID", "MoveInRevenueID");
                //revenueFields.Add("Billing ID", "BillingID");
                revenueFields.Add("Customer ID", "CustomerID");
                revenueFields.Add("Resident", "Resident");
                //revenueFields.Add("Last Name", "Last_Name");
                revenueFields.Add("Day of Residencey", "DayOfResidence");
                revenueFields.Add("Amount At Create", "AmountAtCreate");
                revenueFields.Add("Amount At Process", "AmountAtProcess");
                revenueFields.Add("Amount At Look Back", "AmountAtLookBack");
                revenueFields.Add("Payment ID", "PaymentID");
                revenueFields.Add("Payment Date", "PaidDt");
                revenueFields.Add("Look Back Date", "LookBackDt");
                revenueFields.Add("Process Date", "ProcessDt");
                revenueFields.Add("Create By", "CreateBy");
                revenueFields.Add("Create Date", "CreateDt");
                revenueFields.Add("Modify By", "ModifyBy");
                revenueFields.Add("Modify Date", "ModifyDt");
                revenueFields.Add("Active", "ActiveFlgImg");

                foreach (DataRow row in dsRevenue.Tables[0].Rows)
                {
                    //sbRevenue.AppendFormat("   <tr onMouseOver=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='#6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='MoveInRevenueProfile.aspx?MoveInRevenueID={0}'\">", row["MoveInRevenueID"].WhenNullOrEmpty(""));
                    sbRevenue.AppendLine("   <tr>");
                    sbRevenue.AppendLine(AtriaBase.UI.Popup.InformationIconGet(row, revenueFields));
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["MoveInRevenueID"].ToString());
                    //sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["admit_id"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["CustomerID"].ToString());
                    //sbRevenue.AppendFormat("   <td class='columnData'>{0}</td>", row["Resident"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData'><a href='MoveInRevenueProfile.aspx?MoveInRevenueID={0}'>{1}</a></td>", row["MoveInRevenueID"].ToString(), row["Resident"].WhenNullOrEmpty("&nbsp;"));
                    //sbRevenue.AppendFormat("   <td class='columnData'>{0}</td>", row["last_name"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData'>{0}</td>", row["Community"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["DayOfResidence"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["MoveInDT"].ToString());
                    sbRevenue.AppendFormat("   <td class='columnData' style='text-align:right'>{0}</td>", row["MoveOutDT"].ToString());
                    sbRevenue.AppendLine("   </tr>");
                }
            }
            else
            {
                sbRevenue.AppendLine("   <tr><td colspan='9'>No Data to Display</td></tr>");
            }

            sbRevenue.AppendLine("   </tbody>");
            sbRevenue.AppendLine("</table>");

            return sbRevenue.ToString();
        }

        #region Helper Methods

        public string FirstDayOfMonthGet(string date)
        {
            DateTime dtDate = Convert.ToDateTime(date);
            if (dtDate.Month == 1) { return new DateTime(dtDate.Year - 1, 12, 1).ToString("d", DateTimeFormatInfo.InvariantInfo); }
            else { return new DateTime(dtDate.Year, dtDate.Month, 1).ToString("d", DateTimeFormatInfo.InvariantInfo); }
        }

        public string LastDayOfMonthGet(string date)
        {
            DateTime dtDate = Convert.ToDateTime(date);
            DateTime firstDayOfMonth = new DateTime(dtDate.Year, dtDate.Month, 1);
            return firstDayOfMonth.AddMonths(1).AddDays(-1).ToString("d", DateTimeFormatInfo.InvariantInfo);
        }

        #endregion
    }
}