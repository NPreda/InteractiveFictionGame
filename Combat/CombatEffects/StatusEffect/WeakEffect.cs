using UnityEngine;

public class WeakEffect : StatusEffect
{

    public WeakEffect(ITarget handler) : base(handler, StatusType.Weak, DisplayType.Debuff)
    {
        this.statusName = "Weak";
        this.statusDescription = "Their strength is not longer in it, their body or soul uncertain.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Weak");
        this.visibleValue = true;

    }

    public override void OnTurnStart()
    {
        this.stack--;
        if(this.stack > 0)
            handler.damageModifier = handler.damageModifier - 1;
    }

}