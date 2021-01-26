using Atria.Framework;
using AtriaBase.UI;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace BonusPlan
{
    public partial class PaymentSearch : System.Web.UI.Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected CommunityBO objCommunity = new CommunityBO();
        //protected PaymentBO objPayment = new PaymentBO();

        #region SearchMembers

        private string searchCustomerID;
        private string searchEmployeeID;
        private string searchCountry;
        private string searchCommunityID;
        private string searchPaymentFromDt;
        private string searchPaymentToDt;
        private string searchPayrollFromDt;
        private string searchPayrollToDt;
        private string searchMoveInFromDt;
        private string searchMoveInToDt;
        private string searchEmployeeFirstName;
        private string searchEmployeeLastName;
        private string searchResidentFirstName;
        private string searchResidentLastName;

        public string SearchCustomerID
        {
            get { return searchCustomerID.WhenNullOrEmpty(string.Empty); }
            set { searchCustomerID = value; }
        }

        public string SearchEmployeeID
        {
            get { return searchEmployeeID.WhenNullOrEmpty(string.Empty); }
            set { searchEmployeeID = value; }
        }

        public string SearchCommunityID
        {
            get { return searchCommunityID.WhenNullOrEmpty(string.Empty); }
            set { searchCommunityID = value; }
        }

        public string SearchCountry
        {
            get { return searchCountry.WhenNullOrEmpty(string.Empty); }
            set { searchCountry = value; }
        }

        public string SearchPaymentFromDt
        {
            get { return searchPaymentFromDt.WhenNullOrEmpty(string.Empty); }
            set { searchPaymentFromDt = value; }
        }

        public string SearchPaymentToDt
        {
            get { return searchPaymentToDt.WhenNullOrEmpty(string.Empty); }
            set { searchPaymentToDt = value; }
        }

        public string SearchPayrollFromDt
        {
            get { return searchPayrollFromDt.WhenNullOrEmpty(string.Empty); }
            set { searchPayrollFromDt = value; }
        }

        public string SearchPayrollToDt
        {
            get { return searchPayrollToDt.WhenNullOrEmpty(string.Empty); }
            set { searchPayrollToDt = value; }
        }

        public string SearchMoveInFromDt
        {
            get { return searchMoveInFromDt.WhenNullOrEmpty(string.Empty); }
            set { searchMoveInFromDt = value; }
        }

        public string SearchMoveInToDt
        {
            get { return searchMoveInToDt.WhenNullOrEmpty(string.Empty); }
            set { searchMoveInToDt = value; }
        }

        public string SearchEmployeeFirstName
        {
            get { return searchEmployeeFirstName.WhenNullOrEmpty(string.Empty); }
            set { searchEmployeeFirstName = value; }
        }

        public string SearchEmployeeLastName
        {
            get { return searchEmployeeLastName.WhenNullOrEmpty(string.Empty); }
            set { searchEmployeeLastName = value; }
        }

        public string SearchResidentFirstName
        {
            get { return searchResidentFirstName.WhenNullOrEmpty(string.Empty); }
            set { searchResidentFirstName = value; }
        }

        public string SearchResidentLastName
        {
            get { return searchResidentLastName.WhenNullOrEmpty(string.Empty); }
            set { searchResidentLastName = value; }
        }

        #endregion SearchMembers

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2442";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            var xmlPath = HttpContext.Current.Server.MapPath("/Application/BonusPlan/App_Data/Default.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            objSecurity.MenuXML = xmldoc.InnerXml.ToString();

            objMenuBar.Username = objSecurity.Username;
            objMenuBar.ApplicationName = objSecurity.ApplicationName;

            SecurityMenu.Text = objSecurity.MenuBarResponsive();
            SecurityTitle.Text = objSecurity.Title;
            SecurityBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br />", RegexOptions.Multiline);

            // 11/14/2017 Fritz Kern - Added Username to UX so we can grab it for payment udpates.
            Username.Value = objSecurity.Username;

            if (!Page.IsPostBack)
            {
                FormInitialize();
                DataToFormBind();
            }
            else
            {
                // Maintain state of user selection upon a PostBack
                if (CommunityID.Value.Length > 0)
                {
                    objCommunity.Country = Country.Value;
                    DataSet dsCommunityFilter = objCommunity.CommunityByCountryGet();

                    DataSet dsUnAssigned = DataHelper.Filter(dsCommunityFilter, CommunityID.Value, "CommunityID", DataHelper.FilterBehavior.UnAssigned);
                    AtriaBase.UI.ListControl.ListPopulate(uiUnAssignedCommunityNumber, "CommunityName", "CommunityID", dsUnAssigned, AtriaBase.UI.ListControl.NoItemSelectedBehavior.Nothing);

                    DataSet dsAssigned = DataHelper.Filter(dsCommunityFilter, CommunityID.Value, "CommunityID", DataHelper.FilterBehavior.Assigned);
                    AtriaBase.UI.ListControl.ListPopulate(uiAssignedCommunityNumber, "CommunityName", "CommunityID", dsAssigned, AtriaBase.UI.ListControl.NoItemSelectedBehavior.Nothing);
                }

                // Maintain selected country
                if (SearchCountry != "")
                {
                    Country.Value = SearchCountry;
                }
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            AtriaBase.UI.ListControl.ListPopulate(uiAssignedCommunityNumber, "Community", "CommunityID", objCommunity.CommunityGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.Nothing);
        }

        public void DataToFormBind()
        {
            PaymentFromDT.Text = FirstDayOfMonthGet(DateTime.Now.ToShortTimeString());
            PaymentToDT.Text = LastDayOfMonthGet(DateTime.Now.ToShortTimeString());
        }

        public void Search()
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                DataSet dsPayment = PaymentSearchGet();

                // 02/08/2018 Fritz Kern - Commenting this out since it is not being used.
                //Dictionary<string, string> parameters = new Dictionary<string, string>();
                //parameters.Add("Employee First Name", SearchEmployeeFirstName.Length == 0 ? "All" : SearchEmployeeFirstName);
                //parameters.Add("Employee Last Name", SearchEmployeeLastName.Length == 0 ? "All" : SearchEmployeeLastName);
                //parameters.Add("Employee ID", SearchEmployeeID.Length == 0 ? "All" : SearchEmployeeID);
                //parameters.Add("Customer ID", SearchCustomerID.Length == 0 ? "All" : SearchCustomerID);
                //parameters.Add("Resident First Name", SearchResidentFirstName.Length == 0 ? "All" : SearchResidentFirstName);
                //parameters.Add("Resident Last Name", SearchResidentLastName.Length == 0 ? "All" : SearchResidentLastName);
                //parameters.Add("Community", SearchCommunityID.Length == 0 ? "All" : AtriaBase.UI.ListControl.ListItemTextGet(uiAssignedCommunityNumber));
                //parameters.Add("Payment Date", SearchPaymentFromDt + " - " + SearchPaymentToDt);
                //parameters.Add("Payroll Date", SearchPayrollFromDt + " - " + SearchPayrollToDt);

                //int rowCount = dsPayment.HasRows() ? dsPayment.Tables[0].Rows.Count : 0;
                
                // 11/14/2017 Fritz Kern - No longer need the criteria search
                //PaymentSearchCriteriaGrid.Text = AtriaBase.UI.Search.SearchCriteriaGrid(parameters, rowCount, 650, 200);

                if (Excel.Checked)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>
                    {
                        {"PaymentID", "ID"},
                        {"CategoryType", "Category Type"},
                        {"ADP_EmployeeID", "EMPLID"},
                        {"EmployeeName", "Employee"},
                        {"CommunityName", "CommunityName"},
                        {"CommunityNumber", "CommunityNumber"},
                        {"CustomerID", "Customer ID"},
                        {"ResidentName", "Resident"},
                        {"PaymentDt", "Processed Date"},
                        {"MoveInDt" , "Move In Date"},
                        {"Amount", "Amount"},
                        {"AmountAtMaturity" , "Revenue"},
                        {"PaymentStatus", "Payment Status"},
                        {"ApprovalFlg", "Approved"},
                        {"PaidFlg", "Paid"},
                        {"Note", "Note"}
                    };

                    AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsPayment, "Payment Search List", fields, AtriaBase.UI.OfficeExportType.Excel);
                }
                else
                {
                    PaymentSearchGrid.Text = PaymentSearchGridGet(dsPayment);
                }
            }
            else
            {
            }
        }

        public void FormToDataBind()
        {
            SearchEmployeeFirstName = EmployeeFirstName.Text;
            SearchEmployeeLastName = EmployeeLastName.Text;
            SearchEmployeeID = EmployeeID.Text;
            SearchCustomerID = CustomerID.Text;
            SearchResidentFirstName = ResidentFirstName.Text;
            SearchResidentLastName = ResidentLastName.Text;
            SearchCountry = Country.Value;
            SearchCommunityID = CommunityID.Value;
            SearchPaymentFromDt = PaymentFromDT.Text;
            SearchPaymentToDt = PaymentToDT.Text;
            SearchPayrollFromDt = PayrollFromDT.Text;
            SearchPayrollToDt = PayrollToDT.Text;
            SearchMoveInFromDt = MoveInFromDT.Text;
            SearchMoveInToDt = MoveInToDT.Text;
        }

        public bool ServerValidate(ref string errors)
        {
            // 08/25/2017 Fritz Kern - Removed the old DateTimes to help make validation easier.
            //DateTime fromDT;
            //DateTime toDT;
            DateTime paymentFromDT;
            DateTime paymentToDT;
            DateTime payrollFromDT;
            DateTime payrollToDT;
            // 02/08/2018 Fritz Kern - Adding validation for the Move In Date range.
            DateTime moveInFromDT;
            DateTime moveInToDT;
            
            // 08/25/2017 Fritz Kern - Moved to top so this would be tested before trying to validate.
            if (this.PaymentFromDT.Text.Length == 0 && this.PaymentToDT.Text.Length == 0 && this.PayrollFromDT.Text.Length == 0 & this.PayrollToDT.Text.Length == 0 && this.MoveInFromDT.Text.Length == 0 && this.MoveInToDT.Text.Length == 0)
            {
                errors += "A Payment Date range OR a Payroll Date range OR a Move In Date range is required.<br />";
                this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.MoveInFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
            }
            // 02/08/2018 Fritz Kern - Changing to be using XOR so we can test if only one of these is true. If more than 1 then throw the error.
            // ex: 1 ^ 0 ^ 1 = FALSE since there are 2 1's. 0 ^ 1 ^ 0 = TRUE since there is only a single 1.
            else if (!((this.PaymentFromDT.Text.Length > 0 || this.PaymentToDT.Text.Length > 0) ^ (this.PayrollFromDT.Text.Length > 0 || this.PayrollToDT.Text.Length > 0) ^ (this.MoveInFromDT.Text.Length > 0 || this.MoveInToDT.Text.Length > 0)))
            {
                errors += "You cannot search on a Payment Date range, a Payroll Date range, and a Move In Date range simultaneously.<br />";
                this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.MoveInFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
            }
            else
            {
                /*
                    08/25/2017 Fritz Kern - Changed the validation to based off the return of the TryParse.
                    If the try parse fails it returns the C# DateTime min value - 01/01/0001. This will be returned for blanks
                    and invalid DateTimes.
                */
                DateTime.TryParse(this.PaymentFromDT.Text, out paymentFromDT);
                DateTime.TryParse(this.PaymentToDT.Text, out paymentToDT);
                DateTime.TryParse(this.PayrollFromDT.Text, out payrollFromDT);
                DateTime.TryParse(this.PayrollToDT.Text, out payrollToDT);
                DateTime.TryParse(this.MoveInFromDT.Text, out moveInFromDT);
                DateTime.TryParse(this.MoveInToDT.Text, out moveInToDT);

                //08/25/2017 Fritz Kern - Reworked the validation to be more concise so there is less IF nesting.

                #region Payment Validation

                if ((paymentFromDT == DateTime.MinValue && paymentToDT == DateTime.MinValue) && (this.PaymentFromDT.Text.Length > 0 && this.PaymentToDT.Text.Length > 0))
                {// If FROM is invalid and TO is invalid and they are both populated.
                    errors += "Please enter a valid date for Payment From and To Date.<br />";
                    this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else if (paymentFromDT == DateTime.MinValue && paymentToDT != DateTime.MinValue)
                {// If FROM is invalid but TO is valid.
                    errors += "Please enter a valid date for Payment From Date.<br />";
                    this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PaymentToDT.Attributes.CssStyle.Clear();
                }
                else if (paymentFromDT != DateTime.MinValue && paymentToDT == DateTime.MinValue)
                {// If FROM is valid but TO is invalid.
                    errors += "Please enter a valid date for Payment To Date.<br />";
                    this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PaymentFromDT.Attributes.CssStyle.Clear();
                }
                else if ((paymentFromDT != DateTime.MinValue && paymentToDT != DateTime.MinValue) && (paymentFromDT > paymentToDT))
                {// If FROM is valid and TO is valid but FROM is AFTER TO.
                    errors += "The Payment To Date must be later than the From Date.<br />";
                    this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PaymentFromDT.Attributes.CssStyle.Clear();
                }
                else if ((paymentFromDT != DateTime.MinValue && paymentToDT != DateTime.MinValue) && (paymentFromDT < paymentToDT) && (paymentFromDT.AddMonths(6) < paymentToDT))
                {// If FROM is valid and TO is valid FROM is BEFORE TO but TO is MORE THAN 6 months away.
                    errors += "The Payment To Date cannot be more than 6 months greater than the From Date.<br />";
                    this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.PaymentFromDT.Attributes.CssStyle.Clear();
                    this.PaymentToDT.Attributes.CssStyle.Clear();
                }

                #endregion Payment Validation

                #region Payroll Validation

                if ((payrollFromDT == DateTime.MinValue && payrollToDT == DateTime.MinValue) && (this.PayrollFromDT.Text.Length > 0 && this.PayrollToDT.Text.Length > 0))
                {// If FROM is invalid and TO is invalid and they are both populated.
                    errors += "Please enter a valid date for Payroll From and To Date.<br />";
                    this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else if (payrollFromDT == DateTime.MinValue && payrollToDT != DateTime.MinValue)
                {// If FROM is invalid but TO is valid.
                    errors += "Please enter a valid date for Payroll From Date.<br />";
                    this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PayrollToDT.Attributes.CssStyle.Clear();
                }
                else if (payrollFromDT != DateTime.MinValue && payrollToDT == DateTime.MinValue)
                {// If FROM is valid but TO is invalid.
                    errors += "Please enter a valid date for Payroll To Date.<br />";
                    this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PayrollFromDT.Attributes.CssStyle.Clear();
                }
                else if ((payrollFromDT != DateTime.MinValue && payrollToDT != DateTime.MinValue) && (payrollFromDT > payrollToDT))
                {// If FROM is valid and TO is valid but FROM is AFTER TO.
                    errors += "The Payroll To Date must be later than the Payroll From Date.<br />";
                    this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PayrollFromDT.Attributes.CssStyle.Clear();
                }
                else if ((payrollFromDT != DateTime.MinValue && payrollToDT != DateTime.MinValue) && (payrollFromDT < payrollToDT) && (payrollFromDT.AddMonths(6) < payrollToDT))
                {// If FROM is valid and TO is valid FROM is BEFORE TO but TO is MORE THAN 6 months away.
                    errors += "The Payroll To Date cannot be more than 6 months greater than the Payroll From Date.<br />";
                    this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.PayrollFromDT.Attributes.CssStyle.Clear();
                    this.PayrollToDT.Attributes.CssStyle.Clear();
                }

                #endregion Payroll Validation

                //02/08/2018 Fritz Kern - Added Move In Date validation.

                #region Move In Validation

                if ((moveInFromDT == DateTime.MinValue && moveInToDT == DateTime.MinValue) && (this.MoveInFromDT.Text.Length > 0 && this.MoveInToDT.Text.Length > 0))
                {// If FROM is invalid and TO is invalid and they are both populated.
                    errors += "Please enter a valid date for Move In From and To Date.<br />";
                    this.MoveInFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else if (moveInFromDT == DateTime.MinValue && moveInToDT != DateTime.MinValue)
                {// If FROM is invalid but TO is valid.
                    errors += "Please enter a valid date for Move In From Date.<br />";
                    this.MoveInFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.MoveInToDT.Attributes.CssStyle.Clear();
                }
                else if (moveInFromDT != DateTime.MinValue && moveInToDT == DateTime.MinValue)
                {// If FROM is valid but TO is invalid.
                    errors += "Please enter a valid date for Move In To Date.<br />";
                    this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.MoveInFromDT.Attributes.CssStyle.Clear();
                }
                else if ((moveInFromDT != DateTime.MinValue && moveInToDT != DateTime.MinValue) && (moveInFromDT > moveInToDT))
                {// If FROM is valid and TO is valid but FROM is AFTER TO.
                    errors += "The Move In To Date must be later than the From Date.<br />";
                    this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.MoveInFromDT.Attributes.CssStyle.Clear();
                }
                else if ((moveInFromDT != DateTime.MinValue && moveInToDT != DateTime.MinValue) && (moveInFromDT < moveInToDT) && (moveInFromDT.AddMonths(6) < moveInToDT))
                {// If FROM is valid and TO is valid FROM is BEFORE TO but TO is MORE THAN 6 months away.
                    errors += "The Move In To Date cannot be more than 6 months greater than the From Date.<br />";
                    this.MoveInFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                    this.MoveInToDT.Attributes.CssStyle.Add("background-color", "Pink");
                }
                else
                {
                    this.MoveInFromDT.Attributes.CssStyle.Clear();
                    this.MoveInToDT.Attributes.CssStyle.Clear();
                }

                #endregion MoveIn Validation

                #region OLD PAYMENT VALIDATION METHOD

                ////// If there is a Payment From Date, it must be in valid form
                ////if (this.PaymentFromDT.Text.Length > 0)
                ////{
                ////    if (!DateTime.TryParse(this.PaymentFromDT.Text, out paymentFromDT))
                ////    {
                ////        errors += "Please enter a valid date for Payment From Date.<br />";
                ////        this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PaymentFromDT.Attributes.CssStyle.Clear();
                ////    }
                ////}
                ////else
                ////{
                ////    if (this.PaymentToDT.Text.Length > 0) //if a Payment To Date has been entered then so must a valid Payment From Date
                ////    {
                ////        errors += "A valid Payment From Date is required.<br />";
                ////        this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PaymentFromDT.Attributes.CssStyle.Clear();
                ////    }
                ////}

                ////// If there is an Payment To Date, it must be in valid form
                ////if (this.PaymentToDT.Text.Length > 0)
                ////{
                ////    if (!DateTime.TryParse(this.PaymentToDT.Text, out paymentToDT))
                ////    {
                ////        errors += "Please enter a valid date for Payment To Date.<br />";
                ////        this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PaymentToDT.Attributes.CssStyle.Clear();
                ////    }
                ////}
                ////else
                ////{
                ////    if (this.PaymentFromDT.Text.Length > 0)  //if a Payment From Date has been entered then so must a valid Payment To Date
                ////    {
                ////        errors += "A valid Payment To Date is required.<br />";
                ////        this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PaymentToDT.Attributes.CssStyle.Clear();
                ////    }
                ////}

                ////if (this.PaymentFromDT.Text.Length > 0 && this.PaymentToDT.Text.Length > 0)
                ////{
                ////    // The End Date cannot be less than the Begin Date
                ////    if (Convert.ToDateTime(this.PaymentToDT.Text) < Convert.ToDateTime(this.PaymentFromDT.Text))
                ////    {
                ////        errors += "The Payment To Date must be later than the From Date.<br />";
                ////        this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PaymentToDT.Attributes.CssStyle.Clear();
                ////    }

                ////    if (Convert.ToDateTime(this.PaymentFromDT.Text).AddMonths(6) < Convert.ToDateTime(this.PaymentToDT.Text))
                ////    {
                ////        errors += "The Payment To Date cannot be more than 6 months greater than the From Date.<br />";
                ////        this.PaymentFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////        this.PaymentToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////}

                #endregion OLD PAYMENT VALIDATION METHOD

                #region OLD PAYROLL VALIDATION METHOD

                ////// If there is a Payroll From Date, it must be in valid form
                ////if (this.PayrollFromDT.Text.Length > 0)
                ////{
                ////    if (!DateTime.TryParse(this.PayrollFromDT.Text, out payrollFromDT))
                ////    {
                ////        errors += "Please enter a valid date for Payroll From Date.<br />";
                ////        this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PayrollFromDT.Attributes.CssStyle.Clear();
                ////    }
                ////}
                ////else
                ////{
                ////    if (this.PayrollToDT.Text.Length > 0)  //if a Payroll To Date has been entered then so must a valid Payroll From Date
                ////    {
                ////        errors += "A valid Payroll From Date is required.<br />";
                ////        this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PayrollFromDT.Attributes.CssStyle.Clear();
                ////    }
                ////}

                ////// If there is a Payroll To Date, it must be in valid form
                ////if (this.PayrollToDT.Text.Length > 0)
                ////{
                ////    if (!DateTime.TryParse(this.PayrollToDT.Text, out payrollToDT))
                ////    {
                ////        errors += "Please enter a valid date for Payroll To Date.<br />";
                ////        this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PayrollToDT.Attributes.CssStyle.Clear();
                ////    }
                ////}
                ////else
                ////{
                ////    if (this.PayrollFromDT.Text.Length > 0)  //if a Payroll From Date has been entered then so must a valid Payroll To Date
                ////    {
                ////        errors += "A valid Payroll To Date is required.<br />";
                ////        this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PayrollToDT.Attributes.CssStyle.Clear();
                ////    }
                ////}

                ////if (this.PayrollFromDT.Text.Length > 0 && this.PayrollToDT.Text.Length > 0)
                ////{
                ////    // The End Date cannot be less than the Begin Date
                ////    if (Convert.ToDateTime(this.PayrollToDT.Text) < Convert.ToDateTime(this.PayrollFromDT.Text))
                ////    {
                ////        errors += "The Payroll To Date must be later than the Payroll From Date.<br />";
                ////        this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////    else
                ////    {
                ////        this.PayrollToDT.Attributes.CssStyle.Clear();
                ////    }

                ////    if (Convert.ToDateTime(this.PayrollFromDT.Text).AddMonths(6) < Convert.ToDateTime(this.PayrollToDT.Text))
                ////    {
                ////        errors += "The Payroll To Date cannot be more than 6 months greater than the Payroll From Date.<br />";
                ////        this.PayrollFromDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////        this.PayrollToDT.Attributes.CssStyle.Add("background-color", "Pink");
                ////    }
                ////}

                #endregion OLD PAYROLL VALIDATION METHOD

                int employeeID;
                if (this.EmployeeID.Text.Length > 0 && !Int32.TryParse(this.EmployeeID.Text, out employeeID))
                {
                    errors += "The Employee ID must be a number.<br />";
                }
            }
            ErrorMessage.Visible = true;
            ErrorMessage.Text = errors;
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        #region Events

        protected void Submit_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentSearch.aspx");
        }

        #endregion Events

        #region SQL

        public DataSet PaymentSearchGet()
        {
            DataSet dsPayment = new DataSet();

            using (SqlConnection Connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.PaymentSearchGet", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 600;

                    Command.Parameters.Add("@EmployeeFirstName", SqlDbType.VarChar);
                    Command.Parameters["@EmployeeFirstName"].Value = SearchEmployeeFirstName.NullIfEmpty();

                    Command.Parameters.Add("@EmployeeLastName", SqlDbType.VarChar);
                    Command.Parameters["@EmployeeLastName"].Value = SearchEmployeeLastName.NullIfEmpty();

                    Command.Parameters.Add("@ADP_EmployeeID", SqlDbType.VarChar);
                    Command.Parameters["@ADP_EmployeeID"].Value = SearchEmployeeID.NullIfEmpty();

                    Command.Parameters.Add("@CustomerID", SqlDbType.Int);
                    Command.Parameters["@CustomerID"].Value = SearchCustomerID.NullIfEmpty();

                    Command.Parameters.Add("@ResidentFirstName", SqlDbType.VarChar);
                    Command.Parameters["@ResidentFirstName"].Value = SearchResidentFirstName.NullIfEmpty();

                    Command.Parameters.Add("@ResidentLastName", SqlDbType.VarChar);
                    Command.Parameters["@ResidentLastName"].Value = SearchResidentLastName.NullIfEmpty();

                    Command.Parameters.Add("@CommunityID", SqlDbType.VarChar);
                    Command.Parameters["@CommunityID"].Value = SearchCommunityID.NullIfEmpty();

                    Command.Parameters.Add("@PaymentFromDate", SqlDbType.DateTime);
                    Command.Parameters["@PaymentFromDate"].Value = SearchPaymentFromDt.NullIfEmpty();

                    Command.Parameters.Add("@PaymentToDate", SqlDbType.DateTime);
                    Command.Parameters["@PaymentToDate"].Value = SearchPaymentToDt.NullIfEmpty();

                    Command.Parameters.Add("@PayrollFromDate", SqlDbType.DateTime);
                    Command.Parameters["@PayrollFromDate"].Value = SearchPayrollFromDt.NullIfEmpty();

                    Command.Parameters.Add("@PayrollToDate", SqlDbType.DateTime);
                    Command.Parameters["@PayrollToDate"].Value = SearchPayrollToDt.NullIfEmpty();

                    Command.Parameters.Add("@MoveInFromDate", SqlDbType.DateTime);
                    Command.Parameters["@MoveInFromDate"].Value = SearchMoveInFromDt.NullIfEmpty();

                    Command.Parameters.Add("@MoveInToDate", SqlDbType.DateTime);
                    Command.Parameters["@MoveInToDate"].Value = SearchMoveInToDt.NullIfEmpty();

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(dsPayment);
                    }
                }
            }

            return dsPayment;
        }

        #endregion SQL

        #region Grid

        public string PaymentSearchGridGet(DataSet dsRevenue)
        {
            StringBuilder sbPayment = new StringBuilder();
            DataSet dsPayment = PaymentSearchGet();

            // 11/14/2017 Fritz Kern - Updated the UX to use bootstrap
            sbPayment.AppendLine("<div class='panel panel-default' style='min-width: 1280px;'>"); //02.23.2018 Amanda Marburger overriding bootstrap size to provide a better UX presentation
            sbPayment.AppendLine("  <div class='panel-heading'>");
            sbPayment.AppendLine("      <div class='row'>");
            sbPayment.AppendLine("          <div class='col-md-8'>");
            sbPayment.AppendLine("              <h3 class='panel-title'>Payment Search Results</h3>");
            sbPayment.AppendLine("          </div>");
            sbPayment.AppendLine("          <div class='col-md-4'>");
            sbPayment.AppendLine("              <div class='input-group'>");
            sbPayment.AppendLine("                  <input id='PaymentSearchFilter' type='search' placeholder='To filter your results, enter your search criteria here' class='form-control input-sm pull-right'>");
            sbPayment.AppendLine("                  <span class='input-group-addon'><span class='glyphicon glyphicon-search'></span></span>");
            sbPayment.AppendLine("              </div>");
            sbPayment.AppendLine("          </div>");
            sbPayment.AppendLine("      </div>");
            sbPayment.AppendLine("  </div>");

            sbPayment.AppendLine("  <div class='panel-body'>");

            sbPayment.AppendLine("      <table id='PaymentSearchGrid' class='table tablesorter table-condensed table-striped'>");
            sbPayment.AppendLine("          <thead>");
            sbPayment.AppendLine("              <tr>");
            sbPayment.AppendLine("                  <th class='numericLong'>Payment ID</th>"); //02.23.2018 Amanda Marburger change from payroll ID since this is a payment search, this seems more relevant
            sbPayment.AppendLine("                  <th class='characterLong'>Community</th>");
            sbPayment.AppendLine("                  <th class='characterLong'>Employee</th>");
            sbPayment.AppendLine("                  <th class='characterShort'>Category</th>"); //02.23.2018 Amanda Marburger removed type for space
            sbPayment.AppendLine("                  <th class='numericShort'>Revenue</th>"); //02.23.2018 Amanda Marburger additional column for revenue
            sbPayment.AppendLine("                  <th class='numericShort'>Amount</th>");
            sbPayment.AppendLine("                  <th class='characterLong'>Status</th>"); //02.23.2018 Amanda Marburger removed payment and break for space
            sbPayment.AppendLine("                  <th class='dateShort'>Payment</th>"); //02.23.2018 Amanda Marburger removed date for spacing
            sbPayment.AppendLine("                  <th class='dateShort'>Processed</th>"); //02.23.2018 Amanda Marburger removed date for spacing
            sbPayment.AppendLine("                  <th class='numericLong'>Customer ID</th>"); //02.23.2018 Amanda Marburger change class 
            sbPayment.AppendLine("                  <th class='characterLong'>Resident</th>");
            sbPayment.AppendLine("                  <th class='dateShort'>Move In</th>"); //02.23.2018 Amanda Marburger additional column for screen
            sbPayment.AppendLine("                  <th class='numbericShort'>Note</th>");
            sbPayment.AppendFormat("                {0}", objSecurity.ModifyFlg == "1" ? "<th class='numbericShort'>&nbsp;</th>" : ""); // 11/14/2017 Fritz Kern - Making sure only people authorized to Modify records can see the edit button.
            sbPayment.AppendLine("              </tr>");
            sbPayment.AppendLine("          </thead>");
            sbPayment.AppendLine("          <tbody>");

            if (dsPayment.HasRows())
            {
                Dictionary<string, string> noteFields = new Dictionary<string, string>();
                noteFields.Add("Note", "Note");

                foreach (DataRow row in dsPayment.Tables[0].Rows)
                {
                    //sbPayment.AppendFormat("   <tr onMouseOver=\"this.style.cursor='hand';this.style.color='White';this.style.backgroundColor='#6699FF';\" onMouseOut=\"this.style.cursor='';this.style.backgroundColor='White';\" onclick=\"window.location.href='MoveInRevenueProfile.aspx?MoveInRevenueID={0}'\">", row["MoveInRevenueID"].WhenNullOrEmpty(""));
                    sbPayment.AppendLine("              <tr>");
                    sbPayment.AppendFormat("                    <td class='columnData' style='text-align:right'>{0}</td>", row["PaymentID"].ToString()); //02.23.2018 Amanda Marburger change from payroll ID since this is a payment search, this seems more relevant
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["Community"].ToString());
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["EmployeeName"].ToString());
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["CategoryType"].ToString()); //02.23.2018 Amanda Marburger moved positions 
                    sbPayment.AppendFormat("                    <td class='columnData' style='text-align:right'>{0}</td>", row["AmountAtMaturity"].ToString()); //02.23.2018 Amanda Marburger additional column output to screen
                    sbPayment.AppendFormat("                    <td class='columnData' style='text-align:right'>{0}</td>", row["Amount"].ToString());
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["PaymentStatus"].ToString());
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["CreateDT"].ToString());
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["PaymentDt"].ToString());
                    if (row["MoveInRevenueID"].ToString() != "")
                    {
                        sbPayment.AppendFormat("                    <td class='columnData' style='text-align:right'><a href='MoveInRevenueProfile.aspx?MoveInRevenueID={0}'>{1}</a></td>", row["MoveInRevenueID"].ToString(), row["CustomerID"].WhenNullOrEmpty("&nbsp;"));
                        sbPayment.AppendFormat("                    <td class='columnData'><a href='MoveInRevenueProfile.aspx?MoveInRevenueID={0}'>{1}</a></td>", row["MoveInRevenueID"].ToString(), row["ResidentName"].WhenNullOrEmpty("&nbsp;"));
                    }
                    else
                    {
                        sbPayment.AppendFormat("                    <td class='columnData' style='text-align:right'>{0}</td>", row["CustomerID"].WhenNullOrEmpty("&nbsp;"));
                        sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["ResidentName"].WhenNullOrEmpty("&nbsp;"));
                    }
                    sbPayment.AppendFormat("                    <td class='columnData'>{0}</td>", row["MoveInDT"].ToString());

                    if (row["Note"].ToString().Length > 0)
                    {
                        sbPayment.AppendLine("                  <td align='center'>");
                        sbPayment.AppendFormat("                        <i class='fa fa-sticky-note fa-lg' aria-hidden='true' data-placement='bottom' data-toggle='popover' data-trigger='hover' data-content='Note: {0}'></i>", row["Note"].ToString());
                        sbPayment.AppendLine("                  </td>");
                    }
                    else
                    {
                        sbPayment.AppendLine("                  <td>&nbsp;</td>");
                    }

                    // 11/14/2017 Fritz Kern - Making sure only people authorized to Modify records can see the edit button.
                    if (objSecurity.ModifyFlg == "1")
                    {
                        if (Convert.ToInt32(row["PaidFlg"]) == 1)
                        {
                            sbPayment.AppendFormat("                    <td>");
                            sbPayment.AppendFormat("                        <a class='modalButton btn btn-xs btn-default btn-sm' data-id='{0}' data-toggle='modal' data-target='#PaymentEditModal' data-height='580' data-width='600'>Edit</a>", row["PaymentID"].ToString());
                            sbPayment.AppendFormat("                    </td>");
                        }
                        else
                        {
                            sbPayment.AppendLine("                  <td>&nbsp;</td>");
                        }
                    }

                    sbPayment.AppendLine("              </tr>");
                }
            }
            else
            {
                sbPayment.AppendLine("              <tr><td colspan='12'>No Data to Display</td></tr>");
            }

            sbPayment.AppendLine("          </tbody>");
            sbPayment.AppendLine("      </table>");

            sbPayment.AppendLine("  </div>");
            sbPayment.AppendLine("</div>");

            return sbPayment.ToString();
        }

        #endregion Grid

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

        #endregion Helper Methods
    }
}