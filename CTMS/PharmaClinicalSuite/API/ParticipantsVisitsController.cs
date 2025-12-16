using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaClinicalSuite.Data;
using PharmaClinicalSuite.Domain.Models;
using PharmaClinicalSuite.Models.Interfaces;


namespace PharmaClinicalSuite.API
{
    [Route("api/visits")]
    [ApiController]
    public class Participantsvisits : ControllerBase
    {
        private readonly IRepository<Visit> _repo;
        private readonly IRepository<Participants> _repoParticipant;


        public Participantsvisits(IRepository<Visit> repo)
        {
           this._repo = repo;
        }


        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetVistists(int participantId)
        {
            var result = await _repo.GetByConditionAsync(v => v.ParticipantId == participantId);                                             
            return Ok(result);
        }

        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetParticipant(int participantId)
        {
            var result = await _repo.GetByConditionAsync(v => v.ParticipantId == participantId);
            return Ok(result);
        }



    }
}
