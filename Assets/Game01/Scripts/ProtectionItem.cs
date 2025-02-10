using UnityEngine;

public class ProtectionItem : MonoBehaviour, IItem
{
    [SerializeField] private bool protectDamage = false;  // ダメージを防ぐか
    [SerializeField] private bool protectSlow = false;    // 鈍足を防ぐか
    [SerializeField] private float protectionDuration = 60f; // 効果時間

    public void ActivateProtection(Player player, float duration)
    {
        // ダメージ防止
        if (protectDamage)
        {
            player.StartDamageProtection(duration);  // ダメージ保護を適用
        }
        
        // 鈍足防止
        if (protectSlow)
        {
            player.StartSpeedProtection(duration);  // 鈍足保護を適用
        }

        Destroy(gameObject);  // アイテムを使ったら消す
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // ダメージ中でも保護を適用
                ActivateProtection(player, protectionDuration);
            }
        }
    }
}
