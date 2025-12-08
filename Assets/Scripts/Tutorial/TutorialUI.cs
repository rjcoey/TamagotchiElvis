using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextBox textBox;
    [SerializeField] private UIMover hudPanel;

    [SerializeField] private float fadeInTime = 0.5f;

    CanvasFader canvasFader;

    private InputAction clickAction;

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
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void StartTutorial()
    {
        StartCoroutine(Co_RunTutorial());
    }

    private IEnumerator Co_RunTutorial()
    {
        yield return canvasFader.Co_FadeIn(fadeInTime);
        yield return textBox.Reveal();
        yield return textBox.TypeText("Listen up maggot, welcome to Tamagotchi Elvis! The all in one musciain simulator game!");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("Ready or not we're going to make you a star, but I gotta explain a few things first.");

    }
}
