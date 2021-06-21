using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StatusDisplay : MonoBehaviour 
{

    List<StatusEffect> sortedEffects = new List<StatusEffect>();
    List<StatusDisplayElement> statusIconElements = new List<StatusDisplayElement>();
    public GameObject elementPrefab;

    public List<StatusEffect> ProcessStatuses(List<StatusEffect> unitStatuses)
    {
        List<StatusEffect> activeEffects = new List<StatusEffect>();
        foreach(StatusEffect status in unitStatuses)
        {
            //make an if statement eventually
            activeEffects.Add(status);
        }

        sortedEffects = activeEffects.OrderBy(x=>(int) x.displayType).ToList();
        return sortedEffects;
    }

// public StatusEffect GetEffectObject(StatusType statusType, List<StatusEffect>)
// {
//     var status = books.Where(book => book.author == search).FirstOrDefault();
//     // or simply:
//     // var book = books.FirstOrDefault(book => book.author == search);
//     return book;
// }

    public void RefreshUI(List<StatusEffect> unitStatuses)
    {
        List<StatusEffect> sortedStatuses = ProcessStatuses(unitStatuses);


        //delete and clear list
        for(int i = statusIconElements.Count -1 ; i >= 0; i--)
        {
            statusIconElements[i].Destroy();
        }

        statusIconElements.Clear();

        foreach(StatusEffect status in sortedEffects)
        {
            if (status.stack != 0)
            {
                GameObject statusElement = Instantiate(elementPrefab, this.gameObject.transform) as GameObject;
                StatusDisplayElement elementScript = statusElement.GetComponent<StatusDisplayElement>();
                statusIconElements.Add(elementScript);
                elementScript.Load(status);    
            }            
                   
        }
    }

}