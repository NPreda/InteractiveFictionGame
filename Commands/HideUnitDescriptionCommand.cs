using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUnitDescriptionCommand : Command
{    
    public HideUnitDescriptionCommand()
    {}

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.HideDescription();
        CommandExecutionComplete();
    }
}
