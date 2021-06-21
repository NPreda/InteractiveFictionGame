using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


[RequireComponent(typeof(FancyTooltipTrigger))]
public class ClueBox : MonoBehaviour
{
    [SerializeField]private Color passColor;
    [SerializeField]private Color failColor;
    public Dictionary<string, bool> conditions;
    private FancyTooltipTrigger tooltipTrigger;
    private FancyTooltip tooltip;

    public void Activate()
    {
        this.gameObject.SetActive(true);
        tooltipTrigger = this.gameObject.GetComponent<FancyTooltipTrigger>();
        GenerateTooltip();
    }

    public void GenerateTooltip(){
        //go through the dictionary and for each condition line, add it to the total text with the appropriate colouring
        string title = "Requirements";
        string body = "";
        foreach(var condition in conditions){
            if(condition.Value == true){
                body = body + "   " + condition.Key + '\n';
            }else{
                body = body + "   " +  "<color=#FF0000>" + condition.Key + "</color>\n";
            }
        }
        tooltipTrigger.SetContent(title, body);
    }
}
