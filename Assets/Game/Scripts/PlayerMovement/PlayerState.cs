using UnityEngine;

public abstract class PlayerState : BaseState<PlayerStateMachine.EPlayerStateMachine>
{
    protected PlayerContext Context;

    public PlayerState(PlayerContext context, PlayerStateMachine.EPlayerStateMachine stateKey) : base(stateKey)
    {
        Context = context;
    }

    // if need can add some methods for any state for deciding if need it and keep my code dry protected method
}
