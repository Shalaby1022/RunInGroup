using RunInGroup.Models;

namespace RunInGroup.ViewModel
{
    public class HomeIpViewModel
    {
        public IEnumerable<Club> clubs { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
