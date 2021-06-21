using UnityEngine;

[System.Serializable]
public class ArmorEffect : CardEffect
{
    private ArmorInfo armorInfo;

    public override void PlayEffect(ITarget target)
    {
        new GiveArmorCommand(source, target,ReturnTotalEffectValue()).AddToQueue();
    }
}
