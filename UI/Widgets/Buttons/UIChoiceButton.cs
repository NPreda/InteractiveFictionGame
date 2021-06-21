using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using System;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIChoiceButton: UIPressButton, IClickInterface<UIChoiceButton>
{
    //most important variable, without choice there is no consequence
    public Choice choice;

    public new event Action<UIChoiceButton> OnClickEvent;        //event sent when an item is left-clicked

    public new ChoiceSkinData skinData
    {
        get => (ChoiceSkinData)_skinData;
        set => _skinData = value;
    }


    [SerializeField] private TMP_Text body;        //flavour text for the choice
    [SerializeField] private TMP_Text note;        //small note at the bottom for extra mechanical information
    [SerializeField] private ClueBox clueBox;   //special object used to show users the qualities needed for the choice.

    private bool _isActive;

    public void Populate(Choice choice)
    {
        this.choice = choice;
        LoadChoice(choice);
    }

    public override void BroadcastClick() => OnClickEvent?.Invoke(this);


     public void LoadChoice(Choice inChoice)
     {
        this.title.text = choice.title;                                     //applies name into text
        this.body.text = choice.body;                                       //applies value into text
        if(inChoice.contested){                                 //if contest exists, populate the gui with the chance of success
            int chance = inChoice.contest.ContestChance();
            this.note.text =  string.Format ("Your {0} quality gives {1}% chance", inChoice.contest.QualityName(), inChoice.contest.ContestChance());;            
        }

        if (choice.conditional && choice.conditions.statConditions.Any()){            //if the choice is contingent on  bunch of prerequisites
            ExpressionParser exParser = new ExpressionParser();


            //Tuple(Dict(txt, success), if_failed, if_hidden)
            var checkTuple = exParser.checkConditions(choice.conditions.statConditions);
            var results = checkTuple.Item1;
            var if_failed = checkTuple.Item2;
            var if_hidden = checkTuple.Item3;

            //load up the clue box
            clueBox.conditions = results;
            clueBox.Activate();
            print(string.Format("These are the result {0}", results));
            if (if_failed == true && if_hidden == true){
                this._isActive=false;
                Unsubscribe();
                gameObject.SetActive(false);
            }else if(if_failed == true){
                this._isActive=false;
                Unsubscribe();
            }
        }
     }

    protected override void OnSkinUI ()
    {
        base.OnSkinUI();

        //override whatever the parent class was doing to give an "inactive look"
        if(!_isActive)
        {
            this.background.sprite = skinData.inactiveBackground;
            this.background.color = skinData.inactiveColor;
        }
            
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (choice != null) base.OnClick(eventData);
    }

}