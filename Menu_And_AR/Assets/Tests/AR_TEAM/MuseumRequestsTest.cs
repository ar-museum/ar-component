using Assets.Scripts.AR_TEAM.Http;
using NUnit.Framework;
using SimpleJSON;
using System;

namespace Assets.Tests.AR_TEAM
{
    class MuseumRequestsTest
    {
        // Test 1 Stefan Obreja
        [Test]
        public void ShouldDownloadAuthor(){
            Author author = new Author();
            var expectedResult = false;

            MuseumRequests.DownloadAuthor(1);
            String result = Endpoints.AUTHORS_URL + "/1";
            if (!result.Equals(null))
            {
                expectedResult = true;
            }
            
        }

        // Test 2 Stefan Obreja
        [Test]
        public void ShouldDownloadExhibit()
        {
            Exhibit exhibit = new Exhibit();
            var expectedResult = false;

            MuseumRequests.DownloadExhibit(1);
            String result = Endpoints.EXHIBITS_RELS_URL + "/1";
            if (!result.Equals(null))
            {
                expectedResult = true;
            }
        }

        // Test 3 Stefan Obreja
        [Test]
        public void ShouldDownloadExposition()
        {
            Exposition exhibit = new Exposition();
            var expectedResult = false;

            MuseumRequests.DownloadExposition(1);
            String result = Endpoints.EXPOSITIONS_RELS_URL + "/1";
            if (!result.Equals(null))
            {
                expectedResult = true;
            }
        }
        // Test 4 Stefan Obreja
        [Test]
        public void ShouldDownloadMuseum()
        {
            Exposition exhibit = new Exposition();
            var expectedResult = false;

            MuseumRequests.DownloadMuseum(1);
            String result = Endpoints.MUSEUMS_RELS_URL + "/1";
            if (!result.Equals(null))
            {
                expectedResult = true;
            }
        }
    }
}
