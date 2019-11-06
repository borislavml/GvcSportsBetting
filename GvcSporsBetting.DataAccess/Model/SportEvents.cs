using System;

namespace GvcSporsBetting.DataAccess.Model
{
    public partial class SportEvents
    {
        public long SportEventId { get; set; }
        public string EventName { get; set; }
        public decimal? OddsForFirstTeam { get; set; }
        public decimal? OddsForDraw { get; set; }
        public decimal? OddsForSecondTeam { get; set; }
        public DateTime EventStartDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
