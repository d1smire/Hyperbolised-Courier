using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum 
{

    protected Dictionary<EState , BaseState<EState>> States = new Dictionary<EState,BaseState<EState>>();
    protected bool IsTransitionState = false;
    public BaseState<EState> CurrentState { get; protected set; }

    //private void Awake() { }

    private void Start() 
    {
        CurrentState.EnterState();
    }

    private void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitionState && nextStateKey.Equals(CurrentState.StateKey)) 
        { 
            CurrentState.UpdateState();
        }
        else if (!IsTransitionState)
        {
            TransitionToState(nextStateKey);    
        }
    }

    private void TransitionToState(EState nextStateKey) 
    {
        IsTransitionState = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        IsTransitionState = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        CurrentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other) 
    {
        CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other) 
    {
        CurrentState.OnTriggerExit(other);
    }

    //private void OnCollisionEnter(Collider other) { } // dont need it rn, but can use it in the future, maybe
    //private void OnCollisionStay(Collider other) { }
    //private void OnCollisionExit(Collider other) { }
}
