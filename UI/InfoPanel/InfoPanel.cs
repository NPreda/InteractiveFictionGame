using Tools.UI;
using UnityEngine;
using Sirenix.OdinInspector;

public class InfoPanel : TweeningMover
{
    [BoxGroup("Button Set")]
    [SerializeField]private UISimplePressButton closeButton;
    [BoxGroup("Button Set")]
    [SerializeField]private InfoButtonSet buttonSet;

    [BoxGroup("Panel Set")]
    [SerializeField]private TweeningMover uiCharacter;
    [BoxGroup("Panel Set")]
    [SerializeField]private TweeningMover uiInventory;
    [BoxGroup("Panel Set")]
    [SerializeField]private TweeningMover uiEquipment;

    public override void Awake()
    {
        base.Awake();
        GameManager.BackendInitialized +=  Setup;
        closeButton.OnClickEvent += Close;
    }

    public override void Enable()
    {
        MoveTo(new Vector3(0, 0, 0), 0.5f);
        Fade(0.5f, 1f, base.Enable);
    }

    public override void Disable()
    {
        ClosePanels();
        buttonSet.DeselectAll();
        base.canvasGroup.blocksRaycasts = false;
        MoveTo(new Vector3(50, 0, 0), 0.5f);
        Fade(0f, 1f, base.Disable);
    }

    public void Close(UIButton button)
    {
        Disable();
    }

    private void Setup()
    {
        ClosePanels();
        Disable();

        buttonSet.OnButtonPressed += SwitchPanels;
    }

    private void ClosePanels()
    {
        uiCharacter.Disable();
        uiInventory.Disable();
        uiEquipment.Disable();
    }

    private void SwitchPanels(UIButton button)
    {
        if(!isActive) Enable();

        switch(button.id)
        {
            case "character":
                uiCharacter.Enable();
                uiInventory.Disable();
                uiEquipment.Disable();
                break;
            case "inventory":
                uiCharacter.Disable();
                uiInventory.Enable();
                uiEquipment.Disable();
                break;
            case "equipment":
                uiCharacter.Disable();
                uiInventory.Disable();
                uiEquipment.Enable();
                break;
            default:
                throw new System.Exception("Invalid info button was pressed of the type: " + button.id);
        }
    }

}