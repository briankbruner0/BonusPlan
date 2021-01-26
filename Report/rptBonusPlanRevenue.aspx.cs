using Atria.Framework;
using AtriaBase;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BonusPlan
{
    public partial class rptBonusPlanRevenue : Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();

        #region Attributes

        private string rptCountry;
        private string rptEffectiveDT;

        #endregion Attributes

        #region Properties

        public string RptCountry
        {
            get { return rptCountry.WhenNullOrEmpty(string.Empty); }
            set { rptCountry = value; }
        }

        public string RptEffectiveDT
        {
            get { return rptEffectiveDT.WhenNullOrEmpty(string.Empty); }
            set { rptEffectiveDT = value; }
        }

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "3801";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);
            SecurityMenu.Text = objSecurity.MenuBar();

            if (!IsPostBack)
            {
                FormInitialize();
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            Search();
        }

        public DataSet rptBonusPlanRevenueGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.rptBonusPlanRevenue", Connection))
                {
                    Command.CommandTimeout = 1000;
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@Country", SqlDbType.VarChar);
                    Command.Parameters["@Country"].Value = RptCountry;

                    Command.Parameters.Add("@EffectiveDT", SqlDbType.VarChar);
                    Command.Parameters["@EffectiveDT"].Value = RptEffectiveDT;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        #region IDataInput Methods

        public void FormInitialize()
        {
            EffectiveDT.Text = DateTime.Today.ToShortDateString();
        }

        public void FormToDataBind()
        {
            if (USA.Checked == true)
            {
                RptCountry = "USA";
            }
            if (CAN.Checked == true)
            {
                RptCountry = "CAN";
            }

            RptEffectiveDT = EffectiveDT.Text;
        }

        public void DataToFormBind()
        {
        }

        // comment why you are using the method name Search instead of Render
        public void Search()
        {
            DataSet ds = new DataSet();
            ds = rptBonusPlanRevenueGet();

            // you can use the atria extension HasRows() to do these next lines
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ErrorMessage.Text = "No data found";
                return;
            }
            else
            {
                Dictionary<string, string> fields = new Dictionary<string, string>(ds.Tables[0].Rows.Count);
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    fields.Add(column.ColumnName, column.ColumnName);
                }

                Dictionary<string, string> dataType = new Dictionary<string, string>
                    {
                        {"RSM","49"},
                        {"Community Number","49"},
                        {"Community Name","49"},
                        {"Country","49"},
                        {"Resident","49"},
                        {"CustomerID","49"},
                        {"BillingID","49"},
                        {"MoveInDt","14"},
                        {"MoveOutDt","14"},
                        {"Days of Residency","49"},
                        {"Revenue At Maturity","39"},
                        {"Payor","49"},
                        {"RoomType","49"},
                        {"RoomTypeStyle","49"},
                        {"RoomNumber","49"},
                        {"BedNumber","49"},
                        {"RoomRate","39"},
                        {"Hospitality Charge","39"},
                        {"Food Tax Charge","39"},
                        {"AMC Charge","39"},
                        {"AMC Concession","39"},
                        {"Service","49"},
                        {"Care","49"},
                        {"Care Rate","39"},
                        {"Series","49"},
                        {"CareAncillary","49"},
                        {"CareAncillaryAmount","39"},
                        {"OtherBilling","39"},
                        {"MoveInIncentiveConcession","39"},
                        {"RecurringAllowance","39"},
                        {"IsLeaseVerifyFlg", "49"}
                    };

                Utility.ExportData(ds, "BonusExtract", fields, ExportType.Excel, dataType);
            }
        }

        public bool ServerValidate(ref string errorMessage)
        {
            //used for 'TryParse'
            DateTime effectiveDTParseTest;
            if (!DateTime.TryParse(EffectiveDT.Text.Trim().WhenNullOrEmpty(""), out effectiveDTParseTest))
                errorMessage += "The Effective Date is not valid.<br />";
            return errorMessage.Length == 0;
        }

        #endregion IDataInput Methods
    }
}