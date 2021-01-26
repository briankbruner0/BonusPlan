/********************************************************************
-- ATRIA SENIOR LIVING GROUP CONFIDENTIAL.  For authorized use only.
-- Except for as expressly authorized by Atria Senior Living,
-- do not disclose, copy, reproduce, distribute, or modify.
-- TM 2012 Atria Senior Living, LLC
*********************************************************************
PURPOSE:		The Data Transfer Object for RevenueEntryType
AUTHOR:			Tony.Thoman
DATE:			12/16/2012
NOTES:
CHANGE CONTROL:
********************************************************************/

using AtriaEM;

namespace BonusPlan
{
    public class RevenueEntryTypeDTO
    {
        private string revenueEntryType;
        private string revenueEntryTypeID;

        private Atria.Framework.AuditDto audit;

        public string RevenueEntryType
        {
            get { return revenueEntryType.WhenNullOrEmpty(string.Empty); }
            set { revenueEntryType = value; }
        }

        public string RevenueEntryTypeID
        {
            get { return revenueEntryTypeID.WhenNullOrEmpty(string.Empty); }
            set { revenueEntryTypeID = value; }
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