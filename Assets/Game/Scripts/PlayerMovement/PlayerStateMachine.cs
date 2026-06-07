using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerStateMachine>
{
    public enum EPlayerStateMachine 
    {
        Idle, Walk, Run,
        Jump, Fall
    }

    private PlayerContext _playerContext; // Обєкт який зберігає всі змінні для передачі їх між станами

    // можна додавати змінні які потім будуть використовуватися для зміни параметрів або для IK анімації.
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference runInput;
    [SerializeField] private InputActionReference jumpInput;
    [SerializeField] private P_ParametersSO playerParameters;

    private void Awake()
    {
        if (characterController == null || animator == null)
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }
        if (movementInput != null)
        {
            _playerContext = new PlayerContext(animator, characterController, movementInput, runInput, jumpInput, playerParameters);
        }
        InitializeStates();
    }

    private void InitializeStates() 
    {
        States.Add(EPlayerStateMachine.Idle, new P_IdleState(_playerContext, EPlayerStateMachine.Idle));
        States.Add(EPlayerStateMachine.Walk, new P_WalkState(_playerContext, EPlayerStateMachine.Walk));
        States.Add(EPlayerStateMachine.Run, new P_RunState(_playerContext, EPlayerStateMachine.Run));
        States.Add(EPlayerStateMachine.Jump, new P_JumpState(_playerContext, EPlayerStateMachine.Jump));
        States.Add(EPlayerStateMachine.Fall, new P_FallState(_playerContext, EPlayerStateMachine.Fall));
        CurrentState = States[EPlayerStateMachine.Idle];
    }

}
