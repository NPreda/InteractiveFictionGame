using System.Collections.Generic;
using UnityEngine;
using System;
using Tools.UI;
using System.Linq;

[RequireComponent(typeof(IMouseInput))]
public class UiUnit : UiAbstractUnit, IUiUnit
{
    public UnitAI unitAI{ get; set; } 

    private UiUnitFsm Fsm{ get; set; } 

    [SerializeField] private InfoPanelTrigger infoPanelTrigger;

    //NECESSARY FOR THE UNIT TURN WORKAROUND TO AVOID LIST-ISSUE ON DESTRUCTION
    public bool finishedTurn{get; set;}

    //unit specific stat
    public int strength{get;set;}

    //combat actions to be done
    public List<CombatAction> combatActions {get; set;}

    //the dialog barks used by the thing
    private List<string> barks = new List<string>();

    //the result for this unit
    public Storylet enemyWin; 
    public Storylet enemyLoss;


    //--------------------------------------------------------------------------------------------------------------
    #region Instantiate

    public void Initialize(string unitName, TargetType allegiance, Unit unit)
    {
        base.Initialize(unitName, allegiance);
        combatActions = new List<CombatAction>();
        Fsm = new UiUnitFsm(this);

        finishedTurn = false;

        unitDescription = unit.unitDescription;
        unitIcon.sprite = unit.icon;
        _maxHp = unit.maxHP;
        _currentHp = unit.maxHP;
        _speed = unit.speed;
        strength = unit.strength;
        unitAI = unit.unitBehaviour;
        barks = unit.barkStrings.ToList();

        enemyWin = unit.enemyWin;
        enemyLoss = unit.enemyLoss;

        this.infoPanelTrigger.Load(this);
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
        if (barks.Count > 0)
        {
            int index = UnityEngine.Random.Range(0,barks.Count);
            string randomString = barks[index];
            new SpawnDialogCommand((ITarget)this, randomString).AddToQueue();
        }
    }

    protected override void OnStunEffect()
    {
        RemoveAllActions();
        base.OnStunEffect();
    }

    public void RemoveAllActions()
    {
        for(int i=combatActions.Count-1; i >= 0; i--)
        {
            combatActions[i].DestroyAction();
        }
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region CombatBehaviours

    public void OnAttack(DamageInfo damageInfo)
    {
        foreach(var statusEffect in statusEffects)
        {
            if (statusEffect.stack != 0) 
                damageInfo = statusEffect.OnAttack(damageInfo);
        }
        
        //space reserved for things that ignore armor

        damageInfo.value = ApplyArmorToDamage(damageInfo.value);  
        if (damageInfo.value == 0) return;
        
        TakeDamage(damageInfo.value);
        new UpdateCombatLogCommand("<source> has hit <target> for <d>.", damageInfo.source, damageInfo.target, damageInfo.value).AddToQueue();
        new SpawnFloatingNumberCommand(damageInfo).AddToQueue();

    }

    public void OnHeal(HealInfo healInfo)
    {
        foreach(var statusEffect in statusEffects)
        {
            if (statusEffect.stack != 0) 
                healInfo = statusEffect.OnHeal(healInfo);
        }

        HealDamage(healInfo.value);
        new UpdateCombatLogCommand("<source> given <target> <h> health.", healInfo.source, healInfo.target, healInfo.value).AddToQueue();
        new SpawnFloatingNumberCommand(healInfo).AddToQueue();

    }

    public void FinishTurn()
    {
        Idle();
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------


    //------------------------------------------------------------------------------------------------------------
    
    #region UI

    public override void RefreshUI(){
        speedValue.text = ReturnSpeed().ToString();
        healthBar.SetSize(_currentHp, _maxHp);
        statusDisplay.RefreshUI(statusEffects);

        _isDirty = false;
    }    
    
    #endregion
    //------------------------------------------------------------------------------------------------------------
    #region FSMStates

    public bool IsTargeted => Fsm.IsCurrent<UiUnitTargeted>();
    public bool IsIdle => Fsm.IsCurrent<UiUnitIdle>();
    public bool isPlaying => Fsm.IsCurrent<UiUnitPlaying>();

    #endregion
    //------------------------------------------------------------------------------------------------------------

    #region FSMOperations

        public void Idle() => Fsm.Idle();
        public void Targeted() => Fsm.Targeted();
        public void PlayTurn() => Fsm.PlayTurn();
        public void Die() => Fsm.Die();

    #endregion
    //--------------------------------------------------------------------------------------------------------------    

    public override void Update()
    {
        if (_isDirty) RefreshUI(); 
        Fsm?.Update();
    }
}

