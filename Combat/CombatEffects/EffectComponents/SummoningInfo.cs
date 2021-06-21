using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningInfo : EffectComponent
{
    Unit unit;

    public SummoningInfo(ITarget source, ITarget target, int value, Unit unit) : base(source, target, value)
    {
        this.unit = unit;
    }

}
