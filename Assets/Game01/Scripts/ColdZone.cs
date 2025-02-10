using UnityEngine;

public class ColdZone : MonoBehaviour, IEnvironmentEffect
{
    [SerializeField] private float speedMultiplier = 0.5f; // 鈍足倍率（50%に遅くする）

    public void ApplyEffect(Player player)
    {
        if (!player.isSpeedProtected)
        {
            player.ModifySpeed(speedMultiplier); // 鈍足を適用
        }
    }

    public void RemoveEffect(Player player)
    {
        player.ModifySpeed(1f); // 速度を元に戻す
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            ApplyEffect(other.GetComponent<Player>());
            WarningUI.Instance.ShowWarning("寒冷地帯に入りました。鈍足になります。");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveEffect(other.GetComponent<Player>());
            //WarningUI.Instance.HideWarning();

        }
    }
}