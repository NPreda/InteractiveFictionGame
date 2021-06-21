﻿using System;
using UnityEngine;
using UnityEngine.UI;


    //Class responsible to bend the cards in the player hand.
    [RequireComponent(typeof(IUiPlayerHand))]
    public class UiPlayerHandBender : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        void Awake()
        {
            PlayerHand = GetComponent<IUiPlayerHand>();
            cardPrefab = PlayerHand.GetPrefab();
            CardTransform =  (RectTransform)cardPrefab.transform;
            PlayerHand.OnPileChanged += Bend;
            
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Fields and Properties

        [SerializeField] UiCardParameters parameters;

        private GameObject cardPrefab;

        [SerializeField] 
        Transform pivot;

        RectTransform CardTransform { get; set; }
        float CardWidth => CardTransform.rect.size.x;
        IUiPlayerHand PlayerHand { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        void Bend(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't bend a card list null");

            var fullAngle = parameters.BentAngle;
            var anglePerCard = fullAngle / cards.Length;
            var firstAngle = CalcFirstAngle(fullAngle);
            var handWidth = CalcHandWidth(cards.Length);

            //calc first position of the offset on X axis
            var offsetX = pivot.position.x + handWidth / 2;

            for (var i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                //set card Z angle
                float angleTwist = 0f;
                if  (cards.Length > 1)
                    angleTwist = (firstAngle + i * anglePerCard);

                //calc x position
                var xPos = offsetX - CardWidth / 2;

                //calc y position
                var yDistance = Mathf.Abs(angleTwist) * parameters.Height;
                var yPos = pivot.localPosition.y - yDistance;

                //set position
                if (!card.IsDragging && !card.IsHovering)
                {
                    var rotation = new Vector3(0, 0, angleTwist);
                    var position = new Vector3(xPos, yPos, card.transform.position.z);

                    card.RotateTo(rotation, parameters.RotationSpeed);
                    card.MoveTo(position, parameters.MovementSpeed);
                }

                //increment offset
                offsetX -= CardWidth + parameters.Spacing;
            }
        }

        /// <summary>
        ///     Calculus of the angle of the first card.
        /// </summary>
        /// <param name="fullAngle"></param>
        /// <returns></returns>
        static float CalcFirstAngle(float fullAngle)
        {
            var magicMathFactor = 0.1f;
            return -(fullAngle / 2) + fullAngle * magicMathFactor;
        }

        /// <summary>
        ///     Calculus of the width of the player's hand.
        /// </summary>
        /// <param name="quantityOfCards"></param>
        /// <returns></returns>
        float CalcHandWidth(int quantityOfCards)
        {
            var widthCards = quantityOfCards * CardWidth;
            var widthSpacing = (quantityOfCards - 1) * parameters.Spacing;
            return widthCards + widthSpacing;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
