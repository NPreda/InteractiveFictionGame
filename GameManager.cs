using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{ 
    [SerializeField]
    private GameObject mainCanvas;     
    [SerializeField]
    private GameObject infoGroup;       

    //We want an event that will handle UI WIDGET initializations after the backend has been properly set up
    //delegate()
    public delegate void widgetHandler ();
    //event  
    public static event widgetHandler BackendInitialized;

    [SerializeField] private Storylet startingStorylet;

    //Implementing new character cartouche system as interface for the player
    [SerializeField]
    private UnitFactory friendPanel;

    //TOOLTIP OBJECTS TO BE REFERENCED
    [SerializeField]    private SimpleTooltip smallTooltip;    ///tooltip with generic text logic
    [SerializeField]    private FancyTooltip bigTooltip;    ///tooltip with generic text logic

    //Instance Control
    //variables to help global instance selection
    private static GameManager m_Instance;
    public static  GameManager Instance { get { return m_Instance; } }

    void Awake(){ 
        Debug.Log("STARTING GAME");
        m_Instance = this;          //Get this instance
    }
    
    void Start(){ 
        Character.Instance.Setup(); //setup the character stats
        QualityDB.Instance.Setup();
        ExploreCardDB.Instance.Setup();
        CombatCardDB.Instance.Setup();

        SceneControl.Instance.Setup();

        BackendInitialized();

        //instantiate player
        ITarget player = friendPanel.LoadPlayer(); 

        //start the story
        SceneControl.Instance.DisplayStory(startingStorylet);

    }

    public SimpleTooltip returnSimpleTooltip(){
        return smallTooltip;
    }

    public FancyTooltip returnFancyTooltip(){
        return bigTooltip;
    }

}
