using CityInfrastructure.Data.Model;
using CityInfrastructure.Data.Model.Common;
using CityInfrastructure.Data.Model.MunicipalTransport;
using Microsoft.EntityFrameworkCore;

namespace CityInfrastructure.Data
{
    public  class InfrastructureDbContext : DbContext
    {
        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options)
        {

        }

        public DbSet<SubwayStation> SubwayStations { get; set; }
        public DbSet<SubwayLine> SubwayLines { get; set; }
        public DbSet<SubwayStationStatus> SubwayStationStatuses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleRecord> ScheduleRecords { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteType> RouteTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubwayStation>()
                .Property(x => x.Depth)
                .HasColumnType("decimal(12,0)");

            modelBuilder.Entity<Station>()
                .HasMany(x => x.PassingRoutes)
                .WithOne();

            modelBuilder.Entity<Route>()
                .HasMany(x => x.Stations)
                .WithOne();

            modelBuilder.Entity<Route>()
                .HasOne(x => x.RouteType)
                .WithMany(x => x.Routes);
            
            modelBuilder.Entity<Schedule>()
                .HasOne(x => x.Route)
                .WithOne(x => x.Schedule)
                .HasForeignKey<Schedule>(x => x.RouteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RouteType>()
                .HasMany(x => x.Routes)
                .WithOne(x => x.RouteType);

            modelBuilder.Entity<Schedule>()
                .HasMany(x => x.ScheduleRecords)
                .WithOne(x => x.Schedule);

            modelBuilder.Entity<ScheduleRecord>()
                .HasOne(x => x.Schedule)
                .WithMany(x => x.ScheduleRecords);

            modelBuilder.Entity<SubwayStation>()
                .HasMany(x => x.Statuses)
                .WithOne(x => x.SubwayStation);

            modelBuilder.Entity<SubwayStation>()
                .HasOne(x => x.IntersectingStation).WithOne();
            modelBuilder.Entity<SubwayLine>()
                .HasMany(x => x.Stations)
                .WithOne(x => x.Line);

            modelBuilder.Entity<SubwayStationStatus>()
                .HasOne(x => x.SubwayStation)
                .WithMany(x => x.Statuses);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
