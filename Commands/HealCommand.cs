using UnityEngine;
using System.Collections;

public class HealCommand : Command
{
    private ITarget target;
    private HealInfo healInfo;

    public HealCommand(ITarget source, ITarget target,int effectValue, HealType healType)
    {        
        this.target = target;

        this.healInfo = new HealInfo();
        this.healInfo.source = source;
        this.healInfo.target = target;
        this.healInfo.value = effectValue;
        this.healInfo.healType = healType;
    }

    public override void StartCommandExecution()
    {
        target.OnHeal(healInfo);
        CommandExecutionComplete();
    }

}
