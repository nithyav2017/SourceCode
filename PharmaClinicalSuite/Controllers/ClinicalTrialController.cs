using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Packaging.Signing;
using PharmaClinicalSuite.Data;
using PharmaClinicalSuite.Models;
using PharmaClinicalSuite.Utility;

namespace PharmaClinicalSuite.Controllers
{
    public class ClinicalTrialController : Controller
    {
     //   private readonly ILogger _logger;
        private readonly PharmaClinicalSuiteContext _context;

        public ClinicalTrialController( PharmaClinicalSuiteContext context)
        {
          //  _logger = logger;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var participants = await _context.Participants.ToListAsync();
            return View(participants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Participants
            {
                MedicalHistoryOption = AddMedicalHistory(),
                GenderListOption = new List<SelectListItem>
                {
                    new SelectListItem {Text="Male", Value ="Male"},
                    new SelectListItem{Text="Female", Value="Female"},
                     new SelectListItem{Text="Other", Value="Other"}
                }
            };
            return View(model);
        }
        [HttpGet("Edit/{id}")]
        
        public async Task< IActionResult>  Edit(string? id)
        {
            if (id == null)
                return NotFound();
            ViewBag.decodeId = id;

            int? decodedId = HashIdHelper.DecodeId(id);
            if (decodedId == null) return BadRequest("Invalid ID");

            var participant = await _context.Participants.FindAsync(decodedId);

            if (participant == null)
                return NotFound();

            else
            {
                var model = new Participants
                {
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    DateOfBirth = participant.DateOfBirth,
                    Email = participant.Email,
                    Phone = participant.Phone,
                    Gender = GetGenderType(Convert.ToChar(participant.Gender)),
                    GenderListOption = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "Male", Text = "Male" },
                        new SelectListItem { Value = "Female", Text = "Female" },
                        new SelectListItem { Value = "Other", Text = "Other" }
                    },
                    Address1 = participant.Address1,
                    City = participant.City,
                    MedicalHistory = participant.MedicalHistory,
                    MedicalHistoryOption = AddMedicalHistory(),
                    Allergies = participant.Allergies,
                    BMI = participant.BMI,
                    GuardianInfo = participant.GuardianInfo
                };
                return View(model);
            }
             
        }

        [HttpPost("Edit/{id}")]
        
        public async Task<IActionResult> Edit(string id, Participants model)
        {
            int? Id = HashIdHelper.DecodeId(id);
            if (ModelState.IsValid)
            {
                model.ParticipantId =(int) Id!;
                model.Gender = model.Gender.Substring(0, 1);
                _context.Update(model);
                _context.SaveChanges();
                return  RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "");
                return View();
            }
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Participants participant)
        {
            
            if (ModelState.IsValid)
            {
                
 
                    _context.Participants.Add(new Participants
                    {
                        FirstName = participant.FirstName,
                        LastName = participant.LastName,
                        DateOfBirth = participant.DateOfBirth,
                        Email = participant.Email,
                        Phone = participant.Phone,
                        Gender = participant.Gender.Substring(0, 1), //Gender accepts only single char 
                        Address1 = participant.Address1,
                        City = participant.City,
                        MedicalHistory = participant.MedicalHistory,
                        Allergies = participant.Allergies,
                        BMI = participant.BMI,
                        GuardianInfo = participant.GuardianInfo
                    });
                 
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                  ModelState.AddModelError("", "");
                  return View();

            }
        }

        private string GetGenderType(char gender)
        {
            switch (gender)
            {
                case 'M': return "Male";
                case 'F': return "Female";
                default: return "Other";
            };
            
        }
       private List<SelectListItem> AddMedicalHistory()
        {
            var list = new List<SelectListItem>() {
                        new SelectListItem { Text = "No Known Medical Problems", Value = "No Known Medical Problems" },
                        new SelectListItem { Text = "Hypertension(High Blood Pressure)", Value = "Hypertension(High Blood Pressure)" },
                        new SelectListItem { Text = "Diabetes Mellitus", Value = "Diabetes Mellitus" },
                        new SelectListItem { Text = "Hyperlipidemia(High Cholesterol)", Value = "Hyperlipidemia(High Cholesterol)" },
                        new SelectListItem { Text = "Coronary Artery Disease", Value = "Coronary Artery Disease" },
                        new SelectListItem { Text = "Myocardial Infarction(Heart Attack)", Value = "Myocardial Infarction(Heart Attack)" },
                        new SelectListItem { Text = "Congestive Heart Failure", Value = "Congestive Heart Failure" },
                        new SelectListItem { Text = "Atrial Fibrillation", Value = "Atrial Fibrillation" },
                        new SelectListItem { Text = "Asthma", Value = "Asthma" },
                        new SelectListItem { Text = "Chronic Obstructive Pulmonary Disease(COPD)", Value = "Chronic Obstructive Pulmonary Disease(COPD)" },
                        new SelectListItem { Text = "Stroke", Value = "Stroke" },
                        new SelectListItem { Text = "Transient Ischemic Attack(TIA)", Value = "Transient Ischemic Attack(TIA)" },
                        new SelectListItem { Text = "Thyroid Disorders", Value = "Thyroid Disorders" },
                        new SelectListItem { Text = "Cancer", Value = "Cancer" },
                        new SelectListItem { Text = "Kidney Disease", Value = "Kidney Disease" },
                        new SelectListItem { Text = "Liver Disease", Value = "Liver Disease" },
                        new SelectListItem { Text = "Peptic Ulcer Disease", Value = "Peptic Ulcer Disease" },
                        new SelectListItem { Text = "Gastroesophageal Reflux Disease(GERD)", Value = "Gastroesophageal Reflux Disease(GERD)" },
                        new SelectListItem { Text = "Depression", Value = "Depression" },
                        new SelectListItem { Text = "Anxiety", Value = "Anxiety" },
                        new SelectListItem { Text = "Osteoarthritis", Value = "Osteoarthritis" },
                        new SelectListItem { Text = "Rheumatoid Arthritis", Value = "Rheumatoid Arthritis" },
                        new SelectListItem { Text = "Osteoporosis", Value = "Osteoporosis" },
                        new SelectListItem { Text = "Anemia", Value = "Anemia" },
                        new SelectListItem { Text = "Bleeding Disorders", Value = "Bleeding Disorders" },
                        new SelectListItem { Text = "Seizure Disorders", Value = "Seizure Disorders" },
                        new SelectListItem { Text = "HIV / AIDS", Value = "HIV / AIDS" },
                        new SelectListItem { Text = "Tuberculosis", Value = "Tuberculosis" },
                        new SelectListItem { Text = "Sleep Apnea", Value = "Sleep Apnea" },
                        new SelectListItem { Text = "Substance Abuse", Value = "Substance Abuse" },
                        new SelectListItem { Text = "Other(please specify)", Value = "Other(please specify)" },
                    };
                    return list;
       }
    }
}
