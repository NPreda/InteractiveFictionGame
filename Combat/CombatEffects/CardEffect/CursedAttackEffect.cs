
[System.Serializable]
public class CursedAttackEffect: SimpleAttackEffect //gives a status effect to the player
{
    public int statusAmount;
    public StatusType statusType;

    public override void PlayEffect(ITarget target)
    {
        base.PlayEffect(target);
        new GiveStatusEffectCommand(this.source, this.statusType, statusAmount).AddToQueue();
    }

}