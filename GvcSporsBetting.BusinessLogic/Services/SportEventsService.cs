using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GvcSporsBetting.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace GvcSporsBetting.BusinessLogic.Services
{
    public class SportEventsService : ISportEventsService
    {
        private readonly GvcSportsBettingContext _dbContext;

        public SportEventsService(GvcSportsBettingContext dbContex) =>
            _dbContext = dbContex;

        public async Task<IEnumerable<SportEvents>> GetSportEvents() =>
            await _dbContext.SportEvents.Where(x => !x.IsDeleted).ToListAsync();

        public async Task<SportEvents> GetSportEvent(long id) =>
            await _dbContext.SportEvents.SingleOrDefaultAsync(x => x.SportEventId == id && !x.IsDeleted);

        public async Task<SportEvents> CreateDeafultSportEvent()
        {
            var currentDateTime = DateTime.UtcNow;
            var evetnDate = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 23, 59, 0);

            var sportEvent = new SportEvents() { EventStartDate = evetnDate };
            _dbContext.SportEvents.Add(sportEvent);

            await _dbContext.SaveChangesAsync();

            return sportEvent;
        }

        public async Task<(int saveResult, bool eventExists)> UpdateSportEvent(SportEvents sportEvent)
        {
            if (!SportEventExists(sportEvent.SportEventId))
                return (saveResult: 0, eventExists: false);

            var exsistingEvent = await GetSportEvent(sportEvent.SportEventId);
            exsistingEvent.OddsForFirstTeam = sportEvent.OddsForFirstTeam;
            exsistingEvent.OddsForSecondTeam = sportEvent.OddsForSecondTeam;
            exsistingEvent.OddsForDraw = sportEvent.OddsForDraw;
            exsistingEvent.EventStartDate = sportEvent.EventStartDate;
            exsistingEvent.EventName = sportEvent.EventName;

            return (saveResult: await _dbContext.SaveChangesAsync(), eventExists: true);
        }

        public async Task<long> CreateSportEvent(SportEvents sportEvent)
        {
            _dbContext.SportEvents.Add(sportEvent);
            await _dbContext.SaveChangesAsync();

            return sportEvent.SportEventId;
        }

        public async Task<(int saveResult, bool eventExists)> DeleteSportEvent(long id)
        {
            if (!SportEventExists(id))
                return (saveResult: 0, eventExists: false);

            var exsistingEvent = await GetSportEvent(id);
            exsistingEvent.IsDeleted = true;

            return (saveResult: await _dbContext.SaveChangesAsync(), eventExists: true);
        }

        private bool SportEventExists(long id) =>
            _dbContext.SportEvents.SingleOrDefault(x => x.SportEventId == id && !x.IsDeleted) != null;

    }
}
