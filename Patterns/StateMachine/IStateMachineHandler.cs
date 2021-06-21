namespace Patterns.StateMachine
{
    //Handler for the FSM
    //Usually a class which holds the FSM

    public interface IStateMachineHandler
    {
        string Name {get;}
    }
}