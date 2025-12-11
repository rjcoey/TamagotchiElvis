using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameFlagsSO gameFlags;

    public GigDataSO currentGig = null;
    private int daysUntilGig = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (gameFlags.HasPlayedTutorial)
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
    }

    void OnDisable()
    {
        TutorialEventBus.OnCompleteTutorial -= CompleteTutorial;
        GigEventBus.OnGigSelected -= SelectGig;
        GigEventBus.OnGigComplete -= CompleteGig;
        ClockEventBus.OnDayComplete -= CompleteDay;
    }

    void CompleteTutorial()
    {
        GigEventBus.RaiseStartGigSelection();
        gameFlags.SetHasPlayTutorial(true);
    }

    void CompleteGig()
    {
        GigEventBus.RaiseStartGigSelection();
    }

    void SelectGig(GigDataSO gigData)
    {
        currentGig = gigData;
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
            GigEventBus.RaisePlayGig(currentGig);
        }
        else
        {
            ClockEventBus.RaiseStartDay(daysUntilGig);
        }
    }
}
