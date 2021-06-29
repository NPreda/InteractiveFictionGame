using UnityEngine;

[System.Serializable]
public class RemoveStatusDrawCardEffect: RemoveStatusEffect
{
    [SerializeField]private int cardAmount;

    public override void PlayEffect(ITarget target)
    {
        for(int i = 0 ; i < cardAmount; i++ )
            new DrawCombatCardCommand().AddToQueue();
    }

}