using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } //シングルトン
    
    private float coldPoint;
    private float coldPointMax = 100f;
    private bool isDead = false;
    private List<int> haveCondition = new List<int>();
    private List<int> itemList = new List<int>();
    private Rigidbody rb;
    
    [SerializeField] private float playerHP = 10f;
    [SerializeField] private float playerRotateSpeed = 5f;
    [SerializeField] private float playerMoveSpeed = 3f;
    
    private void Awake()
    {
        rb = TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
    }

    public void Move(Vector2 inputVector)
    {
        Vector3 MoveDir = new Vector3(inputVector.x, 0, inputVector.y);
        MoveDir = MoveDir.normalized;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * playerRotateSpeed);
        transform.position += MoveDir * playerMoveSpeed * Time.deltaTime;
    }

    public void Damaged(float damage)
    {
        playerHP -= damage;
    }

    public void CheckDead()
    {
        if (isDead)
        {
            Debug.Log("Dead");
        }
    }
}
