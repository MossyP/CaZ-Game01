using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } //シングルトン
    
    private float coldPoint;
    private float coldPointMax = 100f;
    private bool isDead = false;
    private List<string> haveCondition = new List<string>();
    private List<string> itemList = new List<string>();
    private Rigidbody rb;
    
    [SerializeField] private float playerHP = 10f;
    [SerializeField] private float playerRotateSpeed = 5f;
    [SerializeField] private float playerMoveSpeed = 3f;
    private float baseMoveSpeed;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        rb = TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
        baseMoveSpeed = playerMoveSpeed;
    }

    public void Move(Vector2 inputVector)
    {
        Vector3 MoveDir = new Vector3(inputVector.x, 0, inputVector.y);
        MoveDir = MoveDir.normalized;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * playerRotateSpeed);
        transform.position += MoveDir * playerMoveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        playerHP -= damage;
        if (playerHP <= 0)
        {
            isDead = true;
            CheckDead();
        }
    }

    public void CheckDead()
    {
        if (isDead)
        {
            Debug.Log("Dead");
        }
    }
    
    public void ApplyEffect(string effectName, float damage = 0, float speedModifier = 1f) //気候変動を受ける
    {
        if (!haveCondition.Contains(effectName))
        {
            haveCondition.Add(effectName);
        }

        // スピード低下（寒冷用）
        playerMoveSpeed = baseMoveSpeed * speedModifier;

        // ダメージ処理
        if (damage > 0)
        {
            TakeDamage(damage);
        }
    }
    
    public void RemoveEffect(string effectName)
    {
        if (haveCondition.Contains(effectName))
        {
            haveCondition.Remove(effectName);
            playerMoveSpeed = baseMoveSpeed; // 元のスピードに
        }
    }
    
    
    //防御アイテムの効果発動
    public void ActivateProtection(string effectName, float duration)
    {
        if (!haveCondition.Contains(effectName))
        {
            haveCondition.Add(effectName);
            StartCoroutine(RemoveProtectionAfterTime(effectName, duration));
        }
    }

    private System.Collections.IEnumerator RemoveProtectionAfterTime(string effectName, float duration)
    {
        yield return new WaitForSeconds(duration);
        haveCondition.Remove(effectName);
    }

    //防御中か判定
    public bool HasProtection(string effectName)
    {
        return haveCondition.Contains(effectName);
    }
    
    
}
