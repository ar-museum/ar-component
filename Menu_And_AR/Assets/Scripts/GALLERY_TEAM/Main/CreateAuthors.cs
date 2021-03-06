﻿using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAuthors : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject thisCanvas;
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        int count = 0;
        foreach (var author in MuseumManager.Instance.CurrentMuseum.Authors)
        {
            createButton(author.AuthorId, count);
            ++count;
        }
        if (count > 5)
        {
            panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta = new Vector2(panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta.x, 1680 + (count - 5) * 250);
        }
    }

    void createButton(int index, int count)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250 - count * 250);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(index); });

        string das, imagePath;
        int born, died;

        (button.transform.GetChild(0).GetComponent<Text>().text, born, died, das, imagePath) = MuseumManager.Instance.CurrentMuseum.GetAuthorDataById(index);
        byte[] byteArray = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(8, 8);
        texture.LoadImage(byteArray);
        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
        button.transform.GetChild(1).GetComponent<Image>().sprite = s;
    }

    void OnClick(int index)
    {
        Debug.Log("Clicked button " + index);
        PlayerPrefs.SetInt("Gallery_AuthorID", index);
        SceneManager.LoadScene("AuthorScene");
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