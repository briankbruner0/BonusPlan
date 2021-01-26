using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class ApprovalManagementDTO
    {
        private string paymentToApprovalID;
        private string paymentID;
        private string bonusPlanID;
        private string jobCategoryID;
        private string jobCategoryIDAlternate;
        private string sort;
        private string exceptionNote;
        private string noteTypeID;
        private string administrationNote;
        private string usernameAlternate;
        private string approveFlg;
        private string denyFlg;
        private string exceptionFlg;
        private string overrideFlg;
        private string amount;
        private string communityNumber;
        private AuditDto audit;

        public string PaymentToApprovalID
        {
            get { return paymentToApprovalID.WhenNullOrEmpty(string.Empty); }
            set { paymentToApprovalID = value; }
        }

        public string PaymentID
        {
            get { return paymentID.WhenNullOrEmpty(string.Empty); }
            set { paymentID = value; }
        }

        public string BonusPlanID
        {
            get { return bonusPlanID.WhenNullOrEmpty(string.Empty); }
            set { bonusPlanID = value; }
        }

        public string JobCategoryID
        {
            get { return jobCategoryID.WhenNullOrEmpty(string.Empty); }
            set { jobCategoryID = value; }
        }

        public string JobCategoryIDAlternate
        {
            get { return jobCategoryIDAlternate.WhenNullOrEmpty(string.Empty); }
            set { jobCategoryIDAlternate = value; }
        }

        public string Sort
        {
            get { return sort.WhenNullOrEmpty(string.Empty); }
            set { sort = value; }
        }

        public string ExceptionNote
        {
            get { return exceptionNote.WhenNullOrEmpty(string.Empty); }
            set { exceptionNote = value; }
        }

        public string NoteTypeID
        {
            get { return noteTypeID.WhenNullOrEmpty(string.Empty); }
            set { noteTypeID = value; }
        }

        public string AdministrationNote
        {
            get { return administrationNote.WhenNullOrEmpty(string.Empty); }
            set { administrationNote = value; }
        }

        public string UsernameAlternate
        {
            get { return usernameAlternate.WhenNullOrEmpty(string.Empty); }
            set { usernameAlternate = value; }
        }

        public string ApproveFlg
        {
            get { return approveFlg.WhenNullOrEmpty(string.Empty); }
            set { approveFlg = value; }
        }

        public string DenyFlg
        {
            get { return denyFlg.WhenNullOrEmpty(string.Empty); }
            set { denyFlg = value; }
        }

        public string ExceptionFlg
        {
            get { return exceptionFlg.WhenNullOrEmpty(string.Empty); }
            set { exceptionFlg = value; }
        }

        public string OverrideFlg
        {
            get { return overrideFlg.WhenNullOrEmpty(string.Empty); }
            set { overrideFlg = value; }
        }

        public string Amount
        {
            get { return amount.WhenNullOrEmpty(string.Empty); }
            set { amount = value; }
        }

        public string CommunityNumber
        {
            get { return communityNumber.WhenNullOrEmpty(string.Empty); }
            set { communityNumber = value; }
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