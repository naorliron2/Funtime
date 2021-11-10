using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class State : MonoBehaviour
{
    public List<Transition> Transitions;
    [SerializeField] private Component[] components;
    protected FSM _myFSM;
    /// <summary>
    ///Function will be called at whatever intervals we set
    /// </summary>
    /// <param name="deltaTick"></param>
    public virtual void UpdateTick(float deltaTime, out bool checkTransition) { checkTransition = false; }
    public virtual void FixedUpdateTick(float deltaTime, out bool checkTransition) { checkTransition = false; }
    public virtual void LateUpdateTick(float deltaTime, out bool checkTransition) { checkTransition = false; }

    /// <summary>
    ///Function will be called when we first enter the state
    /// </summary>
    public virtual void OnStateEnter() { Init(); }

    /// <summary>
    /// Function will be called when we exit the state
    /// </summary>
    public virtual void OnStateExit() { }
    /// <summary>
    /// Binds the state to the correct FSM
    /// </summary>
    /// <param name="myFSM"></param>
    public virtual void Bind(FSM myFSM)
    {
        _myFSM = myFSM;
    }
    public abstract void Init();
    /// <summary>
    /// Check if we need to transition to a different state
    /// </summary>
    public virtual void CheckTransitions()
    {
        int counter = 0;
        //For each transition
        for (int i = 0; i < Transitions.Count; i++)
        {
            //Check their parameters
            foreach (var param in Transitions[i].conditions)
            {
                //Check if the parameter in the Transition and our FSM are equal
                if (_myFSM.GetParameterByName(param.Name) == param.Value)
                {
                    counter++;
                }
            }
            //If all the parameters were equals we switch state
            if (counter == Transitions[i].conditions.Length)
            {
                _myFSM.SwitchState(Transitions[i].StateToTransition);
            }
            counter = 0;
        }
    }
    /// <summary>
    /// Update out FSM Parameters
    /// </summary>
    public virtual void UpdateParameters() { }

    public T GetLocalComponent<T>() where T : Component
    {
        foreach (Component component in components)
        {
            if (component is T)
            {
                return (T)component;
            }
        }

        return null;
    }

}

