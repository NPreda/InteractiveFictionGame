using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogCommand : Command
{   
    ITarget target;
    string dialogText;

    public SpawnDialogCommand(ITarget target, string dialogText)
    {        
        this.target = target;
        this.dialogText = dialogText;
    }

    public override void StartCommandExecution()
    {
        var prefab = Resources.Load("Prefabs/BattleUI/DialogObject");
        var floater = GameObject.Instantiate(prefab,target.gameObject.transform) as GameObject;
        floater.transform.localPosition = new Vector3(0,0,0);
        PopUpDialog dScript = floater.GetComponent<PopUpDialog>();
        dScript.Load(dialogText);
        CommandExecutionComplete();
    }
}
