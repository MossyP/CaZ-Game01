using UnityEngine;

public class ColdZone : MonoBehaviour
{
    public string EffectName => "Cold Freeze";
    public float slowDownFactor = 0.5f;
    public int coldDamage = 1;
    public float maxColdTime = 10f;
    private float coldTimer = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            // 防御中なら影響なし
            if (player.HasProtection(EffectName)) return;

            player.ApplyEffect(EffectName, coldDamage, slowDownFactor);
            coldTimer += Time.deltaTime;
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