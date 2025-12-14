using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RequestUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private AnimationCurve scaleCurve;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private GameObject buttonsObject;

    private CanvasFader canvasFader;
    private InputAction clickAction;
    private PlayerStats playerStats;

    private RequestDataSO currentRequest = null;
    private Coroutine activeRoutine;

    void OnEnable()
    {
        RequestEventBus.OnStartRequest += StartRunRequest;
        PlayerEventBus.OnSpawnPlayer += SetPlayerStats;
    }

    void OnDisable()
    {
        RequestEventBus.OnStartRequest -= StartRunRequest;
        PlayerEventBus.OnSpawnPlayer -= SetPlayerStats;
    }

    void Awake()
    {
        canvasFader = GetComponent<CanvasFader>();
        clickAction = InputSystem.actions.FindAction("Click");
    }

    private void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }

    public void AcceptRequest()
    {
        titleText.transform.localScale = Vector3.zero;
        bodyText.text = string.Empty;
        buttonsObject.SetActive(false);
        activeRoutine = StartCoroutine(Co_AcceptRequest());
    }

    public void DeclineRequest()
    {
        titleText.transform.localScale = Vector3.zero;
        bodyText.text = string.Empty;
        buttonsObject.SetActive(false);
        activeRoutine = StartCoroutine(Co_DeclineRequest());
    }

    private void StartRunRequest(RequestDataSO request)
    {
        currentRequest = request;
        titleText.transform.localScale = Vector3.zero;
        titleText.text = string.Empty;
        bodyText.text = string.Empty;
        buttonsObject.SetActive(false);
        StartCoroutine(Co_DisplayRequest(currentRequest));
    }

    private IEnumerator Co_AcceptRequest()
    {
        titleText.text = "ACCEPTED";

        playerStats.IncreaseStatImmediate(currentRequest.StatToIncrease, currentRequest.IncreaseAmount);
        playerStats.DecreaseStatImmediate(currentRequest.StatToDecrease, currentRequest.DecreaseAmount);

        yield return UITweener.LerpElementSize(titleText.transform, Vector3.zero, Vector3.one, 0.5f, scaleCurve);
        yield return Typewriter.TypewriterEffect(bodyText, currentRequest.AcceptedText);

        yield return new WaitUntil(() => clickAction.WasCompletedThisFrame());
        yield return canvasFader.Co_FadeOut(fadeDuration);
        currentRequest = null;
        RequestEventBus.RaiseCompleteRequest();
    }

    private IEnumerator Co_DeclineRequest()
    {
        titleText.text = "DECLINED";
        yield return UITweener.LerpElementSize(titleText.transform, Vector3.zero, Vector3.one, 0.5f, scaleCurve);
        yield return Typewriter.TypewriterEffect(bodyText, currentRequest.RejectedText);
        yield return new WaitUntil(() => clickAction.WasCompletedThisFrame());
        yield return canvasFader.Co_FadeOut(fadeDuration);
        currentRequest = null;
        RequestEventBus.RaiseCompleteRequest();
    }

    private IEnumerator Co_DisplayRequest(RequestDataSO request)
    {
        titleText.text = request.RequestTitle;
        yield return canvasFader.Co_FadeIn(fadeDuration);
        yield return UITweener.LerpElementSize(titleText.transform, Vector3.zero, Vector3.one, 0.5f, scaleCurve);
        yield return Typewriter.TypewriterEffect(bodyText, request.SetupText);
        buttonsObject.SetActive(true);

    }
}
