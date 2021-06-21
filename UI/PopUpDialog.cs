using UnityEngine;
using TMPro;

public class PopUpDialog : MonoBehaviour
{
    [SerializeField]private TMP_Text text;
    [SerializeField]private float expandTime;
    [SerializeField]private float fadeTime;
    [SerializeField]private float waitTime;

    public void Start()
    {
        this.gameObject.transform.localScale = new Vector3(0,0,0);
    }

    public void Load(string newText)
    {
        this.text.text = newText;
        LeanTween.scale(this.gameObject, new Vector3(1,1,1), expandTime).setOnComplete(FadeOut);
    }

    public void FadeOut()
    {
        LeanTween.alphaCanvas(this.gameObject.GetComponent<CanvasGroup>(), 0.0f, 2f).setRecursive(true).setDelay(waitTime).setOnComplete(Destroy);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
