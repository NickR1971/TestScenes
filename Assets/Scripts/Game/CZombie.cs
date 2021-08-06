using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombie : CActor
{
    private Animator animator;

    void Start()
    {
        InitActor();
        animator = GetComponent<Animator>();
        walkSpeed = 0.5f;
        runSpeed = 2.0f;
    }

    public override void SetState(ActorState _state)
    {
        state = _state;
        switch (state)
        {
            case ActorState.walk:
                animator.SetBool("walk", true);
                break;
            case ActorState.run:
                animator.SetBool("run", true);
                break;
            case ActorState.melee:
                animator.SetBool("attack", true);
                break;
            default:
                animator.SetBool("attack", false);
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                break;
        }
    }

    public override void Walk()
    {
        SetState(ActorState.walk);
        positionControl.MoveForward(walkSpeed);
    }

    public override void Turn(float _angle)
    {
        positionControl.Rotate(_angle);
    }

    public override void Melee()
    {
        SetState(ActorState.melee);
    }

    public override void Idle()
    {
        SetState(ActorState.idle);
    }
}
