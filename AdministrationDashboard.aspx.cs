using Atria.Framework;
using AtriaEM;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class AdministrationDashboard : Page, IDataInputForm
    {
        #region Private Members

        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected AdministrationDashboardBO objAdministrationDashboard = new AdministrationDashboardBO();
        protected CommunityBO objCommunity = new CommunityBO();

        #endregion Private Members

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2410";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            objMenuBar.Username = objSecurity.Username;
            objMenuBar.ApplicationName = objSecurity.ApplicationName;
            objMenuBar.CommunityNumber = objSecurity.CommunityNumber;

            if (Request.QueryString.HasValue("EffectiveDt", typeof(DateTime)))
            {
                objSecurity.EffectiveDT = Request.QueryString["EffectiveDt"].ToString();
            }
            else
            {
                objSecurity.EffectiveDT = DateTime.Today.ToShortDateString();
            }

            SecurityMenu.Text = objSecurity.MenuBar();
            SecurityMenu.Text += objMenuBar.MenuBarMonth();
            SecurityMenu.Text += objMenuBar.MenuUserProfile();
            SecurityTitle.Text = objSecurity.Title;
            SecurityBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br />", RegexOptions.Multiline);

            objCommunity.CommunityNumber = objSecurity.CommunityNumber;
            objCommunity.CommunityDetailGet();
            objAdministrationDashboard.CommunityNumber = objSecurity.CommunityNumber;
            objAdministrationDashboard.EffectiveDT = objSecurity.EffectiveDT;
            objAdministrationDashboard.Audit.UserName = objSecurity.Username;

            if (!Page.IsPostBack)
            {
                FormInitialize();
            }
        }

        #region IDataInputForm Members

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
        }

        public bool ServerValidate(ref string errors)
        {
            return errors.Length == 0;
        }

        public void FormInitialize()
        {
            PaymentScheduleBO objPaymentSchedule = new PaymentScheduleBO();

            snpQuickLinks.Text = objAdministrationDashboard.snpQuickLinks();
            snpRevenueAcccounts.Text = objAdministrationDashboard.snpRevenueAcccounts();
            snpPaymentApprovalException.Text = objAdministrationDashboard.snpPaymentApprovalException();
            snpEmployeeOverride.Text = objAdministrationDashboard.snpEmployeeOverride();
            snpManagePaymentApproval.Text = objAdministrationDashboard.snpManagePaymentApproval();

            //Only Active
            objPaymentSchedule.Audit.ActiveFlg = "1";
            DataSet dsPayrollSchedule = objPaymentSchedule.PaymentScheduleGet();
            snpPayrollSchedule.Text = objAdministrationDashboard.snpPayrollSchedule(dsPayrollSchedule);
        }

        #endregion IDataInputForm Members
    }
}