using UnityEngine;
using UnityEngine.UI;

//This state disables the collider of the card.
public class UiCardDissolve : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardDissolve(IUiCard handler, UiCardParameters parameters) : base(handler, parameters)
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState(){
        Handler.CancelAnimation();
        Vector3 centerScreen = new Vector3 (0, Screen.height * 0.5f, 0);
        Handler.MoveTo(centerScreen, 0.5f, Extinguish);
    
    }

    public override void OnExitState()
    {
    }

    public void Extinguish() => LeanTween.value(Handler.gameObject, DissolveEffect, 0.8f, 0.3f, Parameters.DissolveSpeed).setOnComplete(Handler.DestroyCard);    //make more generic later

        
    public void DissolveEffect(float dissolveLevel)       //note that dissolve level goes from 1 to 0, with zero being fully dissolved
    {
        var mat = Handler.CardFace.GetComponent<Image>().material;  //get the material instance
        mat.SetFloat("_Level", dissolveLevel);
    }
}
