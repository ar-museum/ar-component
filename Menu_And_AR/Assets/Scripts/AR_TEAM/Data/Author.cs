using System;

namespace Assets.Scripts.AR_TEAM.Http {
    public class Author {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public int BornYear { get; set; }
        public int DiedYear { get; set; }
        public string Location { get; set; }
        public int PhotoId { get; set; }
        public int StaffId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PhotoPath { get; set; }
        public string PhotoPathOnDisk { get; set; }
    }
}
