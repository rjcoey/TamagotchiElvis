using UnityEngine;

[CreateAssetMenu(fileName = "New Gig Data", menuName = "Gigs/New Gig Data", order = 0)]
public class GigDataSO : ScriptableObject
{
    [field: SerializeField] public string GigLocation { get; private set; }
    [field: SerializeField] public int DaysUntilGig { get; private set; }
    [field: SerializeField] public int BaseGigCash { get; private set; }
    [field: SerializeField] public int GigCashRating { get; private set; }
    [field: SerializeField] public int BaseGigFans { get; private set; }
    [field: SerializeField] public int GigFansRating { get; private set; }
}
