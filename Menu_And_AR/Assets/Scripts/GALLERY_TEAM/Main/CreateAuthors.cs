using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateAuthors : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        for (int i = 0; i < 4; i++)
            createButton(i);
    }

    void createButton(int index)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250 - index * 250);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(index); });
        button.transform.GetChild(0).GetComponent<Text>().text = "This is button text " + index;
    }

    void OnClick(int index)
    {
        Debug.Log("Clicked button " + index);
		SceneManager.LoadScene("AuthorScene");
    }

}