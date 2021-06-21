using Patterns.StateMachine;
using UnityEngine;


///     State Machine that holds all states of a UI Card.
public class UiCardFsm : BaseStateMachine
{
    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public UiCardFsm(UiCardParameters cardConfigsParameters, IUiCard handler = null) :
        base(handler)
    {
        CardConfigsParameters = cardConfigsParameters;

        IdleState = new UiCardIdle(handler, CardConfigsParameters);
        DisableState = new UiCardDisable(handler, CardConfigsParameters);
        DragState = new UiCardDrag(handler, CardConfigsParameters);
        ArrowState = new UiCardArrow(handler, CardConfigsParameters);
        HoverState = new UiCardHover(handler, CardConfigsParameters);
        DrawState = new UiCardDraw(handler, CardConfigsParameters);
        DiscardState = new UiCardDiscard(handler, CardConfigsParameters);
        DissolveState = new UiCardDissolve(handler, CardConfigsParameters);

        RegisterState(IdleState);
        RegisterState(DisableState);
        RegisterState(DragState);
        RegisterState(ArrowState);
        RegisterState(HoverState);
        RegisterState(DrawState);
        RegisterState(DiscardState);
        RegisterState(DissolveState);

        Initialize();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    UiCardIdle IdleState { get; }
    UiCardDisable DisableState { get; }
    UiCardDrag DragState { get; }
    UiCardArrow ArrowState { get; }
    UiCardHover HoverState { get; }
    UiCardDraw DrawState { get; }
    UiCardDiscard DiscardState { get; }
    UiCardDissolve DissolveState{get;}
    UiCardParameters CardConfigsParameters { get; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    public void Hover() => PushState<UiCardHover>();

    public void Arrow() => PushState<UiCardArrow>();

    public void Disable() => PushState<UiCardDisable>();

    public void Idle() => PushState<UiCardIdle>();

    public void Drag() => PushState<UiCardDrag>();

    public void Unselect() => Idle();

    public void Draw() => PushState<UiCardDraw>();

    public void Discard() => PushState<UiCardDiscard>();

    public void Dissolve() => PushState<UiCardDissolve>();

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}