using Patterns.StateMachine;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


// A complete UI card.
public interface IUiCard : IStateMachineHandler, IUiCardComponents, IUiCardTransform
{    
    Vector3 startPosition { get; set; }
    Vector3 startRotation { get; set; }
    Vector3 startScale { get; set; }

    //IUiPlayerHand Hand { get; }
    Card card{ get; }
    bool isSingleUse{get;}
    bool isDiscardable{get;}
    bool IsDragging { get; }
    bool IsArrowing { get; }
    bool IsHovering { get; }
    bool IsDisabled { get; }
    bool IsDrawing { get; }
    bool IsIdle { get; }
    void Disable();
    void Idle();
    void Select();
    void Unselect();
    void Hover();
    void Draw();
    void Discard();
    void Dissolve();
    void ExpendCard();      //a fancy, dissolve effect way of destroying a card
    void DestroyCard();
    void CacheValues();
    void Setup(Card card);
    Card returnCard();
    UiCardFace CardFace{get;}
}