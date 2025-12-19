using UnityEngine;

public class GameCompleteUI : MonoBehaviour
{
    private CanvasFader canvasFader;

    void OnEnable()
    {
        GameEventBus.OnGameComplete += ShowGameComplete;
    }

    void OnDisable()
    {
        GameEventBus.OnGameComplete += ShowGameComplete;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    private void ShowGameComplete()
    {
        StartCoroutine(canvasFader.Co_FadeIn());
    }
}
