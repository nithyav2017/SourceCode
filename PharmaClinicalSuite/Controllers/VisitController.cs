using Azure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaClinicalSuite.Application.Events.ScheduledVisit;
using PharmaClinicalSuite.Data;
using PharmaClinicalSuite.Domain.Models;
using PharmaClinicalSuite.Utility;

namespace PharmaClinicalSuite.Controllers
{
    public class VisitController : Controller
    {
        private readonly PharmaClinicalSuiteContext _dbcontext;

        private readonly IMediator _mediator;

        [BindProperty]
        public Visit NewVisit { get; set; }
        public List<Visit> Visits { get; set; }
        public VisitController(PharmaClinicalSuiteContext dbcontext, IMediator mediator)
        {
            _dbcontext = dbcontext;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVisit(string participantid)
        {   
            int? id= HashIdHelper.DecodeId(participantid);
            var visits = await _dbcontext.Visit.Where(x => x.ParticipantId ==  id).ToListAsync();

            //   NewVisit = new Visit { ParticipantId = participantid };
            ViewBag.ParticipantId = id;
            return View(visits);
        }

        /* public async Task<IActionResult> AddVisit([FromBody] Visit visit)
         {
             if (ModelState.IsValid)
             {

                 await _dbcontext.Visits.AddAsync(visit);
                 await  _dbcontext.SaveChangesAsync();                

                 return View();
             }
             else
             {
                  ModelState.AddModelError("","Something went wrong");
                  return View();
             }
         }*/

     /*   [HttpPost]
        public async Task<IActionResult> ScheduleVisit(int visitId)
        {
            if (visitId > 0)
            {
                var visit = _dbcontext.Visit.Find(visitId);
                if (visit == null)
                    return NotFound();
                return View("_VisitForm", visit);
                
            }
            return NotFound();
      //      return RedirectToAction("GetVisit", new { participantId = visit.ParticipantId });
        }*/
        [HttpPost]
        public async Task<IActionResult> ScheduleVisit([FromForm] Visit model)
        {
            /*if (visit.ParticipantId >0)
            {
                visit.Participant = await _dbcontext.Participants.FirstOrDefaultAsync(x => x.ParticipantId == visit.ParticipantId);
                if (ModelState.IsValid)
                {
                    _dbcontext.Visit.Add(visit);
                    await _dbcontext.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("", "Error occurd");
                    return View("_VisitForm");
                }
            }*/

            var command = new SchduleVisitCommand
            {
                ParticipantId = model.ParticipantId,
                SchduleDate = model.ScheduledDate,
                VisitType = model.VisitType,
                Notes=model.Notes               

            };
            var result =await  _mediator.Send(command);

            if(!result.IsSucess)
            {
                ModelState.AddModelError("", "");
                return View(model);
            }

            return RedirectToAction("GetVisit", new { participantId = model.ParticipantId });
        }

        [HttpPost]
        public async Task<IActionResult> LogVisit(int visitId, DateTime actualVisitDate)
        {
            var visit = await _dbcontext.Visit.FindAsync(visitId);
            if (visit != null)
            {
                visit.ActualVisitDate = actualVisitDate;
                visit.Status = "Completed";
                await _dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("GetVisit", new { participantId = visit.ParticipantId });
        }

    }
}
