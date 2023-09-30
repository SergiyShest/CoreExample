using System;

namespace CookieReaders.Models
{
    public class UserItem
    {
        public int? UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}