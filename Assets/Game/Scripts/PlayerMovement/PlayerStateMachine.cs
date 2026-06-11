using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
public class PlayerStateMachine : StateManager<PlayerStateMachine.EPlayerStateMachine>
{
    public enum EPlayerStateMachine 
    {
        Idle, Walk, Run,
        Jump, Fall
    }

    private PlayerContext _playerContext; // Обєкт який зберігає всі змінні які можуть використовувати стани які ми ініціалізуємо
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference runInput;
    [SerializeField] private InputActionReference jumpInput;
    [SerializeField] private P_ParametersSO playerParameters; // for now its SO but later i think i should use json or something like that to save player progress and parameters

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
