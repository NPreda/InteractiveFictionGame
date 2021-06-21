
using UnityEngine;
using TMPro;
using Tools.UI;

public class SimpleTooltip : TweeningMover
{
    public float xBuffer;
    public float yBuffer;
    public TMP_Text title;
    public RectTransform canvasRectTransform;  
    protected RectTransform rectTransform;

    public override void Awake(){
        base.Awake();
        rectTransform = transform.GetComponent<RectTransform>();
    }

    public void Update(){
        Vector3 anchoredPosition = Input.mousePosition;
        if (anchoredPosition.x + rectTransform.rect.width > canvasRectTransform.rect.width){
            //Tooltip left screen on left side
            anchoredPosition.x = canvasRectTransform.rect.width + xBuffer - rectTransform.rect.width;
        }else{
            anchoredPosition.x = anchoredPosition.x + xBuffer;
        }
        if (anchoredPosition.y > canvasRectTransform.rect.height){
            //Tooltip left screen on top
            anchoredPosition.y = canvasRectTransform.rect.height + yBuffer - rectTransform.rect.height;
        }else{
            anchoredPosition.y = anchoredPosition.y + yBuffer;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public virtual void ShowTooltip(string top){
        title.text = top;
        gameObject.SetActive(true);
    }

    public virtual void ShowTooltip(string top, string bottom){
        title.text = top;
        gameObject.SetActive(true);
    }

    public void HideTooltip(){
        gameObject.SetActive(false);
    }

}