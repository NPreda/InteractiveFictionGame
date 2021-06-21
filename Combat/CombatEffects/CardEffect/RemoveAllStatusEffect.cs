
[System.Serializable]
public class RemoveAllStatusEffect: CardEffect
{
    public override void PlayEffect(ITarget target)
    {
        new RemoveAllStatusEffectsCommand(target).AddToQueue();
    }

}