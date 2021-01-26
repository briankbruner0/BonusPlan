using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class BonusPlanDTO
    {
        private string bonusPlanID;
        private string bonusPlan;
        private string payrollCommunity;
        private string processPeriod;
        private string excludeLookBackFlg;
        private string emailFlg;

        //private string lookBackPeriod;
        private string revenueEntryTypeToLedgerEntryID;

        private string revenueEntryTypeID;
        private string ledgerEntryID;
        private string typeOfEarning;
        private string bonusPlanToJobCodeID;
        private string jobCodeID;
        private string jobCodeCommunityNumber;
        private string jobCodeCommissionBase;
        private string jobCodeMultiplier;
        private string jobCodePercentage;
        private string jobCodeFlatRate;
        private string approverManagementID;
        private string jobCategoryID;
        private string approvalSort;
        private string approvalAmount;
        private string employeeID;
        private string employee;
        private string aDPEmployeeID;
        private string communityNumber;
        private string percentage;
        private string flatRate;
        private string beginDt;
        private string endDt;
        private string rollforwardFlg;
        private string bonusPlanToUserCommunityID;
        private string username;
        private string operationClusterID;
        private string effectiveDt;
        private string paymentProcessFlg;
        // 02/14/2018 Fritz Kern - Adding some more employee information
        private string jobTitle;
        private string community;
        private string photoPath;
        private AuditDto audit;

        public string BonusPlanID
        {
            get { return bonusPlanID.WhenNullOrEmpty(string.Empty); }
            set { bonusPlanID = value; }
        }

        public string BonusPlan
        {
            get { return bonusPlan.WhenNullOrEmpty(string.Empty); }
            set { bonusPlan = value; }
        }

        public string PayrollCommunity
        {
            get { return payrollCommunity.WhenNullOrEmpty(string.Empty); }
            set { payrollCommunity = value; }
        }

        public string ProcessPeriod
        {
            get { return processPeriod.WhenNullOrEmpty(string.Empty); }
            set { processPeriod = value; }
        }

        public string ExcludeLookBackFlg
        {
            get { return excludeLookBackFlg.WhenNullOrEmpty(string.Empty); }
            set { excludeLookBackFlg = value; }
        }

        public string EmailFlg
        {
            get { return emailFlg.WhenNullOrEmpty(string.Empty); }
            set { emailFlg = value; }
        }

        //public string LookBackPeriod
        //{
        //    get { return lookBackPeriod.WhenNullOrEmpty(string.Empty); }
        //    set { lookBackPeriod = value; }
        //}
        public string RevenueEntryTypeToLedgerEntryID
        {
            get { return revenueEntryTypeToLedgerEntryID.WhenNullOrEmpty(string.Empty); }
            set { revenueEntryTypeToLedgerEntryID = value; }
        }

        public string RevenueEntryTypeID
        {
            get { return revenueEntryTypeID.WhenNullOrEmpty(string.Empty); }
            set { revenueEntryTypeID = value; }
        }

        public string LedgerEntryID
        {
            get { return ledgerEntryID.WhenNullOrEmpty(string.Empty); }
            set { ledgerEntryID = value; }
        }

        public string TypeOfEarning
        {
            get { return typeOfEarning.WhenNullOrEmpty(string.Empty); }
            set { typeOfEarning = value; }
        }

        public string BonusPlanToJobCodeID
        {
            get { return bonusPlanToJobCodeID.WhenNullOrEmpty(string.Empty); }
            set { bonusPlanToJobCodeID = value; }
        }

        public string JobCodeID
        {
            get { return jobCodeID.WhenNullOrEmpty(string.Empty); }
            set { jobCodeID = value; }
        }

        public string JobCodeCommissionBase
        {
            get { return jobCodeCommissionBase.WhenNullOrEmpty(string.Empty); }
            set { jobCodeCommissionBase = value; }
        }

        public string JobCodeMultiplier
        {
            get { return jobCodeMultiplier.WhenNullOrEmpty(string.Empty); }
            set { jobCodeMultiplier = value; }
        }

        public string JobCodeCommunityNumber
        {
            get { return jobCodeCommunityNumber.WhenNullOrEmpty(string.Empty); }
            set { jobCodeCommunityNumber = value; }
        }

        public string JobCodePercentage
        {
            get { return jobCodePercentage.WhenNullOrEmpty(string.Empty); }
            set { jobCodePercentage = value; }
        }

        public string JobCodeFlatRate
        {
            get { return jobCodeFlatRate.WhenNullOrEmpty(string.Empty); }
            set { jobCodeFlatRate = value; }
        }

        public string ApproverManagementID
        {
            get { return approverManagementID.WhenNullOrEmpty(string.Empty); }
            set { approverManagementID = value; }
        }

        public string JobCategoryID
        {
            get { return jobCategoryID.WhenNullOrEmpty(string.Empty); }
            set { jobCategoryID = value; }
        }

        public string ApprovalSort
        {
            get { return approvalSort.WhenNullOrEmpty(string.Empty); }
            set { approvalSort = value; }
        }

        public string ApprovalAmount
        {
            get { return approvalAmount.WhenNullOrEmpty(string.Empty); }
            set { approvalAmount = value; }
        }

        public string EmployeeID
        {
            get { return employeeID.WhenNullOrEmpty(string.Empty); }
            set { employeeID = value; }
        }

        public string Employee
        {
            get { return employee.WhenNullOrEmpty(string.Empty); }
            set { employee = value; }
        }

        public string ADPEmployeeID
        {
            get { return aDPEmployeeID.WhenNullOrEmpty(string.Empty); }
            set { aDPEmployeeID = value; }
        }

        public string CommunityNumber
        {
            get { return communityNumber.WhenNullOrEmpty(string.Empty); }
            set { communityNumber = value; }
        }

        public string Percentage
        {
            get { return percentage.WhenNullOrEmpty(string.Empty); }
            set { percentage = value; }
        }

        public string FlatRate
        {
            get { return flatRate.WhenNullOrEmpty(string.Empty); }
            set { flatRate = value; }
        }

        public string BeginDt
        {
            get { return beginDt.WhenNullOrEmpty(string.Empty); }
            set { beginDt = value; }
        }

        public string EndDt
        {
            get { return endDt.WhenNullOrEmpty(string.Empty); }
            set { endDt = value; }
        }

        public string RollforwardFlg
        {
            get { return rollforwardFlg.WhenNullOrEmpty(string.Empty); }
            set { rollforwardFlg = value; }
        }

        public string BonusPlanToUserCommunityID
        {
            get { return bonusPlanToUserCommunityID.WhenNullOrEmpty(string.Empty); }
            set { bonusPlanToUserCommunityID = value; }
        }

        public string Username
        {
            get { return username.WhenNullOrEmpty(string.Empty); }
            set { username = value; }
        }

        public string OperationClusterID
        {
            get { return operationClusterID.WhenNullOrEmpty(string.Empty); }
            set { operationClusterID = value; }
        }

        public string EffectiveDt
        {
            get { return effectiveDt.WhenNullOrEmpty(string.Empty); }
            set { effectiveDt = value; }
        }

        public string PaymentProcessFlg
        {
            get { return paymentProcessFlg.WhenNullOrEmpty(string.Empty); }
            set { paymentProcessFlg = value; }
        }


        // 02/14/2018 Fritz Kern - Adding some more employee information
        public string JobTitle
        {
            get { return jobTitle.WhenNullOrEmpty(string.Empty); }
            set { jobTitle = value; }
        }

        public string Community
        {
            get { return community.WhenNullOrEmpty(string.Empty); }
            set { community = value; }
        }

        public string PhotoPath
        {
            get { return photoPath.WhenNullOrEmpty(string.Empty); }
            set { photoPath = value; }
        }

        public AuditDto Audit
        {
            get
            {
                if (audit == null)
                {
                    audit = new AuditDto();
                }
                return audit;
            }
            set
            {
                audit = value;
            }
        }
    }
}