using Microsoft.EntityFrameworkCore;
using TeamUpCore.Model;
using TeamUpData;
using TeamUpServices.REGISTRATIONS.ABSTRACTION;
using TeamUpServices.REGISTRATIONS.DTOS;
using TeamUpServices.REGISTRATIONS.MAPPINGDTOs;

namespace TeamUpServices.REGISTRATIONS.IMPLEMENTATION
{
    public class RegistrationCommand : IRegistrationCommand
    {
        private readonly TeamUpDbContext _context;

        public RegistrationCommand(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRegistrationAsync(RegistrationCreateDto createDto)
        {
            if (createDto == null) return false;

            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == createDto.ProjectId);

            if (project == null) return false; 

            var userExists = await _context.Users
                .AnyAsync(u => u.Id == createDto.UserId);

            if (!userExists) return false;

            var isDuplicate = await _context.Registrations
                .AnyAsync(r => r.ProjectID == createDto.ProjectId && r.UserID == createDto.UserId);

            if (isDuplicate) return false; 

            var approvedMembersCount = await _context.Registrations
                .CountAsync(r => r.ProjectID == createDto.ProjectId && r.Status == 1);

            if (approvedMembersCount >= project.CapaCity)
                return false; 

            Registration reg = RegistrationMapping.MAPPINGDTO(createDto);

            await _context.Registrations.AddAsync(reg);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateRegistrationStatusAsync(RegistrationUpdateStatusDto updateDto)
        {
            if(updateDto == null) return false;

            var registration = await _context.Registrations.FindAsync(updateDto.RegistrationId);
            if (registration == null) return false;

             registration.Status = updateDto.Status;

            _context.Registrations.Update(registration);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }
    }
}
