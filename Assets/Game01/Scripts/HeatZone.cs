using UnityEngine;

public class HeatZone : MonoBehaviour
{
    public string EffectName => "Heat Wave";
    public int damagePerSecond = 1;
    private float damageInterval = 1f;
    private float damageTimer = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            // 防御中ならダメージなし
            if (player.HasProtection(EffectName)) return;

            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                player.ApplyEffect(EffectName, damagePerSecond);
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.RemoveEffect(EffectName);
        }
    }
}