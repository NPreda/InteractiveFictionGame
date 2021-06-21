using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Tools.UI;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class ClickSpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Sprite _icon;

    [SerializeField] private Sprite idleIcon;
    [SerializeField] private Sprite pressedIcon;
    private IMouseInput mInput;

    private bool _isForced = false;
    private bool _isSelected = false;
    private bool _isDirty = true;

    void Awake()
    {
        mInput = this.gameObject.GetComponent<UiMouseInputProvider>();
        mInput.OnPointerDown += PressSprite;
        mInput.OnPointerUp += IdleSprite;
        mInput.OnPointerExit += IdleSprite;
    }

    public void ForceSelect()
    {
        _isForced = true;
        _isDirty = true;
    }

    public void UnforceSelect()
    {
        _isForced = false;
        _isDirty = true;        
    }

    private void IdleSprite(PointerEventData eventData)
    {
        if(!_isForced)
        {
            _isSelected = false;
            _isDirty = true;
        }
    }

    private void PressSprite(PointerEventData eventData)
    {
        _isSelected = true;
        _isDirty = true;
    }

    private void RefreshUI()
    {
        if (_isSelected || _isForced)   icon.sprite = pressedIcon;
        else  icon.sprite = idleIcon;

        _isDirty = true;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }
}
