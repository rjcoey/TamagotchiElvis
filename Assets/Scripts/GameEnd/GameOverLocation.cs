using UnityEngine;

public class GameOverLocation : MonoBehaviour
{
    [field: SerializeField] public Transform UsePoint { get; private set; }
    [SerializeField][TextArea] private string gameOverMessage = string.Empty;
    [field: SerializeField] public GameOverReason Reason { get; private set; }

    private Collider gameOverCollider;

    void Awake()
    {
        gameOverCollider = GetComponent<Collider>();
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
