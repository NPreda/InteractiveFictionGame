using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CombatEffect
{
    private const float DodgeConstant = 70f;
    public bool isDodgeable;

    //information to be populated on creation
    public ITarget source;
    public List<ITarget> targets;


    public virtual void Run(ITarget source, List<ITarget> targets)
    {
        this.source = source;
        this.targets = targets;

        foreach(var target in targets)
        {
            if (isDodgeable){
                if (EffectDodged(source, target))
                {
                    new TriggerDodgeEffectsCommand(source, target).AddToQueue();
                }else{
                    PlayEffect(target);
                }
            }else{
                PlayEffect(target);
            }
        }

    }

    public virtual void PlayEffect(ITarget target){

    }

    private bool EffectDodged(ITarget source, ITarget target)
    {
        float diceResult = Random.Range(0f, 1f);
        if (diceResult >= CalculateHitChance(source.ReturnSpeed(),target.ReturnSpeed()))
        {
            return true;
        }else{
            return false;
        }
    }

    private float CalculateHitChance(int attackerSpeed, int defenderSpeed)
    {
        float hitChance = (DodgeConstant/100f) * (attackerSpeed/defenderSpeed);
        return hitChance;
    }
}