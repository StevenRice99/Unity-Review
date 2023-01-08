using UnityEngine;

/// <summary>
/// Handle the health of any kind of entity.
/// In this sample, we only have characters, so this easily could have been built into the character class.
/// However, this makes it easier if we with to expand a game to have destroyable cover, vehicles, etc.
/// Everything that could have health should only have one said health value, so disallow multiple components.
/// </summary>
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The maximum health this entity can have.")]
    [Range(1, 100)]
    private int maxHealth = 100;

    /// <summary>
    /// Internal variable to store the current health of this entity.
    /// </summary>
    private float _health;

    /// <summary>
    /// Heal the health.
    /// </summary>
    /// <param name="heal">The amount to heal for.</param>
    public void Heal(int heal)
    {
        _health += heal;

        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
    }

    /// <summary>
    /// Damage the health.
    /// </summary>
    /// <param name="damage">The amount to damage for.</param>
    public void Damage(int damage)
    {
        _health -= damage;

        // If dead, destroy the entire game object rather than just this component.
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void Awake()
    {
        // Start health off at the maximum health.
        // Since both variables are part of this same component, it is safe to do this in Awake().
        _health = maxHealth;
    }
}