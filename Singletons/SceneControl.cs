using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{

    public StoryDisplay storyDisplay;
    public ExploreDisplay exploreDisplay;
    public CombatSystem combatSystem;
    public CharMenu charMenu;

    //Instance Control
    //variables to help global instance selection
    private static SceneControl m_Instance;
    public static  SceneControl Instance { get { return m_Instance; } }

    void Awake(){ 
        m_Instance = this;          //Get this instance
    }

    public void Setup()
    {
        combatSystem.Setup();
        exploreDisplay.Setup();
    }

    public void DisplayCombat(CombatTrigger ct){
        charMenu.Deactivate();
        storyDisplay.Disable();
        exploreDisplay.Disable();
        combatSystem.Enable(ct);
    }
    
    public void DisplayExplore(){       //add the location type later
        charMenu.Activate();
        storyDisplay.Disable();
        combatSystem.Disable();
        exploreDisplay.Enable();
    }

    public void DisplayStory(Storylet st){
        charMenu.Activate();
        exploreDisplay.Disable();
        combatSystem.Disable();
        storyDisplay.Enable(st);
    }
}
