using UnityEngine;

[System.Serializable]
public class HealHealthEffect: CardEffect
{
    private HealInfo healInfo;

    public override void PlayEffect(ITarget target)
    {
        this.healInfo = new HealInfo();
        new HealCommand(source, target, ReturnTotalEffectValue() , HealType.Health).AddToQueue();
    }

}