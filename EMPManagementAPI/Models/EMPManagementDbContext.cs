using EMPManagementAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMPManagementAPI.Models
{
    public class EMPManagementDbContext : DbContext
    {
        public EMPManagementDbContext(DbContextOptions<EMPManagementDbContext> options) : base(options) { }

        public DbSet<EmployeeInformation> EmployeeInformation { get; set; }

        public DbSet<Manager> Manager { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Project> ProjectEmployee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeInformation>()
                        .HasOne(e => e.Manager)
                        .WithMany()
                        .HasForeignKey(e => e.ManagerId);

            modelBuilder.Entity<EmployeeInformation>()
                        .HasOne(e => e.Department)
                        .WithMany()
                        .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<ProjectEmployee>()
                       .HasOne(pe => pe.EmployeeInformation)
                       .WithMany()
                       .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<ProjectEmployee>()
                        .HasOne(pe => pe.Project)
                        .WithMany()
                        .HasForeignKey(pe => pe.ProjectId);
        }

    }
}
