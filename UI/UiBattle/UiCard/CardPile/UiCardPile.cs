using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EasyButtons; 

//     Pile of cards. Add or Remove cards and be notified when changes happen.
public abstract class UiCardPile : MonoBehaviour, IUiCardPile
{
    //--------------------------------------------------------------------------------------------------------------
    #region Unitycallbacks

    public virtual void Awake()
    {
        //initialize register
        Cards =  new List<IUiCard>();

        Clear();
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    //List with all cards
    public List<IUiCard> Cards {get; private set;}

    //Event raised when add or remove a card
    event Action<IUiCard[]> onPileChanged = hand => { };

    public Action<IUiCard[]> OnPileChanged
    {
        get => onPileChanged;
        set => onPileChanged = value;
    }

    #endregion
    //--------------------------------------------------------------------------------------------------------------
    #region Operations

    //Add a card to the pile.
    public virtual void AddCard(IUiCard card)
    {
        if(card == null)
            throw new ArgumentNullException("Null is not a valid argument.");

        Cards.Add(card);
        card.transform.SetParent(transform);
        NotifyPileChange();
        
        card.Draw();
    }

    //Remove a card from the pile.
    public virtual void RemoveCard(IUiCard card)
    {
        if (card == null)
            throw new ArgumentNullException("Null is not a valid argument.");        
    
        Cards.Remove(card);
        NotifyPileChange();
    }

    //Clear all the pile.
    protected virtual void Clear()
    {
        var childCards = GetComponentsInChildren<IUiCard>();
        foreach (var uiCardHand in childCards)
            Destroy(uiCardHand.gameObject);

        Cards.Clear();
    }

    //Notify all listeners of this pile that some change has been made.
    [Button]
    public void NotifyPileChange() => onPileChanged?.Invoke(Cards.ToArray());

    #endregion
    //--------------------------------------------------------------------------------------------------------------
}
