using UnityEngine;
using static PlayerStateMachine;

public class P_FallState : PlayerState
{
    public P_FallState(PlayerContext context, EPlayerStateMachine estate) : base(context, estate)
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
        else
        {
            return EPlayerStateMachine.Fall;
        }
    }

    public override void EnterState()
    {
        Debug.Log("Entered Fall State");
    }

    public override void UpdateState()
    {
        Context.UpdateMovementAndAnimator(0f, Context.InAirSpeed, 0f); // погратися з значеннями додати перевірки чи ми рухаємося щоб мати можливість контролювати персонажа в повітрі
    }

    public override void ExitState()
    {
        Debug.Log("Left Fall State");
        Context.P_Animator.SetBool("IsJump", false);
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
