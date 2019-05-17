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
        public List<Author> Authors { get; set; }
        public List<string> VuforiaFilesOnDisk { get; set; }

        public void PopulateFields() {
            Exhibits = Expositions
                .Where(x => x.Exhibits != null)
                .SelectMany(x => x.Exhibits)
                .Distinct()
                .ToList();
            Authors = Exhibits
                .Select(x => x.Author)
                .GroupBy(x => x.AuthorId)
                .Select(x => x.First())
                .ToList();
        }
        
        public void ResolvePaths() {
            Expositions
                .Where(x => x.Exhibits != null)
                .SelectMany(x => x.Exhibits)
                .ToList()
                .ForEach(x => x.AudioUrl = Endpoints.SITE_URL + x.AudioUrl);
        }

        public (string /*title*/, string /*author*/, int /*author_id*/) FindArSceneInfoByExhibitId(int id) {
            foreach(var exhibit in Exhibits)
            {
                if(exhibit.ExhibitId == id)
                {
                    return (exhibit.Title, exhibit.Author.FullName, exhibit.Author.AuthorId);
                }
            }
            return ("not found", "not found", 0);
        }

        public (string /*title*/, string /*author*/, string /*description*/, string /*photoUrl*/) GetExhibitDataById(int id)
        {
            foreach (var exhibit in Exhibits)
            {
                if (exhibit.ExhibitId == id)
                {
                    return (exhibit.Title, exhibit.Author.FullName, exhibit.ShortDescription, exhibit.PhotoUrl);
                }
            }
            return ("not found", "not found", "not found","not found");
        }

        public (string /*FullName*/,int /*bornYear*/,int /*diedYear*/,string /*Location*/, string /*photoUrl*/) GetAuthorDataById(int id)
        {
            foreach (var author in Authors)
            {
                if (author.AuthorId == id)
                {
                    return (author.FullName,author.BornYear, author.DiedYear, author.Location, author.PhotoPath);
                }
            }
            return ("not found", 0, 0, "Westeros", "not found");
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
