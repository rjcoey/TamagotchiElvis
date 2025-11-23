using UnityEngine;

/// <summary>
/// Represents a physical location in the world that triggers the game over sequence.
/// This component listens for a specific GameOverReason and only activates its trigger
/// when that reason occurs. The player must then walk into the trigger to show the UI.
/// </summary>
[RequireComponent(typeof(Collider))]
public class GameOverLocation : MonoBehaviour
{
    [Tooltip("The point where the player character will be moved to upon game over.")]
    [field: SerializeField] public Transform UsePoint { get; private set; }

    [Tooltip("The message to display on the game over screen when triggered.")]
    [SerializeField][TextArea] private string gameOverMessage = string.Empty;

    [Tooltip("This location will only become active if the game over reason matches this value.")]
    [field: SerializeField] public GameOverReason Reason { get; private set; }

    private Collider gameOverCollider;

    void Awake()
    {
        gameOverCollider = GetComponent<Collider>();
        // The trigger is disabled by default to prevent it from firing prematurely.
        gameOverCollider.enabled = false;
    }


    void OnEnable()
    {
        GameEndEventBus.OnGameOver += SetActive;
    }

    void OnDisable()
    {
        GameEndEventBus.OnGameOver -= SetActive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEndEventBus.RaiseTriggerGameOverUI(gameOverMessage);
        }
    }

    private void SetActive(GameOverReason reason)
    {
        if (Reason == reason)
        {
            gameOverCollider.enabled = true;
        }
    }
}
