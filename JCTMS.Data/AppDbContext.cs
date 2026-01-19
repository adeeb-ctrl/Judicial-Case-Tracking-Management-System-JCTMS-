using JCTMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JCTMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Hearing> Hearings { get; set; }
        public DbSet<Sequence> Sequences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Case - Party Relationship (One-to-Many)
            modelBuilder.Entity<Case>()
                .HasMany(c => c.Parties)
                .WithOne(p => p.Case)
                .HasForeignKey(p => p.CaseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Case - Hearing Relationship (One-to-Many)
            modelBuilder.Entity<Case>()
                .HasMany(h => h.Hearings)
                .WithOne(h => h.Case)
                .HasForeignKey(h => h.CaseID)
                .OnDelete(DeleteBehavior.Cascade);

            // Case - Judge Relationship (Many-to-One)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Judge)
                .WithMany(j => j.AssignedCases)
                .HasForeignKey(c => c.JudgeID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
