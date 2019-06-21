using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.Models
{
    public enum QualificationType
    {

        None,
        [Display(Name = "Personal Certification")]
        ProfessionalCertification,
        [Display(Name = "Work Experience")]
        WorkExperience,
        [Display(Name = "College Degree")]
        CollegeDegree
    }
}
