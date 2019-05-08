using Assets.Scripts.AR_TEAM.Http;
using NUnit.Framework;
using SimpleJSON;
using System;

namespace Tests
{
    public class HTTPRequestTest
    {

        // [Test 1] Stefan Obreja

        [Test]
        public void GivenHttpRequstWhenDeserielizeExhibitThenExhibitMustBeEqual()
        {

            //    //Arrange

            string jsonExhibit = 
                "{" +
                "\"exhibit_id\": 1," +
                "\"exposition_id\": 1," +
                "\"staff_id\": 1," +
                "\"title\": \"0396865CRq\"," +
                "\"short_description\": \"So deep\"," +
                "\"description\": \"Cea mai splendida poezie ever\"," +
                "\"start_year\": 1873," +
                "\"end_year\": 2019," +
                "\"size\": \"20x30cm\"," +
                "\"location\": \"Iasi\"," +
                "\"created_at\": \"2019-05-05 12:31:14\"," +
                "\"updated_at\": \"2019-05-05 12:31:14\"," +
                "\"photo_path\": \"museum.lc/uploads\"}";

            var expected_result = false;
            var exhibit = new Exhibit();
            //Act

            var node = JSON.Parse(jsonExhibit);

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

            //    //Assert

            if (exhibit.ExhibitId.Equals(node["exhibit_id"])) { expected_result = true; }
            if (exhibit.ExpositionId.Equals(node["exposition_id"])) { expected_result = true; }
            if (exhibit.StaffId.Equals(node["staff_id"])) { expected_result = true; }
            if (exhibit.Title.Equals(node["title"])) { expected_result = true; }
            if (exhibit.ShortDescription.Equals(node["short_description"])) { expected_result = true; }
            if (exhibit.Description.Equals(node["description"])) { expected_result = true; }
            if (exhibit.StartYear.Equals(node["start_year"])) { expected_result = true; }
            if (exhibit.EndYear.Equals(node["end_year"])) { expected_result = true; }
            if (exhibit.Size.Equals(node["size"])) { expected_result = true; }
            if (exhibit.Location.Equals(node["location"])) { expected_result = true; }
            if (exhibit.CreatedAt.Equals(Convert.ToDateTime(createdAt))) { expected_result = true; }
            if (exhibit.UpdatedAt.Equals(Convert.ToDateTime(updatedAt))) { expected_result = true; }
            if (exhibit.PhotoUrl.Equals(node["photo_path"])) { expected_result = true; }

            Assert.True(expected_result);
        }

        
        // [Test 2] Stefan Obreja

        [Test]
        public void GivenHttpRequstWhenDeserielizeAuthorThenAuthorMustBeEqual()
        {
            //    //Arrange
            string jsonAuthor = 
                "{" +
                "\"author_id\": 1," +
                "\"full_name\": \"Mihai Eminescu\"," +
                "\"born_year\": 1850," +
                "\"died_year\": 1889," +
                "\"location\": \"Ipotesti\"," +
                "\"photo_id\": 1," +
                "\"staff_id\": 1," +
                "\"created_at\": \"2019-05-05 12:31:14\"," +
                "\"updated_at\": \"2019-05-05 12:31:14\"," +
                "\"photo_path\": \"museum.lc/uploads\"}";

            var expected_result = false;
            var author = new Author();

            //Act
            var node = JSON.Parse(jsonAuthor);

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

            //    //Assert

            if (author.AuthorId.Equals(1)) { expected_result = true; }
            if (author.FullName.Equals("Mihai Eminescu")) { expected_result = true; }
            if (author.BornYear.Equals(1850)) { expected_result = true; }
            if (author.DiedYear.Equals(1889)) { expected_result = true; }
            if (author.Location.Equals("Ipotesti")) { expected_result = true; }
            if (author.PhotoId.Equals(1)) { expected_result = true; }
            if (author.StaffId.Equals(1)) { expected_result = true; }
            if (author.CreatedAt.Equals(Convert.ToDateTime(createdAt))) { expected_result = true; }
            if (author.UpdatedAt.Equals(Convert.ToDateTime(updatedAt))) { expected_result = true; }
            if (author.PhotoPath.Equals("museum.lc/uploads")) { expected_result = true; }

            Assert.True(expected_result);
        }


        // [Test 3] Stefan Obreja

        [Test]
        public void GivenHttpRequstWhenDeserielizeExpositionThenExpositionMustBeEqual()
        {

            //    //Arrange

            string jsonExposition =
                "{\"exposition_id\": 1," +
                "\"title\": \"Carti Mihai Eminescu\"," +
                "\"description\": \"Cea mai veche carte\"," +
                "\"museum_id\": 1," +
                "\"staff_id\": 1," +
                "\"photo_id\": 1," +
                "\"created_at\": \"2019-05-05 12:31:14\"," +
                "\"updated_at\": \"2019-05-05 12:31:14\"," +
                "\"photo_path\": \"museum.lc/uploads/\"}";

            var expected_result = false;
            var exposition = new Exposition();

            //Act

            var node = JSON.Parse(jsonExposition);


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


            //    //Assert

            if (exposition.ExpositionId.Equals(1)) { expected_result = true; }
            if (exposition.Title.Equals("Carti Mihai Eminescu")) { expected_result = true; }
            if (exposition.Description.Equals(node["description"])) { expected_result = true; }
            if (exposition.MuseumId.Equals(node["museum_id"])) { expected_result = true; }
            if (exposition.StaffId.Equals(node["staff_id"])) { expected_result = true; }
            if (exposition.PhotoId.Equals(node["photo_id"])) { expected_result = true; }
            if (exposition.CreatedAt.Equals(Convert.ToDateTime(createdAt))) { expected_result = true; }
            if (exposition.UpdatedAt.Equals(Convert.ToDateTime(updatedAt))) { expected_result = true; }
            if (exposition.PhotoPath.Equals(node["photo_path"])) { expected_result = true; }

            Assert.True(expected_result);
        }
    }
}
