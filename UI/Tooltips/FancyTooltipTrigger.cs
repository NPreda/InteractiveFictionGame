using UnityEngine;
using Tools.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(IMouseInput))]
public class FancyTooltipTrigger : SimpleTooltipTrigger
{
    [SerializeField] 
    [TextArea(3,3)]
    protected string tooltipContent;

    public override void Start()
    {
        MyInput = gameObject.GetComponent<IMouseInput>();
        this.tooltip = GameManager.Instance.returnFancyTooltip();
    }

    public void SetContent(string title, string body)
    {
        tooltipTitle = title;
        tooltipContent = body;
    }

    protected override void ShowTooltip(PointerEventData pointerEventData)
    {
        tooltip.ShowTooltip(tooltipTitle, tooltipContent);
    }

}
