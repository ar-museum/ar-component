using UnityEngine;
using UnityEngine.UI;

public class CreateAuthors : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    void Start()//Creates a button and sets it up
    {
        for (int i = 0; i < 10; i++)
            createButton(i);

    }

    void createButton(int index)
    {
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.transform.SetParent(panelToAttachButtonsTo.transform);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150 - index *100);
        button.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        button.GetComponent<Button>().onClick.AddListener(OnClick);
        button.transform.GetChild(0).GetComponent<Text>().text = "This is button text";
    }

    void OnClick()
    {
        Debug.Log("clicked!");
    }
}