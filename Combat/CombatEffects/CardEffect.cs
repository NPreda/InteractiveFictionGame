using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CardEffect : CombatEffect
{
    [SerializeField]protected int effectValue;
    [SerializeField]protected CardStrengthType strengthType;

    public CardStrengthType ReturnStrengthType() => strengthType;

    public void Setup()         //another workaround to get the stat-based damage bonus for the description
    {
        this.source = CombatBlackboard.Player;
    }

    public int ReturnTotalEffectValue()
    {
        return effectValue + ReturnEffectValueBonus();
    }

    protected virtual int ReturnEffectValueBonus()
    {
        return ReturnScaleValue(strengthType);
    }

    //an overly complicated and bullshity way to get what we need to calculate the strength bonus
    protected int ReturnScaleValue(CardStrengthType sType)
    {

        //now lets get the actual strength depending on what sort of card we're dealing with
        int baseStat = 0;
        switch(sType)
        {
            case CardStrengthType.Standard:
                baseStat = 0;
                break;
            case CardStrengthType.Might:
                baseStat = Character.Instance.ReturnStatPacket("Might").value; 
                break;
            case CardStrengthType.Finesse:
                baseStat = Character.Instance.ReturnStatPacket("Finesse").value; 
                break;
            case CardStrengthType.Presence:
                baseStat = Character.Instance.ReturnStatPacket("Presence").value; 
                break;
            default:
                baseStat = 0;
                break;
        }

        int extraDamage = (int)Mathf.Round(baseStat/10f);
        return extraDamage;
    }
}