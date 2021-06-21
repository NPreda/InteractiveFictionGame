using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class CombatAction : MonoBehaviour
{
    public Image icon;
    
    public string actionName;
    public string actionDescription;

    public TMP_Text effectValue;
    public int _effectValue;

    public int ticks;

    public ITarget source;
    public List<ITarget> targets;
    private IUiUnit handler;

    private int _damageModifier;


    //SPACE RESERVED FOR SOUND EFFECT VARIABLE

    public UnitEffect unitEffect;

    public void Load(UnitEffect unitEffect, IUiUnit handler)
    {
        this.handler = handler;

        this.actionName = unitEffect.effectName;
        this.actionDescription = unitEffect.effectDescription;

        this.icon.sprite = unitEffect.icon;
        this._effectValue = unitEffect.effectValue;
        this.effectValue.text = _effectValue.ToString();
        this.ticks = unitEffect.tickCost;
        source = unitEffect.source;
        targets = unitEffect.targets;
        this.unitEffect = unitEffect;
    }

    public void Play()
    {
        unitEffect.Run();
        DestroyAction();
    }

    public void UpdateValues()
    {
        _damageModifier = handler.damageModifier;
        TickDown();
    }

    public void TickDown()
    {
        ticks--;
    }

    public int GetTick() => ticks;

    public void DestroyAction()
    {
        this.handler.combatActions.Remove(this);
        Destroy(this.gameObject);
    }
    
}