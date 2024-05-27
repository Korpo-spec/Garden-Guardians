using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using Object = System.Object;

public class StateController : MonoBehaviour
{

    [SerializeField] private State defaultState;

    [SerializeField]private List<State> currentState;

    private Queue<(State state, bool hardshift)> nextState;

    private bool changeState;

    private bool ignoreFirstUpdate;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
        currentState = new List<State>();
        nextState = new Queue<(State state, bool hardshift)>();
        currentState.Add( Instantiate(defaultState));
        currentState[0].OnEnter(this);
    }

    
    

    // Update is called once per frame
    void Update()
    {
        
        if (ignoreFirstUpdate)
        {
            ignoreFirstUpdate = false;
            return;
        }

        foreach (var state in currentState)
        {
            state.UpdateState();
        }
        
        if (changeState)
        {
            for (int i = 0; i < nextState.Count; i++)
            {
                var nextstate = nextState.Dequeue();
                
                if (nextstate.hardshift)
                {
                    OnTransition(nextstate.state);
                }
                else
                {
                    OnAdd(nextstate.state);
                }
                
            }
            
            return;
        }
    }

    public void Transistion(State newstate)
    {
        Debug.Log("Wants to transistion");
        nextState.Enqueue((newstate, true)); 
        changeState = true;
    }

    public void AddState(State newstate)
    {
        foreach (var state in currentState)
        {
            if (state.GetType() == newstate.GetType())
            {
                return;
            }
        }
        nextState.Enqueue((newstate, false)); 
        changeState = true;
    }

    private void OnTransition(State changestate)
    {
        RemoveActiveStates();
        changeState = false;
        
        State newstate = Instantiate(changestate);
        currentState.Add( newstate);
        newstate.OnEnter(this);
        ignoreFirstUpdate = true;
    }


    public void ClearCurrentStates()
    {
        RemoveActiveStates();
    }
    public void ClearCurrentStates(State exception)
    {
        RemoveActiveStates(exception);
    }
    private void RemoveActiveStates()
    {
        foreach (var state in currentState)
        {
            state.OnExit();
        }
        currentState.Clear();
    }
    
    private void RemoveActiveStates(State exception)
    {
        foreach (var state in currentState)
        {
            if (state == exception)
            {
                continue;
            }
            state.OnExit();
        }
        currentState.Clear();
        currentState.Add(exception);
    }

    private void OnAdd(State changestate)
    {
        changeState = false;
        
        State newstate = Instantiate(changestate);
        currentState.Add( newstate);
        newstate.OnEnter(this);
        ignoreFirstUpdate = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var state in currentState)
        {
            state.OnTriggerEnter(other);
        }
        
    }

    public void AnimationEvent(string eventName)
    {
       
        
        foreach (var state in currentState) 
        {
                state.OnAnimatorEvent(eventName);
        }
        
        
    }
    [HideInInspector]
    public EnemyRangedAttackState RangedAttackState;
    public void RangeAttack()
    {
        RangedAttackState.Instansiateprojectile();
    }

    [HideInInspector] public EnemyAttackState EnemyAttackState;
    public void TransitionToAttack()
    {
        Transistion(EnemyAttackState);
    }
}
