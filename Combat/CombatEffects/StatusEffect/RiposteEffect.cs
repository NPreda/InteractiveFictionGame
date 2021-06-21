using UnityEngine;

public class RiposteEffect : StatusEffect
{

    public RiposteEffect(ITarget handler) : base(handler, StatusType.Riposte, DisplayType.Buff)
    {
        this.statusName = "Riposte";
        this.statusDescription = "Triumph turned to sorrow. 5 Dammage for the trouble.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Riposte");
        this.visibleValue = true;
    }


    public override void OnTurnStart()
    {
        this.stack--;
        base.OnTurnStart();
    }


    public override DamageInfo OnAttack(DamageInfo damageInfo)
    {
        DamageInfo riposteDamage = new DamageInfo();
        riposteDamage.target = damageInfo.source;
        riposteDamage.source = damageInfo.target;
        riposteDamage.damageType = DamageType.Standard;
        riposteDamage.value = 5;

        new DealDamageCommand(riposteDamage).AddToQueue();

        return base.OnAttack(damageInfo);
    }


}