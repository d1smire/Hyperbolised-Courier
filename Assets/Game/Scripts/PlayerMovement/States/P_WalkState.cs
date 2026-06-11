using UnityEngine;
using static PlayerStateMachine;

public class P_WalkState : PlayerState
{
    public P_WalkState(PlayerContext context, EPlayerStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    public override EPlayerStateMachine GetNextState() // Return the next state based on conditions
    {
        if (Context.IsJumping())
        {
            return EPlayerStateMachine.Jump;
        }
        else if (!Context.IsMoving())
        {
            return EPlayerStateMachine.Idle; 
        }
        else if (Context.IsRunning())
        {
            return EPlayerStateMachine.Run; 
        }
        else
            return EPlayerStateMachine.Walk;
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        Context.UpdateMovementAndAnimator(1f, Context.WalkSpeed, 0f);
    }

    public override void ExitState()
    {
        
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
