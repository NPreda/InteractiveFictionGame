
public class SwitchToStoryModeCommand : Command
{

    public  SwitchToStoryModeCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.DisplayStory();
        CommandExecutionComplete();
    }

}