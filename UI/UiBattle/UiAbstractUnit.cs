using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Tools.UI;
using System.Collections.Generic;

[RequireComponent(typeof(IMouseInput))]
public class UiAbstractUnit : MonoBehaviour 
{
    public string unitName {get; set; }
    virtual public string unitDescription{get; set;}
    public TargetType allegiance {get; set;}            //the allegiance of the unit
    public bool isPlayer{get;set;}

    public List<StatusEffect> statusEffects{get;set;}


    //unity elements to load in editor
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected ShieldScript armorDisplay;
    [SerializeField] protected StatusDisplay statusDisplay;
    [SerializeField] protected TMP_Text unitTitle;
    [SerializeField] protected Image unitIcon;
    [SerializeField] protected TMP_Text speedValue;

    //monobehaviour components
    public string Name => gameObject.name;
    public IMouseInput Input { get; set; }

    //unit specific stat
    protected virtual int _currentHp{get; set;}
    protected virtual int _maxHp{get; set;}
    protected virtual int _speed{get; set;}

    public int damageModifier{get;set;}
    public int speedModifier{get;set;}

    protected int armor
    {
        get{return _armor;}
        set
        {
            _armor = value;
            armorDisplay.armorValue = _armor;
        }
    }
    private int _armor;


    //animation stuff
    protected bool _isDirty;
    [SerializeField] protected TMP_Text popText;
    public PulsatingAlpha glow;
    //--------------------------------------------------------------------------------------------------------------
    #region Initialize

    public virtual void Initialize(string unitName, TargetType allegiance)
    {
        Input = gameObject.GetComponent<IMouseInput>();
    
        this.unitName = unitName;
        this.allegiance = allegiance;

        //populate the gui with the information
        //unitTitle.text = unitName;

        //set the isPlayer to false by default
        isPlayer = false;

    }

    public void GiveStatus(StatusType statusType, int givenValue)
    {

        //stun patch to eliminate the current actions
        if(statusType == StatusType.Stun) OnStunEffect();

        foreach(StatusEffect status in statusEffects)
        {

            if(status.statusType == statusType)
            {
                status.AddValue(givenValue);
                status.OnApply();
            }

        }
        new UpdateCombatLogCommand("<source> has been given status " + statusType.ToString() + "(<v>).", (ITarget)this, (ITarget)this , givenValue).AddToQueue();
        DirtUI();
    }

    public void RemoveStatus(StatusType statusType)
    {
        foreach(StatusEffect status in statusEffects)
        {
            if(status.statusType == statusType)
            {
                status.Remove();
            }
        } 
        DirtUI();
    }

    public void RemoveAllStatuses()
    {
        foreach(StatusEffect status in statusEffects)
        {
                status.Remove();
        }          
        DirtUI();
    }

    protected virtual void OnStunEffect()
    {
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region UI

    public virtual void RefreshUI(){}

    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region Animations

    public void setGlowActive(bool state)
    {
        glow.setPulse(state);
    }

    public void AnimateDodge(){
        SplashText("Dodged");
    }

    public void SplashText(string displayText)
    {
        TMP_Text resSplash = Instantiate(popText,  gameObject.transform) as TMP_Text;                             //gets the UI prefab                  
        resSplash.text= displayText;
        resSplash.GetComponent<FadeTween>().StartTextFade();   
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region Utility

    public void DirtUI() => _isDirty = true;

    public void DestroyUnit() => Destroy(gameObject);

    public int ReturnSpeed() =>  (int) Mathf.Clamp(_speed + speedModifier, 0, Mathf.Infinity); 

    public void OnArmor(ArmorInfo armorInfo)
    {
        foreach(var statusEffect in statusEffects)
        {
            if (statusEffect.stack != 0) 
                armorInfo = statusEffect.OnArmor(armorInfo);
        }

        armor = armor + armorInfo.value;
        if (armorInfo.source == armorInfo.target)
        {
            new UpdateCombatLogCommand("<target> readied itself for <v> block.", armorInfo.source, armorInfo.target, armorInfo.value).AddToQueue();
        }else{
            new UpdateCombatLogCommand("<target> defended by <source> for <v> block", armorInfo.source, armorInfo.target, armorInfo.value).AddToQueue();
        }
        new SpawnShieldAnimationCommand(armorInfo.target).AddToQueue();;
    }

    public void HealDamage(int healAmount)
    {
        _currentHp = (int) Mathf.Clamp( _currentHp + (float)healAmount,0, _maxHp);

        DirtUI();
    }

    public int ApplyArmorToDamage(int damage)
    {
        if(armor >= damage)
        {
            armor = armor - damage;
            damage = 0;
        }else if(armor < damage){
            damage = damage - armor;
            armor = 0;
        }

        return damage;
    }

    public void TakeDamage(int damage)
    {
        //If player it gives a "death's door" effect which keeps you at 1HP, otherwise normal rules and death
        if(allegiance != TargetType.Player){
            _currentHp = (int) Mathf.Clamp( _currentHp - (float)damage,0,Mathf.Infinity);
        }else{
            if (_currentHp > 1) _currentHp = (int) Mathf.Clamp( _currentHp - (float)damage,1,Mathf.Infinity);
            else _currentHp = (int) Mathf.Clamp( _currentHp - (float)damage,0,Mathf.Infinity);
        }

        if(_currentHp == 0) new TriggerDeathCommand((ITarget)this).AddToQueue();

        DirtUI();
    } 


    public float GetHpRatio() => (float)_currentHp/(float)_maxHp;

    #endregion
    //--------------------------------------------------------------------------------------------------------------

    public virtual void Update()
    {
        if (_isDirty) RefreshUI();
    }
}