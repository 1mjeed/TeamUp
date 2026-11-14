using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TeamUpCore.Model;

namespace TeamUpData
{
    public class TeamUpDbContext : DbContext
    {
        #region Constructor

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamUpDbContext(DbContextOptions<TeamUpDbContext> options , IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region DbSets 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            #endregion

            #region Roles

            modelBuilder.Entity<Role>().HasData
                (
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "Student"
                }
                );

            #endregion

        }
        #endregion

        #region SaveChangesAsync

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //Request Cancellation Propagation
        {
            #region  currentUserId 
            int currentUserId = 0;
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userIdClaim))
            {
                int.TryParse(userIdClaim, out currentUserId);
            }
            #endregion


            var entries = ChangeTracker.Entries<GlobalEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CREATEDAT = DateTime.UtcNow;
                        entry.Entity.ISDELETED = false;
                         entry.Entity.CREATEDBY = currentUserId; 
                        break;

                    case EntityState.Modified:
                        entry.Entity.UPDATEDAT = DateTime.UtcNow;
                        entry.Entity.UPDATEDBY = currentUserId;
                        entry.Entity.DELETEDBY = currentUserId;

                        entry.Property(x => x.CREATEDAT).IsModified = false;
                        entry.Property(x => x.CREATEDBY).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.ISDELETED = true; 
                        entry.Entity.DELETEDAT = DateTime.UtcNow;

                        entry.Property(x => x.CREATEDAT).IsModified = false;
                        entry.Property(x => x.CREATEDBY).IsModified = false;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion


    }
}
