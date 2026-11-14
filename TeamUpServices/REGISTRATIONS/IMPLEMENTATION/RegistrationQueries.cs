
using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.REGISTRATIONS.ABSTRACTION;
using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpServices.REGISTRATIONS.IMPLEMENTATION
{
    public class RegistrationQueries : IRegistrationQueries
    {
        private readonly TeamUpDbContext _context;

        public RegistrationQueries(TeamUpDbContext context)
        {
            _context = context;
        }

        // 1. استعلام شاشة صاحب المشروع (يجلب كل الطلاب المتقدمين لمشروعه)
        public async Task<IEnumerable<RegistrationReturnDto>> GetRegistrationsByProjectIdAsync(int projectId)
        {
            if (projectId <= 0) return Enumerable.Empty<RegistrationReturnDto>();

            return await _context.Registrations
                .AsNoTracking() 
                .Include(r => r.User) 
                .Include(r => r.Project) 
                .Where(r => r.ProjectID == projectId && !r.ISDELETED) 
                .Select(r => new RegistrationReturnDto
                {
                    Id = r.Id,
                    Status = r.Status,
                    ProjectId = r.ProjectID,
                    ProjectName = r.Project.Title, 
                    ApplicantId = r.UserID,
                    ApplicantName = r.User.Name,  
                    AppliedAt = r.CREATEDAT
                })
                .ToListAsync();
        }

        // 2. استعلام شاشة الطالب (يجلب كل المشاريع التي قدم عليها)
        public async Task<IEnumerable<RegistrationReturnDto>> GetRegistrationsByUserIdAsync(int userId)
        {
            if (userId <= 0) return Enumerable.Empty<RegistrationReturnDto>();

            return await _context.Registrations
                .AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Project)
                .Where(r => r.UserID == userId && !r.ISDELETED)
                .Select(r => new RegistrationReturnDto
                {
                    Id = r.Id,
                    Status = r.Status,
                    ProjectId = r.ProjectID,
                    ProjectName = r.Project.Title,
                    ApplicantId = r.UserID,
                    ApplicantName = r.User.Name,
                    AppliedAt = r.CREATEDAT
                })
                .ToListAsync();
        }
    }
}
