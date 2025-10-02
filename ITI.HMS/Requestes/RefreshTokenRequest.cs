using System.ComponentModel.DataAnnotations;

namespace ITI.HMS.Requestes
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
