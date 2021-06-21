using UnityEngine;

public class HasteEffect : StatusEffect
{

    public HasteEffect(ITarget handler) : base(handler, StatusType.Haste, DisplayType.Buff)
    {
        this.statusName = "Haste";
        this.statusDescription = "Faster. not stronger or better, just faster.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Haste");
        this.visibleValue = true;

    }

    public override void OnApply()
    {
        handler.speedModifier = handler.speedModifier + 10;
        base.OnApply();
    }

    public override void OnTurnStart()
    {
        this.stack--;
        if(this.stack > 0)
            handler.speedModifier = handler.speedModifier + 10;
    }
}