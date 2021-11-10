#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AnimationSync))]
public class AnimationSyncInspector : Editor
{
    AnimationSync myTarget;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        myTarget = (AnimationSync)target;

        DropAreaGUI();
    }
    public void DropAreaGUI()
    {
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(drop_area, "Add Observable");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        if (dragged_object is ScriptableObject)
                        {
                            myTarget.addObservable((ScriptableObject)dragged_object);
                        }
                        else
                        {
                            throw new System.Exception("The object you are trying to add is not an observable object");
                        }
                    }
                }
                break;
        }
    }
}
#endif