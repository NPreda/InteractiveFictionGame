using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text planText;

    public void Load(List<StatusEffect> statusEffects)
    {
        string statusText = "";
        foreach(var status in statusEffects)
        {
            if(status.stack != 0)
            {
                string statusEntry = status.statusName+ "(" + status.stack + ")" + " - " + status.statusDescription;
                statusText = statusText + statusEntry;
            }               
        }
        planText.text = statusText;
    }
}
