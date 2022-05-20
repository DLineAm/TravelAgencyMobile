using System;
using System.Collections.Generic;


namespace TravelAgency
{
    public partial class Hotel
    {
        public Hotel()
        {
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string TheContactPerson { get; set; }

        public bool IsGreatLocation { get; set; }
        public bool IsParkingAllow { get; set; }

        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
