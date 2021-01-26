using System.Web.Services;

namespace BonusPlan.AJAXData
{
    public partial class CommunityModalAJAX : System.Web.UI.Page
    {
        [WebMethod]
        public static string CommunityModalGet(string Username)
        {
            SecurityBO objSecurity = new SecurityBO();
            objSecurity.Username = Username;

            return objSecurity.CommunityUserAccessMenuModal();
        }
    }
}