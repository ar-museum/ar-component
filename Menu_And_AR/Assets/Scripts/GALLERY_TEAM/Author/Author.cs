using UnityEngine.UI;
using UnityEngine;
using Scripts;
using UnityEngine.SceneManagement;

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
        public GameObject buttonPrefab;
        public GameObject panelToAttachButtonsTo;
        private float offset;
        private float contentHeight;

        // Start is called before the first frame update
        void Start()
        {
            loadContent();
            if (!PlayerPrefs.HasKey("author" + Globals.author + "offset"))
                PlayerPrefs.SetFloat("author" + Globals.author + "offset", 0);
            Invoke("setContentDimension", 0.01f);
            int count = 0;
            foreach (var exhibit in MuseumManager.Instance.CurrentMuseum.Exhibits)
            {
                if (count > 2)
                    break;
                if (PlayerPrefs.GetInt("Author_Id") == exhibit.Author.AuthorId)
                {
                    createButton(exhibit.ExhibitId);
                    ++count;
                }
            }
        
        }

        void createButton(int index)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);
            button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(index); });

            string das, das2, title;
            int born, died;

            (title, born, died, das2, das) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(index);
        }

        void OnClick(int index)
        {
            Debug.Log("Clicked button " + index);
            PlayerPrefs.SetInt("Exhibit_Id", index);
            SceneManager.LoadScene("ExhibitScene");
        }

        void loadContent()
        {
            Debug.Log(PlayerPrefs.GetInt("Author_Id"));
            int authorId = PlayerPrefs.GetInt("Author_Id");
            if (imgOpera != null)
            {
                imgOpera.sprite = Resources.Load<Sprite>("GALLERY_TEAM/Sprites/74");
            }
            int born, died;
            string location, photo_path;
            (txtTitle.text, born, died, location, photo_path) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(authorId);
            txtDescription.text = "Born : " + born + "\n";
            txtDescription.text += "Died : " + died + "\n";
            txtDescription.text += "Location : " + location;
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