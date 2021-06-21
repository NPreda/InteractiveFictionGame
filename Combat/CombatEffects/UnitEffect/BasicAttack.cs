using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : UnitEffect
{
    public DamageInfo damageInfo;

    public BasicAttack(ITarget source, List<ITarget> targets, int value,bool isDodgeable) : base(source, targets)
    {
        //Load the basic info this thing needs to be shown
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/AttackIcons/BasicAttack"); 

        this.tickCost = 1;
        this.effectName = "Attack";
        this.effectDescription = "Strike at <target> for <d> damage.";
        this.effectValue = value;

        this.isDodgeable = isDodgeable;
        damageInfo = new DamageInfo();
        damageInfo.damageType = DamageType.Standard;
        this.damageInfo.value = value;
    }


    public override void PlayEffect(ITarget target)
    {
        this.damageInfo.source = source;
        this.damageInfo.target = target;
        int damageModifier = source.damageModifier;
        damageInfo.value = damageInfo.value + damageModifier;
        new DealDamageCommand(damageInfo).AddToQueue();
    }

}
