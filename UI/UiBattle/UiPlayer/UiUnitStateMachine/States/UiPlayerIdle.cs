using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiPlayerIdle : UiBasePlayerState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiPlayerIdle(IUiPlayer handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        SubscribeInput();
    }

    public override void OnExitState()
    {
        UnsubscribeInput();

    }

    //--------------------------------------------------------------------------------------------------------------


    private void SubscribeInput()
    {
        Handler.Input.OnPointerEnter += StartTargetBehaviour;
    }

    private void UnsubscribeInput()
    {
        Handler.Input.OnPointerEnter -= StartTargetBehaviour;
    }

    //--------------------------------------------------------------------------------------------------------------


    void StartTargetBehaviour(PointerEventData pointerEventData)
    {
        new TriggerTargetBehaviourCommand((ITarget) Handler).AddToQueue();
    }

}
