using Atria.Framework;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BonusPlan
{
    public partial class rptRSMBonusExtract : Page, IDataSearchForm
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
            objSecurity.ApplicationID = "169";
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

        public DataSet rptRSMBonusExtractGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("ResidentManagement.rptRSMBonusPlanExtract", Connection))
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

        public void Search()
        {
            DataSet dsExtract = new DataSet();
            dsExtract = rptRSMBonusExtractGet();
            Dictionary<string, string> fields = new Dictionary<string, string>(dsExtract.Tables[0].Rows.Count);
            foreach (DataColumn column in dsExtract.Tables[0].Columns)
            {
                fields.Add(column.ColumnName, column.ColumnName);
            }

            AtriaBase.UI.MicrosoftOfficeUtility.ExportData(dsExtract, "rptRSMBonusExtract", fields, AtriaBase.UI.OfficeExportType.Excel);
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