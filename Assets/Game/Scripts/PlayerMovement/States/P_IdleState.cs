using UnityEngine;
using static PlayerStateMachine;

public class P_IdleState : PlayerState
{
    private float _idleTimer;
    private float _timeToChangeIdle;
    
    private float _targetIdleType = 0f;  // До якого айдлу прагнемо (0, 1 або 2)
    private float _currentIdleType = 0f; // Поточне згладжене значення аніматора

    // Налаштування таймера для зміни айдлу
    private const float MinIdleTime = 8f;  // Мінімум 8 секунд в одному айдлі
    private const float MaxIdleTime = 15f; // Максимум 15 секунд

    public P_IdleState(PlayerContext context, EPlayerStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    public override EPlayerStateMachine GetNextState() // Return the next state based on conditions
    {
        if (Context.IsJumping())
        {
            return EPlayerStateMachine.Jump;
        }
        else if (Context.IsMoving())
        {
            return EPlayerStateMachine.Walk;
        }
        else
            return EPlayerStateMachine.Idle;
    }

    public override void EnterState()
    {
        _idleTimer = 0f;
        _timeToChangeIdle = Random.Range(MinIdleTime, MaxIdleTime);
        _targetIdleType = 0f;
    }

    public override void UpdateState()
    {
        Context.UpdateMovementAndAnimator(0f, 0f, 0f);

        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _timeToChangeIdle)
        {
            _idleTimer = 0f;
            _timeToChangeIdle = Random.Range(MinIdleTime, MaxIdleTime);

            _targetIdleType = Random.Range(0, 2); 
        }

        _currentIdleType = Mathf.MoveTowards(_currentIdleType, _targetIdleType, Time.deltaTime * 3f); // Число 3f регулює швидкість перетікання айдлів між собою. Менше число — м'якший перехід, більше — швидший.

        Context.P_Animator.SetFloat("IdleVariation", _currentIdleType);
    }

    public override void ExitState()
    {
        Context.P_Animator.SetFloat("IdleVariation", 0f);
    }

    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
}
