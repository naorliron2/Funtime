using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
public class AnimationEvents : MonoBehaviour
{
    [HideInInspector] public AnimationClip[] test_anim;
    [HideInInspector] public float timeSelected;
    public Animator animator;
    [HideInInspector] public GameObject m_object;
    public UnityEvent Event;
    // Update is called once per frame
    void Update()
    {
        
    }
}
