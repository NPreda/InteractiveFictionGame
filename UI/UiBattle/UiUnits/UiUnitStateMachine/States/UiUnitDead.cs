using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiUnitDead : UiBaseUnitState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiUnitDead(IUiUnit handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        CombatBlackboard.RemoveEnemy((ITarget)Handler);
        Deathrattle();  
    }

    public override void OnExitState()
    {

    }

    //--------------------------------------------------------------------------------------------------------------

    //the animation and logic that surrounds a unit dieing
    public void Deathrattle(){
        LeanTween.color(Handler.gameObject.GetComponent<RectTransform>(), Color.black, 1f).setRecursive(true);
        LeanTween.alpha(Handler.gameObject.GetComponent<RectTransform>(), 0.0f, 2f).setRecursive(true).setOnComplete(Handler.DestroyUnit);
    }


}
