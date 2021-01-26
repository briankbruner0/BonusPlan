using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class PaymentScheduleDTO
    {
        private string paymentScheduleID;
        private string emailAlertDt;
        private string finalNotificationDt;
        private string processDt;
        private string payrollDt;

        /// <summary>
        /// 08/29/2016  Amanda Marburger
        ///             Added bonus plan id as a property
        /// </summary>
        private string bonusPlanID;

        private AuditDto audit;

        public string PaymentScheduleID
        {
            get { return paymentScheduleID.WhenNullOrEmpty(string.Empty); }
            set { paymentScheduleID = value; }
        }

        public string EmailAlertDt
        {
            get { return emailAlertDt.WhenNullOrEmpty(string.Empty); }
            set { emailAlertDt = value; }
        }

        public string FinalNotificationDt
        {
            get { return finalNotificationDt.WhenNullOrEmpty(string.Empty); }
            set { finalNotificationDt = value; }
        }

        public string ProcessDt
        {
            get { return processDt.WhenNullOrEmpty(string.Empty); }
            set { processDt = value; }
        }

        public string PayrollDt
        {
            get { return payrollDt.WhenNullOrEmpty(string.Empty); }
            set { payrollDt = value; }
        }

        public string BonusPlanID
        {
            get { return bonusPlanID.WhenNullOrEmpty(string.Empty); }
            set { bonusPlanID = value; }
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