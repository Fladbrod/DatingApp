using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    //Gives the photo entity the name photos when it enters the database
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        // Main photo/profile photo
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        // In the below two properties, we are fully defining the relationship between our app user -
        // and our photo in order to not risk orphaned photos when a user is deleted.
        // If we didn't apply this, the AppUserId would be allowed to be nullable, which mean we could have a photo without an AppUser.
        // This also sets the onDelete: ReferentialAction.Restrict; in our migrations to be Cascade type.
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}