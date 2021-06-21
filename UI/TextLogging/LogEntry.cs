using UnityEngine;
using TMPro;

public class LogEntry: MonoBehaviour
{

    public void SetText(string enrichedText)
    {
        this.gameObject.GetComponent<TMP_Text>().text = enrichedText;
    }

}
