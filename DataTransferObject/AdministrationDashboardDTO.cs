using Atria.Framework;
using AtriaEM;
using System;

namespace BonusPlan
{
    public class AdministrationDashboardDTO
    {
        private string communityNumber;
        private string buildingID;
        private string effectiveDT;
        private AuditDto audit;

        public string CommunityNumber
        {
            get { return communityNumber.WhenNullOrEmpty(String.Empty); }
            set { communityNumber = value; }
        }

        public string BuildingID
        {
            get { return buildingID.WhenNullOrEmpty(String.Empty); }
            set { buildingID = value; }
        }

        public string EffectiveDT
        {
            get { return effectiveDT.WhenNullOrEmpty(String.Empty); }
            set { effectiveDT = value; }
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