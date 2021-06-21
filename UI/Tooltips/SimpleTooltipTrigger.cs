using UnityEngine;
using Tools.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(IMouseInput))]
public class SimpleTooltipTrigger : MonoBehaviour
{
    [SerializeField] protected string tooltipTitle;
    protected IMouseInput MyInput { get; set; }
    protected SimpleTooltip tooltip;

    public virtual void Start()
    {
        MyInput = gameObject.GetComponent<IMouseInput>();
        this.tooltip = GameManager.Instance.returnSimpleTooltip();
    }

    private void OnEnable()
    {
        this.Start();
        MyInput.OnPointerEnter += ShowTooltip;
        MyInput.OnPointerExit += HideTooltip;
    }

    private void OnDisable()
    {
        this.Start();
        MyInput.OnPointerEnter -= ShowTooltip;
        MyInput.OnPointerExit -= HideTooltip;
        this.tooltip.HideTooltip();
    }

    public virtual void SetContent(string title)
    {
        tooltipTitle = title;
    }

    protected virtual void ShowTooltip(PointerEventData pointerEventData)
    {
        tooltip.ShowTooltip(tooltipTitle);
    }

    protected void HideTooltip(PointerEventData pointerEventData)
    {
       this.tooltip.HideTooltip();
    }

}
