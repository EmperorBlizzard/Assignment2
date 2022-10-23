using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    internal class ContactPerson
    {
        public ContactPerson()
        {

        }

        //Mallen av kontakter
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
