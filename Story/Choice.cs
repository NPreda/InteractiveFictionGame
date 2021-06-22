
using Sirenix.OdinInspector;
using UnityEngine;


[System.Serializable]
public class Choice 
{
    //Information for the choice button and its various logic-y bits
    public Sprite choiceImage;
    public string title;
    public string body;
    public bool conditional;
    public bool contested;
    [ShowIf("conditional")] public Conditions conditions;
    [HideIf("contested")] public Result defaultResult;
    [ShowIf("contested")] public Contest contest;

}
