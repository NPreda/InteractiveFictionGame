
[System.Serializable]
public class GiveStatusEffect: CardEffect
{
    public StatusType statusType;

    public override void PlayEffect(ITarget target)
    {
        new GiveStatusEffectCommand(target, this.statusType, ReturnTotalEffectValue() ).AddToQueue();
    }

}