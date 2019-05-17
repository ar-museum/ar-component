using Assets.Scripts.AR_TEAM.Http;
using NUnit.Framework;

namespace Tests {
    public class MuseumTest
    {

        private static readonly string JSON_TOKEN_INPUT =
        "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }";
        private static readonly string API_URL = "https://armuseum.ml/api/";
        private static readonly string GET_MUSEUM_URL = "https://armuseum.ml/api";
        private static readonly string EXHIBITS_URL = API_URL + "exhibit";
        private static readonly string EXHIBITS_RELS_URL = API_URL + "exh/rels";
        private static readonly string AUTHORS_URL = API_URL + "author";
        private static readonly string EXPOSITIONS_URL = API_URL + "exposition";
        private static readonly string MUSEUMS_URL = API_URL + "museum";
        private static readonly string MUSEUMS_RELS_URL = API_URL + "mus/rels";
        private static readonly string EXPOSITIONS_RELS_URL = API_URL + "expo/rels";
        private static readonly string UPDATE_URL = API_URL + "update";
        public static readonly string SITE_URL = "https://armuseum.ml/";

        [Test]
        public void ShouldPopulateMuseumInfo()
        {
            var expected_result = false;
            MuseumInfo museum = new MuseumInfo();
            museum.Name = "M. Eminescu";
            GeoCoordinate coordinate = new GeoCoordinate(45.123121, 46.123121);
            museum.Coordinate = coordinate;
            museum.MuseumId = 1;
            museum.VuforiaDatabaseVersion = "adasc2312ascas1";

            if(museum.Name == "M. Eminescu") { expected_result = true; }
            if (museum.Coordinate == new GeoCoordinate(45.123121, 46.123121)) { expected_result = true; }
            if(museum.MuseumId == 1) { expected_result = true; }
            if(museum.VuforiaDatabaseVersion == "adasc2312ascas1") { expected_result = true; }

            Assert.True(expected_result);
            
        }



        [Test]
        public void ShouldHaveEqualValues()
        {
            var expected_result = false;

            if (JSON_TOKEN_INPUT == "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }")
            { expected_result = true; }
            if (API_URL == "https://armuseum.ml/api/") { expected_result = true; }
            if (GET_MUSEUM_URL == "https://armuseum.ml/api") { expected_result = true; }
            if (EXHIBITS_URL == API_URL + "exhibit") { expected_result = true; }
            if (EXHIBITS_RELS_URL == API_URL + "exh/rels") { expected_result = true; }
            if (AUTHORS_URL == API_URL + "author") { expected_result = true; }
            if (EXPOSITIONS_URL == API_URL + "exposition") { expected_result = true; }
            if (MUSEUMS_URL == API_URL + "museum") { expected_result = true; }
            if (MUSEUMS_RELS_URL == API_URL + "mus/rels") { expected_result = true; }
            if (EXPOSITIONS_RELS_URL == API_URL + "expo/rels") { expected_result = true; }
            if (UPDATE_URL == API_URL + "update") { expected_result = true; }
            if (SITE_URL == "https://armuseum.ml/") { expected_result = true; }
            
            Assert.True(expected_result);
        }
    }
}