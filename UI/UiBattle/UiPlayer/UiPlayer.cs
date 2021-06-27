using System;
using UnityEngine;
using TMPro;
using Tools.UI;
using System.Collections.Generic;


[RequireComponent(typeof(IMouseInput))]
public class UiPlayer : UiAbstractUnit , IUiPlayer
{
    //NECESSARY FOR THE UNIT TURN WORKAROUND TO AVOID LIST-ISSUE ON DESTRUCTION, NOT USED FOR PLAYER
    public bool finishedTurn{ get; set;}
    
    [SerializeField] private ResolveBar resolveBar;

    public int strength{get;set;}


    public UiPlayerFsm Fsm{ get; set; }
    //--------------------------------------------------------------------------------------------------------------
    #region CharacterInstanceGetters

    //Players way of getting stats is fairly complex
    override protected int _currentHp
        {   
            get => Character.Instance.ReturnStatPacket("Health").value; 
            set => Character.Instance.statManager.statSet("Health", value);
        }
    override protected int _maxHp
        {   
            get => Character.Instance.ReturnStatPacket("Health").maxValue;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    override protected int _speed
        {   
            get => Character.Instance.ReturnStatPacket("Speed").maxValue;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    protected int _currentResolve
        {   
            get => Character.Instance.ReturnStatPacket("Resolve").value;
            set => Character.Instance.statManager.statSet("Resolve", value);
        }
    protected int _maxResolve
        {   
            get => Character.Instance.ReturnStatPacket("Resolve").maxValue;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    protected int _might
        {   
            get => Character.Instance.ReturnStatPacket("Might").value;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    protected int _finesse
        {   
            get => Character.Instance.ReturnStatPacket("Finesse").value;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    protected int _presence
        {   
            get => Character.Instance.ReturnStatPacket("Presence").value;
            set => throw new NotSupportedException("Using this setter is not allowed");
        }
    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region Instantiate

    public override void Initialize(string unitName, TargetType allegiance)
    {
        base.Initialize(unitName, allegiance);
        isPlayer = true;
        Fsm = new UiPlayerFsm(this);
        Idle();
        finishedTurn = false;
        PopulateStatusEffects();

        DirtUI();
    }

    public virtual void PopulateStatusEffects()
    {
        this.statusEffects = new List<StatusEffect>();
        StatusEffectFactory factory = new StatusEffectFactory();
        foreach(var type in Enum.GetValues(typeof(StatusType)))
        {
            statusEffects.Add(factory.ReturnStatusEffect(this, (StatusType) type));
        }
    }

    public void Bark()
    {

    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------


    private void OnEnable(){
        Character.OnCharChanged += DirtUI;
    }

    private void OnDisable(){
        Character.OnCharChanged -= DirtUI;
    }

    public void Die(){}

    public void RunTurn(){}

    //--------------------------------------------------------------------------------------------------------------
    #region CombatBehaviours

    public void OnAttack(DamageInfo damageInfo)
    {

        foreach(var statusEffect in statusEffects)
        {
            if (statusEffect.stack != 0) 
                damageInfo = statusEffect.OnAttack(damageInfo);
        }

        if(damageInfo.damageType == DamageType.Mental)
        {
            TakeResolveDamage(damageInfo.value);
            new UpdateCombatLogCommand("RESOLVE: <source> has hit <target> for <v>.", damageInfo.source, damageInfo.target, damageInfo.value).AddToQueue();
        }else{
            damageInfo.value = ApplyArmorToDamage(damageInfo.value);  
            if (damageInfo.value == 0) return;

            TakeDamage(damageInfo.value);
            new UpdateCombatLogCommand("<source> has hit <target> for <v>.", damageInfo.source, damageInfo.target, damageInfo.value).AddToQueue();
        }

    }

    public void OnHeal(HealInfo healInfo)
    {
        if(healInfo.healType ==  HealType.Resolve)
        {
            HealResolveDamage(healInfo.value);
            new UpdateCombatLogCommand("RESOLVE: <source> given <target> <h> health.", healInfo.source, healInfo.target, healInfo.value).AddToQueue();
        }else{
            HealDamage(healInfo.value);
            new UpdateCombatLogCommand("<source> given <target> <h> health.", healInfo.source, healInfo.target, healInfo.value).AddToQueue();            
        }
    }


    #endregion
    //--------------------------------------------------------------------------------------------------------------

    #region StatCalc

    public void HealResolveDamage(int healAmount)
    {
        _currentResolve = (int) Mathf.Clamp( _currentResolve + (float)healAmount,0, _maxResolve);

        DirtUI();
    }

    public void TakeResolveDamage(int damage){
        _currentResolve = (int) Mathf.Clamp( _currentResolve + (float)damage, 0 , _maxResolve);

        if(_currentResolve == 0) new TriggerDeathCommand((ITarget)this).AddToQueue();

        DirtUI();
    } 

    #endregion
    //------------------------------------------------------------------------------------------------------------
    #region UI

    public override void RefreshUI(){
        speedValue.text = ReturnSpeed().ToString();

        healthBar.SetSize(_currentHp, _maxHp);
        resolveBar.UpdateResolve(_currentResolve,  _maxResolve);
        statusDisplay.RefreshUI(statusEffects);

        _isDirty = false;
    }    
    

    #endregion
    //------------------------------------------------------------------------------------------------------------
    #region FSMStates

    public bool IsTargeted => Fsm.IsCurrent<UiPlayerTargeted>();
    public bool IsIdle => Fsm.IsCurrent<UiPlayerIdle>();

    #endregion
    //------------------------------------------------------------------------------------------------------------

    #region FSMOperations

    public void Idle() => Fsm.Idle();
    public void Targeted() => Fsm.Targeted();
    public void PlayTurn() => Fsm.StartTurn();
    public void FinishTurn() => Fsm.EndTurn();

    #endregion
    //--------------------------------------------------------------------------------------------------------------    

    public override void Update()
    {
        if (_isDirty) RefreshUI();
        Fsm?.Update();
    }
}