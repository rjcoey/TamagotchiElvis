using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private TextMeshProUGUI messageText;

    private CanvasFader fader;

    void OnEnable()
    {
        GameEventBus.OnTriggerGameOverUI += RunGameOver;
    }

    void OnDisable()
    {
        GameEventBus.OnTriggerGameOverUI -= RunGameOver;
    }

    void Awake()
    {
        fader = GetComponent<CanvasFader>();
    }

    private void RunGameOver(string message)
    {
        StartCoroutine(GameOverRoutine(message));
    }

    private IEnumerator GameOverRoutine(string message)
    {
        messageText.text = message;
        yield return fader.Co_FadeIn(fadeDuration);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
