using Atria.Framework;
using AtriaEM;

namespace BonusPlan
{
    public class MoveInRevenueDTO
    {
        private string customerID;
        private string community;
        private string communityID;
        private string moveInDate;
        private string moveOutDate;
        private string moveInRevenueID;
        private AuditDto audit;

        public string CustomerID
        {
            get { return customerID.WhenNullOrEmpty(string.Empty); }
            set { customerID = value; }
        }

        public string Community
        {
            get { return community.WhenNullOrEmpty(string.Empty); }
            set { community = value; }
        }

        public string CommunityID
        {
            get { return communityID.WhenNullOrEmpty(string.Empty); }
            set { communityID = value; }
        }

        public string MoveInDate
        {
            get { return moveInDate.WhenNullOrEmpty(string.Empty); }
            set { moveInDate = value; }
        }

        public string MoveOutDate
        {
            get { return moveOutDate.WhenNullOrEmpty(string.Empty); }
            set { moveOutDate = value; }
        }

        public string MoveInRevenueID
        {
            get { return moveInRevenueID.WhenNullOrEmpty(string.Empty); }
            set { moveInRevenueID = value; }
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