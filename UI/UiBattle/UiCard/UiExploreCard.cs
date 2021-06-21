using UnityEngine;
using System;
using Tools.UI;
using TMPro;
using UnityEngine.UI;


public class UiExploreCard : UiCard, IUiCard
{
        //--------------------------------------------------------------------------------------------------------------
        #region Properties

        public new ExploreCard card{ get; set; }
        public UiCardSkin barrierSkin;
        public UiCardSkin dangerSkin;

        #endregion

        //--------------------------------------------------------------------------------------------------------------
        #region Unity Callbacks


        public override void SetupCard(Card inputCard){
            this.card = (ExploreCard) inputCard;

            //this is always true for explore cards, since we always reshufle the deck on dealing
            this.isSingleUse = true;

            //set if the card can be discarded based on the card type
            if(card.type == ExploreType.Normal)
                this.isDiscardable = true;
            else
                this.isDiscardable = false;

            if(card.type == ExploreType.Barrier)
            {
                this.CardFace.LoadFace(barrierSkin, card.image,  card.title, card.description);
            }else if(card.type == ExploreType.Danger){
                this.CardFace.LoadFace(dangerSkin, card.image,  card.title, card.description);
            }else{
                this.CardFace.LoadFace(card.image, card.title, card.description);
            }
        }

        public override Card returnCard(){
            return this.card;
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public override void Select()
        {
            new SelectExploreCardCommand(this).AddToQueue();
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------
}
