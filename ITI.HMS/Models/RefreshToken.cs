using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.HMS.Models
{
    [Owned]
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokenOn { get; set; }
        public bool IsActive => ExpiresOn > DateTime.UtcNow && RevokenOn == null;
    }
}
