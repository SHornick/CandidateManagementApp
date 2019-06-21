using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.Models
{
    public class Candidate
    {
        public int ID { get; set; }
        [DisplayName("First Name"), Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name"), Required]
        public string LastName { get; set; }
        [DisplayName("Email Address"), Required, EmailAddress]
        public string EmailAddress { get; set; }
        [DisplayName("Phone Number"), Required, Phone]
        public string PhoneNumber { get; set; }
        [DisplayName("Zip Code"), Required, Range(10000, 99999)]
        public int ZipCode { get; set; }

        public List<Qualification> Qualifications { get; set; }

        public Candidate()
        {
            Qualifications = new List<Qualification>();
        }
    }
}
