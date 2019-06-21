using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.Models
{
    public class Qualification
    {
        public int ID { get; set; } // Foreign key for Candidate

        [DisplayName("Qualification Type")]
        public QualificationType? QualificationType { get; set; }

        public string Name { get; set; }

        [DisplayName("Date Started"), DataType(DataType.Date)]
        public DateTime? DateStarted { get; set; }

        [DisplayName("Date Completed (optional)"), DataType(DataType.Date)]
        public DateTime? DateCompleted { get; set; }

        public List<QualificationType> RemoveNone = new List<QualificationType>{Models.QualificationType.None};

    }
}