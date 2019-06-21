using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateManagementApp.DataContext;
using CandidateManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementApp.Controllers
{
    public class CandidateController : Controller
    {
        private CandidatesDBContext _context;

        public CandidateController(CandidatesDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var candidates = _context.Candidates.ToList();
            return View(candidates);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                var nextID = _context.Candidates.Count() > 0 ? _context.Candidates.Select(x => x.ID).Max() + 1 : 0;
                candidate.ID = nextID;

                if (candidate.Qualifications != null && candidate.Qualifications.Count > 0)
                {
                    foreach (var qualification in candidate.Qualifications)
                    {
                        qualification.ID = candidate.ID;
                    }
                    // Created a separate DbSet for Qualifications because 
                    // the in-memory DB was not retrieving the qualifications from each individual candidate
                    _context.Qualifications.AddRange(candidate.Qualifications);
                }

                _context.Candidates.Add(candidate);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int ID)
        {
            _context.Candidates.Remove(_context.Candidates.Find(ID));
            _context.Qualifications.RemoveRange(_context.Qualifications.Where(x => x.ID == ID));
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            var candidate = _context.Candidates.Find(ID);
            var qualifications = _context.Qualifications.Where(x => x.ID == ID).ToList();

            if (qualifications.Count > 0)
            {
                candidate.Qualifications = new List<Qualification>(qualifications);
            }

            return View(candidate);
        }

        [HttpPost]
        public IActionResult Edit(Candidate candidate)
        {
            _context.Candidates.Update(candidate);

            if (candidate.Qualifications != null && candidate.Qualifications.Count > 0)
            {
                var qualifications = _context.Qualifications.Where(x => x.ID == candidate.ID).ToList();
                _context.Qualifications.RemoveRange(qualifications);

                foreach (var qualification in candidate.Qualifications)
                {
                    qualification.ID = candidate.ID;
                }
                _context.Qualifications.AddRange(qualifications);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(CandidateSearch candidate)
        {
            bool foundQualifications = false;
            var candidates = from m in _context.Candidates select m;
            var qualifications = from f in _context.Qualifications select f;

            // Search allows for use of any of the fields in the form, including 0 or 1 qualification
            if (!string.IsNullOrEmpty(candidate.FirstName))
            {
                candidates = candidates.Where(x => x.FirstName == candidate.FirstName);
            }
            if (!string.IsNullOrEmpty(candidate.LastName))
            {
                candidates = candidates.Where(x => x.LastName == candidate.LastName);
            }
            if (!string.IsNullOrEmpty(candidate.EmailAddress))
            {
                candidates = candidates.Where(x => x.EmailAddress == candidate.EmailAddress);
            }
            if (!string.IsNullOrEmpty(candidate.PhoneNumber))
            {
                candidates = candidates.Where(x => x.PhoneNumber == candidate.PhoneNumber);
            }
            if (candidate.ZipCode != null)
            {
                candidates = candidates.Where(x => x.ZipCode == candidate.ZipCode);
            }

            // Searching through qualifications table
            if (!string.IsNullOrEmpty(candidate.SearchQualification.Name) || (candidate.SearchQualification.DateStarted != null) 
                || candidate.SearchQualification.DateCompleted != null || !string.IsNullOrEmpty(candidate.SearchQualification.QualificationType.ToString()))
            {
                if (!string.IsNullOrEmpty(candidate.SearchQualification.Name))
                {
                    qualifications = qualifications.Where(x => x.Name == candidate.SearchQualification.Name);
                }
                if (candidate.SearchQualification.DateStarted != null)
                {
                    qualifications = qualifications.Where(x => x.DateStarted == candidate.SearchQualification.DateStarted);
                }
                if (candidate.SearchQualification.DateCompleted != null)
                {
                    qualifications = qualifications.Where(x => x.DateCompleted == candidate.SearchQualification.DateCompleted);
                }
                if (!candidate.AnyQualificationTypeFlag) // Search based on the specific Qualification Type
                {
                    qualifications = qualifications.Where(x => x.QualificationType == candidate.SearchQualification.QualificationType);
                }
                List<Qualification> filteredQualifications = qualifications.ToList();

                if (filteredQualifications.Count > 0)
                {
                    foreach (var qualification in filteredQualifications)
                    {
                        candidates = candidates.Where(x => x.ID == qualification.ID);
                    }
                    foundQualifications = true;
                }
            }

            List<Candidate> results = await candidates.ToListAsync();
            candidate.FilteredCandidates = results;

            return View(candidate);
        }

        [HttpGet]
        public IActionResult QualificationView()
        {
            // Used for partial view to add a new qualification
            return PartialView(new Qualification());
        }

        [HttpGet]
        public IActionResult DeleteQualification(int ID)
        {
            
            return RedirectToAction("Edit");
        }
    }
}