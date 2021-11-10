using System;
using UnityEngine;


[System.Serializable]
//Struct that holds all the dependencies out states will ever need
public struct FSMDependencies
{
    public Rigidbody rigidBody;
    public Camera camera;
}


[System.Serializable]
public struct BoolNamePair
{
    [SerializeField]
    private string name;
    [SerializeField]
    private bool value;

    public BoolNamePair(string name, bool value)
    {
        this.name = name;
        this.value = value;
    }
    public void SetValue(bool value)
    {
        this.value = value;
    }

    public bool GetValue()
    {
        return value;
    }


    public string GetName()
    {
        return name;
    }

}

[System.Serializable]
public struct ConditionParameter
{
    public string Name;
    public bool Value;
}

[System.Serializable]
public struct Transition
{
    public State StateToTransition;
    public ConditionParameter[] conditions;

}




