using Patterns.StateMachine;
using UnityEngine;


//State when a cards has been discarded.
public class UiCardDiscard : UiBaseCardState
{
        //--------------------------------------------------------------------------------------------------------------

        public UiCardDiscard(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
        {
        }

        //--------------------------------------------------------------------------------------------------------------


        #region Operations

        public override void OnEnterState()
        {
            Disable();
            Handler.RotateTo(new Vector3(0,0,-90f), 0.4f);
            Handler.ScaleTo(Vector3.zero, 0.4f, Handler.DestroyCard);
        }
        

        #endregion
}
