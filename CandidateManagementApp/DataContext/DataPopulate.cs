using CandidateManagementApp.DataContext;
using CandidateManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.DataContext
{
    public static class DataPopulate
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CandidatesDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<CandidatesDBContext>>()))
            {
                // Look for any candidates already in database.
                if (context.Candidates.Any())
                {
                    return;   // Database has been seeded
                }

                context.Candidates.AddRange(
                    new Candidate
                    {
                        ID = 1,
                        FirstName = "Jacob",
                        LastName = "Marshall",
                        EmailAddress = "jmarshall@yahoo.com",
                        PhoneNumber = "281-455-1524",
                        ZipCode = 77006,
                        Qualifications = new List<Qualification>()
                        {
                            new Qualification
                            {
                                ID = 1, QualificationType = QualificationType.CollegeDegree, Name = "Test University", DateStarted = new DateTime(2012, 12, 12), DateCompleted = new DateTime(2014, 10, 10)
                            }
                        }
                    },
                    new Candidate
                    {
                        ID = 2,
                        FirstName = "Ashley",
                        LastName = "Johannson",
                        EmailAddress = "ashjohannson19@gmail.com",
                        PhoneNumber = "832-559-6256",
                        ZipCode = 77008
                    },
                    new Candidate
                    {
                        ID = 3,
                        FirstName = "Erica",
                        LastName = "Rodriguez",
                        EmailAddress = "ericarodriguez@hotmail.com",
                        PhoneNumber = "281-205-7556",
                        ZipCode = 77027
                    },
                    new Candidate
                    {
                        ID = 4,
                        FirstName = "Marcus",
                        LastName = "Ashford",
                        EmailAddress = "mash91@mywebsite.com",
                        PhoneNumber = "281-330-8004",
                        ZipCode = 77077
                    },
                    new Candidate
                    {
                        ID = 5,
                        FirstName = "Raj",
                        LastName = "Patel",
                        EmailAddress = "rappy@gmail.com",
                        PhoneNumber = "713-559-2699",
                        ZipCode = 77057
                    },
                    new Candidate
                    {
                        ID = 6,
                        FirstName = "Eric",
                        LastName = "Ngo",
                        EmailAddress = "angogo@gmail.com",
                        PhoneNumber = "281-766-2121",
                        ZipCode = 77339
                    }) ;

                context.SaveChanges();
            }
        }
    }
}
