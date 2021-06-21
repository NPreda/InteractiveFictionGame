using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIPressButton: UIButton
{

    protected override void OnClick(PointerEventData eventData)
    {
        //to prevent shennanigans were the user enter the button with the button already down
        if(_isSelected){
            Deselect();
            BroadcastClick();  
        } 
    }

    protected void OnPressed(PointerEventData eventData)
    {
        Select();
    }

    protected void OnCanceled(PointerEventData eventData)
    {
        Deselect();
    }

    //--------------------------------------------------------------------------------------------------
    #region VisualControl

    public override void Select(){
        _isSelected = true;
        _isDirty = true;
    }

    public  override void Deselect(){
        _isSelected = false;
        _isDirty = true;
    } 

    #endregion
    //--------------------------------------------------------------------------------------------------
    #region EventSubscriptionControls

    protected override void Subscribe()
    {
        mInput.OnPointerDown += OnPressed;
        mInput.OnPointerUp += OnClick;
        mInput.OnPointerExit += OnCanceled;
    }
    
    
    protected override void Unsubscribe()
    {
        mInput.OnPointerDown -= OnPressed;
        mInput.OnPointerUp -= OnClick;
        mInput.OnPointerExit -= OnCanceled;
    }

    #endregion
    //--------------------------------------------------------------------------------------------------
}