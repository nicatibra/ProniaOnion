using Microsoft.AspNetCore.Identity;

namespace ProniaOnion.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        //Profil bloklanıb mı?
        public bool IsActive { get; set; }

        public AppUser()
        {
            IsActive = true;
        }
    }
}
