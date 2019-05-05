using SimpleJSON;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.Http {
    class Deserializers {
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

            return author;
        }
    }
}
