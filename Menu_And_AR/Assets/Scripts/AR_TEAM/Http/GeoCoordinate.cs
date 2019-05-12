namespace Assets.Scripts.AR_TEAM.Http {
    public class GeoCoordinate {
        public double Latitude { get; }

        public double Longitude { get; }

        public GeoCoordinate(double latitude, double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
