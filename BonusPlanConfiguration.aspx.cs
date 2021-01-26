using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace BonusPlan
{
    public partial class BonusPlanConfiguration : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();
        protected BonusPlanBO objBonusPlan = new BonusPlanBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "3862";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            var xmlPath = HttpContext.Current.Server.MapPath("/Application/BonusPlan/App_Data/Default.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            objSecurity.MenuXML = xmldoc.InnerXml.ToString();

            SecurityMenu.Text = objSecurity.MenuBarResponsive();

            if (Request.QueryString.HasValue("EffectiveDt", typeof(DateTime)))
            {
                objSecurity.EffectiveDT = Request.QueryString["EffectiveDt"].ToString();
            }
            else
            {
                objSecurity.EffectiveDT = DateTime.Today.ToShortDateString();
            }

            PageTitle.Text = objSecurity.Title;
            PageBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br />", RegexOptions.Multiline);

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