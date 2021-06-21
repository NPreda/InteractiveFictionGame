using UnityEngine;
using System.Collections;

public class TriggerDeathCommand : Command
{
    private ITarget target;

    public TriggerDeathCommand(ITarget target)
    {        
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        if(target.isPlayer)
            new TriggerLossStateCommand().AddToQueue();
        else if (CombatBlackboard.Enemies.Count == 1)
            new TriggerWinStateCommand().AddToQueue();
        else
            target.Die();
        CommandExecutionComplete();
    }

}
