using SimpleJSON;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.Http {
    public class Deserializers {
        public delegate T DeserializeThing<T>(JSONNode node);
        
        private delegate T DeserializeSomething<T>(JSONNode node);

        private static List<T> DeserializeList<T>(JSONNode node, DeserializeSomething<T> deserialize) {
            var result = new List<T>();

            foreach (var i in node.AsArray) {
                result.Add(deserialize(i));
            }

            return result;
        }

        public static List<Exhibit> DeserializeExhibitList(JSONNode node) => DeserializeList(node, DeserializeExhibit);

        public static Exhibit DeserializeExhibit(JSONNode node) {
            var exhibit = new Exhibit();

            exhibit.ExhibitId = node["exhibit_id"];
            exhibit.ExpositionId = node["exposition_id"];
            exhibit.StaffId = node["staff_id"];
            exhibit.Title = node["title"];
            exhibit.ShortDescription = node["short_description"];
            exhibit.Description = node["description"];
            exhibit.StartYear = node["start_year"];
            exhibit.EndYear = node["end_year"];
            exhibit.Size = node["size"];
            exhibit.Location = node["location"];
            string createdAt = node["created_at"];
            exhibit.CreatedAt = Convert.ToDateTime(createdAt);
            string updatedAt = node["updated_at"];
            exhibit.UpdatedAt = Convert.ToDateTime(updatedAt);
            exhibit.PhotoUrl = node["photo_path"];
            exhibit.AudioUrl = node["audio_path"];
            if (node["authors"] != null) {
                exhibit.Author = DeserializeAuthor(node["authors"]);
            }

            return exhibit;
        }

        public static List<Author> DeserializeAuthorList(JSONNode node) => DeserializeList(node, DeserializeAuthor);

        public static Author DeserializeAuthor(JSONNode node) {
            var author = new Author();

            author.AuthorId = node["author_id"];
            author.FullName = node["full_name"];
            author.BornYear = node["born_year"];
            author.DiedYear = node["died_year"];
            author.Location = node["location"];
            author.PhotoId = node["photo_id"];
            author.StaffId = node["staff_id"];
            string createdAt = node["created_at"];
            author.CreatedAt = Convert.ToDateTime(createdAt);
            string updatedAt = node["updated_at"];
            author.UpdatedAt = Convert.ToDateTime(updatedAt);
            author.PhotoPath = node["photo_path"];
            author.Description = node["description"];

            return author;
        }

        public static List<Exposition> DeserializeExpositionsList(JSONNode node) => DeserializeList(node, DeserializeExposition);

        public static Exposition DeserializeExposition(JSONNode node) {
            var exposition = new Exposition();

            exposition.ExpositionId = node["exposition_id"];
            exposition.Title = node["title"];
            exposition.Description = node["description"];
            exposition.MuseumId = node["museum_id"];
            exposition.StaffId = node["staff_id"];
            exposition.PhotoId = node["photo_id"];
            string createdAt = node["created_at"];
            exposition.CreatedAt = Convert.ToDateTime(createdAt);
            string updatedAt = node["updated_at"];
            exposition.UpdatedAt = Convert.ToDateTime(updatedAt);
            exposition.PhotoPath = node["photo_path"];
            if (node["exhibits"] != null) {
                exposition.Exhibits = DeserializeExhibitList(node["exhibits"]);
            }
            return exposition;
        }

        public static MuseumDto DeserializeMuseum(JSONNode node) {
            var museum = new MuseumDto();

            museum.MuseumId = node["museum_id"];
            museum.Name = node["name"];
            museum.Address = node["address"];
            museum.Latitude = node["latitude"];
            museum.Longitude = node["longitude"];
            museum.PhotoPath = node["photo_path"];
            museum.Expositions = DeserializeExpositionsList(node["expositions"]);

            return museum;
        }

        public static GeoCoordinate DeserializeGeoCoordinate(JSONNode node) {
            double latitude = node["latitude"];
            double longitude = node["longitude"];

            return new GeoCoordinate(latitude, longitude);
        }

        public static MuseumInfo DeserializeMuseumInfo(JSONNode node) {

            if (node["message"] != "Nu exista niciun muzeu in apropiere.")
            {
                return new MuseumInfo
                {
                    Name = node["name"],
                    MuseumId = node["museum_id"],
                    VuforiaDatabaseVersion = node["version"],
                    Coordinate = DeserializeGeoCoordinate(node)
                };
            }
            return null;
        }

        public static List<string> DeserializeStringArray(JSONNode node) {
            var result = new List<string>();

            foreach (var i in node.AsArray) {
                string str = i.Value;
                result.Add(str);
            }

            return result;
        }
    }
}
