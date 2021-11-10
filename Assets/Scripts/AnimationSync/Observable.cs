using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Observable<T>:ScriptableObject
{
    protected T value;
    public delegate T ObservableDataGetFunc();

    protected ObservableDataGetFunc getterFuncion;

    public virtual void SetValue(T value)
    {
      
        this.value = value;

    }

    public virtual void SetGetterFunction(ObservableDataGetFunc func)
    {
        getterFuncion = func;

    }

    public virtual T GetValue()
    {
        if (getterFuncion != null)
        {
            return getterFuncion();
        }

        return value;
    }

    public static Observable<T> CreateObservable<V>(string Path="") where V : Observable<T>
    {
        Observable<T> obj = CreateInstance<V>();
        obj.name = Path;
        return obj;
    }

    public static void DestroyObservable(Observable<T> obj)
    {
        ScriptableObject.DestroyImmediate(obj);
        Resources.UnloadAsset(obj);
    }


}


