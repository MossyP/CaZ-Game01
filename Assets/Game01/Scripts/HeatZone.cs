using UnityEngine;

public class HotZone : MonoBehaviour, IEnvironmentEffect
{
    [SerializeField] private float damagePerSecond = 1f; // 1秒ごとのダメージ

    public void ApplyEffect(Player player)
    {
        player?.StartTakingDamage(damagePerSecond); // ダメージ開始
    }

    public void RemoveEffect(Player player)
    {
        player?.StopTakingDamage(); // ダメージ停止
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.GetComponent<Player>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveEffect(other.GetComponent<Player>());
        }
    }
}