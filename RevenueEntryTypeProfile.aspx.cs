using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class RevenueEntryTypeProfile : System.Web.UI.Page
    {
        protected SecurityBO objSecurity = new SecurityBO();

        //protected MenuBO objMenuBar = new MenuBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();

        protected RevenueEntryTypeBO objRevenueEntryType = new RevenueEntryTypeBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2403";
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
            RevenueEntryTypeGrid.Text = objRevenueEntryType.RevenueEntryTypeGrid();

            if (Request.QueryString.HasValue("RevenueEntryTypeID", typeof(int)))
            {
                objRevenueEntryType.RevenueEntryTypeID = Request.QueryString["RevenueEntryTypeID"];
                objRevenueEntryType.RevenueEntryTypeDetailGet();

                Delete.Visible = (objSecurity.DeleteFlg == "1" && objRevenueEntryType.Audit.ActiveFlg == "1") ? true : false;
            }
        }

        public void DataToFormBind()
        {
            RevenueEntryType.Text = objRevenueEntryType.RevenueEntryType;
            RevenueEntryTypeID.Value = objRevenueEntryType.RevenueEntryTypeID;

            CreateBy.Text = objRevenueEntryType.Audit.CreateBy;
            CreateDT.Text = objRevenueEntryType.Audit.CreateDt;
            ModifyBy.Text = objRevenueEntryType.Audit.ModifyBy;
            ModifyDT.Text = objRevenueEntryType.Audit.ModifyDt;

            if (string.IsNullOrEmpty(objRevenueEntryType.Audit.ActiveFlg) || (objRevenueEntryType.Audit.ActiveFlg == "1"))
            {
                ActiveFlg.Text = "<img src='../../../../images/icon_check.gif' alt='Active'>";
            }
            else
            {
                ActiveFlg.Visible = false;
                ActiveFlgCB.Visible = true;
            }
        }

        public void FormToDataBind()
        {
            objRevenueEntryType.RevenueEntryType = RevenueEntryType.Text;
            objRevenueEntryType.RevenueEntryTypeID = RevenueEntryTypeID.Value;

            objRevenueEntryType.Audit.ModifyBy = objSecurity.Username;
            objRevenueEntryType.Audit.CreateBy = objSecurity.Username;

            if (ActiveFlgCB.Visible)
            {
                objRevenueEntryType.Audit.ActiveFlg = ActiveFlgCB.Checked ? "1" : "0";
            }
            else
            {
                objRevenueEntryType.Audit.ActiveFlg = "1";
            }
        }

        public bool ServerValidate(ref string errors)
        {
            // The Job Category field is required
            if (this.RevenueEntryType.Text.Length == 0)
            {
                errors += "Category is a required field.<br />";
                this.RevenueEntryType.Attributes.CssStyle.Add("background-color", "Pink");
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

                if (string.IsNullOrEmpty(RevenueEntryTypeID.Value))
                {
                    objRevenueEntryType.RevenueEntryTypeInsert();
                }
                else
                {
                    objRevenueEntryType.RevenueEntryTypeUpdate();
                }

                Response.Redirect("RevenueEntryTypeProfile.aspx?RevenueEntryTypeID=" + objRevenueEntryType.RevenueEntryTypeID);
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
                objRevenueEntryType.Audit.ActiveFlg = "0";
                objRevenueEntryType.RevenueEntryTypeUpdate();

                Response.Redirect("RevenueEntryTypeProfile.aspx?RevenueEntryTypeID=" + objRevenueEntryType.RevenueEntryTypeID);
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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("RevenueEntryTypeProfile.aspx");
        }

        #endregion Events
    }
}