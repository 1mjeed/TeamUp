using TeamUpCore.Model;
using TeamUpServices.REGISTRATIONS.DTOS;

namespace TeamUpServices.REGISTRATIONS.MAPPINGDTOs
{
    public class RegistrationMapping
    {
        #region Registration CreateDto
        public static Registration MAPPINGDTO(RegistrationCreateDto createDto)
        {
            return new Registration
            {
                ProjectID = createDto.ProjectId,
                UserID = createDto.UserId,
                Status = 0
            };
        }
        #endregion



    }
}