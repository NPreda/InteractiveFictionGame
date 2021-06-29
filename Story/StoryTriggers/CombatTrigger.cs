using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "combat_trigger", menuName= "StoryElements/Triggers/Combat Trigger")]
public class CombatTrigger : StoryTrigger
{
    public Storylet winResult;
    public Storylet loseResult;
    public Storylet fleeResult;

    public List<Unit> units;

    public void OnEnable()
    {
        if(!units.Any()) throw new System.Exception("ASSIGNMENT ERROR: Empty unit list found in CombatTrigger: " + this.name);

        else if(fleeResult == null) throw new System.Exception("ASSIGNMENT ERROR: Mandatory Flee Storylet missing in CombatTrigger: " + this.name);
    }
}