using UnityEngine;
using System.Collections;

public class TriggerDodgeEffectsCommand : Command
{
    private ITarget source;
    private ITarget target;

    public TriggerDodgeEffectsCommand(ITarget source, ITarget target)
    {        
        this.source = source;
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        target.AnimateDodge();
        new UpdateCombatLogCommand("<target> dodged <source>'s attack.", source, target, 0).AddToQueue();
        CommandExecutionComplete();
    }

}
