using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchResults : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject thisCanvas;
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        string search = PlayerPrefs.GetString("searchInput");
        int count = 0;
        foreach (var author in MuseumManager.Instance.CurrentMuseum.Authors)
        {
            if( (author.AuthorId).ToString().Contains(search) || (author.BornYear).ToString().Contains(search) ||
                author.CreatedAt.ToString().Contains(search) || author.Location.Contains(search) || 
                author.FullName.Contains(search) )
            {
                createButtonAuthor(author.AuthorId, count);
                ++count;
            }
        }

        if (count > 5)
        {
            panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta = new Vector2(panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta.x, 1680 + (count - 5) * 250);
        }
        count = 0;

        foreach (var exhibit in MuseumManager.Instance.CurrentMuseum.Exhibits)
        {
            if( exhibit.Location.Contains(search) ||  exhibit.Size.Contains(search) ||
                exhibit.Title.Contains(search) ||  exhibit.StartYear.ToString().Contains(search) )
            {
                createButtonExhibit(exhibit.ExhibitId, count);
                ++count;
            }
        }

        if (count > 5)
        {
            panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta = new Vector2(panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta.x, 1680 + (count - 5) * 250);
        }
    }

    void createButtonAuthor(int index, int count)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250 - count * 250);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClickA(index); });

        string das, imagePath;
        int born, died;

        (button.transform.GetChild(0).GetComponent<Text>().text, born, died, das, imagePath) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(index);
        byte[] byteArray = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        button.transform.GetChild(1).GetComponent<Image>().sprite = s;
    }
    void createButtonExhibit(int index, int count)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250 - count * 250);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClickE(index); });

        string das, das2, das3, imagePath;
        (button.transform.GetChild(0).GetComponent<Text>().text, das, das2, imagePath, das3) = MuseumManager.Instance.CurrentMuseum.GetExhibitDataById(index);

        byte[] byteArray = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        button.transform.GetChild(1).GetComponent<Image>().sprite = s;
    }

    void OnClickA(int index)
    {
        Debug.Log("Clicked button " + index);
        PlayerPrefs.SetInt("Gallery_AuthorID", index);
        SceneManager.LoadScene("AuthorScene");
    }
    void OnClickE(int index)
    {
        Debug.Log("Clicked button " + index);
        PlayerPrefs.SetInt("Gallery_ExhibitID", index);
        SceneManager.LoadScene("ExhibitScene");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            mainCanvas.SetActive(true);
            thisCanvas.SetActive(false);
        }
    }

}