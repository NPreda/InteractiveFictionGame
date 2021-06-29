
[System.Serializable]
public class RemoveStatusEffect: CardEffect
{
    public StatusType statusType;
    public int removeAmount;

    public override void PlayEffect(ITarget target)
    {
        new RemoveStatusEffectCommand(target, this.statusType, removeAmount).AddToQueue();
    }

}