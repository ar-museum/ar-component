using UnityEngine.UI;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

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
        [SerializeField] private Text txtTitle = null;
        [SerializeField] private Text txtDescription = null;
        string auth = " ";
        private float offset;
        private float contentHeight;

        // Start is called before the first frame update
        void Start()
        {
            loadContent();
            //if (!PlayerPrefs.HasKey("exhibit" + Globals.exhibit + "offset"))
            //    PlayerPrefs.SetFloat("exhibit" + Globals.exhibit + "offset", 0);
            Invoke("setContentDimension", 0.1f);

            
        }

        void loadContent()
        {
            string imagePath,audioPath;
            int exhibitId = PlayerPrefs.GetInt("Gallery_ExhibitID");
            (txtTitle.text,auth,txtDescription.text, imagePath,audioPath) = MuseumManager.Instance.CurrentMuseum.GetExhibitDataById(exhibitId);
            txtTitle.text = txtTitle.text + '\n';

            //music

            WWW www = new WWW("file://" + audioPath);
            AudioClip MusicClip = www.GetAudioClip();
            this.gameObject.AddComponent<AudioSource>();
            var MusicSource = this.GetComponent<AudioSource>();
            MusicSource.clip = MusicClip;
            MusicSource.Play();

            Debug.Log("The played sound is at : " + www.text);

            //end


            byte[] byteArray = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(8, 8);
            texture.LoadImage(byteArray);
            Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
            imgOpera.sprite = s;
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
            //offset = PlayerPrefs.GetFloat("exhibit" + Globals.exhibit + "offset");
            offset = 0;

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

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (string.Compare(PlayerPrefs.GetString("CameFromAuthor"), "true") == 0)
                {
                    SceneManager.LoadScene("AuthorScene");
                    PlayerPrefs.SetString("CameFromAuthor", "false");
                }
                else SceneManager.LoadScene("GalleryScene");
            }
        }
    }

}