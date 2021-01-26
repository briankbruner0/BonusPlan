using Atria.Framework;
using AtriaEM;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace BonusPlan
{
    public partial class AddAPayment : System.Web.UI.Page//, IDataInputForm -- 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
    {
        protected SecurityBO objSecurity = new SecurityBO();
        // 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
        //protected MenuBarBO objMenuBar = new MenuBarBO();
        //protected CommunityBO objCommunity = new CommunityBO();
        //protected PaymentBO objPayment = new PaymentBO();
        //protected BonusPlanBO objBonusPlan = new BonusPlanBO();

        protected void Page_Load(object sender, EventArgs e)
        {
            objSecurity.ApplicationID = "161";
            objSecurity.FeatureID = "2424";
            objSecurity.AccessVerify(objSecurity.ApplicationID, objSecurity.FeatureID);

            var xmlPath = HttpContext.Current.Server.MapPath("/Application/BonusPlan/App_Data/Default.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            objSecurity.MenuXML = xmldoc.InnerXml.ToString();

            SecurityMenu.Text = objSecurity.MenuBarResponsive();
            SecurityTitle.Text = objSecurity.Title;
            SecurityBody.Text = Regex.Replace(objSecurity.Body, @"\r", "<br />", RegexOptions.Multiline);
            Username.Value = objSecurity.Username;

            if (!Page.IsPostBack)
            {
                FormInitialize();
                DataToFormBind();
            }
        }

        #region IDataInputForm Members

        public void FormInitialize()
        {
            // 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
            //objBonusPlan.Audit.ActiveFlg = "1";
            //AtriaBase.UI.ListControl.ListPopulate(CommunityNumber, "Community", "CommunityNumber", objPayment.PaymentCommunityGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
            //AtriaBase.UI.ListControl.ListPopulate(BonusPlanID, "BonusPlan", "BonusPlanID", objBonusPlan.BonusPlanGet(), AtriaBase.UI.ListControl.NoItemSelectedBehavior.SelectOne);
        }

        public void DataToFormBind()
        {
            // 02/13/2018 Fritz Kern - Removing as not needed.
            //CreateBy.Text = objSecurity.Username;
        }

        public void FormToDataBind()
        {
            // 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
            //objPayment.EmployeeID = SearchEmployeeHiddenID.Value;
            //objPayment.Amount = BonusValue.Text;
            //objPayment.CommunityNumber = CommunityNumber.SelectedValue;
            //objPayment.BonusPlanID = BonusPlanID.SelectedValue;
            //objPayment.Note = PaymentNote.Text;
            //objPayment.CustomerID = Resident.Value;
            //objPayment.Audit.CreateBy = objSecurity.Username;
        }

        // 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
        //public bool ServerValidate(ref string errors)
        //{
        //    //set default background color for control
        //    this.BonusValue.Attributes.CssStyle.Add("background-color", "White");
        //    this.SearchEmployeeID.Attributes.CssStyle.Add("background-color", "White");
        //    this.CommunityNumber.Attributes.CssStyle.Add("background-color", "White");
        //    this.BonusPlanID.Attributes.CssStyle.Add("background-color", "White");
        //    this.PaymentNote.Attributes.CssStyle.Add("background-color", "White");

        //    if (BonusValue.Text.Length == 0)
        //    {
        //        errors += "Bonus Value is required.<br />";
        //        this.BonusValue.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (SearchEmployeeHiddenID.Value.Length == 0)
        //    {
        //        errors += "Employee is required.<br />";
        //        this.SearchEmployeeID.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (CommunityNumber.SelectedValue.Length == 0)
        //    {
        //        errors += "Community is required.<br />";
        //        this.CommunityNumber.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (BonusPlanID.Text.Length == 0)
        //    {
        //        errors += "Bonus Plan is required.<br />";
        //        this.BonusPlanID.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (PaymentNote.Text.Length == 0)
        //    {
        //        errors += "Note is required.<br />";
        //        this.PaymentNote.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (Resident.Value.Length == 0)
        //    {
        //        errors += "Resident is required.<br />";
        //        this.Resident.Attributes.CssStyle.Add("background-color", "Pink");
        //    }
        //    if (PaymentNote.Text.Length > 200)
        //    {
        //        errors += "You have reached the Max Length of 200 characters for a Note.<br />";
        //        this.PaymentNote.Attributes.CssStyle.Add("background-color", "Pink");
        //    }

        //    try
        //    {
        //        if (decimal.Parse(BonusValue.Text) < -5000M)
        //        {
        //            errors += "Bonus Value cannot exceed -5000.00<br />";
        //            this.BonusValue.Attributes.CssStyle.Add("background-color", "Pink");
        //        }

        //        if (decimal.Parse(BonusValue.Text) > 20000M)
        //        {
        //            errors += "Bonus Value cannot exceed 20000.00<br />";
        //            this.BonusValue.Attributes.CssStyle.Add("background-color", "Pink");
        //        }
        //    }
        //    catch
        //    {
        //        errors += "Bonus Value must be a numeric format. <br />";
        //        this.BonusValue.Attributes.CssStyle.Add("background-color", "Pink");
        //    }

        //    return errors.Length == 0;
        //}

        #endregion IDataInputForm Members

        #region button events

        // 02/14/2018 Fritz Kern - Removing in favor of the new way we handle data.
        //protected void Submit_Click(object sender, EventArgs e)
        //{
        //    string errors = string.Empty;
        //    if (ServerValidate(ref errors))
        //    {
        //        FormToDataBind();

        //        //Add a payment
        //        objPayment.PaymentInsert();
        //    }
        //    else
        //    {
        //        if (!ClientScript.IsClientScriptBlockRegistered("ValidationFailure"))
        //        {
        //            ClientScript.RegisterClientScriptBlock(GetType(), "ValidationFailure", "<script type='text/javascript'>alert('" + errors.Replace("'", "").Replace("<br />", "\\n") + "');</script>", false);
        //        }
        //    }
        //}

        //protected void Cancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AddPayment.aspx");
        //}

        #endregion button events
    }
}