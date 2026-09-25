namespace TeamUpServices.USERS.DTOS
{
    public class UserReturnDto
    {

        /// <summary>
        /// User ID ||  بدي الايدي عشان الفرونت يميز 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// User Bio / Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Profile Image URL
        /// </summary>
        public string ImgUrl { get; set; }
    }
}
