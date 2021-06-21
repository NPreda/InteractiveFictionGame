using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLogControl : MonoBehaviour
{

    [SerializeField]
    private GameObject textTemplate;

    private List<GameObject> textItems;

    void Start()
    {
        textItems = new List<GameObject>();
    }

    public void LogText(string enrichedText)
    {
        if (textItems.Count == 10) 
        {
            GameObject tempItem = textItems[0];
            Destroy(tempItem.gameObject);
            textItems.Remove(tempItem);
        }
        GameObject newText = Instantiate(textTemplate, this.gameObject.transform) as GameObject;

        newText.GetComponent<LogEntry>().SetText(enrichedText);

        textItems.Add(newText.gameObject);
    }

    public void ShowPanel() => this.gameObject.SetActive(true);
    public void HidePanel() => this.gameObject.SetActive(false);
}
