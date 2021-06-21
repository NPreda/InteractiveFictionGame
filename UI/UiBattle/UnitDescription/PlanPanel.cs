using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text planText;
    private CombatLogParser actionParser = new CombatLogParser();

    public void Load(List<CombatAction> combatActions)
    {
        
        string actionText = "";
        foreach(var action in combatActions)
        {
            foreach(var target in action.targets)
            {
            	string actionDesc = actionParser.ParseEntry(action.actionDescription, action.source, target, action._effectValue);
                string actionEntry = action.actionName + " - " + actionDesc + "\n";
                actionText = actionText + actionEntry;
            }
        }
        planText.text = actionText;
    }
}
