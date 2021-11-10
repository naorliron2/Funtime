using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//#if UNITY_EDITOR
//using UnityEditor;
//[CustomPropertyDrawer(typeof(ObservableGUISettingsAttribute))]
//public class ObservableGUISettingsAttributeDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        base.OnGUI(position, property, label);
//    }
//}




//#endif

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
sealed class EditableObservableAttribute : PropertyAttribute
{

    bool editable = true;

    public bool Editable { get => editable; }

    public EditableObservableAttribute(bool editable)
    {
        this.editable = editable;
    }


}