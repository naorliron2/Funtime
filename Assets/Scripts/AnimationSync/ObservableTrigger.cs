using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableTrigger : Observable<bool>
{
    public override bool GetValue()
    {
        bool returnVal = value;
        SetValue(false);
        return returnVal;
    }

     public override void SetGetterFunction(ObservableDataGetFunc func)
    {
        throw new System.NotSupportedException("Trigger does not support getter functions");
        
    }
}
