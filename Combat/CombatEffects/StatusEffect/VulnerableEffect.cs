using UnityEngine;

public class VulnerableEffect : StatusEffect
{

    public VulnerableEffect(ITarget handler) : base(handler, StatusType.Vulnerable, DisplayType.Debuff)
    {
        this.statusName = "Vulnerable";
        this.statusDescription = "A moment of weakness, to be´exploited and regreted.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Vulnerable");
        this.visibleValue = true;

    }


    public override void OnApply()
    {
        base.OnApply();
    }

    public override DamageInfo OnAttack(DamageInfo damageInfo)
    {
        damageInfo.value = damageInfo.value + stack;
        return base.OnAttack(damageInfo);
    }

    public override void OnTurnStart()
    {
        this.stack--;
    }
}