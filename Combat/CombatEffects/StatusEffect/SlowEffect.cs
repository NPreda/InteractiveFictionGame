using UnityEngine;

public class SlowEffect : StatusEffect
{

    public SlowEffect(ITarget handler) : base(handler, StatusType.Slow, DisplayType.Debuff)
    {
        this.statusName = "Slow";
        this.statusDescription = "Slows you down";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Slow");
        this.visibleValue = true;

    }

    public override void OnApply()
    {
        handler.speedModifier = handler.speedModifier - 10;
        base.OnApply();
    }

    public override void OnTurnStart()
    {
        this.stack--;
        if(this.stack > 0)
            handler.speedModifier = handler.speedModifier - 10;
    }


}