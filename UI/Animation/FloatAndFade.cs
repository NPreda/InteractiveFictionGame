using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatAndFade : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float time;

    private TMP_Text text;

    void Start()
    {

        text = this.gameObject.GetComponent<TMP_Text>();
        if (text == null)
        {

            var img = this.gameObject.GetComponent<Image>();
            Color currentColor = img.color;
            Color finalColor = currentColor;
            finalColor.a = 0f;
            LeanTween.moveLocalY(this.gameObject, height, time);
            LeanTween.value( img.gameObject, a => img.color = a, currentColor , finalColor, time).setOnComplete(Destroy); 
        }else{

            Color textColor = text.color;
            Color invisibleColor = textColor - new Color(0,0,0,1);
            LeanTween.moveLocalY(this.gameObject, height, time);
            LeanTween.value( text.gameObject, a => text.alpha = a, 1 , 0, time).setOnComplete(Destroy);
        }
    }


    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
