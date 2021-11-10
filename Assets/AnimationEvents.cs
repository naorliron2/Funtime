using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AnimationClip test_anim;
    public float timeSelected;
    public Animator animator;
    // Update is called once per frame
    void Update()
    {

        //Type[] ChildClasses = Assembly.GetAssembly(typeof(Editor)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Editor))));

        
    }
}
