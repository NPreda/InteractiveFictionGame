using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RandomAttackEffect: SimpleAttackEffect
{

    [SerializeField]private int targetNumber;

    public override void Run(ITarget source, List<ITarget> targets)
    {
        this.source = source;
        this.targets = new List<ITarget>();
        for(int i = 0; i < targetNumber; i++)
        {
            Debug.Log("This is the amount of targets: " + targets.Count);
            int index = UnityEngine.Random.Range(0, targets.Count);
            this.targets.Add(targets[index]);
        }

        foreach(var target in this.targets)
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

}