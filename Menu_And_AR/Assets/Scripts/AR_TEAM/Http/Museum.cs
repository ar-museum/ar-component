using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.Http {
    public class Museum {
        public int MuseumId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoPath { get; set; }
        public List<Exposition> Expositions { get; set; }
    }
}
