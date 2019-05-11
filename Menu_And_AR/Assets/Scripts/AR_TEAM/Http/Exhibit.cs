using System;

namespace Assets.Scripts.AR_TEAM.Http {
    public class Exhibit {
        public int ExhibitId { get; set; }
        public int ExpositionId { get; set; }
        public int StaffId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Size { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PhotoUrl { get; set; }
        public Author Author { get; set; }
    }
}
