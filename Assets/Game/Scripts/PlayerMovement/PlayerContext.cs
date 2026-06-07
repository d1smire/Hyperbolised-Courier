using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext
{
    // Variables for talking with all states
    private Animator _animator;
    private CharacterController _characterController;
    private InputActionReference _movementInput;
    private InputActionReference _runInput;
    private InputActionReference _jumpInput;
    private P_ParametersSO _playerParameters; // Reference to the ScriptableObject for player parameters



    // constructor
    public PlayerContext(Animator animator, CharacterController characterController, InputActionReference movementInput, InputActionReference runInput = null, InputActionReference jumpInput = null, P_ParametersSO playerParameters = null)
     {
        _animator = animator;
        _characterController = characterController;
        _movementInput = movementInput;
        _runInput = runInput;
        _jumpInput = jumpInput;
        _playerParameters = playerParameters;
        _movementInput?.action?.Enable();
        _runInput?.action?.Enable();
        _jumpInput?.action?.Enable();
     }

    // Readonly variables
    public Animator P_Animator => _animator;
    public CharacterController P_CharacterController => _characterController;

    public Vector2 MovementInput => _movementInput != null ? _movementInput.action.ReadValue<Vector2>() : Vector2.zero;

    // Movement properties for easy access and configuration
    public float WalkSpeed { get => _playerParameters.WalkSpeed; set => _playerParameters.WalkSpeed = value; }
    public float RunSpeed { get => _playerParameters.RunSpeed; set => _playerParameters.RunSpeed = value; }
    public float Gravity { get => _playerParameters.Gravity; set => _playerParameters.Gravity = value; }
    public float JumpForce { get => _playerParameters.JumpForce; set => _playerParameters.JumpForce = value; }
    public bool IsGrounded => _characterController.isGrounded;

    //Testing Gemini shit
    private float _currentAnimSpeed = 0f; // Змінна для збереження поточного стану анімації

    public bool IsMoving()
    {
        return MovementInput.magnitude > 0.1f; // Consider as moving if input magnitude is greater than a small threshold
    }

    public bool IsRunning()
    {
        return _runInput != null && _runInput.action.IsPressed();
    }

    //Gemini code
    public void UpdateMovementAndAnimator(float targetAnimSpeed, float moveSpeed)
    {
        // 1. Рух персонажа в просторі
        Vector2 input = MovementInput;
        Vector3 moveDirection = Vector3.zero;

        if (input.magnitude > 0.1f)
        {
            moveDirection = new Vector3(input.x, 0, input.y).normalized;
            // Тут за бажанням можна додати поворот персонажа в сторону руху:
            // _characterController.transform.forward = moveDirection;
        }

        // Рухаємо CharacterController (поки що без гравітації, як ти просив)
        P_CharacterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 2. Плавне змішування анімацій (Раніше персонаж смикався, тепер ні)
        // Збільшуючи або зменшуючи число 5f, ти регулюєш інертність (плавність) анімації
        _currentAnimSpeed = Mathf.MoveTowards(_currentAnimSpeed, targetAnimSpeed, Time.deltaTime * 5f);
        
        // Передаємо фінальне плавне значення в єдиний параметр аніматора
        P_Animator.SetFloat("Speed", _currentAnimSpeed);
    }


}
