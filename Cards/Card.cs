using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Card", menuName= "Cards/Basic")]
public class Card: ScriptableObject
{
    [HideInInspector]public string id{get => name;}
    public string title;
    public string description;
    public Sprite image;
    public bool isSingleUse;
    [HideInInspector]public UiCardSkin skin;
    public int quantity;
    
    public bool conditional;
    [ShowIf("conditional")] public Conditions conditions;

}
