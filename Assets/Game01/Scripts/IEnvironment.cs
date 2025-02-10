using UnityEngine;

public interface IEnvironmentEffect
{
    void ApplyEffect(Player player);
    void RemoveEffect(Player player);
}