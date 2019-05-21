using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public GameObject panelToAttachButtonsTo2;
    [SerializeField] private Text txtTitle;
    void Start()
    {
        txtTitle.text = MuseumManager.Instance.CurrentMuseum.Name;
        int count = 0;
        foreach (var author in MuseumManager.Instance.CurrentMuseum.Authors)
        {
            createButton(author.AuthorId);
            ++count;
            if (count == 3)
                break;
        }
        count = 0;
        foreach (var exhibit in MuseumManager.Instance.CurrentMuseum.Exhibits)
        {
            createButton2(exhibit.ExhibitId);
            ++count;
            if (count == 3)
                break;
        }
    }
    void createButton(int index)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(index); });

        string das, das2, imagePath;
        int born, died;

        (das2, born, died, das, imagePath) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(index);
        byte[] byteArray = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        button.transform.GetComponent<Image>().sprite = s;
        button.transform.GetComponent<Image>().preserveAspect = false;
    }

    void OnClick(int index)
    {
        PlayerPrefs.SetInt("Gallery_AuthorID", index);
        SceneManager.LoadScene("AuthorScene");
    }

    void createButton2(int index)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo2.transform);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClick2(index); });

        string das, das2, das3, das4, imagePath;
        (das3, das, das2, imagePath, das4) = MuseumManager.Instance.CurrentMuseum.GetExhibitDataById(index);

        byte[] byteArray = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        button.transform.GetComponent<Image>().sprite = s;
        button.transform.GetComponent<Image>().preserveAspect = false;
    }

    void OnClick2(int index)
    {
        PlayerPrefs.SetInt("Gallery_ExhibitID", index);
        SceneManager.LoadScene("ExhibitScene");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
