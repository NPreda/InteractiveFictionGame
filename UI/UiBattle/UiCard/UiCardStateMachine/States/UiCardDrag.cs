using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//Combat Card specific state
public class UiCardDrag : UiBaseCardState
{
        //--------------------------------------------------------------------------------------------------------------

        public UiCardDrag(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
        {
        }
        //--------------------------------------------------------------------------------------------------------------

        void FollowCursor()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Handler.transform.position = worldPosition;  
        }

        //--------------------------------------------------------------------------------------------------------------

        public override void OnUpdate() => FollowCursor();

        public override void OnEnterState()
        {
            new SelectCombatCardCommand(Handler).AddToQueue();

            SubscribeInput();
            var scaleFactor = new Vector3(Parameters.HoverScale,Parameters.HoverScale,Parameters.HoverScale);
            Handler.ScaleTo(scaleFactor,Parameters.ScaleSpeed);
            Handler.RotateTo(Vector3.zero, Parameters.RotationSpeed);
            Handler.MoveTo(Handler.startPosition + new Vector3(0f,Parameters.HoverHeight,0f),Parameters.MovementSpeed);
        }

        public override void OnExitState()
        {
            UnsubscribeInput();
            RestorePosition();
            new UnselectCardCommand().AddToQueue();
        }



        void GoToIdle() => Handler.Idle();
        //--------------------------------------------------------------------------------------------------------------

        void SubscribeInput()
        {
            Handler.Input.OnPointerUp += OnPointerUp;
        }

        void UnsubscribeInput()
        {
            Handler.Input.OnPointerUp -= OnPointerUp;
        }

        void RestorePosition(){
            Handler.ScaleTo(Handler.startScale,Parameters.ScaleSpeed);
            Handler.RotateTo(Handler.startRotation,Parameters.RotationSpeed);
            Handler.MoveTo(Handler.startPosition,Parameters.MovementSpeed);
        }


        void OnPointerUp(PointerEventData pointerEventData)
        {
            //get the object underneath and send the new command to trigger the effect
            GameObject selected = pointerEventData.pointerCurrentRaycast.gameObject;
            if ((selected == null) || (selected.gameObject.name != "HandPanel")){
                var usedCard = (CombatCard)  Handler.returnCard();
                new PlayCombatCardCommand(usedCard.targetType, Handler).AddToQueue();
            }else{
                GoToIdle();
            }
        }
}
