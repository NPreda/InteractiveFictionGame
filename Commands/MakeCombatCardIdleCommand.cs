using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeCombatCardIdleCommand : Command
{
    private IUiCard uiCard;

    public MakeCombatCardIdleCommand(IUiCard uiCard)
    {        
        this.uiCard = uiCard;
    }

    public override void StartCommandExecution()
    {
        uiCard.Idle();
        CommandExecutionComplete();
    }

}