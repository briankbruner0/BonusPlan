using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class MoveInRevenueStatusDTO
    {
        private string moveInRevenueStatusID;
        private string moveInRevenueStatus;
        private string sort;
        private AuditDto audit;

        public string MoveInRevenueStatusID
        {
            get { return moveInRevenueStatusID.WhenNullOrEmpty(string.Empty); }
            set { moveInRevenueStatusID = value; }
        }

        public string MoveInRevenueStatus
        {
            get { return moveInRevenueStatus.WhenNullOrEmpty(string.Empty); }
            set { moveInRevenueStatus = value; }
        }

        public string Sort
        {
            get { return sort.WhenNullOrEmpty(string.Empty); }
            set { sort = value; }
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