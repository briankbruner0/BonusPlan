using System.Web.Services;

namespace BonusPlan
{
    public partial class Approval : System.Web.UI.Page
    {
        [WebMethod]
        public static string ApprovePayment(string PaymentToApprovalID, string CommunityNumber, string UserName)
        {
            ApprovalManagementBO objApproval = new ApprovalManagementBO();
            objApproval.ApproveFlg = "1";
            objApproval.PaymentToApprovalID = PaymentToApprovalID;
            objApproval.Audit.UserName = UserName;
            objApproval.ApprovalUpdate();

            DashboardBO objDashboard = new DashboardBO();
            objDashboard.Audit.UserName = UserName;
            objDashboard.CommunityNumber = CommunityNumber;

            return objDashboard.snpApprovalByUserName();
        }

        //[WebMethod]
        //public static string DenyPayment(string PaymentToApprovalID, string CommunityNumber, string UserName)
        //{
        //    ApprovalManagementBO objApproval = new ApprovalManagementBO();
        //    objApproval.DenyFlg = "1";
        //    objApproval.PaymentToApprovalID = PaymentToApprovalID;
        //    objApproval.Audit.UserName = UserName;
        //    objApproval.ApprovalUpdate();

        //    DashboardBO objDashboard = new DashboardBO();
        //    objDashboard.Audit.UserName = UserName;
        //    objDashboard.CommunityNumber = CommunityNumber;

        //    return objDashboard.snpApprovalByUserName();
        //}

        [WebMethod]
        public static string CreateException(string PaymentToApprovalID, string ExceptionNote, string CommunityNumber, string UserName)
        {
            ApprovalManagementBO objApproval = new ApprovalManagementBO();
            objApproval.ExceptionFlg = "1";
            objApproval.PaymentToApprovalID = PaymentToApprovalID;
            objApproval.ExceptionNote = ExceptionNote;
            objApproval.Audit.UserName = UserName;
            objApproval.ApprovalUpdate();

            DashboardBO objDashboard = new DashboardBO();
            objDashboard.Audit.UserName = UserName;
            objDashboard.CommunityNumber = CommunityNumber;

            return objDashboard.snpApprovalByUserName();
        }
    }
}