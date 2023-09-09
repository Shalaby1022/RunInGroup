using RunInGroup.Data.Enums;
using RunInGroup.Models;

namespace RunInGroup.ViewModel
{
    public class RaceEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public String URL { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
