using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunCombatEffectsCommand : Command
{
    private ITarget attacker;
    private List<ITarget> targets;
    private CombatEffect combatEffect;

    public RunCombatEffectsCommand(ITarget attacker, List<ITarget> targets, CombatEffect combatEffect)
    {        
        this.targets = targets;
        this.attacker = attacker;
        this.combatEffect = combatEffect;
    }

    public override void StartCommandExecution()
    {
        combatEffect.Run(this.attacker, this.targets);
        CommandExecutionComplete();
    }

}
