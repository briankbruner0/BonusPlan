using Atria.Framework;
using AtriaEM;
using System;

namespace BonusPlan
{
    public class CommunityDTO
    {
        private string communityID;
        private string communityNumber;
        private string community;
        private string communityName;
        private string communityNameExternal;
        private string propertyTypeID;
        private string propertyType;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string postalCode;
        private string country;
        private string phoneNumber1;
        private string phoneNumber2;
        private string faxNumber;
        private string division;
        private string region;
        private AuditDto audit;

        public string CommunityID
        {
            get { return communityID.WhenNullOrEmpty(string.Empty); }
            set { communityID = value; }
        }

        public string CommunityNumber
        {
            get { return communityNumber.WhenNullOrEmpty(string.Empty); }
            set { communityNumber = value; }
        }

        public string Community
        {
            get { return community.WhenNullOrEmpty(string.Empty); }
            set { community = value; }
        }

        public string CommunityName
        {
            get { return communityName.WhenNullOrEmpty(string.Empty); }
            set { communityName = value; }
        }

        public string CommunityNameExternal
        {
            get { return communityNameExternal.WhenNullOrEmpty(String.Empty); }
            set { communityNameExternal = value; }
        }

        public string PropertyTypeID
        {
            get { return propertyTypeID.WhenNullOrEmpty(String.Empty); }
            set { propertyTypeID = value; }
        }

        public string PropertyType
        {
            get { return propertyType.WhenNullOrEmpty(String.Empty); }
            set { propertyType = value; }
        }

        public string Address1
        {
            get { return address1.WhenNullOrEmpty(String.Empty); }
            set { address1 = value; }
        }

        public string Address2
        {
            get { return address2.WhenNullOrEmpty(String.Empty); }
            set { address2 = value; }
        }

        public string City
        {
            get { return city.WhenNullOrEmpty(String.Empty); }
            set { city = value; }
        }

        public string State
        {
            get { return state.WhenNullOrEmpty(String.Empty); }
            set { state = value; }
        }

        public string PostalCode
        {
            get { return postalCode.WhenNullOrEmpty(String.Empty); }
            set { postalCode = value; }
        }

        public string Country
        {
            get { return country.WhenNullOrEmpty(String.Empty); }
            set { country = value; }
        }

        public string PhoneNumber1
        {
            get { return phoneNumber1.WhenNullOrEmpty(String.Empty); }
            set { phoneNumber1 = value; }
        }

        public string PhoneNumber2
        {
            get { return phoneNumber2.WhenNullOrEmpty(String.Empty); }
            set { phoneNumber2 = value; }
        }

        public string FaxNumber
        {
            get { return faxNumber.WhenNullOrEmpty(String.Empty); }
            set { faxNumber = value; }
        }

        public string Division
        {
            get { return division.WhenNullOrEmpty(String.Empty); }
            set { division = value; }
        }

        public string Region
        {
            get { return region.WhenNullOrEmpty(String.Empty); }
            set { region = value; }
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