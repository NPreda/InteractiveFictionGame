using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCombatLogCommand : Command
{
    private string template;
    private ITarget source;
    private ITarget target;
    private int effectValue;
    
    private CombatLogParser combatParser = new CombatLogParser();

    public UpdateCombatLogCommand(string template, ITarget source, ITarget target, int effectValue)
    {        
        this.template = template;
        this.source = source;
        this.target = target;
        this.effectValue = effectValue;
    }

    public override void StartCommandExecution()
    {
        string parsedEntry = combatParser.ParseEntry(template, source, target, effectValue);
        SceneControl.Instance.combatSystem.AppendToLog(parsedEntry);

        CommandExecutionComplete();
    }
}
