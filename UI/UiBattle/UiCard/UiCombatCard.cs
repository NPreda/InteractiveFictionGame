using UnityEngine;
using System;
using Tools.UI;
using TMPro;
using UnityEngine.UI;


public class UiCombatCard : UiCard, IUiCard
{
        //--------------------------------------------------------------------------------------------------------------
        #region Properties

        public new CombatCard card{ get; set; }

        [SerializeField]private ClickSpriteSwitcher cardGlow;

        #endregion

        //--------------------------------------------------------------------------------------------------------------
        #region Unity Callbacks

        // public override void Setup(Card card){
        //     this.SetupCard((CombatCard)card);
        // }

        public override void SetupCard(Card inputCard){
            this.card = (CombatCard) inputCard;
            this.card.cardEffect.Setup();
            CombatLogParser combatParser = new CombatLogParser();
            string cardDesc =  combatParser.ParseEntry(this.card.description, this.card.cardEffect.ReturnTotalEffectValue() );
            if (card.skin)
            {
                this.CardFace.LoadFace(card.skin, card.image,  card.title, cardDesc, card.energyCost.ToString());

            }else{
                this.CardFace.LoadFace(card.image, card.title, cardDesc, card.energyCost.ToString());
            }

            this.isSingleUse = inputCard.isSingleUse;
            this.isDiscardable = true;  //right now all combat cards are discardable by default
        }

        public override Card returnCard(){
            return this.card;
        }


/*         private void LoadSkin()
        {
            switch(cardType)
            {
                case UiCardType.Might:
                    break;
                case UiCardType.Finesse:
                    break;
                case UiCardType.Presence:
                    break;
                default:
                    break;
            }
        }

            private void LoadStrength()
        {
            switch(cardType)
            {
                case UiCardType.Might:
                    break;
                case UiCardType.Finesse:
                    break;
                case UiCardType.Presence:
                    break;
                default:
                    strength = 1f;
                    break;
            }
        }
 */
        #endregion
        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public override void Idle()
        {   
            if (card != null)
            {
                if(CheckEnergy(card.energyCost))    cardGlow.UnforceSelect();
                else cardGlow.ForceSelect();
            }

            Fsm.Idle();
        }

        public override void Select()
        {
            if(CheckEnergy(card.energyCost) && card.combatCardType == CombatCardType.Attack)
            {
                cardGlow.ForceSelect();
                Fsm.Arrow();
            }
            else if (CheckEnergy(card.energyCost) && card.combatCardType == CombatCardType.Skill)
            {
                cardGlow.ForceSelect();
                Fsm.Drag();
            }

        }

        //check if there is sufficient energy for it to play, hackish dependency really
        private bool CheckEnergy(int cardCost)
        {
            int currentEnergy = Character.Instance.ReturnStatPacket("Energy").value;
            if (cardCost > currentEnergy || cardCost < 0){
                return false;
            } else {
                return true;
            }
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------
}
