using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUpServices.Authorization.DTOS
{   
        public class TokenResponseDto
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
        }
    
}
