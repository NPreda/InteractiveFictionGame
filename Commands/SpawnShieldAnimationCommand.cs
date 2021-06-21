using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShieldAnimationCommand : Command
{   
    ITarget target;

    public SpawnShieldAnimationCommand(ITarget target)
    {        
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        var prefab = Resources.Load("Prefabs/BattleUI/ShieldFloat");
        var floater = GameObject.Instantiate(prefab,target.gameObject.transform) as GameObject;
        floater.transform.localPosition = new Vector3(0,0,0);
        CommandExecutionComplete();
    }
}
