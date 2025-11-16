using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GigButton : MonoBehaviour
{
    [SerializeField] private CanvasFader fader;
    [SerializeField] private GigDataSO gigData;

    [SerializeField] private TextMeshProUGUI gigLocationText;
    [SerializeField] private TextMeshProUGUI daysAwayText;
    [SerializeField] private TextMeshProUGUI cashRankingText;
    [SerializeField] private TextMeshProUGUI fansRankingText;

    void Awake()
    {

    }

    void Start()
    {
        gigLocationText.text = gigData.GigLocation;
        daysAwayText.text = gigData.DaysUntilGig.ToString() + " Days Away";
        cashRankingText.text = string.Concat(Enumerable.Repeat("$", gigData.GigCashRating));
        fansRankingText.text = string.Concat(Enumerable.Repeat("*", gigData.GigFansRating));
    }

    public void StartSelectGig()
    {
        StartCoroutine(SelectGig(gigData));
    }

    private IEnumerator SelectGig(GigDataSO gigData)
    {
        yield return fader.Fade(1.0f, 0.0f, 0.1f);
        GigEventBus.RaiseGigSelected(gigData);
    }
}
