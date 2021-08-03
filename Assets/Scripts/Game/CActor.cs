using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorState { idle, walk, run, melee, die }

public abstract class CActor : CGameObject
{
    protected ActorState state;
    protected float walkSpeed;
    protected float runSpeed;

    protected void InitActor()
    {
        InitGameObject();
        state = ActorState.idle;
        walkSpeed = 1.0f;
        runSpeed = 2.0f;
    }

    public ActorState GetState() => state;

    public abstract void SetState(ActorState _state);
    public abstract void Walk();
    public abstract void Turn(float _angle);
    public abstract void Melee();
    public abstract void Idle();
}
