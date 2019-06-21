using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.Models
{
    public class CandidateSearch
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DisplayName("Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [DisplayName("Zip Code")]
        [Range(10000, 99999)]
        public int? ZipCode { get; set; } // Made a nullable type because ZipCode is not a required field in searching but it is required in creating

        public Qualification SearchQualification { get; set; }

        [DisplayName("Search For Any Type of Qualification")]
        public bool AnyQualificationTypeFlag { get; set; } // Set to true when searching through any QualificationType

        public List<Candidate> FilteredCandidates = new List<Candidate>(); // The result of the filtered candidates through the search
    }
}
