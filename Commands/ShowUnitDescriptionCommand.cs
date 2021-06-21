using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnitDescriptionCommand : Command
{
    private IUiUnit unit;
    
    public ShowUnitDescriptionCommand(IUiUnit unit)
    {        
        this.unit = unit;
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.ShowDescription(unit);
        CommandExecutionComplete();
    }
}
