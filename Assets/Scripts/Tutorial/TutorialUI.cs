using System.Collections;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 0.5f;

    CanvasFader canvasFader;

    void OnEnable()
    {
        TutorialEventBus.OnStartTutorial += StartTutorial;
    }

    void OnDisable()
    {
        TutorialEventBus.OnStartTutorial -= StartTutorial;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
    }

    void StartTutorial()
    {
        StartCoroutine(Co_RunTutorial());
    }

    private IEnumerator Co_RunTutorial()
    {
        yield return canvasFader.Co_FadeIn(fadeInTime);
    }
}
