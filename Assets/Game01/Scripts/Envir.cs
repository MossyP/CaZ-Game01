using Unity.VisualScripting;
using UnityEngine;

public class Envir : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 1f;
    private float damageTimer = 0f;
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f) // 1秒ごとにダメージを与える
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damagePerSecond);
                }

                damageTimer = 0f; // タイマーをリセット
            }
        }
    }
}
