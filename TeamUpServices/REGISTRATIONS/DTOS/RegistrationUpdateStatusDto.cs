namespace TeamUpServices.REGISTRATIONS.DTOS
{
    public class RegistrationUpdateStatusDto
    {
        public int RegistrationId { get; set; }
        public int Status { get; set; }
        // الحالات 
        //1 request
        //2 retreat
        //3 accept
        //4 reject
    }
}
