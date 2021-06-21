
public enum DisplayType
{
    Priority = 0,
    Buff = 1,
    Debuff = 2
}

public enum StatusType
{
    Mark = 0,
    Bleed = 1,
    Stun = 2,   //statuses above this will initialize before the stun cancels all actions on turn start 
    Weak = 3,
    Vulnerable = 4,
    Haste = 5,
    Slow = 6,
    Riposte = 7
}