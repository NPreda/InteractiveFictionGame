using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(FancyTooltipTrigger))]
public class StatusDisplayElement : MonoBehaviour 
{
    public Image iconImage;
    public TMP_Text value;
    public StatusType statusType;

    private FancyTooltipTrigger tooltipTrigger;

    public void Load(StatusEffect statusEffect)
    {
        tooltipTrigger = this.gameObject.GetComponent<FancyTooltipTrigger>();
        
        iconImage.sprite = statusEffect.icon;
        if(statusEffect.visibleValue)
            value.text = statusEffect.stack.ToString();
        else   
            value.text = "";
        
        tooltipTrigger.SetContent(statusEffect.statusName, statusEffect.statusDescription);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}