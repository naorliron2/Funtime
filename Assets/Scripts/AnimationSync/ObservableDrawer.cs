#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;


public class ObservableDrawer<T,V> : PropertyDrawer where T:Observable<V>
{

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {

        var container = new VisualElement();
        var valueField = new PropertyField(property.FindPropertyRelative("value"));
        var button = new Button();
        container.Add(valueField);
        container.Add(button);
        DrawDragger(property, valueField.contentRect);

        Event.current.Use();
        return container;

    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      object[] attributes  = fieldInfo.GetCustomAttributes(typeof(EditableObservableAttribute), true);
        EditableObservableAttribute drawSettingsAttrib = null;
        if (attributes.Length > 0)
        {
            drawSettingsAttrib = (EditableObservableAttribute)attributes[0];
        }
        
        //base.OnGUI( position,property,label);
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        ScriptableObject propertySO = null;
        if (!property.hasMultipleDifferentValues && property.serializedObject.targetObject != null && property.serializedObject.targetObject is ScriptableObject)
        {
            propertySO = (ScriptableObject)property.serializedObject.targetObject;

        }

        
       


        if (drawSettingsAttrib==null ||drawSettingsAttrib.Editable)
        {
            var amountRect = new Rect(position.x, position.y, 150, position.height);
            GUI.enabled = false;
            EditorGUI.PropertyField(amountRect, property, GUIContent.none);
            GUI.enabled = true;

            DrawDragger(property, amountRect);
            EditorGUI.EndProperty();

            if (GUI.Button(new Rect(position.x + amountRect.width + 15, position.y, 35, position.height), "+"))
            {


                if (property.objectReferenceValue == null)
                    property.objectReferenceValue = (T)Observable<V>.CreateObservable<T>(property.serializedObject.targetObject.name + "/"+ property.name);

            }
            if (GUI.Button(new Rect(position.x + amountRect.width + 50, position.y, 35, position.height), "DEL"))
            {
                Observable<V>.DestroyObservable((property.objectReferenceValue as T));



            }

        }
        else
        {
            var amountRect = new Rect(position.x-30, position.y, 300, position.height);
            EditorGUI.PropertyField(amountRect, property, GUIContent.none);
            
        }


        //Event.current.Use();

    }
    
    private void DrawDragger(SerializedProperty property,Rect myarea)
    {
        Rect newArew = new Rect(0, myarea.y - 20 + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing - 1, myarea.width, myarea.height);
        GUI.Box(newArew, "");
        if (DragAndDrop.objectReferences != null && DragAndDrop.objectReferences.Length > 0)
        {
            

        }

        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

        if (!newArew.Contains(Event.current.mousePosition)) return;

        if (Event.current.type == EventType.MouseDown)
        {

            StartDrag(property);
            DragAndDrop.objectReferences = new Object[1] { (ScriptableObject)property.objectReferenceValue as T};
        }
        //Debug.Log(DragAndDrop.objectReferences[0].GetType());

    }

    private void StartDrag(SerializedProperty property)
    {
        // Clear out drag data (doesn't seem to do much)
        DragAndDrop.PrepareStartDrag();

        //      Debug.Log( "dragging " + focusedTask );

        // Set up what we want to drag
        DragAndDrop.SetGenericData("123", property.FindPropertyRelative("value"));


        // Clear anything we don't use, else we might get weird behaviour when dropping on
        // some other control

        DragAndDrop.paths = null;
        DragAndDrop.objectReferences = new UnityEngine.Object[0];


        // Start the actual drag (don't know what the name is for yet)
        DragAndDrop.StartDrag("Copy Task");
    }

}


[CustomPropertyDrawer(typeof(ObservableFloat))]
public class ObservableFloatDrawer : ObservableDrawer<ObservableFloat, float>
{


}
[CustomPropertyDrawer(typeof(ObservableInt))]
public class ObservableIntDrawer : ObservableDrawer<ObservableInt, int>
{

}
[CustomPropertyDrawer(typeof(ObservableString))]
public class ObservableStringDrawer : ObservableDrawer<ObservableString, string>
{

}

[CustomPropertyDrawer(typeof(ObservableBool))]
public class ObservableBoolDrawer : ObservableDrawer<ObservableBool, bool>
{

}

[CustomPropertyDrawer(typeof(ObservableTrigger))]
public class ObservableTriggerDrawer : ObservableDrawer<ObservableTrigger, bool>
{

}

#endif