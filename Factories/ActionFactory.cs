using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFactory : Factory
{
    public ActionFactory()
    {
        this.prefab = Resources.Load("Prefabs/BattleUI/CombatActionObject") as GameObject;
    }
}
