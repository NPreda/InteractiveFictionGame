using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public class CombatCardDB : MonoBehaviour
{
    [SerializeField]
    public List<CombatCard>  cards;


    //Instance Control
    //variables to help global instance selection
    private static CombatCardDB  m_Instance;
    public static CombatCardDB  Instance { get { return m_Instance; } }

    //hacky things to make the quality-deck system work
    public EquippableQuality defaultWeapon;
    
    //Object to process information of screen data
    ExpressionParser exParser = new ExpressionParser();

    
    void Awake(){ 
        m_Instance = this;          //Get this instance
    }
    
    public void Setup()
    {
        UnityEngine.Object[] qualityObjects = Resources.LoadAll("Cards/Combat");
        CombatCard[] temp = new CombatCard[qualityObjects.Length];
        qualityObjects.CopyTo(temp, 0);
        cards = temp.ToList<CombatCard>();
        Debug.Log("Combat card templates loaded with number:" + cards.Count());    }

    public CombatCard getCard(String id){
        CombatCard card = new CombatCard();
        foreach(var c in cards){
            if(c.id == id){
                card = c;
            }
        }
        return card;    
    }

    //return all cards the deck needs using your qualities
    public List<CombatCard> returnCards(){
        //get the current weapon 
        EquippableQuality equipedWeapon = Inventory.Instance.ReturnEquippedType(EquipmentType.Weapon);
        if (equipedWeapon == null)      //load a default weapon if none, likely fists
            equipedWeapon = defaultWeapon;

        //instantiate the card list    
        List<CombatCard> returnCards = new List<CombatCard>();

        foreach(var c in cards){
            if ((c.item == null) || (c.item == equipedWeapon)){     //if the cards have either no weapon requirement or a valid one
                //Tuple(Dict(txt, success), if_failed, if_hidden)
                var checkTuple = exParser.checkConditions(c.conditions.statConditions);
                var if_failed = checkTuple.Item2;
                if (if_failed == false)
                    returnCards.Add(c);
            }
        }
        return returnCards;
    }
}
