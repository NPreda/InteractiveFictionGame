using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiCardIdle : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardIdle(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
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

    
    void SubscribeInput()
    {
        Handler.Input.OnPointerEnter += OnPointerEnter;
        Handler.Input.OnPointerClick += OnPointerClick;
    }

    void UnsubscribeInput()
    {
        Handler.Input.OnPointerEnter -= OnPointerEnter;
        Handler.Input.OnPointerClick -= OnPointerClick;
    }

    //--------------------------------------------------------------------------------------------------------------

    void OnPointerEnter(PointerEventData obj)
    {  
        if (!(Handler.IsAnimating()))
            Handler.Hover();
    }

    void OnPointerClick(PointerEventData obj)
    {  
        if (!(Handler.IsAnimating()))
            Handler.Hover();
    }

}
