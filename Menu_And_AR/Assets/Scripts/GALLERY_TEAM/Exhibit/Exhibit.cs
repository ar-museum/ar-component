using UnityEngine.UI;
using UnityEngine;


namespace Scripts
{
    [System.Serializable]
    public class ExhibitData
    {
        public string titlu;
        public string descriere;
        public string denumire;
        public string autor;
    }

    public class Exhibit : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private UnityEngine.UI.Image imgOpera = null;
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtDescription;
        private float offset;
        private float contentHeight;

        // Start is called before the first frame update
        void Start()
        {
            loadContent();
            if (!PlayerPrefs.HasKey("exhibit" + Globals.exhibit + "offset"))
                PlayerPrefs.SetFloat("exhibit" + Globals.exhibit + "offset", 0);
            Invoke("setContentDimension", 0.1f);
        }

        void loadContent()
        {
            Globals.exhibit = PlayerPrefs.GetString("exhibit", "op2");
            JsonToObject jo = new JsonToObject();
            ExhibitData exhibit = jo.loadJson<ExhibitData>("GALLERY_TEAM/" + Globals.exhibit);
            txtDescription.text = exhibit.descriere;
            txtTitle.text = exhibit.titlu + '\n';
            if (imgOpera != null)
            {
                imgOpera.sprite = Resources.Load<Sprite>("GALLERY_TEAM/Sprites/" + exhibit.denumire);
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
            thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, (txtDescription.text + txtTitle.text).Length * 5 + 1000);

            //resize offset
            offset = PlayerPrefs.GetFloat("exhibit" + Globals.exhibit + "offset");

            if (offset == 0)
            {
                offset = imgRectTransform.rect.height - firstHeight;
                PlayerPrefs.SetFloat("exhibit" + Globals.exhibit + "offset", offset);
            }
            else
            {
                offset = PlayerPrefs.GetFloat("exhibit" + Globals.exhibit + "offset");
            }
            imgRectTransform.offsetMin = new Vector2(imgRectTransform.offsetMin.x, offset);
            RectTransform lastChildRectTransform = lastChild.GetComponent<RectTransform>();
            lastChildRectTransform.offsetMax = new Vector2(lastChildRectTransform.offsetMax.x, offset);
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.DeleteAll();
        }

    }

}