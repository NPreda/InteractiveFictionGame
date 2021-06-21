using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "combat_trigger", menuName= "StoryElements/Combat Trigger")]
public class CombatTrigger : StoryTrigger
{
        public Storylet winResult;
        public Storylet loseResult;
        public List<Unit> units;
}