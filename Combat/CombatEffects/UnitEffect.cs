using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitEffect : CombatEffect
{
    public Sprite icon;
    public string effectName;
    public string effectDescription;
    public int effectValue;
    public int tickCost;

    public UnitEffect(ITarget source, List<ITarget> targets)
    {
        this.source = source;
        this.targets = targets;
    }

    public void Run()
    {
        base.Run(source, targets);
    }
}