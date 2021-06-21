using UnityEngine;

public enum HealType
{
    Health = 0,
    Resolve = 1
}

public class HealInfo : EffectComponent
{
    public HealType healType;

    public HealInfo(){}

    public HealInfo(ITarget source, ITarget target, int value, HealType healType) : base(source, target, value)
    {
        this.healType = healType;
    }  
}
