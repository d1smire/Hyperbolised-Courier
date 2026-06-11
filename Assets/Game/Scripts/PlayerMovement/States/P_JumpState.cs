using UnityEngine;
using static PlayerStateMachine;

public class P_JumpState : PlayerState
{
    public P_JumpState(PlayerContext context, EPlayerStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    public override EPlayerStateMachine GetNextState() // Return the next state based on conditions
    {
        if (Context.IsGrounded)
        {
            if(Context.IsMoving())
            {
                if (Context.IsRunning())
                {
                    return EPlayerStateMachine.Run; 
                }
                else
                {
                    return EPlayerStateMachine.Walk; 
                }
            }
            else
            {
                return EPlayerStateMachine.Idle; 
            }
        }
        else if (!Context.IsGrounded)
        {
            return EPlayerStateMachine.Fall;    
        }
        else
        {
            return EPlayerStateMachine.Jump;
        }
    }

    public override void EnterState()
    {
        Debug.Log("Entered Jump State");
        Context.P_Animator.SetBool("IsJump", true);
        Context.UpdateMovementAndAnimator(0f, 0f, Context.JumpForce);
    }

    public override void UpdateState() { }

    public override void ExitState()
    {
        Debug.Log("Left Jump State");
        Context.IsJumping();
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
