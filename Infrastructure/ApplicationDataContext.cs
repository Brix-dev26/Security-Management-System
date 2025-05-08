using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        // DbSets for each entity
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Permit> Permits { get; set; }
        public DbSet<SecurityStaff> SecurityStaffs { get; set; }
        public DbSet<EmergencyEvent> EmergencyEvents { get; set; }
        public DbSet<InvolvedParty> InvolvedParties { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<StaffManager> StaffManagers { get; set; }
        public DbSet<StaffManagerCampus> StaffManagerCampuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define primary keys
            modelBuilder.Entity<Visitor>().HasKey(v => v.VisitorId);
            modelBuilder.Entity<Vehicle>().HasKey(veh => veh.VehicleId);
            modelBuilder.Entity<Permit>().HasKey(p => p.PermitId);
            modelBuilder.Entity<SecurityStaff>().HasKey(s => s.Sec_ID);
            modelBuilder.Entity<EmergencyEvent>().HasKey(e => e.Emerg_ID);
            modelBuilder.Entity<InvolvedParty>().HasKey(ip => ip.InvolvedPartyId);
            modelBuilder.Entity<LogEntry>().HasKey(l => l.LogId);
            modelBuilder.Entity<Gate>().HasKey(g => g.GateId);
            modelBuilder.Entity<Campus>().HasKey(c => c.CampusId);

            // Define relationships

            // Visitor may own multiple Vehicles
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Visitor)
                .WithMany(vis => vis.Vehicles)
                .HasForeignKey(v => v.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Visitor can receive multiple Permits
            modelBuilder.Entity<Permit>()
                .HasOne(p => p.Visitor)
                .WithMany(v => v.Permits)
                .HasForeignKey(p => p.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Security Staff grants Permits
            modelBuilder.Entity<Permit>()
                .HasOne(p => p.SecurityStaff)
                .WithMany(s => s.PermitsGranted)
                .HasForeignKey(p => p.Sec_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // Security Staff is involved in Emergency Events
            modelBuilder.Entity<EmergencyEvent>()
                .HasOne(e => e.SecurityStaff)
                .WithMany(s => s.EmergencyEvents)
                .HasForeignKey(e => e.Sec_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Emergency Event involves multiple Security Staff via InvolvedParty
            modelBuilder.Entity<InvolvedParty>()
                .HasOne(ip => ip.EmergencyEvent)
                .WithMany(ee => ee.InvolvedParties)
                .HasForeignKey(ip => ip.Emerg_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvolvedParty>()
                .HasOne(ip => ip.SecurityStaff)
                .WithMany(s => s.InvolvedParties)
                .HasForeignKey(ip => ip.Sec_ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmergencyEvent>()
                .HasOne(e => e.Campus)
                .WithMany()
                .HasForeignKey(e => e.CampusId)
                .OnDelete(DeleteBehavior.NoAction);

            // LogEntry - Records each visit with a Vehicle and Security Staff
            modelBuilder.Entity<LogEntry>()
                .HasOne(l => l.Visitor)
                .WithMany()
                .HasForeignKey(l => l.VisitorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LogEntry>()
                .HasOne(l => l.Vehicle)
                .WithMany()
                .HasForeignKey(l => l.VehicleId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<LogEntry>()
                .HasOne(l => l.SecurityStaff)
                .WithMany()
                .HasForeignKey(l => l.Sec_ID)
                .OnDelete(DeleteBehavior.NoAction);


            // LogEntry references Gate
            modelBuilder.Entity<LogEntry>()
                .HasOne(l => l.Gate)
                .WithMany()
                .HasForeignKey(l => l.GateId)
                .OnDelete(DeleteBehavior.NoAction);

            // LogEntry references Campus
            modelBuilder.Entity<LogEntry>()
                .HasOne(l => l.Campus)
                .WithMany()
                .HasForeignKey(l => l.CampusId)
                .OnDelete(DeleteBehavior.NoAction);

            // Security Staff belongs to a Gate
            modelBuilder.Entity<SecurityStaff>()
                .HasOne(s => s.Gate)
                .WithMany()
                .HasForeignKey(s => s.GateId)
                .OnDelete(DeleteBehavior.NoAction);

            // Security Staff belongs to a Campus
            modelBuilder.Entity<SecurityStaff>()
                .HasOne(s => s.Campus)
                .WithMany()
                .HasForeignKey(s => s.CampusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Campus>()
             .HasMany(c => c.Gates)
            .WithOne(g => g.Campus)
             .HasForeignKey(g => g.CampusId);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StaffManagerCampus>()
                .HasKey(sm => new { sm.StaffManagerId, sm.CampusId });

            modelBuilder.Entity<StaffManagerCampus>()
                .HasOne(sm => sm.StaffManager)
                .WithMany(s => s.StaffManagerCampuses)
                .HasForeignKey(sm => sm.StaffManagerId);

            modelBuilder.Entity<StaffManagerCampus>()
                .HasOne(sm => sm.Campus)
                .WithMany()
                .HasForeignKey(sm => sm.CampusId);

        }
    }
}