using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.Http {
    public class MuseumInfo {
        public string Name { get; set; }
        public GeoCoordinate Coordinate { get; set; }
        public int MuseumId { get; set; }
        public string VuforiaDatabaseVersion { get; set; }
        public List<string> VuforiaFiles { get; set; }
    }
}
