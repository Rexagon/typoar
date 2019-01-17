using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpriteActivator : MonoBehaviour
{
    public string variable;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
    }

    public void Activate()
    {
        animator.SetBool(variable, true);
    }

    public void Deactivate()
    {
        animator.SetBool(variable, false);
    }
}
