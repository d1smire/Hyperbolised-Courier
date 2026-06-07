using UnityEngine;

[CreateAssetMenu(menuName = "Player/Parameters", fileName = "PlayerParametersSO")]
public class P_ParametersSO : ScriptableObject
{
    public float WalkSpeed = 3f;
    public float RunSpeed = 6f;
    public float Gravity = -9.81f;
    public float JumpForce = 5f;
}
