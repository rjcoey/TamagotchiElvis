using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the gig button's display and selection functionality.
/// </summary>
public class GigButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gigLocationText;
    [SerializeField] private TextMeshProUGUI daysAwayText;
    [SerializeField] private TextMeshProUGUI cashRankingText;
    [SerializeField] private TextMeshProUGUI fansRankingText;

    private GigSelect gigSelector;

    public void InitialiseButton(GigSelect gigSelector, GigDataSO gigData)
    {
        this.gigSelector = gigSelector;
        gigLocationText.text = gigData.GigLocation;
        daysAwayText.text = gigData.DaysUntilGig.ToString() + " Days Away";
        cashRankingText.text = string.Concat(Enumerable.Repeat("$", gigData.GigCashRating));
        fansRankingText.text = string.Concat(Enumerable.Repeat("*", gigData.GigFansRating));
    }

    public void SelectGig(int index)
    {
        gigSelector.RunSelectGig(index);
    }
}
