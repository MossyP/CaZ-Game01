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
            WarningUI.Instance.ShowWarning("灼熱地帯に入りました。体力が減少します。");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveEffect(other.GetComponent<Player>());
            WarningUI.Instance.HideWarning();
        }
    }
}