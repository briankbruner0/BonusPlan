using Atria.Framework;
using AtriaEM;
using System;
using System.Web.UI;

namespace BonusPlan
{
    public partial class TrainingandDocumentation : System.Web.UI.Page, IDataInputForm
    {
        protected SecurityBO objSecurity = new SecurityBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2364";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);
            Title.Text = objSecurity.Title;
            Body.Text = objSecurity.Body;

            if (Request.QueryString.HasValue("EffectiveDt", typeof(DateTime)))
            {
                objSecurity.EffectiveDT = Request.QueryString["EffectiveDt"].ToString();
            }
            else
            {
                objSecurity.EffectiveDT = DateTime.Today.ToShortDateString();
            }

            SecurityMenu.Text = objSecurity.MenuBar();

            if (!Page.IsPostBack)
            {
                FormInitialize();
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            TrainingPathGrid.Text = objSecurity.TrainingPathGrid();
        }

        public void DataToFormBind()
        {
        }

        public void FormToDataBind()
        {
        }

        public bool ServerValidate(ref string errors)
        {
            return true;
        }

        #endregion IDataInputForm Members
    }
}