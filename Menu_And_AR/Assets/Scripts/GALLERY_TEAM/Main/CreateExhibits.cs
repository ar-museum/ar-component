using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateExhibits : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        int count = 0;
        foreach (var exhibit in MuseumManager.Instance.CurrentMuseum.Exhibits)
        { createButton(exhibit.ExhibitId, count); ++count; }
        if(count>5)
        {
            panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta = new Vector2(panelToAttachButtonsTo.GetComponent<RectTransform>().sizeDelta.x, 1680 + (count-5) * 250);
        }
    }

    void createButton(int index, int count)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250 - count * 250);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(index); });

        string das;
        string das2, das3;
        (button.transform.GetChild(0).GetComponent<Text>().text, das,das2,das3) = MuseumManager.Instance.CurrentMuseum.GetExhibitDataById(index);
    }

    void OnClick(int index)
    {
        Debug.Log("Clicked button " + index);
        PlayerPrefs.SetInt("Exhibit_Id", index);
        SceneManager.LoadScene("ExhibitScene");
    }

}