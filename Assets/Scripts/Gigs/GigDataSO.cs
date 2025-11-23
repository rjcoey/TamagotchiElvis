using UnityEngine;

/// <summary>
/// ScriptableObject that holds all the data for a single gig.
/// </summary>
[CreateAssetMenu(fileName = "New Gig Data", menuName = "Gigs/New Gig Data", order = 0)]
public class GigDataSO : ScriptableObject
{
    [field: SerializeField] public string GigLocation { get; private set; }
    [field: SerializeField] public int DaysUntilGig { get; private set; }

    // --- Rewards ---
    [field: SerializeField] public int BaseGigCash { get; private set; }
    [field: SerializeField] public int GigCashRating { get; private set; }
    [field: SerializeField] public int BaseGigFans { get; private set; }
    [field: SerializeField] public int GigFansRating { get; private set; }

    // --- Reviews ---
    [field: SerializeField] public string PositiveReview { get; private set; }
    [field: SerializeField] public string NeutralReview { get; private set; }
    [field: SerializeField] public string NegativeReview { get; private set; }

    // --- Difficulty ---
    /// <summary>
    /// Maps player performance (X-axis, 0-1) to gig score (Y-axis, 0-1).
    /// A lower curve results in a harder gig.
    /// </summary>
    [field: SerializeField] public AnimationCurve GigDifficultyCurve { get; private set; }
}
