using UnityEngine;

public enum DamageType{
    Standard = 0,
    Magical = 1,
    Mental = 2,
    Debuff = 3
}

public class DamageInfo : EffectComponent
{
    public DamageType damageType;

    public DamageInfo(){}

    public DamageInfo(ITarget source, ITarget target, int value, DamageType damageType) : base(source, target, value)
    {
        this.damageType = damageType;
    }
}
