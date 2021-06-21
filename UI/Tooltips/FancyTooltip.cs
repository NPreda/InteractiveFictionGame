using UnityEngine;
using TMPro;


public class FancyTooltip : SimpleTooltip
{
    public TMP_Text content;

    public override void Awake(){
        base.Awake();
        rectTransform = transform.GetComponent<RectTransform>();
    }

    public override void ShowTooltip(string bottom){
        title.text = "Tooltip";
        content.text = bottom;
        gameObject.SetActive(true);
    }

    public override void ShowTooltip(string top, string bottom){
        title.text= top;
        content.text = bottom;
        gameObject.SetActive(true);
    }

}
