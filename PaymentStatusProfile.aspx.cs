using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class PaymentStatusProfile : System.Web.UI.Page
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected PaymentStatusBO objPaymentStatus = new PaymentStatusBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2367";
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
            //Must be called before the status detail get because the status detail get will set all the attributes in the DTO
            //And the active flag is an attribute that would be set specifically for that status and the
            //status grid calls StatusGet() which passes the active flg to the Stored Procedure and if the Detail sets the active flag to 1 then
            //the grid will send that value to the database and only return active records for the grid, if you call it before the active flag is null and all records are returned
            PaymentStatusGrid.Text = objPaymentStatus.PaymentStatusGrid();

            if (Request.QueryString.HasValue("PaymentStatusID", typeof(int)))
            {
                objPaymentStatus.PaymentStatusID = Request.QueryString["PaymentStatusID"];
                objPaymentStatus.PaymentStatusDetailGet();

                Delete.Visible = (objSecurity.DeleteFlg == "1" && objPaymentStatus.Audit.ActiveFlg == "1") ? true : false;
            }
        }

        public void DataToFormBind()
        {
            PaymentStatus.Text = objPaymentStatus.PaymentStatus;
            PaymentStatusID.Value = objPaymentStatus.PaymentStatusID;
            Sort.Text = objPaymentStatus.Sort;

            if (string.IsNullOrEmpty(objPaymentStatus.Audit.ActiveFlg) || (objPaymentStatus.Audit.ActiveFlg == "1"))
            {
                ActiveFlg.Text = "<img src='../../../../images/icon_check.gif' alt='Active'>";
            }
            else
            {
                ActiveFlg.Visible = false;
                ActiveFlgCB.Visible = true;
            }

            CreateBy.Text = objPaymentStatus.Audit.CreateBy;
            CreateDT.Text = objPaymentStatus.Audit.CreateDt;
            ModifyBy.Text = objPaymentStatus.Audit.ModifyBy;
            ModifyDT.Text = objPaymentStatus.Audit.ModifyDt;
        }

        public void FormToDataBind()
        {
            objPaymentStatus.PaymentStatus = PaymentStatus.Text;
            objPaymentStatus.PaymentStatusID = PaymentStatusID.Value;
            objPaymentStatus.Sort = Sort.Text;

            objPaymentStatus.Audit.ModifyBy = objSecurity.Username;
            objPaymentStatus.Audit.CreateBy = objSecurity.Username;

            if (ActiveFlgCB.Visible)
            {
                objPaymentStatus.Audit.ActiveFlg = ActiveFlgCB.Checked ? "1" : "0";
            }
            else
            {
                objPaymentStatus.Audit.ActiveFlg = "1";
            }
        }

        public bool ServerValidate(ref string errors)
        {
            // The PaymentStatus field is required
            if (this.PaymentStatus.Text.Length == 0)
            {
                errors += "PaymentStatus is a required field.<br />";
                this.PaymentStatus.Attributes.CssStyle.Add("background-color", "Pink");
                ErrorMessage.Visible = true;
            }

            // The Sort field is required
            if (this.Sort.Text.Length == 0)
            {
                errors += "Sort is a required field.<br />";
                this.Sort.Attributes.CssStyle.Add("background-color", "Pink");
                ErrorMessage.Visible = true;
            }

            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        #region Events

        protected void Submit_Click(object sender, EventArgs e)
        {
            string errors = string.Empty;
            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                if (string.IsNullOrEmpty(PaymentStatusID.Value))
                {
                    objPaymentStatus.PaymentStatusInsert();
                }
                else
                {
                    objPaymentStatus.PaymentStatusUpdate();
                }

                Response.Redirect("PaymentStatusProfile.aspx?PaymentStatusID=" + objPaymentStatus.PaymentStatusID);
            }
            else
            {
                ErrorMessage.Visible = true;
                ErrorMessage.Text = errors;

                if (!ClientScript.IsClientScriptBlockRegistered("ValidationFailure"))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "ValidationFailure", "<script type='text/javascript'>alert('" + errors.Replace("'", "").Replace("<br />", "\\n") + "');</script>", false);
                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string errors = string.Empty;
            if (ServerValidate(ref errors))
            {
                FormToDataBind();
                //set active flg to (0) zero after form fields have been gathered
                objPaymentStatus.Audit.ActiveFlg = "0";
                objPaymentStatus.PaymentStatusUpdate();

                Response.Redirect("PaymentStatusProfile.aspx?PaymentStatusID=" + objPaymentStatus.PaymentStatusID);
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
            Response.Redirect("PaymentStatusProfile.aspx");
        }

        #endregion Events
    }
}