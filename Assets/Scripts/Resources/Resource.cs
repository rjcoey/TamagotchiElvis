using UnityEngine;

/// <summary>
/// Base class for all interactable resources in the game world (e.g., food, guitar, bed).
/// Provides common functionality for player interaction points and usage logic.
/// </summary>
public class Resource : MonoBehaviour
{
    [Tooltip("The specific point the player should move to before they can use this resource.")]
    [field: SerializeField] public Transform UsePoint { get; private set; }

    [Tooltip("The amount of the corresponding stat to replenish per second while using this resource.")]
    [SerializeField] protected float resourceFillRate = 5.0f;

    // --- Global State Flags ---
    // Note: These static flags represent global player states. This can be fragile.
    // A more robust solution might involve a dedicated PlayerState manager or an enum state machine.
    public static bool IsEating = false;
    public static bool IsHappy = false;
    public static bool IsPractising = false;


    /// <summary>
    /// A Unity callback executed when another collider enters this object's trigger zone.
    /// Currently used for debugging purposes.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered use trigger for " + gameObject.name);
        }
    }

    /// <summary>
    /// Called when the player begins using the resource.
    /// This method is intended to be overridden by derived classes (e.g., FoodResource, FunResource)
    /// to implement specific stat-filling logic.
    /// </summary>
    /// <param name="playerStats">A reference to the player's stats to be modified.</param>
    public virtual void Use(PlayerStats playerStats) { }

    /// <summary>
    /// Called when the player stops using the resource.
    /// This method can be overridden by derived classes to handle any necessary cleanup,
    /// such as stopping animations or sound effects.
    /// </summary>
    public virtual void StopUsing() { }
}
