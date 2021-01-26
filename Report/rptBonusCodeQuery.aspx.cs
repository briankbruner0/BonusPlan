using Atria.Framework;
using AtriaBase.UI;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class rptBonusCodeQuery : Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected RevenueEntryTypeBO objRevenueEntryType = new RevenueEntryTypeBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2405";
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
                DataToFormBind();
                Search();
            }
        }

        protected void Search()
        {
            FormToDataBind();

            if (Excel.Checked)
            {
                Dictionary<string, string> revenueEntryFields = new Dictionary<string, string>();
                revenueEntryFields.Add("BonusPlan", "Bonus Plan");
                revenueEntryFields.Add("CreateDT", "Create Date");
                revenueEntryFields.Add("EffectiveDT", "Effective Date");
                revenueEntryFields.Add("RevenueEntryType", "Revenue Entry Type");
                revenueEntryFields.Add("LedgerEntry", "Ledger Entry");

                MicrosoftOfficeUtility.ExportData(objRevenueEntryType.RevenueEntrySearch(), "Bonus Code Query Report", revenueEntryFields, OfficeExportType.Excel);
            }
            else
            {
                SearchResultGrid.Text = objRevenueEntryType.SearchResultGrid();
            }
        }

        #region IDataInputForm Members

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
            objRevenueEntryType.Audit.CreateDt = CreateDT.Text;
            objRevenueEntryType.RevenueEntryTypeID = RevenueEntryTypeID.SelectedValue;
        }

        public void FormInitialize()
        {
            AtriaBase.UI.ListControl.ListPopulate(RevenueEntryTypeID, "RevenueEntryType", "RevenueEntryTypeID", objRevenueEntryType.RevenueEntryTypeGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectAll);
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
            Search();
        }

        #endregion Events
    }
}