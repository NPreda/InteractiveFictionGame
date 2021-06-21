using UnityEngine;

public class VulnerableEffect : StatusEffect
{

    public VulnerableEffect(ITarget handler) : base(handler, StatusType.Weak, DisplayType.Debuff)
    {
        this.statusName = "Vulnerable";
        this.statusDescription = "A moment of weakness, to be´exploited and regreted.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Vulnerable");

    }

    public override DamageInfo OnAttack(DamageInfo damageInfo)
    {
        damageInfo.value = damageInfo.value + 1;
        return base.OnAttack(damageInfo);
    }

}