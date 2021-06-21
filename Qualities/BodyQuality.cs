using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bodyType{
    Hair,
    Eyes,
    Ears,
    Face,
    Chest,
    Nethers,
    Legs,
    Hands
}

public class BodyQuality : Quality
{
    public bodyType bodyType;
    [Space]
    public int bonusMight;
    public int bonusFinesse;
    public int bonusWits;
    [Space]
    public int bonusHealth;
    public int bonusStamina;
    public int bonusWill;

    public override string ReturnDescription()
    {
        throw new System.NotImplementedException();
    }

}
