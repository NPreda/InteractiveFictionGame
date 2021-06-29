using UnityEngine;

public enum SceneState
{
    Idle,
    Story,
    Explore,
    Combat
}

public class SceneControl : MonoBehaviour
{

    public StoryDisplay storyDisplay;
    public ExploreDisplay exploreDisplay;
    public CombatSystem combatSystem;
    public CharMenu charMenu;
    public SceneState sceneState;
    public Canvas mainCanvas;

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
        DisableAll();
    }

    public void DisplayCombat(CombatTrigger ct){
        charMenu.Deactivate();
        storyDisplay.Disable();
        exploreDisplay.Disable();
        combatSystem.Enable(ct);
        sceneState = SceneState.Combat;
    }
    
    public void DisplayExplore(){       //add the location type later
        charMenu.Activate();
        storyDisplay.Disable();
        combatSystem.Disable();
        exploreDisplay.Enable();
        sceneState = SceneState.Explore;
    }

    public void DisplayStory(){
        charMenu.Activate();
        exploreDisplay.Disable();
        combatSystem.Disable();
        storyDisplay.Enable();
        sceneState = SceneState.Story;
    }

    public void DisableAll()
    {
        charMenu.Activate();
        exploreDisplay.Disable();
        combatSystem.Disable();
        storyDisplay.Disable();    
        sceneState = SceneState.Idle;
  
    }
}
