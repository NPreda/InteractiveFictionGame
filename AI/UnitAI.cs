using System.Collections.Generic;
using UnityEngine;

public class UnitAI 
{
    private TargetSelector targetSelector = new TargetSelector();

    public virtual UnitEffect Run(ITarget source, List<ITarget> targets)
    {
        return null;
    }

}
