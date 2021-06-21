using System.Collections.Generic;
using Tools.UI;
using Patterns.StateMachine;

public interface IUiUnit: IStateMachineHandler, ITarget
{
    IMouseInput Input { get; }

    void Initialize(string unitName, TargetType allegiance, Unit unitInput);
    void setGlowActive(bool state);

    UnitAI unitAI { get; }
    List<CombatAction> combatActions {get;}

    string unitDescription{get;}

    bool IsIdle { get; }
    bool IsTargeted { get; }

    void Idle();
    void DirtUI();
    void DestroyUnit();
}