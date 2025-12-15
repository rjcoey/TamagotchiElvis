using UnityEngine;

/// <summary>
/// ScriptableObject that holds all the data for a single gig.
/// </summary>
[CreateAssetMenu(fileName = "New Gig Data", menuName = "Scriptable Objects/Gig Data", order = 0)]
public class GigDataSO : ScriptableObject
{
    [field: SerializeField] public string GigLocation { get; private set; }
    [field: SerializeField] public int DaysUntilGig { get; private set; }

    // --- Rewards ---
    [field: SerializeField] public int BaseGigFans { get; private set; }
    [field: SerializeField] public int GigFansRating { get; private set; }
    [field: SerializeField] public int BaseGigCash { get; private set; }
    [field: SerializeField] public int GigCashRating { get; private set; }

    // --- Reviews ---
    [field: SerializeField] public string PositiveReview { get; private set; }
    [field: SerializeField] public string NeutralReview { get; private set; }
    [field: SerializeField] public string NegativeReview { get; private set; }

    [field: SerializeField] public AnimationCurve GigDifficultyCurve { get; private set; } = default;

    public void Initialise(string gigLocation, int daysUntilGig, int baseCash, int cashRating, int baseFans, int fansRating, string postiveReview, string neutralReview, string negativeReview, int difficulty)
    {
        GigLocation = gigLocation;
        DaysUntilGig = daysUntilGig;

        BaseGigCash = baseCash;
        GigCashRating = cashRating;

        BaseGigFans = baseFans;
        GigFansRating = fansRating;

        PositiveReview = postiveReview;
        NeutralReview = neutralReview;
        NegativeReview = negativeReview;

        if (difficulty == 1)
        {
            Keyframe[] easyKeys = new Keyframe[2];
            easyKeys[0] = new Keyframe(0.0f, 0.2f); // Starts easy
            easyKeys[1] = new Keyframe(1.0f, 0.8f); // Ends easy
            GigDifficultyCurve = new AnimationCurve(easyKeys);
        }
        else if (difficulty == 2)
        {
            GigDifficultyCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (difficulty == 3)
        {
            Keyframe[] hardKeys = new Keyframe[3];
            hardKeys[0] = new Keyframe(0.0f, 0.0f);
            hardKeys[1] = new Keyframe(0.5f, 0.8f); // Steep increase in the middle
            hardKeys[2] = new Keyframe(1.0f, 1.0f);
            GigDifficultyCurve = new AnimationCurve(hardKeys);
        }
        else
        {
            GigDifficultyCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        }

    }
}
