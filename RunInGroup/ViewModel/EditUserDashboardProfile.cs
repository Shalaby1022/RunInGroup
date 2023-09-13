using RunInGroup.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunInGroup.ViewModel
{
    public class EditUserDashboardProfile
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        //navigation property
        public Address? Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }

    }
}
