using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.netAddressBook
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Zip { get; set; }
        public double PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
