using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class FSM : MonoBehaviour
{
    [Header("My Dependencies")]
    public FSMDependencies Dependencies;
    [Header("State Info")]
    [SerializeField] protected State currentState;
    public BoolNamePair[] Parameters;
    State[] states;

    //Responsible for switching states
    public virtual void SwitchState(State newState)
    {
        //Call the old states exit function
        if (currentState != null)
            currentState.OnStateExit();
        //Switch the current state
        currentState = newState;
        currentState.Bind(this);
        //Call the new states enter function
        if (currentState != null)
            currentState.OnStateEnter();

    }

    private void FixedUpdate()
    {
        bool checkTransition;
        currentState.FixedUpdateTick(Time.fixedDeltaTime, out checkTransition);

        if (checkTransition)
            currentState.CheckTransitions();
    }
    private void Update()
    {
        bool checkTransition;
        currentState.UpdateTick(Time.deltaTime, out checkTransition);

        if (checkTransition)
            currentState.CheckTransitions();
    }
    private void LateUpdate()
    {
        bool checkTransition;
        currentState.LateUpdateTick(Time.deltaTime, out checkTransition);

        if (checkTransition)
            currentState.CheckTransitions();
    }

    private void Awake()
    {
        states = GetComponents<State>();

        foreach (var item in states)
        {
            item.Init();
        }
    }
    //This function get a value from the parameter list
    public virtual bool GetParameterByName(string name)
    {
        //Check if a parameter has a certain name
        foreach (var param in Parameters)
        {
            // If it does return its value(always a bool)
            if (param.GetName() == name) return param.GetValue();
        }

        return false;
    }

    //this function sets a value to a parameter from the list
    public virtual void SetParameterByName(string name, bool value)
    {
        //Check if a parameter has a certain name
        for (int i = 0; i < Parameters.Length; i++)
        {
            //Set the value of that parameter
            if (Parameters[i].GetName() == name) { Parameters[i].SetValue(value); }
        }

    }
}

