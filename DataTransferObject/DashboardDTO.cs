using Atria.Framework;
using AtriaEM;
using System;

namespace BonusPlan
{
    public class DashboardDTO
    {
        private string communityNumber;
        private string buildingID;
        private string effectiveDT;
        private string operationClusterID;
        private string bonusPlanID;
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

        public string OperationClusterID
        {
            get { return operationClusterID.WhenNullOrEmpty(String.Empty); }
            set { operationClusterID = value; }
        }

        public string BonusPlanID
        {
            get { return bonusPlanID.WhenNullOrEmpty(String.Empty); }
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