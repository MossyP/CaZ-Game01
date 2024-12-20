using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputSystem inputSystem;
    [SerializeField] private float playerRotateSpeed = 5f;
    [SerializeField] private float playerMoveSpeed = 3f;
    private void Awake()
    {
        inputSystem = new InputSystem();
        inputSystem.Player.Enable();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector2 inputVector = inputSystem.Player.Move.ReadValue<Vector2>();
        Vector3 MoveDir = new Vector3(inputVector.x, 0, inputVector.y);
        MoveDir = MoveDir.normalized;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * playerRotateSpeed);
        transform.position += MoveDir * playerMoveSpeed * Time.deltaTime;
        
    }
}
