/********************************************************************
-- ATRIA SENIOR LIVING GROUP CONFIDENTIAL.  For authorized use only.
-- Except for as expressly authorized by Atria Senior Living,
-- do not disclose, copy, reproduce, distribute, or modify.
-- TM 2012 Atria Senior Living, LLC
*********************************************************************
PURPOSE:		The Data Transfer Object for PaymentStatus
AUTHOR:			Tony.Thoman
DATE:			12/16/2012
NOTES:
CHANGE CONTROL:
********************************************************************/

using AtriaEM;

namespace BonusPlan
{
    public class PaymentStatusDTO
    {
        private string paymentStatus;
        private string paymentStatusID;
        private string sort;

        private Atria.Framework.AuditDto audit;

        public string PaymentStatus
        {
            get { return paymentStatus.WhenNullOrEmpty(string.Empty); }
            set { paymentStatus = value; }
        }

        public string PaymentStatusID
        {
            get { return paymentStatusID.WhenNullOrEmpty(string.Empty); }
            set { paymentStatusID = value; }
        }

        public string Sort
        {
            get { return sort.WhenNullOrEmpty(string.Empty); }
            set { sort = value; }
        }

        public Atria.Framework.AuditDto Audit
        {
            get
            {
                if (audit == null)
                {
                    audit = new Atria.Framework.AuditDto();
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