using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BonusPlan
{
    public partial class MoveInRevenueProfile : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected CommunityBO objCommunity = new CommunityBO();
        protected MoveInRevenueBO objMoveInRevenue = new MoveInRevenueBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2400";
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

            if (objMoveInRevenue.MoveInRevenueDeleteVerify() == true)
            {
                Delete.Visible = true;
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            if (Request.QueryString.HasValue("MoveInRevenueID", typeof(int)))
            {
                MoveInRevenueIDHidden.Value = Request.QueryString["MoveInRevenueID"].ToString();
                objMoveInRevenue.MoveInRevenueID = MoveInRevenueIDHidden.Value;
                RevenueDetailGrid.Text = objMoveInRevenue.MoveInRevenueDetailGrid();
            }
            else
            {
                Response.Write("The appears to be problem with your query string.");
                Response.End();
            }
        }

        public void DataToFormBind()
        {
            MoveInRevenueID.Text = objMoveInRevenue.MoveInRevenueID;
            Community.Text = objMoveInRevenue.Community;
            CommunityID.Text = objMoveInRevenue.CommunityID;
            CustomerID.Text = objMoveInRevenue.CustomerID;
            MoveInDT.Text = objMoveInRevenue.MoveInDate;
            MoveOutDT.Text = objMoveInRevenue.MoveOutDate;
            CreateBy.Text = objSecurity.Username;
        }

        public void FormToDataBind()
        {
            objMoveInRevenue.MoveInRevenueID = MoveInRevenueIDHidden.Value;
        }

        public bool ServerValidate(ref string errors)
        {
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        #region Events

        protected void Delete_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            objMoveInRevenue.MoveInRevenueDelete();
            Response.Redirect("MoveInRevenueSearch.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MoveInRevenueSearch.aspx");
        }

        #endregion Events
    }
}