using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ImpAI : UnitAI
{
    private TargetSelector targetSelector = new TargetSelector();

    public ImpAI()
    {
    }

    public override UnitEffect Run(ITarget source, List<ITarget> targets)
    {
        ITarget newTarget = targetSelector.GetTarget(targets, TargetType.Friend, TargetingMethod.Random);
        var unitEffect = new BasicAttack(source, new List<ITarget>() {newTarget}, 10, true);
        return unitEffect;
    }
    
}