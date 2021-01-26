using Atria.Framework;
using AtriaEM;
using System;

namespace BonusPlan
{
    public class PaymentModel
    {
        private string employeeID;
        private string communityNumber;
        private string amount;
        private string bonusPlanID;
        private string note;
        private string customerID;
        private AuditDto audit;

        public string EmployeeID
        {
            get { return employeeID.WhenNullOrEmpty(String.Empty); }
            set { employeeID = value; }
        }

        public string CommunityNumber
        {
            get { return communityNumber.WhenNullOrEmpty(String.Empty); }
            set { communityNumber = value; }
        }

        public string Amount
        {
            get { return amount.WhenNullOrEmpty(String.Empty); }
            set { amount = value; }
        }

        public string BonusPlanID
        {
            get { return bonusPlanID.WhenNullOrEmpty(String.Empty); }
            set { bonusPlanID = value; }
        }

        public string Note
        {
            get { return note.WhenNullOrEmpty(String.Empty); }
            set { note = value; }
        }

        public string CustomerID
        {
            get { return customerID.WhenNullOrEmpty(String.Empty); }
            set { customerID = value; }
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