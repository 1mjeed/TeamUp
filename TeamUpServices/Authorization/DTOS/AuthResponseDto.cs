namespace TeamUpServices.Authorization.DTOS
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
// هذا الكلاس هو الرد من السيرفر للعميل  بعد التحقق من المعلومات 