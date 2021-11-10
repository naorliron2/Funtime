using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimationSync : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] List<ObservableFloatWithParamName> floats = new List<ObservableFloatWithParamName>();
    [SerializeField] List<ObservableIntWithParamName> ints = new List<ObservableIntWithParamName>();
    // [SerializeField] List<ObservableStringWithParamName> strings = new List<ObservableStringWithParamName>();
    [SerializeField] List<ObservableBoolWithParamName> bools = new List<ObservableBoolWithParamName>();
    [SerializeField] List<ObservableTriggerWithParamName> triggers = new List<ObservableTriggerWithParamName>();

    private void Update()
    {
        foreach (var item in floats)
        {
            if (item.ObservableObj != null)
                animator.SetFloat(item.parameterName, item.ObservableObj.GetValue());
        }
        foreach (var item in ints)
        {
            if (item.ObservableObj != null)
                animator.SetInteger(item.parameterName, item.ObservableObj.GetValue());
        }
        foreach (var item in bools)
        {
            if (item.ObservableObj != null)
                animator.SetBool(item.parameterName, item.ObservableObj.GetValue());
        }
        foreach (var item in triggers)
        {
            if (item.ObservableObj != null && item.ObservableObj.GetValue())
                animator.SetTrigger(item.parameterName);
        }
    }


    
    public void addObservable(ScriptableObject obj)
    {
        if (obj is ObservableFloat)
        {
            floats.Add(new ObservableFloatWithParamName(obj.GetType().Name, obj as ObservableFloat));

        }
        else if (obj is ObservableInt)
        {
            ints.Add(new ObservableIntWithParamName(obj.GetType().Name, obj as ObservableInt));

        }
        else if (obj is ObservableString)
        {
            //strings.Add(new ObservableStringWithParamName(t.GetType().Name, t as ObservableString));

        }
        else if (obj is ObservableBool)
        {
            bools.Add(new ObservableBoolWithParamName(obj.GetType().Name, obj as ObservableBool));
        }

        else if (obj is ObservableTrigger)
        {
            triggers.Add(new ObservableTriggerWithParamName(obj.GetType().Name, obj as ObservableTrigger));
        }
        else
        {
            throw new Exception(obj.GetType() + " does not derive from Observable<T>");
        }
    }
}
[Serializable]
struct ObservableFloatWithParamName
{
    [EditableObservable(false)] public ObservableFloat ObservableObj;

    public string parameterName;

    public ObservableFloatWithParamName(string parameterName, ObservableFloat ObservableObj)
    {
        this.ObservableObj = ObservableObj;
        this.parameterName = parameterName;
    }
}
[Serializable]

struct ObservableIntWithParamName
{
    [EditableObservable(false)] public ObservableInt ObservableObj;

    public string parameterName;

    public ObservableIntWithParamName(string parameterName, ObservableInt ObservableObj)
    {
        this.ObservableObj = ObservableObj;
        this.parameterName = parameterName;
    }
}
[Serializable]

struct ObservableStringWithParamName
{
    [EditableObservable(false)] public ObservableString ObservableObj;

    public string parameterName;

    public ObservableStringWithParamName(string parameterName, ObservableString ObservableObj)
    {
        this.ObservableObj = ObservableObj;
        this.parameterName = parameterName;
    }
}
[Serializable]

struct ObservableBoolWithParamName
{
    [EditableObservable(false)] public ObservableBool ObservableObj;

    public string parameterName;

    public ObservableBoolWithParamName(string parameterName, ObservableBool ObservableObj)
    {
        this.ObservableObj = ObservableObj;
        this.parameterName = parameterName;
    }
}

[System.Serializable]
struct ObservableTriggerWithParamName
{
    [EditableObservable(false)] public ObservableTrigger ObservableObj;

    public string parameterName;

    public ObservableTriggerWithParamName(string parameterName, ObservableTrigger ObservableObj)
    {
        this.ObservableObj = ObservableObj;
        this.parameterName = parameterName;
    }
}
