using UnityEngine;
using Tools.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IMouseInput))]
public class InfoPanelTrigger : MonoBehaviour
{
    private IUiUnit handler{ get; set; }
    private IMouseInput MyInput { get; set; }

    void Awake()
    {
        MyInput = gameObject.GetComponent<IMouseInput>();
    }

    public void Load(IUiUnit handler)
    {
        this.handler = handler;
    }

    private void OnEnable()
    {
        MyInput.OnPointerEnter += ShowInfoPanel;
        MyInput.OnPointerExit += HideInfoPanel;
    }

    private void OnDisable()
    {
        MyInput.OnPointerEnter -= ShowInfoPanel;
        MyInput.OnPointerExit -= HideInfoPanel;
    }

    private void ShowInfoPanel(PointerEventData pointerEventData)
    {
        if(this.handler != null)
            new ShowUnitDescriptionCommand(handler).AddToQueue();
    }   

    private void HideInfoPanel(PointerEventData pointerEventData)
    {
        new HideUnitDescriptionCommand().AddToQueue();
    }

}