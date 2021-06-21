using UnityEngine;

public class BleedEffect : StatusEffect
{

    public BleedEffect(ITarget handler) : base(handler, StatusType.Bleed, DisplayType.Debuff)
    {
        this.visibleValue = true;
        this.statusName = "Bleed";
        this.statusDescription = "The life is drawing out of them, slowly but surely.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Bleed");

    }

    public override void OnTurnStart()
    {
        handler.TakeDamage(2);
        new UpdateCombatLogCommand("<source> has bleed  for <v> damage.", handler, handler, 2).AddToQueue();
        this.stack--;
    }

}