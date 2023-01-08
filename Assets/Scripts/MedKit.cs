using UnityEngine;

/// <summary>
/// Med kit class for our simulation.
/// Although we only use a single weapon on certain characters, what's stopping something from having multiple?
/// Thus, we don't disallow multiple components to ensure this can more easily be repurposed if this game is expanded.
/// </summary>
public class MedKit : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much healing this med kit does.")]
    [Min(1)]
    private int healing = 1;
    
    /// <summary>
    /// Heal a given entity.
    /// </summary>
    /// <param name="health">The health component to heal.</param>
    public void Heal(Health health)
    {
        health.Heal(healing);
    }
}