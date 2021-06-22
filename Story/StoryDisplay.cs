
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Tools.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StoryDisplay : TweeningMover
{
    //[SerializeField] Character character;
    [SerializeField] private TMP_Text textTitle;
    [SerializeField] private Image sideImage;
    [SerializeField] private TMP_Text textBody;    
    [SerializeField] private GameObject choiceGroup;    
    [SerializeField] private ResultBox resultGroup;
    [SerializeField] private GameObject choiceTemplate;  
    [SerializeField] private TMP_Text  popTemplate;     
    public List<UIChoiceButton> choiceButtons;         //reference to the scripts of all the button choices
    
    //Object to process information of screen data
    ExpressionParser exParser = new ExpressionParser();

    //------------------------------------------------------------------------------
    #region ActivationMethods

    public override void Enable()
    {
        Fade(1f, 1f, base.Enable);
        Subscribe();
        DisplayStorylet();
    }

    public override void Disable()
    {
        base.canvasGroup.blocksRaycasts = false;
        Unsubscribe();
        Fade(0f, 1f, base.Disable);
    }

    #endregion
    //------------------------------------------------------------------------------
    #region SubscriptionMethods

    protected virtual void Subscribe()
    {
        Character.OnCharChanged += RefreshScreen;
    }

    protected virtual void Unsubscribe()
    {
        Character.OnCharChanged -= RefreshScreen;
    }

    #endregion
    //------------------------------------------------------------------------------

    //it refreshes the choices to allow updates to stats to affect them
    void RefreshScreen()
    {   
        if(choiceGroup.activeSelf == true)
        {
            DisplayChoices(StoryletCache.choices);
        }
    }

    void DisplayStorylet()
    {
        //Format and Display the main text body
        textTitle.text = StoryletCache.title;
        textBody.text = StoryletCache.body;

        if(StoryletCache.image != null) sideImage.sprite= StoryletCache.image;
        else sideImage.gameObject.SetActive(false);

        DisplayChoices(StoryletCache.choices);        
    }


    void ClearChoices()
    {
        //Clear previous choices and their gameobjects
        foreach(var b in choiceButtons)
        {
            b.Destroy();
        }
        choiceButtons.Clear();   
    }

    void DisplayChoices(List<Choice> choices)
    {
        //Format and display the choices related to this storylet
        //Clear previous choices and their gameobjects
        ClearChoices();
        resultGroup.SetInactive();   
        choiceGroup.SetActive(true);       
        //load new ones
        try 
        {
            foreach(var choice in choices)
            {
                createButton(choice, choiceGroup);
            }
        } 
        catch (System.Exception ex) 
        {
            Debug.Log(string.Format("Choices failed to display for storylet with error: {0}", ex));
        }

        foreach(var b in choiceButtons)
        {
            b.OnClickEvent += OnChoiceClick; //if a choice button has been clicked, do stuff  
        }
        //subscribe to the result button as well. Be wary of issues
        resultGroup.button.OnClickEvent +=OnChoiceClick;
   }

   void DisplayResult(Result rslt)
   {
            string rewardTxt = "";

            ClearChoices();
            choiceGroup.SetActive(false);
            resultGroup.SetActive();            
            resultGroup.body.text = rslt.body;
            var rewardDict = exParser.processRewards(rslt.reward);          //a list of Dictionary<String, [int,int]>
            foreach (var reward in rewardDict){
                string qualityName = reward.Key;
                int[] oldAndNewValue=  reward.Value;
                if (oldAndNewValue[0] >oldAndNewValue[1]){             //your old value is higher than your new value
                    rewardTxt = rewardTxt + string.Format("Your {0} has decreased to {1} \n", qualityName, oldAndNewValue[1]); 
                }else if(oldAndNewValue[1] >oldAndNewValue[0]){         //your old value is lower than your new value
                    rewardTxt = rewardTxt + string.Format("Your {0} has increased to {1} \n", qualityName, oldAndNewValue[1]); 
                }else{
                    rewardTxt = rewardTxt + string.Format("Your {0} has remained unchanged, despite your efforts \n", qualityName); 
                }
            }

            resultGroup.rewards.text = rewardTxt;

            resultGroup.button.choice.defaultResult.goTo = rslt.goTo;

   }

    void createButton(Choice choice, GameObject choiceGroup)
    {                       
        ChoiceButtonFactory choiceFactory = new ChoiceButtonFactory();
        UIChoiceButton choiceButton = choiceFactory.GetNewInstance(choiceGroup, choice, choiceTemplate);
        choiceButtons.Add(choiceButton);                                     //add it to a list to keep track of it and reference it for event management
    }

    void OnChoiceClick(UIChoiceButton button)
    {
            if(!button.choice.contested){         //if there is no contest attached, go directly to the stored goTo;
                if (button.choice.defaultResult.body.Trim() != "" || button.choice.defaultResult.reward.Trim() != ""){   //check if there is any result attached to this choice
                    DisplayResult(button.choice.defaultResult);                  //Load up the failure result 
                }else{
                    new PlayStoryTriggerCommand(button.choice.defaultResult.goTo).AddToQueue();
                }
            }else{                                     //otherwise run the contest and go to the contest goTo;
                var cont = button.choice.contest;
                if(cont.RollContest() == true){
                    DisplayResult(button.choice.contest.success);                  //Load up the success result 

                    TMP_Text resSplash = Instantiate(popTemplate, transform.position, Quaternion.identity, resultGroup.gameObject.transform) as TMP_Text;                             //gets the UI prefab      
                    //resSplash.transform.position = gameObject.transform.position;
                    resSplash.transform.position = resultGroup.bodyPanel.transform.position;
                    resSplash.text = "Success";
                    //resSplash.color = col;
                    resSplash.GetComponent<FadeTween>().StartTextFade();                       //Load up the failure result 
                }else{
                    DisplayResult(button.choice.contest.failure);                  //Load up the failure result 

                    TMP_Text resSplash = Instantiate(popTemplate, transform.position, Quaternion.identity, resultGroup.gameObject.transform) as TMP_Text;                             //gets the UI prefab      
                    //resSplash.transform.position = gameObject.transform.position;
                    resSplash.transform.position = resultGroup.bodyPanel.transform.position;
                    Color col = new Color(5, 164, 26, 255);             
                    resSplash.text = "Failure";
                    //resSplash.color = col;
                    resSplash.GetComponent<FadeTween>().StartTextFade();      
                }
            }
    }
        
}
