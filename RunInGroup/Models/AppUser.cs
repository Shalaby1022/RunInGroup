using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunInGroup.Models
{
    public class AppUser : IdentityUser
    {

     
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? ProfileImageUrl { get; set; }

        
        //public IFormFile Image { get; set; }
        //navigation property
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
    }

}
