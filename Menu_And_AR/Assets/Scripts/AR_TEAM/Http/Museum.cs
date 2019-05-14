using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assets.Scripts.AR_TEAM.Http {
    public class MuseumDto {
        public int MuseumId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PhotoPath { get; set; }
        public List<Exposition> Expositions { get; set; }
        public List<Exhibit> Exhibits { get; set; }
        public List<string> VuforiaFilesOnDisk { get; set; }

        public void PopulateExhibits() {
            Exhibits = Expositions
                .SelectMany(x => x.Exhibits)
                .Distinct()
                .ToList();
        }
        
        public void ResolvePaths() {
            Expositions
                .SelectMany(x => x.Exhibits)
                .ToList()
                .ForEach(x => x.AudioUrl = HttpRequests.HttpRequests.SITE_URL + x.AudioUrl);
        }

        public (string /*title*/,int /*exhibit_id*/, string /*author*/, int /*author_id*/) FindArSceneInfoByExhibitId(int id) {
            var exhibit = Exhibits
                .First(x => x.ExhibitId == id);
            if (exhibit == null) {
                return ("not found", 0, "not found", 0);
            }
            return (exhibit.Title, exhibit.ExhibitId, exhibit.Author.FullName, exhibit.Author.AuthorId);
        }

        public string GetSongForExhibitId(int id)
        {
            return Exhibits
                .Where(x => x.ExhibitId == id)
                .Select(x => x.AudioPathOnDisk)
                .First();
        }

        public string GetVuforiaXMLPath()
        {
            return VuforiaFilesOnDisk
                .Where(x => new FileInfo(x).Extension == ".xml")
                .First();
        }

        public void SetPhotoPath()
        {
            PhotoPath = "AR_TEAM/images/Museums/" + Name.Replace(" ", "_");
        }
    }
}
