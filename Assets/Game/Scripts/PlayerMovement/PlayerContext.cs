using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContext
{
    private Animator _animator;
    private CharacterController _characterController;
    
    // Input actions
    private InputActionReference _movementInput;
    private InputActionReference _runInput;
    private InputActionReference _jumpInput;  

    private P_ParametersSO _playerParameters; // SO
    private Vector3 _gravityVector; 
    private bool _isJump;
    private float _inAirControlMultiplier = 0.75f;
    private float _inAirMoveSpeed = 0f;


    // constructor
    public PlayerContext(Animator animator, CharacterController characterController, InputActionReference movementInput, 
    InputActionReference runInput = null, InputActionReference jumpInput = null, P_ParametersSO playerParameters = null)
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
    public Vector3 GravityVector { get => _gravityVector; set => _gravityVector = value; }

    // Movement properties for easy access and configuration
    public float WalkSpeed { get => _playerParameters.WalkSpeed; set => _playerParameters.WalkSpeed = value; }
    public float RunSpeed { get => _playerParameters.RunSpeed; set => _playerParameters.RunSpeed = value; }
    public float Gravity { get => _playerParameters.Gravity; set => _playerParameters.Gravity = value; }
    public float JumpForce { get => _playerParameters.JumpForce; set => _playerParameters.JumpForce = value; }
    public bool IsGrounded => _characterController.isGrounded;
    public float InAirSpeed { get => _inAirMoveSpeed; set => _inAirMoveSpeed = value; }

    //Testing Gemini / variables 
    private float _currentAnimSpeed = 0f; // Змінна для збереження поточного стану анімації

    public bool IsMoving()
    {
        return MovementInput.magnitude > 0.1f;
    }

    public bool IsRunning()
    {
        return _runInput != null && _runInput.action.IsPressed();
    }

    public bool IsJumping()
    {
        _isJump = _jumpInput != null && _jumpInput.action.IsPressed() && IsGrounded;
        return _isJump;
    }

    public void SetInAirSpeed()
    {
        if (IsMoving())
        {
            _inAirMoveSpeed = IsRunning() ? RunSpeed * _inAirControlMultiplier : WalkSpeed * _inAirControlMultiplier;
        }
        else
        {
            _inAirMoveSpeed = 0f;
        }
    }

    //Testing Gemini / methods
    public void UpdateMovementAndAnimator(float targetAnimSpeed, float moveSpeed, float jumpForce)
    {
        // 1. Зчитуємо інпут та створюємо вектор руху (по X та Z)
        Vector2 input = MovementInput;
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y).normalized;

        // 2. Поворот персонажа (якщо є рух)
        if (input.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            P_CharacterController.transform.rotation = Quaternion.RotateTowards(
                P_CharacterController.transform.rotation, 
                targetRotation, 
                Time.deltaTime * 500f
            );
        }

        if(_isJump)
        {
            _gravityVector.y = jumpForce;
        }

        // 3. Розрахунок кінцевого вектора швидкості
        // Множимо напрямок руху на швидкість
        Vector3 finalVelocity = moveDirection * moveSpeed;

        // Оновлюємо внутрішній GravityVector залежно від заземлення
        if (!IsGrounded && !_isJump)
        {
            _gravityVector.y += Gravity * Time.deltaTime;
        }
        else if (_gravityVector.y < 0) // додаємо умову, щоб не скидати під час стрибка
        {
            _gravityVector.y = -2f; 
        }

        // Додаємо гравітацію (вісь Y) до нашого загального вектора руху
        finalVelocity.y = _gravityVector.y;

        // 4. ОДИН єдиний виклик Move для всього руху!
        P_CharacterController.Move(finalVelocity * Time.deltaTime);

        // 5. Анімації
        _currentAnimSpeed = Mathf.MoveTowards(_currentAnimSpeed, targetAnimSpeed, Time.deltaTime * 5f);
        P_Animator.SetFloat("Speed", _currentAnimSpeed);
        SetInAirSpeed();
    }
}
