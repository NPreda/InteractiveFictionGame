
[System.Serializable]
public class ModifiedAttackEffect: SimpleAttackEffect
{
    public int statusAmount;
    public StatusType statusType;

    public override void PlayEffect(ITarget target)
    {
        base.PlayEffect(target);
        new GiveStatusEffectCommand(target, this.statusType, statusAmount).AddToQueue();
    }

}