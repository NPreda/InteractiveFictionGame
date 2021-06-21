using UnityEngine;
using System;
using Tools.UI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CanvasGroup))]
public class UiCard : TweeningMover, IUiCard
{
    //--------------------------------------------------------------------------------------------------------------
    #region Unity Callbacks

        public override void Awake()
        {
            base.Awake();

            //components
            MyTransform = transform;
            MyInput = GetComponent<IMouseInput>();
            MyLineRenderer = GetComponent<LineRenderer>();

            //copy the material to allow individual property manipulation
            SetupMaterial();

            //Cache the current parameters of the card to be returned to if modified
            CacheValues();


            //fsm
            Fsm = new UiCardFsm(cardConfigsParameters, this);
        }

        public virtual void Setup(Card c)
        {
            SetupCard(c);
        }

        public virtual void SetupCard(Card c){
            this.card = c;
            if (c.skin)
            {
                this.CardFace.LoadFace(c.skin, c.image,c.title, c.description);

            }else{
                this.CardFace.LoadFace(c.image, c.title, c.description);

            }
        } 
    
        public virtual Card returnCard(){
            return this.card;
        }

        public void SetupMaterial(){     
            var mat = CardFace.GetComponent<Image>().material;  //get the material instance
            CardFace.GetComponent<Image>().material = new Material(mat);  //give the cardface a copy of that material instead
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------

        #region Properties
        
        public Vector3 startPosition { get; set; }
        public Vector3 startRotation { get; set; }
        public Vector3 startScale { get; set; }

        public string Name => gameObject.name;
        [SerializeField] public UiCardParameters cardConfigsParameters;
        public UiCardFsm Fsm{ get; set; }
        Transform MyTransform { get; set; }
        public bool isDiscardable{get;set;}

        IMouseInput MyInput { get; set; }
        public LineRenderer MyLineRenderer { get; set; }
        public bool IsDisabled => Fsm.IsCurrent<UiCardDisable>();
        public bool IsDragging => Fsm.IsCurrent<UiCardDrag>();
        public bool IsArrowing => Fsm.IsCurrent<UiCardArrow>();
        public bool IsHovering => Fsm.IsCurrent<UiCardHover>();
        public bool IsDrawing => Fsm.IsCurrent<UiCardDraw>();
        public bool IsIdle => Fsm.IsCurrent<UiCardIdle>();

        #endregion
        //--------------------------------------------------------------------------------------------------------------

    
        #region InterfaceReferences
        public Card card{ get; set; }
        [SerializeField] UiCardFace _cardFace;
        [HideInInspector] public bool isSingleUse{get;set;}
        public UiCardFace CardFace  {   
                                        get => _cardFace;
                                        set => _cardFace = value;
                                    }
        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Components
        IMouseInput IUiCardComponents.Input => MyInput;
        LineRenderer IUiCardComponents.LineRenderer => MyLineRenderer;
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------

        #region Transform

        public void CacheValues()
        {
            startPosition = this.transform.localPosition;
            startRotation = this.transform.eulerAngles;
            startScale = this.transform.localScale;
        }


        #endregion
        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public void Hover() => Fsm.Hover();

        public override void Disable() => Fsm.Disable();

        public virtual void Idle() => Fsm.Idle();

        public virtual void Select()
        {
            //Hand.SelectCard(this);
            Fsm.Drag();
        }

        public void Unselect() => Fsm.Unselect();

        public void Draw() => Fsm.Draw();

        public void Discard() => Fsm.Discard();

        public void Dissolve() => Fsm.Dissolve();

        public void ExpendCard()
        {
            if(this.isSingleUse)
            {
                Dissolve();
            }else{
                Discard();
            }
        } 

        public void DestroyCard()
        {
            Destroy(gameObject);
        }

        #endregion
        //--------------------------------------------------------------------------------------------------------------

        void Update()
        {
            Fsm?.Update();
        }
}
