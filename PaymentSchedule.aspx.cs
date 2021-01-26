using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class PaymentSchedule : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected PaymentScheduleBO objPayment = new PaymentScheduleBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2402";
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
            PaymentScheduleGrid.Text = objPayment.PaymentScheduleGrid();

            if (Request.QueryString.HasValue("PaymentScheduleID", typeof(int)))
            {
                objPayment.PaymentScheduleID = Request.QueryString["PaymentScheduleID"].ToString();
                PaymentScheduleHiddenID.Value = objPayment.PaymentScheduleID;

                objPayment.PaymentScheduleDetailGet();

                Delete.Visible = objSecurity.DeleteFlg.ToString() == "1" ? true : false;
            }

            //08/29/2016 Amanda Marburger
            //Only want Active Bonus plans to create payment schedules
            objBonusPlan.Audit.ActiveFlg = "1";
            AtriaBase.UI.ListControl.ListPopulate(BonusPlanID, "BonusPlan", "BonusPlanID", objBonusPlan.BonusPlanGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
        }

        public void DataToFormBind()
        {
            PaymentScheduleID.Text = objPayment.PaymentScheduleID;
            EmailAlertDt.Text = objPayment.EmailAlertDt;
            FinalNotificationDt.Text = objPayment.FinalNotificationDt;
            ProcessDt.Text = objPayment.ProcessDt;
            PayrollDt.Text = objPayment.PayrollDt;
            BonusPlanID.SelectedValue = objPayment.BonusPlanID;

            if (string.IsNullOrEmpty(objPayment.Audit.ActiveFlg) || objPayment.Audit.ActiveFlg.ToString() == "1")
            {
                ActiveFlg.Text = "<img src='../../../../images/icon_check.gif' alt='Active Flg' />";
            }
            else
            {
                ActiveFlg.Visible = false;
                ActiveFlgCB.Visible = true;
            }
        }

        public void FormToDataBind()
        {
            objPayment.PaymentScheduleID = PaymentScheduleHiddenID.Value;
            objPayment.EmailAlertDt = EmailAlertDt.Text;
            objPayment.ProcessDt = ProcessDt.Text;
            objPayment.PayrollDt = PayrollDt.Text;

            if (ActiveFlgCB.Visible)
            {
                objPayment.Audit.ActiveFlg = ActiveFlgCB.Checked ? "1" : "0";
            }
            else
            {
                objPayment.Audit.ActiveFlg = "1";
            }
            objPayment.FinalNotificationDt = FinalNotificationDt.Text;
            objPayment.BonusPlanID = BonusPlanID.SelectedValue;
        }

        public bool ServerValidate(ref string errors)
        {
            if (BonusPlanID.SelectedValue.Length == 0)
            {
                errors += "A Bonus Plan is required.<br/>";
            }

            if (EmailAlertDt.Text.Length == 0)
            {
                errors += "Approval Notification Date is required.<br/>";
            }

            if (ProcessDt.Text.Length == 0)
            {
                errors += "Process Date is required.<br/>";
            }

            if (PayrollDt.Text.Length == 0)
            {
                errors += "Payroll Date is required.<br/>";
            }

            if (FinalNotificationDt.Text.Length == 0)
            {
                errors += "Final Notification Date is required.<br/>";
            }

            if (!EmailAlertDt.Text.IsDate())
            {
                errors += "Approval Notification Date Format is Incorrect.<br/>";
            }

            if (!ProcessDt.Text.IsDate())
            {
                errors += "Process Date Format is Incorrect.<br/>";
            }

            if (!PayrollDt.Text.IsDate())
            {
                errors += "Payroll Date Format is Incorrect.<br/>";
            }

            if (!FinalNotificationDt.Text.IsDate())
            {
                errors += "Final Notification Date Format is Incorrect.<br/>";
            }

            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        protected void Submit_Click(object sender, EventArgs e)
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                if (string.IsNullOrEmpty(PaymentScheduleHiddenID.Value))
                {
                    objPayment.PaymentScheduleInsert();
                }
                else
                {
                    objPayment.PaymentScheduleUpdate();
                }

                Response.Redirect("PaymentSchedule.aspx");
            }
            else
            {
                if (!ClientScript.IsClientScriptBlockRegistered("ValidationFailure"))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "ValidationFailure", "<script type='text/javascript'>alert('" + errors.Replace("'", "").Replace("<br />", "\\n") + "');</script>", false);
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaymentSchedule.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            objPayment.Audit.ActiveFlg = "0";
            objPayment.PaymentScheduleUpdate();

            Response.Redirect("PaymentSchedule.aspx");
        }
    }
}