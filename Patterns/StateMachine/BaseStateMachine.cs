using System;
using UnityEngine;
using System.Collections.Generic;

namespace Patterns.StateMachine
{
    public abstract class BaseStateMachine
    {
        //----------------------------------------------------------------------------------------------------------
        #region Constructor
        protected BaseStateMachine(IStateMachineHandler handler = null) => Handler = handler;

        #endregion
        //----------------------------------------------------------------------------------------------------------

        #region Properties

        //Boolean that indicates whether the FSM has been initialized or not
        public bool isInitialized{get; protected set;}
        
        //Stack of States
        readonly Stack<IState> stack = new Stack<IState>();

        //This register doesn't allow the FSM to have two states of the same type
        readonly Dictionary<Type, IState> register = new Dictionary<Type, IState>();

        //Handler for the FSM.Usually the Monobehaviour which holds this FSM.
        public IStateMachineHandler Handler {get;set;}

        //Returns the state on the top of the stack. Can be Null.
        public IState Current => PeekState();

        #endregion
        //----------------------------------------------------------------------------------------------------------

        #region Initialization
        
        //Register a state into the state machine
        public void RegisterState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException("Null is not a valid state");
                var type = state.GetType();
                register.Add(type,state);
                Debug.Log(Handler.Name + " Registered: " + type);
        }

        //Initialize all states. All states have to be registered before the initialization. See OnBeforeInitialize.
        public void Initialize() {
            //create states
            OnBeforeInitialize();

            //register all states
            foreach (var state in register.Values)
                state.OnInitialize();
            
            isInitialized = true;
        
            OnInitialize();
            Debug.Log(Handler.Name + ", Initialized!");
        }


        //Create and register the states overriding this method. It happens right before the Initialization.
        protected virtual void OnBeforeInitialize()
        {
        }

        //If you need to do something after the initialization, override this method.
        protected virtual void OnInitialize()
        {
        }

        #endregion
        //----------------------------------------------------------------------------------------------------------
 
        #region Operations

        //Update the FSM, consequently, updating the state on the top of the stack.
        public void Update() => Current?.OnUpdate();

        //Checks whether a Type is the same as the Type as the current state.
        public bool IsCurrent<T>() where T : IState => Current?.GetType() == typeof(T);

        //Check if a StateType is the current state.
        public bool IsCurrent(IState state){
            if (state == null)
                throw new ArgumentNullException();
            
            return Current?.GetType() == state.GetType();
        }

        //Pushes a state by Type triggering OnEnterState for the pushe state
        //and OnExitState for the previous state in the stack.
        public void PushState<T>(bool isSilent = false) where T : IState
        {
            var stateType = typeof(T);
            var state = register[stateType];
            PushState(state, isSilent);
        }

        //Pushes state by instance of the class triggering OnEnterState for the
        //pushed state and if not silent OnExitState for the previous state in the stack
        public void PushState(IState state, bool isSilent = false)
        {
            var type = state.GetType();
            if (!register.ContainsKey(type))
                throw new ArgumentException("State" + state + "not registered yet.");

            Debug.Log(Handler.Name + ", Push state: " + type);
            if (stack.Count > 0 && !isSilent)
                Current?.OnExitState();
            
            stack.Push(state);
            state.OnEnterState();
        }

        //Peeks a state from the stack. A peek returns null if the stack is empty.
        //It doesn't trigger any call.
        public IState PeekState() => stack.Count > 0 ? stack.Peek() : null;

        //Pops a state from the stack. It triggers OnExitState for the
        //popped state and if not silent OnEnterState for the subsequent stacked state.       
        public void PopState(bool isSilent = false)
        {
            if (Current == null)
                return;
            
            var state = stack.Pop();
            Debug.Log(Handler.Name + ", Pop state:" + state.GetType());
            state.OnExitState();

            if (!isSilent)
                Current?.OnEnterState();
        }

        //Clears and restart the states register
        public virtual void Clear()
        {
            foreach (var state in register.Values)
                state.OnClear();
            
            stack.Clear();
            register.Clear();
        }

        #endregion
        //----------------------------------------------------------------------------------------------------------
 
    

    }
}
