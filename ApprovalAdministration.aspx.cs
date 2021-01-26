using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class ApprovalAdministration : System.Web.UI.Page
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();
        protected NoteTypeBO objNoteType = new NoteTypeBO();
        protected ApprovalManagementBO objApproval = new ApprovalManagementBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2412";
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
            SecurityMenu.Text += objMenuBar.MenuBarMonth();
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
            CommunityNumberHidden.Value = objSecurity.CommunityNumber;
            objApproval.CommunityNumber = CommunityNumberHidden.Value;

            if (Request.QueryString.HasValue("PaymentID", typeof(int)))
            {
                objApproval.PaymentID = Request.QueryString["PaymentID"].ToString();
                PaymentHiddenID.Value = objApproval.PaymentID;
            }
            else
            {
                Response.Write("There is a problem with your query string variables.");
                Response.End();
            }

            if (Request.QueryString.HasValue("PaymentToApprovalID", typeof(int)))
            {
                objApproval.PaymentToApprovalID = Request.QueryString["PaymentToApprovalID"].ToString();
                PaymentToApprovalHiddenID.Value = objApproval.PaymentToApprovalID;
                objApproval.ApprovalManagementDetailGet();
                Delete.Visible = objSecurity.DeleteFlg == "1" ? true : false;
            }

            objNoteType.Audit.ActiveFlg = "1";//only want active note types
            AtriaBase.UI.ListControl.ListPopulate(JobCategoryID, "JobCategory", "JobCategoryID", objBonusPlan.JobCategoryGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            AtriaBase.UI.ListControl.ListPopulate(JobCategoryIDAlternate, "JobCategory", "JobCategoryID", objBonusPlan.JobCategoryGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
        }

        public void DataToFormBind()
        {
            AdministrationInformationBlock.Text = objApproval.ApprovalManagementInformationBlock();
            ApprovalManagementGrid.Text = objApproval.ApprovalManagementGrid();
            PaymentToApprovalID.Text = objApproval.PaymentToApprovalID;
            BonusPlanHiddenID.Value = objApproval.BonusPlanID;
            JobCategoryID.SelectedValue = objApproval.JobCategoryID;
            JobCategoryIDAlternate.SelectedValue = objApproval.JobCategoryIDAlternate;
            UsernameAlternate.Text = objApproval.UsernameAlternate;
            //CriteriaValue.Text = objApproval.CriteriaValue;
            Sort.Text = objApproval.Sort;
            ApproveFlg.Checked = objApproval.ApproveFlg == "1" ? true : false;
            DenyFlg.Checked = objApproval.DenyFlg == "1" ? true : false;
            ExceptionFlgCB.Checked = objApproval.ExceptionFlg == "1" ? true : false;
            ExceptionNote.Text = objApproval.ExceptionNote;
            AdministrationNote.Text = objApproval.AdministrationNote;
            CreateBy.Text = objApproval.Audit.CreateBy;
            CreateDt.Text = objApproval.Audit.CreateDt;
            ModifyBy.Text = objApproval.Audit.ModifyBy;
            ModifyDt.Text = objApproval.Audit.ModifyDt;

            if (string.IsNullOrEmpty(objApproval.Audit.ActiveFlg) || objApproval.Audit.ActiveFlg.ToString() == "1")
            {
                ActiveFlg.Text = "<img src='../../../../images/icon_check.gif' alt='Active Flg' />";
            }
            else
            {
                ActiveFlg.Visible = false;
                ActiveFlgCB.Visible = true;
            }

            OverrideFlg.Text = "<img src='../../../../images/icon_check.gif' alt='Override' />";
        }

        public void FormToDataBind()
        {
            objApproval.PaymentToApprovalID = PaymentToApprovalHiddenID.Value;
            objApproval.BonusPlanID = BonusPlanHiddenID.Value;
            objApproval.PaymentID = PaymentHiddenID.Value;
            objApproval.JobCategoryID = JobCategoryID.SelectedValue;
            objApproval.JobCategoryIDAlternate = JobCategoryIDAlternate.SelectedValue;
            objApproval.UsernameAlternate = UsernameAlternate.Text;
            objApproval.Sort = Sort.Text;
            objApproval.ApproveFlg = ApproveFlg.Checked ? "1" : "0";
            objApproval.DenyFlg = DenyFlg.Checked ? "1" : "0";
            objApproval.ExceptionFlg = ExceptionFlgCB.Checked ? "1" : "0";
            objApproval.OverrideFlg = "1";
            objApproval.ExceptionNote = ExceptionNote.Text;
            objApproval.AdministrationNote = AdministrationNote.Text;
            objApproval.Audit.CreateBy = objSecurity.Username;
            objApproval.Audit.ModifyBy = objSecurity.Username;

            if (ActiveFlgCB.Visible)
            {
                objApproval.Audit.ActiveFlg = ActiveFlgCB.Checked ? "1" : "0";
            }
            else
            {
                objApproval.Audit.ActiveFlg = "1";
            }
        }

        public bool ServerValidate(ref string errors)
        {
            if (string.IsNullOrEmpty(JobCategoryID.SelectedValue))
            {
                errors += "A Job Role is required.<br/>";
            }

            if (string.IsNullOrEmpty(Sort.Text))
            {
                errors += "A Sort value is required.<br/>";
            }

            //if (string.IsNullOrEmpty(CriteriaValue.Text))
            //{
            //    errors += "A Criteria Value is required.<br/>";
            //}

            if (string.IsNullOrEmpty(AdministrationNote.Text))
            {
                errors += "An Override Note value is required.<br/>";
            }
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("ApprovalAdministration.aspx?PaymentID={0}", PaymentHiddenID.Value));
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                if (string.IsNullOrEmpty(PaymentToApprovalHiddenID.Value))
                {
                    objApproval.ApprovalManagementInsert();
                }
                else
                {
                    objApproval.ApprovalManagementUpdate();
                }

                Response.Redirect(string.Format("ApprovalAdministration.aspx?PaymentID={0}", PaymentHiddenID.Value));
            }
            else
            {
                if (!ClientScript.IsClientScriptBlockRegistered("ValidationFailure"))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "ValidationFailure", "<script type='text/javascript'>alert('" + errors.Replace("'", "").Replace("<br />", "\\n") + "');</script>", false);
                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            objApproval.Audit.ActiveFlg = "0";
            objApproval.ApprovalManagementUpdate();

            Response.Redirect(string.Format("ApprovalAdministration.aspx?PaymentID={0}", PaymentHiddenID.Value));
        }
    }
}