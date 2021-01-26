using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class MoveInRevenueStatusProfile : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected MoveInRevenueStatusBO objMoveInRevenueStatus = new MoveInRevenueStatusBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2396";
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
            MoveInRevenueStatusGrid.Text = objMoveInRevenueStatus.MoveInRevenueStatusGrid();

            if (Request.QueryString.HasValue("MoveInRevenueStatusID", typeof(int)))
            {
                objMoveInRevenueStatus.MoveInRevenueStatusID = Request.QueryString["MoveInRevenueStatusID"].ToString();
                MoveInRevenueStatusHiddenID.Value = objMoveInRevenueStatus.MoveInRevenueStatusID;
                objMoveInRevenueStatus.MoveInRevenueStatusDetailGet();

                Delete.Visible = objSecurity.DeleteFlg.ToString() == "1" ? true : false;
            }
        }

        public void DataToFormBind()
        {
            MoveInRevenueStatusID.Text = objMoveInRevenueStatus.MoveInRevenueStatusID;
            MoveInRevenueStatus.Text = objMoveInRevenueStatus.MoveInRevenueStatus;
            Sort.Text = objMoveInRevenueStatus.Sort;
            CreateBy.Text = objMoveInRevenueStatus.Audit.CreateBy;
            CreateDt.Text = objMoveInRevenueStatus.Audit.CreateDt;
            ModifyBy.Text = objMoveInRevenueStatus.Audit.ModifyBy;
            ModifyDt.Text = objMoveInRevenueStatus.Audit.ModifyDt;

            if (string.IsNullOrEmpty(objMoveInRevenueStatus.Audit.ActiveFlg) || objMoveInRevenueStatus.Audit.ActiveFlg.ToString() == "1")
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
            objMoveInRevenueStatus.MoveInRevenueStatusID = MoveInRevenueStatusHiddenID.Value;
            objMoveInRevenueStatus.MoveInRevenueStatus = MoveInRevenueStatus.Text;
            objMoveInRevenueStatus.Sort = Sort.Text;
            objMoveInRevenueStatus.Audit.ModifyBy = objSecurity.Username;

            if (ActiveFlgCB.Visible)
            {
                objMoveInRevenueStatus.Audit.ActiveFlg = ActiveFlgCB.Checked ? "1" : "0";
            }
            else
            {
                objMoveInRevenueStatus.Audit.ActiveFlg = "1";
            }
        }

        public bool ServerValidate(ref string errors)
        {
            if (MoveInRevenueStatus.Text.Length == 0)
            {
                errors += "A move in revenue status is required.<br/>";
            }

            if (Sort.Text.Length == 0)
            {
                errors += "A sort is required.<br/>";
            }

            if (!Sort.Text.IsNumeric())
            {
                errors += "Please provide a valid sort value.<br/>";
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

                if (string.IsNullOrEmpty(MoveInRevenueStatusHiddenID.Value))
                {
                    objMoveInRevenueStatus.MoveInRevenueStatusInsert();
                }
                else
                {
                    objMoveInRevenueStatus.MoveInRevenueStatusUpdate();
                }

                Response.Redirect("MoveInRevenueStatusProfile.aspx");
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
            Response.Redirect("MoveInRevenueStatusProfile.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            objMoveInRevenueStatus.Audit.ActiveFlg = "0";
            objMoveInRevenueStatus.MoveInRevenueStatusUpdate();

            Response.Redirect(string.Format("MoveInRevenueStatusProfile.aspx?MoveInRevenueStatusID={0}", MoveInRevenueStatusHiddenID.Value));
        }
    }
}