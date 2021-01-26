using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Linq;
using System.Web.UI.WebControls;

namespace BonusPlan
{
    public partial class BonusPlanProfile : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected MenuBarBO objMenuBar = new MenuBarBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();
        protected RevenueEntryTypeBO objRevenueEntry = new RevenueEntryTypeBO();
        protected CommunityBO objCommunity = new CommunityBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2352";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            var xmlPath = HttpContext.Current.Server.MapPath("/Application/BonusPlan/App_Data/Default.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            objSecurity.MenuXML = xmldoc.InnerXml.ToString();

            SecurityMenu.Text = objSecurity.MenuBarResponsive();

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

            Username.Value = objSecurity.Username;

            //SecurityMenu.Text = objSecurity.MenuBar();
            //SecurityMenu.Text += objMenuBar.MenuUserProfile();
            PageTitle.Text = objSecurity.Title;
            PageBody.Text = Regex.Replace(objSecurity.Body.WhenNullOrEmpty(""), @"\r", "<br />", RegexOptions.Multiline); // Added extension method to prevent null input parameter in the regex replace method - 05/19/2017 Rob Azinger

            if (!Page.IsPostBack)
            {
                FormInitialize();
                DataToFormBind();
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            if (Request.QueryString.HasValue("BonusPlanID", typeof(int)))
            {
                objBonusPlan.BonusPlanID = Request.QueryString["BonusPlanID"].ToString();
                BonusPlanHiddenID.Value = objBonusPlan.BonusPlanID;
                objBonusPlan.BonusPlanDetailGet();

                DeleteFlg.Value = objSecurity.DeleteFlg.ToString();
                //Delete.Visible = objSecurity.DeleteFlg.ToString() == "1" ? true : false;
            }

            DropDownList dbOptions = new DropDownList();
            AtriaBase.UI.ListControl.ListPopulate(JobCodeID, "JobCode", "JobCodeID", objBonusPlan.JobCodeGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            AtriaBase.UI.ListControl.ListPopulate(JobCodeCommunity, "Community", "CommunityNumber", objSecurity.CommunityUserAccessGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectAll);
            AtriaBase.UI.ListControl.ListPopulate(JobCategoryID, "JobCategory", "JobCategoryID", objBonusPlan.JobCategoryGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            AtriaBase.UI.ListControl.ListPopulate(dbOptions, "Community", "CommunityNumber", objBonusPlan.BonusPlanToCommunityGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            AtriaBase.UI.ListControl.ListPopulate(JobCodeEffectiveDt, "MonthName", "EffectiveDT", objBonusPlan.BonusPlanToJobCodeEffectiveDateGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            JobCodeEffectiveDt.SelectedIndex = 2; // Default to current month
            var communities = dbOptions.Items.Cast<ListItem>()
                .Where(i => !string.IsNullOrEmpty(i.Value))
                .GroupBy(g => g.Value)
                .Select(i => i.FirstOrDefault())
                .OrderBy(i => i.Text).ToList();
            communities.Insert(0, new ListItem() { Text = "Select One...", Value = "" });
            
            ListItemCollection communitiesCollection = new ListItemCollection();
            communitiesCollection.AddRange(communities.ToArray());
            CommunityNumber.DataTextField = "Text";
            CommunityNumber.DataValueField = "Value";
            CommunityNumber.DataSource = communitiesCollection;
            CommunityNumber.DataBind();
        }

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
            objBonusPlan.JobCodeID = JobCodeID.SelectedValue;
            objBonusPlan.JobCodeCommunityNumber = JobCodeCommunity.SelectedValue;
            objBonusPlan.JobCodeCommissionBase = JobCodeCommissionBase.Text;
            objBonusPlan.JobCodeMultiplier = JobCodeMultiplier.Text;
            objBonusPlan.JobCodePercentage = JobCodePercentage.Text;
            objBonusPlan.JobCategoryID = JobCategoryID.SelectedValue;
            objBonusPlan.ApprovalSort = ApprovalSort.Text;
            objBonusPlan.ApprovalAmount = ApprovalAmount.Text;
            objBonusPlan.RevenueEntryTypeToLedgerEntryID = Request.Form["RevenueAccount"];
            objBonusPlan.BonusPlanToJobCodeID = Request.Form["JobCode"];
            objBonusPlan.ApproverManagementID = Request.Form["ApprovalWorkflow"];
            objBonusPlan.BonusPlanToUserCommunityID = Request.Form["UserCommunity"];

            objBonusPlan.BonusPlanID = BonusPlanHiddenID.Value;
            objBonusPlan.Audit.ModifyBy = objSecurity.Username;
        }

        public bool ServerValidate(ref string errors)
        {
            return errors.Length == 0;
        }

        #endregion IDataInputForm Members

        protected void Submit_Click(object sender, EventArgs e)
        {
            string errors = string.Empty;

            if (ServerValidate(ref errors))
            {
                FormToDataBind();

                if (string.IsNullOrEmpty(BonusPlanHiddenID.Value))
                {
                    objBonusPlan.BonusPlanInsert();
                }
                else
                {
                    objBonusPlan.BonusPlanUpdate();
                }

                Response.Redirect(string.Format("BonusPlanProfile.aspx?BonusPlanID={0}", BonusPlanHiddenID.Value));
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
            Response.Redirect("Default.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            FormToDataBind();
            objBonusPlan.Audit.ActiveFlg = "0";
            objBonusPlan.BonusPlanUpdate();

            Response.Redirect(string.Format("BonusPlanProfile.aspx?BonusPlanID={0}", BonusPlanHiddenID.Value));
        }
    }
}