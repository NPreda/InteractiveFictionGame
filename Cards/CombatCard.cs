
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName= "Cards/Combat")]
public class CombatCard : Card
{
    public int energyCost;
    public EquippableQuality item;
    public TargetType targetType;
    public CombatCardType combatCardType;
    [SerializeReference]public CardEffect cardEffect;
}
