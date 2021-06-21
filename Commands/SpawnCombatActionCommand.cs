using UnityEngine;

public class SpawnCombatActionCommand : Command
{
    private ActionFactory factory =  new ActionFactory(); 
    
    private IUiUnit unit;
    private UnitEffect unitEffect;

    public SpawnCombatActionCommand(IUiUnit unit, UnitEffect unitEffect)
    {     
        this.unit = unit;
        this.unitEffect = unitEffect;
    }

    public override void StartCommandExecution()
    {
        var inst = factory.GetNewInstance(unit.gameObject);
        var script = inst.GetComponent<CombatAction>();
        unit.combatActions.Add(script);
        script.Load(unitEffect, unit);
        CommandExecutionComplete();
    }

}
