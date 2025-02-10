using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } //シングルトン
    
    private bool isTakingDamage = false;
    public bool isSpeedProtected = false;
    private bool isDamageProtected = false;
    private float coldPoint;
    private float coldPointMax = 100f;
    private bool isDead;
    private List<string> haveCondition = new List<string>();
    private List<string> itemList = new List<string>();
    private Rigidbody rb;
    
    [SerializeField] private float playerHP = 10f;
    [SerializeField] private float playerRotateSpeed = 5f;
    [SerializeField] private float playerMoveSpeed = 3f;
    private float baseMoveSpeed;
    
    private Coroutine damageCoroutine;

    private void Update()
    {
        Debug.Log(playerHP);
        if (isDead)
        {
            playerMoveSpeed = 0f;
        }
    }

    private void Awake()
    {
        isDead  = false;
        rb = TryGetComponent(out rb) ? rb : gameObject.AddComponent<Rigidbody>();
        baseMoveSpeed = playerMoveSpeed;
    }

    public void Move(Vector2 inputVector)
    {
        if (isDead) return;
        
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        moveDir = moveDir.normalized;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * playerRotateSpeed);
        transform.position += moveDir * (playerMoveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        if (isDead || isDamageProtected) return;
        
        playerHP -= damage;
        if (playerHP <= 0)
        {
            playerHP = 0;
            isDead = true;
            CheckDead();
        }
    }

    public void CheckDead()
    {
        if (isDead)
        {
            Debug.Log("Dead");
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }
    
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 指定した時間を待つ
        Destroy(gameObject);  // その後にオブジェクトを削除
    }

    private IEnumerator DamageOverTime(float damagePerSecond)
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartTakingDamage(float damagePerSecond)
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DamageOverTime(damagePerSecond));
        }
    }

    public void StopTakingDamage()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
            isTakingDamage = false;
        }
    }

    // ダメージ無効化を開始
    public void StartDamageProtection(float duration)
    {
        if (!isDamageProtected)
        {
            isDamageProtected = true;
            StartCoroutine(DamageProtectionTimer(duration));
        }
    }

    // ダメージ無効化の効果時間を管理するコルーチン
    private IEnumerator DamageProtectionTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDamageProtected = false; // 時間経過後に解除
    }

    // 鈍足無効化を開始
    public void StartSpeedProtection(float duration)
    {
        if (!isSpeedProtected)
        {
            isSpeedProtected = true;
            StartCoroutine(SpeedProtectionTimer(duration));
        }
    }

    // 鈍足無効化の効果時間を管理するコルーチン
    private IEnumerator SpeedProtectionTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        isSpeedProtected = false; // 時間経過後に解除
        
        ResetSpeed();
    }

    // 移動速度を変更する
    public void ModifySpeed(float multiplier)
    {
        playerMoveSpeed = baseMoveSpeed * multiplier;
    }

    // 移動速度を元に戻す
    public void ResetSpeed()
    {
        playerMoveSpeed = baseMoveSpeed; // 速度を元に戻す
    }
}
