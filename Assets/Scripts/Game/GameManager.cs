using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<RequestDataSO> allRequests = new();
    [SerializeField] private List<GigDataSO> allGigs = new();
    [SerializeField] private float requestChance = 0.5f;

    public GigDataSO CurrentGig { get; private set; } = null;

    private int daysUntilGig = -1;

    private List<RequestDataSO> availableRequests = new();
    private List<GigDataSO> availableGigs = new();

    private bool hasPlayedTutorial = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        availableRequests = new(allRequests);
        if (hasPlayedTutorial)
        {
            GigEventBus.RaiseStartGigSelection();
        }
        else
        {
            TutorialEventBus.RaiseStartTutorial();
        }
    }

    void OnEnable()
    {
        TutorialEventBus.OnCompleteTutorial += CompleteTutorial;
        GigEventBus.OnGigSelected += SelectGig;
        GigEventBus.OnGigComplete += CompleteGig;
        ClockEventBus.OnDayComplete += CompleteDay;
        RequestEventBus.OnCompleteRequest += CompleteRequest;
    }

    void OnDisable()
    {
        TutorialEventBus.OnCompleteTutorial -= CompleteTutorial;
        GigEventBus.OnGigSelected -= SelectGig;
        GigEventBus.OnGigComplete -= CompleteGig;
        ClockEventBus.OnDayComplete -= CompleteDay;
        RequestEventBus.OnCompleteRequest -= CompleteRequest;
    }

    void CompleteTutorial()
    {
        GigEventBus.RaiseStartGigSelection();
        hasPlayedTutorial = true;
    }

    void CompleteGig()
    {
        GigEventBus.RaiseStartGigSelection();
    }

    void SelectGig(GigDataSO gigData)
    {
        CurrentGig = gigData;
        daysUntilGig = gigData.DaysUntilGig;
        ClockEventBus.RaiseStartDay(daysUntilGig);
    }

    void CompleteDay()
    {
        ClockEventBus.RaisePauseTimer();
        PlayerEventBus.RaiseDisablePlayer();

        daysUntilGig--;
        if (daysUntilGig <= 0)
        {
            GigEventBus.RaisePlayGig(CurrentGig);
        }
        else
        {
            if (Random.value < requestChance)
            {
                RequestDataSO request = GetRequestData();
                RequestEventBus.RaiseStartRequest(request);
            }
            else
            {
                requestChance = Mathf.Min(1.0f, requestChance + 0.25f);
                ClockEventBus.RaiseStartDay(daysUntilGig);
            }
        }
    }

    void CompleteRequest()
    {
        ClockEventBus.RaiseStartDay(daysUntilGig);
    }

    public GigDataSO GetGigData()
    {
        if (availableGigs.Count <= 0)
        {
            availableGigs = new(allGigs);
        }

        int i = Random.Range(0, availableGigs.Count);
        GigDataSO gigData = availableGigs[i];
        availableGigs.RemoveAt(i);
        return gigData;
    }

    private RequestDataSO GetRequestData()
    {
        if (availableRequests.Count <= 0)
        {
            availableRequests = new(allRequests);
        }

        int i = Random.Range(0, availableRequests.Count);
        RequestDataSO request = availableRequests[i];
        availableRequests.RemoveAt(i);
        return request;
    }
}
