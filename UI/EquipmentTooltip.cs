using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class EquipmentTooltip : MonoBehaviour
{
    [SerializeField] Text TextTitle;
    [SerializeField] Text TextSubtitle;    
    [SerializeField] Text TextDetail;
    [SerializeField] private RectTransform canvasRectTransform;   

    private RectTransform backgroundRectTransform;
    private StringBuilder sb = new StringBuilder();
    private RectTransform rectTransform;
    
    private void Start(){
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Update(){
        Vector2 anchoredPosition = Input.mousePosition;
        if (anchoredPosition.x + rectTransform.rect.width > canvasRectTransform.rect.width){
            //Tooltip left screen on left side
            anchoredPosition.x = canvasRectTransform.rect.width - rectTransform.rect.width;
        }
        if (anchoredPosition.y > canvasRectTransform.rect.height){
            //Tooltip left screen on top
            anchoredPosition.y = canvasRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void ShowTooltip(EquippableQuality item){
        TextTitle.text = item.qualityName;
        TextSubtitle.text = item.equipmentType.ToString();

        sb.Length = 0;
        
        foreach(KeyValuePair<RPGStatType, int> modifier in item.modifiers)      //go through the modifiers dictionary and add them to the stats
        {
            string statName = modifier.Key.ToString();
            int value = modifier.Value;
            AddStat(statName, value);
        }
        TextDetail.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void ShowTooltip(GenericQuality item){
        TextTitle.text = item.qualityName;
        TextSubtitle.text = "Quantity: " + item.val.ToString();
        TextDetail.text = item.ReturnDescription();
        gameObject.SetActive(true);
    }

    public void ShowTooltip(Quality item){
        TextTitle.text = item.qualityName;
        TextDetail.text = item.ReturnDescription();
        gameObject.SetActive(true);
    }

    public void FlushTooltip(){
        TextTitle.text = "";
        TextSubtitle.text = "";
        TextDetail.text = "";
    }

    public void HideTooltip(){
        FlushTooltip();
        gameObject.SetActive(false);
    }

    private void AddStat(string statName, int value){
        if(value != 0){
            if(sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
        }
    }
}
