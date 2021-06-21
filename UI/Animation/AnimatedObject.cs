using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    public Animator animator;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public string CurrentState() => currentState;

    public void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassing the current state
        currentState = newState;
    }
}
