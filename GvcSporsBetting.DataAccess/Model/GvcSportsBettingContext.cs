using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GvcSporsBetting.DataAccess.Model
{
    public partial class GvcSportsBettingContext : DbContext
    {
        public GvcSportsBettingContext()
        {}

        public GvcSportsBettingContext(DbContextOptions<GvcSportsBettingContext> options)
            : base(options)
        {}

        public virtual DbSet<SportEvents> SportEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["GvcSportsBettingContext"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<SportEvents>(entity =>
            {
                entity.HasKey(e => e.SportEventId)
                    .HasName("PK_SportEvents_EventID");

                entity.Property(e => e.SportEventId).HasColumnName("SportEventID");

                entity.Property(e => e.EventName)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.EventStartDate).HasColumnType("datetime");

                entity.Property(e => e.OddsForDraw).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.OddsForFirstTeam).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.OddsForSecondTeam).HasColumnType("decimal(5, 2)");
            });
        }
    }
}
