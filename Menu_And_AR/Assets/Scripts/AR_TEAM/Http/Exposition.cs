﻿using System;

namespace Assets.Scripts.AR_TEAM.Http {
    class Exposition {
        public int ExpositionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MuseumId { get; set; }
        public int StaffId { get; set; }
        public int PhotoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PhotoPath { get; set; }
    }
}
