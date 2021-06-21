using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public class ExploreCardDB : MonoBehaviour
{
    [SerializeField]
    public List<ExploreCard>  cards;


    //Instance Control
    //variables to help global instance selection
    private static ExploreCardDB  m_Instance;
    public static ExploreCardDB  Instance { get { return m_Instance; } }

    
    void Awake(){ 
        m_Instance = this;          //Get this instance
    }
    
    public void Setup()
    {
        UnityEngine.Object[] qualityObjects = Resources.LoadAll("Cards/Explore");
        ExploreCard[] temp = new ExploreCard[qualityObjects.Length];
        qualityObjects.CopyTo(temp, 0);
        cards = temp.ToList<ExploreCard>();
        Debug.Log("Card templates loaded with number:" + cards.Count());
    }

    public ExploreCard getCard(String id){
        ExploreCard card = new ExploreCard();
        foreach(var c in cards){
            if(c.id == id){
                card = c;
            }
        }
        return card;    
    }

    public List<ExploreCard> returnCards(ExploreLocation type){
        List<ExploreCard> returnCards = new List<ExploreCard>();
        foreach(var c in cards){
            if(c.location == type){
                returnCards.Add(c);
            }
        }
        return returnCards;
    }

    
}
