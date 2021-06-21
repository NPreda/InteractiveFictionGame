using Patterns.StateMachine;
using Tools.UI;
using UnityEngine;

public interface IUiPlayer: IStateMachineHandler,  ITarget
{
    IMouseInput Input { get; }

    void Initialize(string unitName, TargetType allegiance);
    void setGlowActive(bool state);

    bool IsIdle { get; }
    bool IsTargeted { get; }

    void DirtUI();
    void Idle();
}