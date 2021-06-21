using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


//This state disables the collider of the card.
public class UiCardArrow : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardArrow(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
    {
    }

    //--------------------------------------------------------------------------------------------------------------
    public float arrowheadSize = 1;
    Vector3 startPosition, mouseWorld;
    LineRenderer arrowLine;

    public override void OnEnterState()
    {
        //initiate properties
        this.arrowLine = Handler.LineRenderer;
        //this.mouseWorld = new Vector3 ();


        new SelectCombatCardCommand(Handler).AddToQueue();

        SubscribeInput();
        CreateArrow();
    }

    public override void OnExitState()
    {
        UnsubscribeInput();
        Handler.ScaleTo(Handler.startScale,Parameters.ScaleSpeed);
        Handler.RotateTo(Handler.startRotation,Parameters.RotationSpeed);
        Handler.MoveTo(Handler.startPosition,Parameters.MovementSpeed);
        new UnselectCardCommand().AddToQueue();
    }

    public override void OnUpdate() => DrawArrow();


    public void CreateArrow(){
        mouseWorld = Camera.main.ScreenToWorldPoint (
        new Vector3 (Input.mousePosition.x,
        Input.mousePosition.y,
        9
        ));
        startPosition = mouseWorld;
        this.arrowLine.enabled = true;
    }

    void DrawArrow(){
        mouseWorld = Camera.main.ScreenToWorldPoint (
        new Vector3 (Input.mousePosition.x,
        Input.mousePosition.y,
        10
        ));
        //The longer the line gets, the smaller relative to the entire line the arrowhead should be
        float percentSize = (float) (arrowheadSize / Vector3.Distance (startPosition, mouseWorld));
        //h/t ShawnFeatherly (http://answers.unity.com/answers/1330338/view.html)
        arrowLine.SetPosition (0, startPosition);
        arrowLine.SetPosition (1, Vector3.Lerp(startPosition, mouseWorld, 0.999f - percentSize));
        arrowLine.SetPosition (2, Vector3.Lerp (startPosition, mouseWorld, 1 - percentSize));
        arrowLine.SetPosition (3, mouseWorld);
        arrowLine.widthCurve = new AnimationCurve (

        new Keyframe (0, 0.4f),
        new Keyframe (0.999f - percentSize, 0.4f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1, 0f));
    }

    public void OnPointerUp(PointerEventData pointerEventData){
        //Turn off the arrow
        arrowLine.enabled = false;

        //get the object underneath and send the new command to trigger the effect
        GameObject selected = pointerEventData.pointerCurrentRaycast.gameObject;
        if (selected){
            //Debug.Log("OBJECT SELECTED:" + selected);
            ITarget enemyTarget = selected.GetComponentInParent<ITarget>(); 
            CombatCard usedCard = (CombatCard) Handler.returnCard();
            if (enemyTarget != null && usedCard.targetType == TargetType.Friend)
            {
                if(enemyTarget.allegiance == TargetType.Friend || enemyTarget.allegiance == TargetType.Player)
                    new PlayCombatCardCommand(enemyTarget, Handler).AddToQueue();
            }else if(enemyTarget != null && usedCard.targetType == enemyTarget.allegiance){
                new PlayCombatCardCommand(enemyTarget , Handler).AddToQueue();
            }
        }

        GoToIdle();
    }
    
    void SubscribeInput()
    {
        Handler.Input.OnPointerUp += OnPointerUp;
    }

    void UnsubscribeInput()
    {
        Handler.Input.OnPointerUp -= OnPointerUp;
    }

    void GoToIdle() => Handler.Idle();
}
