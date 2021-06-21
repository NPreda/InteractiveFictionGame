using UnityEngine;

public class CardingInfo : EffectComponent
{
    CombatCard combatCard;

    public CardingInfo(ITarget source, ITarget target, int value, CombatCard combatCard) : base(source, target, value)
    {
        this.combatCard = combatCard;
    }
}
