using Microsoft.EntityFrameworkCore;
using TeamUpCore;
using TeamUpCore.Model;

namespace TeamUpData
{
    public class TeamUpDbContext : DbContext
    {
        #region Constructor
        public TeamUpDbContext(DbContextOptions<TeamUpDbContext> options) : base(options) { }
        #endregion

        #region DbSets 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
