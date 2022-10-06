using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        // Nickname
        public string KnownAs { get; set; }
        // When the user was created
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        // Introductary text
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; } 
        // One to many relationship between our AppUser and our photo entity.
        public ICollection<Photo> Photos { get; set; }
        // List of user Who liked current logged in user
        public ICollection<UserLike> LikedByUsers{ get; set; }
        // List of users the currently logged in user has liked
        public ICollection<UserLike> LikedUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesRecieved { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}