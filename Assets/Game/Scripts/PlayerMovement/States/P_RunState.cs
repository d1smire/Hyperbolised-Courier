using UnityEngine;
using static PlayerStateMachine;

public class P_RunState : PlayerState
{
    public P_RunState(PlayerContext context, EPlayerStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    public override EPlayerStateMachine GetNextState() // Return the next state based on conditions
    { 
        if (!Context.IsRunning() || !Context.IsMoving())
        {
            if (Context.IsMoving())
            {
                return EPlayerStateMachine.Walk; 
            }
            else
            {
                return EPlayerStateMachine.Idle; 
            }
        }
        else
        {
            return EPlayerStateMachine.Run;
        }
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        Context.UpdateMovementAndAnimator(2f, Context.RunSpeed);
    }

    public override void ExitState()
    {
        if (!Context.IsMoving())
        {
            Context.P_Animator.SetFloat("IdleVariation", 2f);
        }
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
