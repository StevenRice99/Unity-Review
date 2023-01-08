using UnityEngine;

/// <summary>
/// Weapon class for our simulation.
/// Although we only use a single weapon on certain characters, what's stopping something from having multiple?
/// Thus, we don't disallow multiple components to ensure this can more easily be repurposed if this game is expanded.
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much damage this weapon does.")]
    [Min(1)]
    private int damage = 1;

    /// <summary>
    /// Damage a given entity.
    /// </summary>
    /// <param name="health">The health component to damage.</param>
    public void Damage(Health health)
    {
        health.Damage(damage);
    }
}