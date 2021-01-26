using Atria.Framework;
using AtriaBase.UI;
using AtriaEM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BonusPlan
{
    public partial class rptCanadaBonusExtract : Page, IDataSearchForm
    {
        protected SecurityBO objSecurity = new SecurityBO();

        #region Properties

        private string startDT;
        private string endDT;
        private string monthText;
        private string yearText;

        public string StartDT
        {
            get { return startDT.WhenNullOrEmpty(string.Empty); }
            set { startDT = value; }
        }

        public string EndDT
        {
            get { return endDT.WhenNullOrEmpty(string.Empty); }
            set { endDT = value; }
        }

        public string MonthText
        {
            get { return monthText.WhenNullOrEmpty(string.Empty); }
            set { monthText = value; }
        }

        public string YearText
        {
            get { return yearText.WhenNullOrEmpty(string.Empty); }
            set { yearText = value; }
        }

        #endregion Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "3270";
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

        public void FormInitialize()
        {
            YearListPopulate();
            MonthListPopulate();
        }

        public void FormToDataBind()
        {
            var startDTTime = MonthYearToDateTime(MonthList.SelectedValue, YearList.SelectedValue);
            var endDTTime = startDTTime.AddMonths(1).AddDays(-1);

            startDT = startDTTime.ToString();
            endDT = endDTTime.ToString();
            MonthText = MonthList.SelectedItem.Text;
            YearText = YearList.SelectedItem.Text;
        }

        public bool ServerValidate(ref string errorMessage)
        {
            return true; // Form does not contain any free form or subjective inputs.
        }

        public void DataToFormBind()
        {
            // No business object data to bind to a form.
        }

        public void Search()
        {
            DataSet dsExtract = new DataSet();

            dsExtract = rptCanadaBonusExtractGet();

            Dictionary<string, string> fields = new Dictionary<string, string>(dsExtract.Tables[0].Rows.Count);

            foreach (DataColumn column in dsExtract.Tables[0].Columns)
            {
                fields.Add(column.ColumnName, column.ColumnName);
            }

            MicrosoftOfficeUtility.ExportData(dsExtract, monthText + " " + yearText + " Canada Extract", fields, OfficeExportType.Excel);
        }

        public DataSet rptCanadaBonusExtractGet()
        {
            DataSet ds = new DataSet();

            using (SqlConnection Connection = new SqlConnection(ConfigurationManager.AppSettings["WebApplication"]))
            {
                using (SqlCommand Command = new SqlCommand("BonusPlan.rptCanadaBonusExtract", Connection))
                {
                    Command.CommandTimeout = 1000;
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.Add("@StartDT", SqlDbType.DateTime);
                    Command.Parameters["@StartDT"].Value = StartDT;

                    Command.Parameters.Add("@EndDT", SqlDbType.DateTime);
                    Command.Parameters["@EndDT"].Value = EndDT;

                    using (SqlDataAdapter DataAdapter = new SqlDataAdapter(Command))
                    {
                        DataAdapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        #region Helpers

        private void YearListPopulate()
        {
            YearList.Items.Clear();

            ListItem lt = new ListItem();

            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 1; i--)
            {
                lt = new ListItem();

                lt.Text = i.ToString();
                lt.Value = i.ToString();

                YearList.Items.Add(lt);
            }

            YearList.Items.FindByValue(DateTime.Now.AddMonths(-1).Year.ToString()).Selected = true;
        }

        private void MonthListPopulate()
        {
            MonthList.Items.Clear();

            ListItem lt = new ListItem();

            for (int i = 1; i <= 12; i++)
            {
                lt = new ListItem();

                lt.Text = Convert.ToDateTime(i.ToString() + "/1/1900").ToString("MMMM");
                lt.Value = i.ToString();

                MonthList.Items.Add(lt);
            }

            MonthList.Items.FindByValue(DateTime.Now.AddMonths(-1).Month.ToString()).Selected = true;
        }

        private DateTime MonthYearToDateTime(string month, string year)
        {
            var monthYearDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);

            return monthYearDate;
        }

        #endregion Helpers
    }
}