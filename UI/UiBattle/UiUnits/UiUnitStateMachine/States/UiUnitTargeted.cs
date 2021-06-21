using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiUnitTargeted : UiBaseUnitState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiUnitTargeted(IUiUnit handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        SubscribeInput();
        Handler.setGlowActive(true);
    }

    public override void OnExitState()
    {
        Handler.setGlowActive(false);
        UnsubscribeInput();
    }

    //--------------------------------------------------------------------------------------------------------------


    void SubscribeInput()
    {
        Handler.Input.OnPointerUp += StopTargetBehaviour;
        Handler.Input.OnPointerExit += StopTargetBehaviour;
    }

    void UnsubscribeInput()
    {
        Handler.Input.OnPointerUp -= StopTargetBehaviour;
        Handler.Input.OnPointerExit -= StopTargetBehaviour;
    }

    void GoToIdle() => Handler.Idle();
    //--------------------------------------------------------------------------------------------------------------



    void StopTargetBehaviour(PointerEventData pointerEventData)
    {
         GoToIdle();
    }
}
