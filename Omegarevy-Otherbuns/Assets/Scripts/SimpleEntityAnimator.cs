using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleEntityAnimator : MonoBehaviour
{
    //Made to work with IMovement, IDamagable and IAttack.
    //Spriterenderer needs to be set on a child gameObject.

    public event EventHandler OnDeathAnimationCompleted;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private IMovementStrategy movement;
    [SerializeField] private IAttack attack;
    [SerializeField] private IDamageable damageable;
    [SerializeField] private bool DEBUGMODE;
    private bool isDeathAnimationCompleted;
    private enum AnimatorState
    {
        Still,
        Moving,
        Attacing,
        Hurt,
        Dead,
    }
    private AnimatorState currentAnimatorState = AnimatorState.Still;

    private void Start()
    {
        GetAndSetInterfaces();
        SetExternEventObservers();


    }
    void Update()
    {
        switch (currentAnimatorState)
        {
            case AnimatorState.Still:
            if(Mathf.Abs(movement.getXVelocity()) > 0.2f) ChangeToMoveAnim(this, EventArgs.Empty);
            break;

            case AnimatorState.Moving:
            CorrectSpriteRendererFlip(); //Flips the sprite if moving to the left
            if(Mathf.Abs(movement.getXVelocity()) < 0.2f) ChangeFromMoveAnim(this, EventArgs.Empty);
            break;

            case AnimatorState.Attacing:
            
            if(!AnimatorIsPlaying("Attack")) animator.Play("Attack", 0);
            else if(AnimatorIsPlaying())currentAnimatorState = AnimatorState.Still;
            
            break;

            case AnimatorState.Hurt:
            
            if(!AnimatorIsPlaying("Hurt")) animator.Play("Hurt", 0);
            else if(AnimatorIsPlaying())currentAnimatorState = AnimatorState.Still;
            
            break;

            case AnimatorState.Dead:

            if(!AnimatorIsPlaying("Dead")) animator.Play("Dead", 0);
            else if(!AnimatorIsPlaying())
            {
                isDeathAnimationCompleted = true;
                OnDeathAnimationCompleted?.Invoke(this, EventArgs.Empty);
            }
            break;

            default:
            currentAnimatorState = AnimatorState.Still;
            break;
        }
    }
    
    private void CorrectSpriteRendererFlip()
    {
        switch (movement.GetMovementDirection().horizontalMovementDirection)
        {
            case IMovementStrategy.HorizontalMovementDirection.Left:
            spriteRenderer.flipX = true;
            break;

            case IMovementStrategy.HorizontalMovementDirection.Right:
            spriteRenderer.flipX = false;
            break;
            
            default:
            break;
        }
    }
    private void ChangeToMoveAnim(object sender, EventArgs e)
    {   
        CorrectSpriteRendererFlip();
        if(movement.isGrounded)
        {
            animator.speed = movement.movementSpeed/5; //Standard movementspeed is 5
            animator.SetBool("isWalking", true);
            currentAnimatorState = AnimatorState.Moving;
        }
    }
    private void ChangeFromMoveAnim(object sender, EventArgs e)
    {
        animator.speed = 1;
        animator.SetBool("isWalking", false);
        if(currentAnimatorState == AnimatorState.Moving) currentAnimatorState = AnimatorState.Still;

    }
    private void ChangeToAttackAnim(object sender, EventArgs e)
    {
        currentAnimatorState = AnimatorState.Attacing;
    }
    private void ChangeToHurtAnim(object sender, EventArgs e)
    {
        currentAnimatorState = AnimatorState.Hurt;
    }
    private void ChangeToDeadAnim(object sender, EventArgs e)
    {
        currentAnimatorState = AnimatorState.Dead;
        animator.SetBool("isAlive", false);
    }
    private void GetAndSetInterfaces()
    {
        Component[] components = gameObject.GetComponents(typeof(MonoBehaviour));
        for (int i = 0; i < components.Length; i++)
        {
            if(movement == null & components[i] is IMovementStrategy) movement = components[i] as IMovementStrategy;
            if(attack == null & components[i] is IAttack) attack = components[i] as IAttack;
            if(damageable == null & components[i] is IDamageable) damageable = components[i] as IDamageable;
        }

        if (DEBUGMODE) Debug.Log("SimpleEditor getting and setting interfaces on gameobject: " + gameObject.name + "\n" + 
        "movement: " + (movement != null).ToString() + "\n" +
        "attack: " + (attack != null).ToString() + "\n" +
        "damagable: " + (damageable != null).ToString());
    }
    private void SetExternEventObservers()
    {
        if(movement != null)
        {
            movement.OnEntityStartMovement += ChangeToMoveAnim;
            movement.OnEntityStopMovement += ChangeFromMoveAnim;
        }
        if(attack != null)
        {
            attack.onAttack += ChangeToAttackAnim;
        }
        if(damageable != null)
        {
            damageable.onDamageRecieved += ChangeToHurtAnim;
            damageable.OnHealthMin += ChangeToDeadAnim;
        }
    }
    private bool AnimatorIsPlaying()
    {
        //Returns false if the currents animation is at end
        return (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
    private bool AnimatorIsPlaying(string stateName)
    {
        //Returns true if statName animation is being played
        return(AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));
    }
}
