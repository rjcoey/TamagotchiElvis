using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorials;

    [SerializeField] private TextBox textBox;
    [SerializeField] private UIMover hudPanel;
    [SerializeField] private float fadeInTime = 0.5f;

    private CanvasFader canvasFader;
    private InputAction clickAction;
    private Coroutine tutorialCoroutine;

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

    public void SkipTutorial()
    {
        if (tutorialCoroutine != null)
        {
            StopCoroutine(tutorialCoroutine);
            tutorialCoroutine = null;
        }

        textBox.HideImmediate();
        canvasFader.SetAlphaImmediate(0.0f, false);
        tutorials[tutorials.Length - 1].OnTutorialFinish?.Invoke();
        TutorialEventBus.RaiseCompleteTutorial();
    }

    private void StartTutorial()
    {
        if (tutorialCoroutine != null) StopCoroutine(tutorialCoroutine);
        tutorialCoroutine = StartCoroutine(Co_RunTutorial());
    }

    private IEnumerator Co_RunTutorial()
    {
        yield return canvasFader.Co_FadeIn(fadeInTime);
        yield return textBox.Reveal();

        foreach (Tutorial tutorial in tutorials)
        {
            yield return Co_PlayDialogue(tutorial);
            yield return null;
        }

        yield return textBox.Hide();
        yield return canvasFader.Co_FadeOut(fadeInTime);

        TutorialEventBus.RaiseCompleteTutorial();
    }

    private IEnumerator Co_PlayDialogue(Tutorial tutorial)
    {
        textBox.StartTyping(tutorial.TutorialText);
        tutorial.OnTutorialStart?.Invoke();


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
        tutorial.OnTutorialFinish?.Invoke();
    }
}

[System.Serializable]
public struct Tutorial
{
    public string TutorialText;
    public UnityEvent OnTutorialStart;
    public UnityEvent OnTutorialFinish;
}
