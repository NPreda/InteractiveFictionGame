using System.Collections.Generic;
using System;
using UnityEngine;

public enum TargetingMethod
{
    Random = 0,
}

public class TargetSelector 
{

    public ITarget GetTarget(List<ITarget> targetList, TargetType targetType, TargetingMethod targetingMethod)
    {
        List<ITarget> validTargets = GetValidTargets(targetList, targetType);
        ITarget target;
        switch(targetingMethod)
        {
            case TargetingMethod.Random:
                int index = UnityEngine.Random.Range(0, validTargets.Count);
                target=validTargets[index];
                break;
            default:
                throw new ArgumentException(string.Format("Targeting method used is not recognized by the TargetSelector: '{0}' ", targetingMethod));
        }

        return target;
    }

    //seemingly simple, but since the player is both its own thing and abstractly a member of the "Friend group", needs a bit more logic
    private List<ITarget> GetValidTargets(List<ITarget> targetList, TargetType targetType)
    {
        List<ITarget> validTargets = new List<ITarget>();
        switch(targetType)
        {
            case TargetType.Friend:
                foreach(var target in targetList)
                {
                    if(target.allegiance == TargetType.Player || target.allegiance == TargetType.Friend)
                        validTargets.Add(target);
                }
                break;
            default:
                foreach(var target in targetList)
                {
                    if(target.allegiance == targetType)
                        validTargets.Add(target);
                }     
                break;           
        }

        return validTargets;
    }
}
