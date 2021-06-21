using UnityEngine;
using System.Collections.Generic;

public interface ITarget
{
    bool isPlayer{get;}

    string unitName{get; }

    TargetType allegiance {get; }
    
    GameObject gameObject { get; }

    bool finishedTurn{ get; set;}

    List<StatusEffect> statusEffects{get; set;}

    void GiveStatus(StatusType statusType, int givenValue);

    void RemoveStatus(StatusType statusType);

    void RemoveAllStatuses();

    int ReturnSpeed();

    void OnAttack(DamageInfo damageInfo);

    void OnHeal(HealInfo healInfo);

    void OnArmor(ArmorInfo armorInfo);

    void TakeDamage(int damage);

    void PlayTurn();

    void FinishTurn();

    void Targeted();

    void Die();

    void AnimateDodge();

    void Bark();

    int damageModifier{get; set;}
    
    int speedModifier{get;set;}

    int strength{get;set;}
}