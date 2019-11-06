using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GvcSporsBetting.DataAccess.Model;
using GvcSporsBetting.BusinessLogic.Services;

namespace GvcSporsBetting.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SportEventsController : Controller
    {
        private readonly ISportEventsService _sportEventsService;

        public SportEventsController(ISportEventsService service) =>
            _sportEventsService = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportEvents>>> GetSportEvents() =>
            Ok(await _sportEventsService.GetSportEvents());

        [HttpGet("{id}")]
        public async Task<ActionResult<SportEvents>> GetSportEvent(long id)
        {
            var sportEvent = await _sportEventsService.GetSportEvent(id);
            return sportEvent != null 
                ? (ActionResult)Ok(sportEvent) 
                : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateSportEvent(long id, SportEvents sportEvents)
        {
            if (id != sportEvents.SportEventId)
                return BadRequest();

            var updateResult = await _sportEventsService.UpdateSportEvent(sportEvents);         
            
            if (!updateResult.eventExists)
                return NotFound();

            return Ok(updateResult.saveResult == 1);
        }

        [HttpPost]
        public async Task<ActionResult<SportEvents>> CreateSportEvent(SportEvents sportEvent)
        {
            var spoertEventId = await _sportEventsService.CreateSportEvent(sportEvent);

            return CreatedAtAction(nameof(GetSportEvent), new { id = spoertEventId }, sportEvent);
        }


        [HttpPost]
        public async Task<ActionResult<SportEvents>> CreateDeafultSportEvent()
        {
            var sportEvent = await _sportEventsService.CreateDeafultSportEvent();

            return CreatedAtAction(nameof(GetSportEvent), new { id = sportEvent.SportEventId }, sportEvent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteSportEvent(long id)
        {
            var deleteResult = await _sportEventsService.DeleteSportEvent(id);
            if (!deleteResult.eventExists)
                return NotFound();

            return Ok(deleteResult.saveResult == 1);
        }
    }
}
