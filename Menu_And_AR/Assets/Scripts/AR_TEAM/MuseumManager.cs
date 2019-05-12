using Assets.Scripts.AR_TEAM.Http;
using Assets.Scripts.AR_TEAM.HttpRequests;
using System.Collections;

public sealed class MuseumManager
{
    private static MuseumManager instance;

    public MuseumDto CurrentMuseum { get; private set; }

    private MuseumManager()
    {
    }

    public static MuseumManager Instance
    {
        get
        {
            if ( instance == null)
            {
                instance = new MuseumManager();
            }
            return instance;
        }
    }

    private void OnMuseumLoaded(MuseumDto museum)
    {
        museum.PopulateExhibits();
        CurrentMuseum = museum;
    }

    public IEnumerator RequestMuseumByID(int id)
    {
        yield return new HttpRequests().GetMuseumData(OnMuseumLoaded, id);
    }
}
