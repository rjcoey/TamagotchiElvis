using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextBox textBox;
    [SerializeField] private UIMover hudPanel;
    [SerializeField] private float fadeInTime = 0.5f;

    private CanvasFader canvasFader;
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

        yield return PlayDialogue("Listen up maggot, welcome to Tamagotchi Elvis! The all in one musician simulator game!");

        yield return textBox.Hide();
        yield return canvasFader.Co_FadeOut(fadeInTime);

        TutorialEventBus.RaiseCompleteTutorial();
    }

    private IEnumerator PlayDialogue(string text)
    {
        textBox.StartTyping(text);

        while (textBox.IsTyping)
        {
            if (clickAction.WasPerformedThisFrame())
            {
                textBox.SkipTypewriter();
            }
            yield return null;
        }
        yield return null;

        while (!clickAction.WasPerformedThisFrame())
        {
            yield return null;
        }
    }
}
