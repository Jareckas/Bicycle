using Microsoft.AspNetCore.Identity;

namespace dtWebApi.Models
{
    public class Settings
    {
        // Parameterless constructor
        public Settings()
        {
        }

        public string UserId { get; set; }
        public string UserName { get; set; }

        // Navigation property for the user
        public IdentityUser User { get; set; }

    }
}
