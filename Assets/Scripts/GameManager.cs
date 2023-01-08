using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A singleton to handle the logic of our game.
/// Given it is a singleton, we never need more than one of this component on a game object.
/// </summary>
[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Variable to ensure only one singleton exists.
    /// </summary>
    private static GameManager _singleton;

    [SerializeField]
    [Tooltip("The radius of which characters can try to move from the origin.")]
    [Min(0)]
    private float gameRadius = 100;

    /// <summary>
    /// List of all characters in the game for the singleton to handle.
    /// </summary>
    private readonly List<Character> _characters = new();

    /// <summary>
    /// The character for the singleton to handle in a given frame.
    /// </summary>
    private int _characterIndex;

    /// <summary>
    /// Get a random position.
    /// In this simple example game, its just a random point within a given radius of the origin of the world.
    /// This is calculated as a Vector2, as we keep all characters at the same height, and then converted to a Vector3.
    /// </summary>
    /// <returns>A random position.</returns>
    public static Vector3 RandomPosition()
    {
        Vector2 position = Random.insideUnitCircle * _singleton.gameRadius;
        return new(position.x, 0, position.y);
    }

    /// <summary>
    /// Add a character to the singleton for it to handle.
    /// </summary>
    /// <param name="character">The character to add to the singleton.</param>
    public static void AddCharacter(Character character)
    {
        _singleton._characters.Add(character);
    }

    /// <summary>
    /// Remove a character from the singleton. Used when destroying a character to ensure no null characters exist.
    /// </summary>
    /// <param name="character">The character to remove from the singleton.</param>
    public static void RemoveCharacter(Character character)
    {
        bool removed = _singleton._characters.Remove(character);
        if (!removed)
        {
            return;
        }

        _singleton._characterIndex--;
        if (_singleton._characterIndex < 0)
        {
            _singleton._characterIndex = 0;
        }
    }

    /// <summary>
    /// Find an ally of a character.
    /// </summary>
    /// <param name="character">The character to find an ally of.</param>
    /// <returns>A random ally of the character or null if there are no allies.</returns>
    public static Character FindAlly(Character character)
    {
        return RandomCharacter(_singleton._characters.Where(c => c.TeamNumber == character.TeamNumber && c != character));
    }

    /// <summary>
    /// Find an enemy of a character.
    /// </summary>
    /// <param name="character">The character to find an enemy of.</param>
    /// <returns>A random enemy of the character or null if there are no allies.</returns>
    public static Character FindEnemy(Character character)
    {
        return RandomCharacter(_singleton._characters.Where(c => c.TeamNumber != character.TeamNumber));
    }
    
    /// <summary>
    /// Randomly select a character.
    /// </summary>
    /// <param name="characters">Collection of characters.</param>
    /// <returns>A random character from a given collection.</returns>
    private static Character RandomCharacter(IEnumerable<Character> characters)
    {
        System.Random random = new();
        return characters.OrderBy(_ => random.Next()).FirstOrDefault();
    }
    
    private void Awake()
    {
        // If there is no singleton, this game manager must be the singleton.
        // Since singleton is a variable of this exact component, it is safe to use in Awake().
        if (_singleton == null)
        {
            _singleton = this;
            return;
        }

        // If there is a game manager but it is not this, it can't exist as it would violate the singleton property.
        if (_singleton != this)
        {
            Destroy(gameObject);
        }
    }

    /*private void Update()
    {
        // Comment this method out if performing Act() from the characters themselves.
        // Perform the actions of a single character, and increment the value for the next frame.
        _characters[_characterIndex++].Act();
        if (_characterIndex >= _characters.Count)
        {
            _characterIndex = 0;
        }
    }*/
}