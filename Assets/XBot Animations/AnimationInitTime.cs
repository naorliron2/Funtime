using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AnimationInitTime : MonoBehaviour
{
    public float beginTime = 0;

    private Animation m_Animation;
    private string m_DefaultClipName;

    private void Awake()
    {
        m_Animation = GetComponent<Animation>();
        m_DefaultClipName = m_Animation.clip.name;
        //m_Animation[m_DefaultClipName].normalizedTime = beginTime;
    }

#if UNITY_EDITOR


    [CustomEditor(typeof(AnimationInitTime))]
    private class AnimationInitTimeEditor : Editor
    {
        private Animation m_Animation;
        //Avoid conflict with animation editor
        private bool m_IsEditing;

        private void Awake()
        {
            if (target is AnimationInitTime obj)
            {
                m_Animation = obj.GetComponent<Animation>();
            }
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();

            EditorGUI.BeginChangeCheck();
            GUILayout.Label(m_IsEditing.ToString());
            m_IsEditing = GUILayout.Toggle(m_IsEditing, "Preview", GUI.skin.button, GUILayout.Height(31));
            if (EditorGUI.EndChangeCheck())
            {
                if (m_IsEditing)
                {
                    AnimationMode.StartAnimationMode();
                }
                else
                {
                    AnimationMode.StopAnimationMode();
                }
            }
        }

        public void OnSceneGUI()
        {
            if (!m_IsEditing) return;
            if (target is AnimationInitTime obj)
            {
                if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
                {
                    AnimationMode.BeginSampling();
                    AnimationMode.SampleAnimationClip(obj.gameObject, m_Animation.clip, obj.beginTime);
                    AnimationMode.EndSampling();
                    SceneView.RepaintAll();
                }
            }
        }
    }

#endif
}
