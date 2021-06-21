using UnityEngine;
using Tools.UI;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(IMouseInput))]
public class ClickSizer : TweeningMover
{
    [SerializeField]float clickScale = 0.9f;
    private IMouseInput mInput;


    public override void Awake()
    {
        base.Awake();
        mInput = this.gameObject.GetComponent<UiMouseInputProvider>();
        mInput.OnPointerDown += Shrink;
        mInput.OnPointerUp += Grow;
        mInput.OnPointerExit += Grow;
    }

    private void Shrink(PointerEventData eventData)
    {
        ScaleTo(new Vector3(clickScale,clickScale,clickScale), 0.1f);
    }

    private void Grow(PointerEventData eventData)
    {
        ScaleTo(new Vector3(1,1,1), 0.1f);
    }

    
}
