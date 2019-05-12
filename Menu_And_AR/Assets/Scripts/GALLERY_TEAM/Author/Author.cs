using UnityEngine.UI;
using UnityEngine;
using Scripts;

namespace Scripts
{
    [System.Serializable]
    public class AuthorData
    {
        public string titlu;
        public string descriere;
        public string denumire;
        public string autor;
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
            Globals.author = PlayerPrefs.GetString("author", "author");
            JsonToObject jo = new JsonToObject();
            AuthorData author = jo.loadJson<AuthorData>("GALLERY_TEAM/" + Globals.author);
            txtDescription.text = author.descriere;
            txtTitle.text = author.titlu + '\n';
            if (imgOpera != null)
            {
                imgOpera.sprite = Resources.Load<Sprite>("GALLERY_TEAM/Sprites/" + author.denumire);
            }
        }

        void setContentDimension()
        {
            RectTransform thisRectTransform = GetComponent<RectTransform>();
            RectTransform imgRectTransform = imgOpera.GetComponent<RectTransform>();

            //image height before the resize
            float firstHeight = imgRectTransform.rect.height;

            Transform lastChild = null;
            foreach (Transform child in transform)
            {
                lastChild = child;
            }
            //resize
            thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, (txtDescription.text + txtTitle.text).Length * 5 + 1250);

            //resize offset
            offset = PlayerPrefs.GetFloat("author" + Globals.author + "offset");

            if (offset == 0)
            {
                offset = imgRectTransform.rect.height - firstHeight;
                PlayerPrefs.SetFloat("author" + Globals.author + "offset", offset);
            }
            else
            {
                offset = PlayerPrefs.GetFloat("author" + Globals.author + "offset");
            }
            imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
            RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
            lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteAll();
        }

        /*
        public string GetHtmlFromUri(string resource)
        {
            string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                    if (isSuccess)
                    {
                        using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                        {
                            //We are limiting the array to 80 so we don't have
                            //to parse the entire html document feel free to 
                            //adjust (probably stay under 300)
                            char[] cs = new char[80];
                            reader.Read(cs, 0, cs.Length);
                            foreach (char ch in cs)
                            {
                                html += ch;
                            }
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return html;
        }
        */
    }
}