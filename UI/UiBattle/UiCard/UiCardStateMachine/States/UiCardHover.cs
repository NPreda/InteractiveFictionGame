using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiCardHover : UiBaseCardState
{
        //--------------------------------------------------------------------------------------------------------------

        public UiCardHover(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
        {
        }

        //--------------------------------------------------------------------------------------------------------------


        void OnPointerExit(PointerEventData obj)
        {
            UnsubscribeInput();
            var hoverSpeed = Parameters.HoverSpeed;
            Handler.ScaleTo(Handler.startScale,Parameters.ScaleSpeed);
            Handler.RotateTo(Handler.startRotation,Parameters.RotationSpeed);
            Handler.MoveTo(Handler.startPosition,Parameters.MovementSpeed, GoToIdle);
        }

        void OnPointerDown(PointerEventData obj)
        {
            GoToSelect(); 
        }


        //--------------------------------------------------------------------------------------------------------------

        public override void OnEnterState()
        {
            Handler.CacheValues();
            SubscribeInput();
            var scaleFactor = new Vector3(Parameters.HoverScale,Parameters.HoverScale,Parameters.HoverScale);
            Handler.ScaleTo(scaleFactor,Parameters.ScaleSpeed);
            Handler.RotateTo(Vector3.zero, Parameters.MovementSpeed);
            Handler.MoveTo(Handler.startPosition + new Vector3(0f,Parameters.HoverHeight,0f),Parameters.MovementSpeed);
        }

        public override void OnExitState()
        {
            UnsubscribeInput();
        }

        void GoToIdle() => Handler.Idle();
        void GoToSelect() => Handler.Select();

        //--------------------------------------------------------------------------------------------------------------

        void SubscribeInput()
        {
            Handler.Input.OnPointerExit += OnPointerExit;
            Handler.Input.OnPointerDown += OnPointerDown;

        }

        void UnsubscribeInput()
        {
            Handler.Input.OnPointerExit -= OnPointerExit;
            Handler.Input.OnPointerDown -= OnPointerDown;

        }

        //--------------------------------------------------------------------------------------------------------------
}
