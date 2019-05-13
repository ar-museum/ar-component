using UnityEngine.UI;
using UnityEngine;
using Scripts;

namespace Scripts
{
    [System.Serializable]
    public class AuthorData
    {
        public string full_name;
        public string born_year;
        public string died_year;
        public string location;
        public string photo_id;
    }

    public class Author : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Image imgOpera = null;
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtDescription;
        private float offset;
        private float contentHeight;

        // Start is called before the first frame update
        void Start()
        {
            loadContent();
            if (!PlayerPrefs.HasKey("author" + Globals.author + "offset"))
                PlayerPrefs.SetFloat("author" + Globals.author + "offset", 0);
            Invoke("setContentDimension", 0.01f);
        }

        void loadContent()
        {
            Globals.author = PlayerPrefs.GetString("author", "Anonim");
            JsonToObject jo = new JsonToObject();
            AuthorData author = jo.loadJson<AuthorData>("GALLERY_TEAM/" + Globals.author);
            if (imgOpera != null)
            {
                imgOpera.sprite = Resources.Load<Sprite>("GALLERY_TEAM/Sprites/" + author.photo_id);
            }
            txtTitle.text = author.full_name;
            txtDescription.text = "Born : " + author.born_year + "\n";
            txtDescription.text += "Died : " + author.died_year + "\n";
            txtDescription.text += "Location : " + author.location;
        }

        void setContentDimension()
        {
        //    RectTransform thisRectTransform = GetComponent<RectTransform>();
        //    RectTransform imgRectTransform = imgOpera.GetComponent<RectTransform>();

        //    //image height before the resize
        //    float firstHeight = imgRectTransform.rect.height;

        //    Transform lastChild = null;
        //    foreach (Transform child in transform)
        //    {
        //        lastChild = child;
        //    }
        //    //resize
        //    thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, (txtDescription.text + txtTitle.text).Length * 5 + 1250);

        //    //resize offset
        //    offset = PlayerPrefs.GetFloat("author" + Globals.author + "offset");

        //    if (offset == 0)
        //    {
        //        offset = imgRectTransform.rect.height - firstHeight;
        //        PlayerPrefs.SetFloat("author" + Globals.author + "offset", offset);
        //    }
        //    else
        //    {
        //        offset = PlayerPrefs.GetFloat("author" + Globals.author + "offset");
        //    }
        //    imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
        //    RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
        //    lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteAll();
        }
        
    }
}