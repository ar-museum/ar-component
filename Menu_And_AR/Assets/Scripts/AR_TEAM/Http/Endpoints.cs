namespace Assets.Scripts.AR_TEAM.Http {
    public static class Endpoints {
        public static readonly string JSON_TOKEN_INPUT =
            "{ \"deviceId\": \"2535C5EB-D6ED-4ABC-956B-4ACF29938F26\", \"token\": \"680bff9eb1ba0a8d48badd598be95c5642ad2939\" }";
        public static readonly string API_URL = "https://armuseum.ml/api/";
        public static readonly string GET_MUSEUM_URL = "https://armuseum.ml/api";
        public static readonly string EXHIBITS_URL = API_URL + "exhibit";
        public static readonly string EXHIBITS_RELS_URL = API_URL + "exh/rels";
        public static readonly string AUTHORS_URL = API_URL + "author";
        public static readonly string EXPOSITIONS_URL = API_URL + "exposition";
        public static readonly string MUSEUMS_URL = API_URL + "museum";
        public static readonly string MUSEUMS_RELS_URL = API_URL + "mus/rels";
        public static readonly string EXPOSITIONS_RELS_URL = API_URL + "expo/rels";
        public static readonly string UPDATE_URL = API_URL + "update";
        public static readonly string SITE_URL = "https://armuseum.ml/";
    }
}
