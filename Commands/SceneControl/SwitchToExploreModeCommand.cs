using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchToExploreModeCommand : Command
{
    private Storylet storylet;

    public  SwitchToExploreModeCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.DisplayExplore();
        CommandExecutionComplete();
    }

}