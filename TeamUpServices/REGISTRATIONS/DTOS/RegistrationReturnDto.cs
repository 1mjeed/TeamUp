namespace TeamUpServices.REGISTRATIONS.DTOS
{
    public class RegistrationReturnDto
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }


        public int ApplicantId { get; set; }

        public string ApplicantName { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
