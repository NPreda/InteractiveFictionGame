using System;
using System.Threading;

public class WaitCommand : Command
{
    private float waitTime;

    public WaitCommand(float waitTime)
    {        
        this.waitTime = waitTime;
    }

    public override void StartCommandExecution()
    {
        
        LeanTween.delayedCall(waitTime, CommandExecutionComplete);
    }

}
