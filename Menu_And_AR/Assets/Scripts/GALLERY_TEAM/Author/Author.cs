using UnityEngine.UI;
using UnityEngine;
using Scripts;
using UnityEngine.SceneManagement;
using System.IO;

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
            //if (!PlayerPrefs.HasKey("author" + Globals.author + "offset"))
            //    PlayerPrefs.SetFloat("author" + Globals.author + "offset", 0);
            Invoke("setContentDimension", 0.01f);
            int count = 0;
            foreach (var exhibit in MuseumManager.Instance.CurrentMuseum.Exhibits)
            {
                if (count > 2)
                    break;
                if (PlayerPrefs.GetInt("Gallery_AuthorID") == exhibit.Author.AuthorId)
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

            string das,das3;
            string das2, imagePath;
            (das3, das, das2, imagePath) = MuseumManager.Instance.CurrentMuseum.GetExhibitDataById(index);

            byte[] byteArray = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(byteArray);
            Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
            button.transform.GetComponent<Image>().sprite = s;

        }

        void OnClick(int index)
        {
            Debug.Log("Clicked button " + index);
            PlayerPrefs.SetInt("Gallery_ExhibitID", index);
            SceneManager.LoadScene("ExhibitScene");
        }

        void loadContent()
        {
            int authorId = PlayerPrefs.GetInt("Gallery_AuthorID");
            int born, died;
            string location, imagePath;
            (txtTitle.text, born, died, location, imagePath) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(authorId);
            txtDescription.text = "Born : " + born + "\n";
            txtDescription.text += "Died : " + died + "\n";
            txtDescription.text += "Location : " + location;

            byte[] byteArray = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(byteArray);
            Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
            imgOpera.sprite = s;
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