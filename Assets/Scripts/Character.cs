using UnityEngine;

/// <summary>
/// Character class for our simulation.
/// Each character must have a health component, so we can require it.
/// This ensures that if we add a character component in the editor, it will automatically add a health component also.
/// There isn't a need for there to be two characters attached to one object, so disallow this.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The team number to distinguish between allies and enemies.")]
    private int teamNumber;

    [SerializeField]
    [Tooltip("Speed in meters per second which the character moves.")]
    [Min(0)]
    private float moveSpeed = 1;

    /// <summary>
    /// Get the team number of the character.
    /// </summary>
    public int TeamNumber => teamNumber;

    /// <summary>
    /// Health of the character.
    /// </summary>
    private Health _health;
    
    /// <summary>
    /// Weapon of the character, if it has one.
    /// </summary>
    private Weapon _weapon;
    
    /// <summary>
    /// Med kit of the character, if it has one.
    /// </summary>
    private MedKit _medKit;

    /// <summary>
    /// Attack if they have a weapon and heal if they have a health kit.
    /// </summary>
    public void Act()
    {
        // Can only attack if they have a weapon.
        if (_weapon != null)
        {
            // Find an enemy and damage them if one was found.
            Character enemy = GameManager.FindEnemy(this);
            if (enemy != null)
            {
                _weapon.Damage(enemy._health);
                Debug.Log($"{name} attacked {enemy.name}!");
            }
        }

        // Can only heal if they have a med kit.
        if (_medKit != null)
        {
            // Find an ally and heal them if one was found.
            Character ally = GameManager.FindAlly(this);
            if (ally != null)
            {
                _medKit.Heal(ally._health);
                Debug.Log($"{name} healed {ally.name}!");
            }
        }
    }

    private void Start()
    {
        // Get the health, weapon, and med kit.
        // Since these are other components, this is done here in Start() rather than Awake()
        _health = GetComponent<Health>();
        _weapon = GetComponent<Weapon>();
        _medKit = GetComponent<MedKit>();
        
        // Add this character to the singleton.
        GameManager.AddCharacter(this);
    }

    private void Update()
    {
        // Perform actions. Comment this out if choosing to run it from the game manager.
        Act();

        // Move towards a random position.
        transform.position = Vector3.MoveTowards(transform.position, GameManager.RandomPosition(),Time.deltaTime * moveSpeed);
    }

    private void OnDestroy()
    {
        // Remove the character from the singleton when destroyed.
        Debug.Log($"{name} died!");
        GameManager.RemoveCharacter(this);
    }
}