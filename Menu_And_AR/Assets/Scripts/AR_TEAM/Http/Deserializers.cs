using SimpleJSON;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.AR_TEAM.Http {
    class Deserializers {
        public static List<Exhibit> DeserializeExhibitList(JSONNode node) {
            var result = new List<Exhibit>();

            foreach (var i in node.AsArray) {
                result.Add(DeserializeExhibit(i));
            }

            return result;
        }

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
    }
}
