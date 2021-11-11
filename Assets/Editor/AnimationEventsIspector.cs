using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor.Animations;

[CustomEditor(typeof(AnimationEvents))]
public class AnimationEventsIspector : Editor
{
    Editor gameObjectEditor;
    AnimationEvents animationEventsScript;
    GameObject gameObj;
    AnimatorController controller;

    AnimationClip chosenAnim;
    int chosenIndex;

    int enumSelected;

    const string LAYER_NAME = "(***Events Layer, Don't touch***)";

    SerializedProperty m_object;


    private void Awake()
    {
        animationEventsScript = target as AnimationEvents;
        gameObj = animationEventsScript.gameObject;
        animationEventsScript.test_anim = animationEventsScript.animator.runtimeAnimatorController.animationClips;

        /*Quick explanation, Unity makes it very hard to traverse animation states and layers in Mecanim, 
        *so the only way I found to play animations is to add an extra layer and add all of the animations there
        I can then play them from that layer*/
        AddLayer(LAYER_NAME);

        m_object = serializedObject.FindProperty("m_object");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        DrawButtons();

        if (controller && chosenAnim)
        {
            animationEventsScript.timeSelected = EditorGUILayout.Slider(animationEventsScript.timeSelected, 0, chosenAnim.length);

            EditorGUILayout.PropertyField(m_object);
            
            if (animationEventsScript.m_object)
            {
                MonoBehaviour[] components = animationEventsScript.m_object.GetComponents<MonoBehaviour>();
                string[] component_names = new string[components.Length];
                Debug.Log( animationEventsScript.m_object.name);
                for (int i = 0; i < components.Length; i++)
                {
                    component_names[i] = components[i].GetType().Name;
                }
                enumSelected = EditorGUILayout.Popup(enumSelected, component_names);
            }
            ApplyAnimations();

            DrawPreviewWindow();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void ApplyAnimations()
    {
        chosenAnim.SampleAnimation(gameObj, animationEventsScript.timeSelected);

        Animator animator = animationEventsScript.animator;

        animator.Play(chosenAnim.name, animator.GetLayerIndex(LAYER_NAME), Mathf.InverseLerp(0, chosenAnim.length, animationEventsScript.timeSelected));
        animator.SetLayerWeight(animator.GetLayerIndex(LAYER_NAME), 1);
        animator.Update(animationEventsScript.timeSelected);
    }


    private void OnDestroy()
    {
        animationEventsScript.m_object = null;

        foreach (var item in controller.layers)
        {
            if (item.name == LAYER_NAME)
            {
                controller.RemoveLayer(animationEventsScript.animator.GetLayerIndex(LAYER_NAME));
            }
        }
    }

    private void AddLayer(string name)
    {
        controller = animationEventsScript.animator.runtimeAnimatorController as AnimatorController;
        controller.AddLayer(name);

        for (int i = 0; i < animationEventsScript.test_anim.Length; i++)
        {
            AnimatorState state = controller.layers[controller.layers.Length - 1].stateMachine.AddState(animationEventsScript.test_anim[i].name);
            state.motion = animationEventsScript.test_anim[i];
        }

        animationEventsScript.animator.Update(animationEventsScript.timeSelected);
    }

    private void DrawPreviewWindow()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (gameObjectEditor == null)
            gameObjectEditor = CreateEditor(gameObj);

        GUIStyle bgColor = new GUIStyle();

        gameObjectEditor.ReloadPreviewInstances();
        gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);

        EditorGUILayout.EndVertical();
    }

    private void DrawButtons()
    {
        GUILayout.BeginVertical();
        for (int i = 0; i < animationEventsScript.test_anim.Length; i++)
        {
            Color color = GUI.backgroundColor;
            if (chosenIndex == i)
            {
                GUI.backgroundColor = Color.cyan;
            }
            if (GUILayout.Button(animationEventsScript.test_anim[i].name))
            {
                chosenAnim = animationEventsScript.test_anim[i];
                animationEventsScript.timeSelected = 0;
                chosenIndex = i;
            }

            GUI.backgroundColor = color;
        }
        GUILayout.EndVertical();
    }

}