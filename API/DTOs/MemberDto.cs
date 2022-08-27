using System;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        // Automapper will automatically figure out the age based on the GetAge method in the AppUser entity
        // Essentially, if automapper comes across a method with the name "Get" its gonna run the run inside the method to populate the name of the other side of "Get"
        public int Age { get; set; }
        // Nickname
        public string KnownAs { get; set; }
        // When the user was created
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        // Introductary text
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; } 
        // One to many relationship between our AppUser and our photo entity.
        public ICollection<PhotoDto> Photos { get; set; }
        
    }
}