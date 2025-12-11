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
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return hudPanel.LerpPanelPosition(hudPanel.GetComponent<RectTransform>().anchoredPosition, new Vector2(0.0f, 0.0f));
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("These here are your resources, you gotta fill them up by clicking on items in your flat.");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("The TV fills your happiness, the fridge fills your hunger and your guitar fills your talent");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("You gotta be careful though! Too much of the good stuff might make you realise there's more to life than being a rock 'n' roll star!");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("This is where you can see your current fans...");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("And here is where you can see how many days you have until your next gig!");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return textBox.TypeText("Next up, I'm going to send you some gigs to pick between. Together, you and me are making it places!");
        yield return new WaitUntil(() => clickAction.WasPerformedThisFrame());
        yield return canvasFader.Co_FadeOut(fadeInTime);
        TutorialEventBus.RaiseCompleteTutorial();
    }
}
