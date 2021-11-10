using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(AnimationEvents))]
public class AnimationEventsIspector : Editor
{
    Editor gameObjectEditor;
    AnimationEvents animationEventsScript;
    private bool m_IsEditing;
    GameObject gameObj;
    Editor myEditor;
    public override VisualElement CreateInspectorGUI()
    {
        animationEventsScript = target as AnimationEvents;
        gameObj = animationEventsScript.gameObject;
        return base.CreateInspectorGUI();
    }
    float time;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        //animationEventsScript.test_anim = EditorGUILayout.ObjectField(animationEventsScript.test_anim, typeof(AnimationClip[]), false) as AnimationClip[];
        animationEventsScript.animator = EditorGUILayout.ObjectField(animationEventsScript.animator, typeof(Animator), true) as Animator;
        animationEventsScript.timeSelected = EditorGUILayout.Slider(animationEventsScript.timeSelected, 0, animationEventsScript.test_anim.length);
        EditorGUILayout.EndVertical();

        //if (myEditor == null)
        //{
        //    myEditor = CreateEditor(animationEventsScript.test_anim);
        //    myEditor.HasPreviewGUI();
        //}

        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //myEditor.OnPreviewSettings();
        //GUILayout.EndHorizontal();
        //myEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), EditorStyles.whiteLabel);


        EditorGUILayout.BeginVertical("box");
        GUILayout.FlexibleSpace();

        if (gameObjectEditor == null)
            gameObjectEditor = CreateEditor(gameObj);

        GUIStyle bgColor = new GUIStyle();
      
        animationEventsScript.test_anim.SampleAnimation(gameObj, animationEventsScript.timeSelected);
        Animator animator = animationEventsScript.animator;
        animator.Play(animationEventsScript.test_anim.name, 0, Mathf.InverseLerp(0, animationEventsScript.test_anim.length, animationEventsScript.timeSelected));
        animator.Update(animationEventsScript.timeSelected);
        gameObjectEditor.ReloadPreviewInstances();
        EditorGUILayout.EndVertical();
        gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);


    }
}