using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpServices.REGISTRATIONS.ABSTRACTION
{
    public interface IRegistrationQueries
    {
        // 1. شاشة صاحب المشروع: تجلب كل الطلبات لمشروع معين
        Task<IEnumerable<RegistrationReturnDto>> GetRegistrationsByProjectIdAsync(int projectId);

        // 2. شاشة الطالب: تجلب كل الطلبات التي قدمها هو
        Task<IEnumerable<RegistrationReturnDto>> GetRegistrationsByUserIdAsync(int userId);
    }
}
