using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagementApp.DataContext
{
    public class CandidatesDBContext : DbContext
    {
        public CandidatesDBContext(DbContextOptions<CandidatesDBContext> options) : base(options)
        {
        }

        public DbSet<Models.Candidate> Candidates { get; set; }
        public DbSet<Models.Qualification> Qualifications { get; set; }
    }
}
