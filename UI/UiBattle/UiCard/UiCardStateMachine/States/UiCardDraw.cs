using Patterns.StateMachine;
using UnityEngine;


//This state draw the collider of the card.
public class UiCardDraw : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardDraw(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        Handler.transform.localScale = new Vector3(0,0,0);
        Handler.ScaleTo(new Vector3(1f,1f,1f),0.4f, GoToIdle);
        //Handler.MoveTo(new Vector3(0f,100f,0f), 0.5f, GoToIdle);

    }


    void GoToIdle() => Handler.Idle();

}