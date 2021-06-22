using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchToCombatModeCommand : Command
{
    private CombatTrigger combatTrigger;

    public  SwitchToCombatModeCommand(CombatTrigger combatTrigger)
    {        
        this.combatTrigger = combatTrigger;
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.DisplayCombat(combatTrigger);
        CommandExecutionComplete();
    }

}