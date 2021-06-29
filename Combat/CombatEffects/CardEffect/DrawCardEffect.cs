
[System.Serializable]
public class DrawCardEffect: CardEffect
{
    public override void PlayEffect(ITarget target)
    {
        for(int i = 0 ; i < ReturnTotalEffectValue(); i++ )
            new DrawCombatCardCommand().AddToQueue();
    }

}