using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace BonusPlan
{
    public partial class BonusPlanEDDashboard : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "4068";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            var xmlPath = HttpContext.Current.Server.MapPath("/Application/BonusPlan/App_Data/Default.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            objSecurity.MenuXML = xmldoc.InnerXml.ToString();

            SecurityMenu.Text = objSecurity.MenuBarResponsive();

            if (Request.QueryString.HasValue("CommunityNumber", typeof(int)))
            {
                CommunityNumber.Value = Request.QueryString["CommunityNumber"].ToString();
            }
            else
            {
                CommunityNumber.Value = objSecurity.CommunityNumber;
            }

            Username.Value = objSecurity.Username;

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
        }

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

        #endregion IDataInputForm Members
    }
}