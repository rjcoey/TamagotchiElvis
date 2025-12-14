using UnityEngine;

[CreateAssetMenu(fileName = "Request Data", menuName = "Scriptable Objects/Request Data")]
public class RequestDataSO : ScriptableObject
{
    [field: SerializeField] public string RequestTitle { get; private set; }
    [field: SerializeField][TextArea] public string SetupText { get; private set; }
    [field: SerializeField][TextArea] public string AcceptedText { get; private set; }
    [field: SerializeField][TextArea] public string RejectedText { get; private set; }

    [field: SerializeField] public STAT StatToIncrease { get; private set; }
    [field: SerializeField] public float IncreaseAmount { get; private set; }
    [field: SerializeField] public STAT StatToDecrease { get; private set; }
    [field: SerializeField] public float DecreaseAmount { get; private set; }
}
