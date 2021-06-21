using Custom.UI;
using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;


public interface IClickInterface<out T>
{
    event Action<T> OnClickEvent;

    void BroadcastClick();
}

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIButton : CustomUI, IClickInterface<UIButton>
{

    //----------------------------------------------------------------------
    #region Properties

    [SerializeField] public TMP_Text title;

    public virtual ButtonSkinData skinData
    {
        get => _skinData;
        set => _skinData = value;
    }
    [SerializeField]protected ButtonSkinData _skinData;

    protected Image background;
    protected IMouseInput mInput;
    public event Action<UIButton> OnClickEvent;        //event sent when an item is left-clicked

    public string id = "";  //used to as a localization independent identifier

    #endregion
    //----------------------------------------------------------------------
    #region SkinVariables

    protected bool _isSelected;
    protected Sprite _background;
    protected TMP_FontAsset _font; 
    protected Color _color;  
    #endregion
    //--------------------------------------------------------------------------------------------------
    #region Initialization

    public override void Awake()
    {
        background = this.gameObject.GetComponent<Image>();
        mInput = this.gameObject.GetComponent<UiMouseInputProvider>();
        Subscribe();
        base.Awake();
    }


    public void Populate(string objectName)
    {
        this.gameObject.name = objectName;
        this.id = objectName;
        this.title.text = objectName.ToUpper();
    }

    public void Populate(string objectName, string id)
    {
        this.gameObject.name = id;
        this.id = id;
        this.title.text = objectName.ToUpper();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    #endregion
    //--------------------------------------------------------------------------------------------------
    #region ChildClassUtilities

    protected virtual void OnClick(PointerEventData eventData)
    {
        BroadcastClick();
    }

    public virtual void BroadcastClick() => OnClickEvent?.Invoke(this);


    #endregion
    //--------------------------------------------------------------------------------------------------
    #region VisualControls

    protected override void OnSkinUI ()
    {
        
        base.OnSkinUI();
        if(_isSelected)
        {
            _background = skinData.selectBackground;
            _font = skinData.selectFont;
            _color = skinData.selectColor;
        }else{
            _background = skinData.unselectBackground;
            _font = skinData.unselectFont;
            _color = skinData.unselectColor;
        }


        background.sprite = _background;
        if(background.sprite == null) background.color = new Color (0,0,0,0);    //turn off colour and alpha if no image
        else background.color = new Color(255,255,255,255);                     //otherwise go for pure white


        if(title != null)
        {
            title.font = _font;
            title.color = _color;
        } 
    }


    public virtual void Select(){
        Unsubscribe();
        _isSelected = true;
        _isDirty = true;
    }

    public virtual void Deselect(){
        Subscribe();
        _isSelected = false;
        _isDirty = true;
    } 

    #endregion
    //--------------------------------------------------------------------------------------------------
    #region EventSubscriptionControls

    protected virtual void Subscribe()
    {
        mInput.OnPointerUp += OnClick;
    }
    
    
    protected virtual void Unsubscribe()
    {
        mInput.OnPointerUp -= OnClick;
    }

    #endregion
    //--------------------------------------------------------------------------------------------------
}