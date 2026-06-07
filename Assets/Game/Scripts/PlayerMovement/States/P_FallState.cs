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
        return EPlayerStateMachine.Fall;
    }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
