
using Sirenix.OdinInspector;


[System.Serializable]
public class Choice 
{
    //Information for the choice button and its various logic-y bits
    public string title;
    public string body;
    public bool conditional;
    public bool contested;
    [EnableIf("conditional")] public Conditions conditions;
    [DisableIf("contested")] public Result defaultResult;
    [EnableIf("contested")] public Contest contest;

}
