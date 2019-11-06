using System.Collections.Generic;
using System.Threading.Tasks;
using GvcSporsBetting.DataAccess.Model;

namespace GvcSporsBetting.BusinessLogic.Services
{
    public interface ISportEventsService
    {
         Task<IEnumerable<SportEvents>> GetSportEvents();
         Task<SportEvents> GetSportEvent(long id);
         Task<(int saveResult, bool eventExists)> UpdateSportEvent(SportEvents sportEvent);
         Task<long> CreateSportEvent(SportEvents sportEvent);
         Task<SportEvents> CreateDeafultSportEvent();
         Task<(int saveResult, bool eventExists)> DeleteSportEvent(long id);
    }
}
