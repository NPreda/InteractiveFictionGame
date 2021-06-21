using Patterns.StateMachine;
using UnityEngine;


//This state disables the collider of the card.
public class UiCardDisable : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardDisable(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState(){
        Handler.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Handler.Fade(Parameters.DisabledAlpha,0.5f);
    }

    public override void OnExitState()
    {
        Handler.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Handler.Fade(1f,0.5f);
    }
}
