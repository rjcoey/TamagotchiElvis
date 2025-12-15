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

    public void Initialise(string requestName, string setupText, string acceptedText, string rejectedText, string increasedStat, float increaseAmount, string decreasedStat, float decreaseAmount)
    {
        RequestTitle = requestName;
        SetupText = setupText;
        AcceptedText = acceptedText;
        RejectedText = rejectedText;

        switch (increasedStat)
        {
            case "HUNGER":
                StatToIncrease = STAT.HUNGER;
                break;
            case "HAPPINESS":
                StatToIncrease = STAT.HAPPINESS;
                break;
            case "TALENT":
                StatToIncrease = STAT.TALENT;
                break;
            case "FANS":
                StatToIncrease = STAT.FANS;
                break;
            case "CASH":
                StatToIncrease = STAT.CASH;
                break;
            default:
                StatToIncrease = STAT.NULL;
                break;
        }

        IncreaseAmount = increaseAmount;


        switch (decreasedStat)
        {
            case "HUNGER":
                StatToDecrease = STAT.HUNGER;
                break;
            case "HAPPINESS":
                StatToDecrease = STAT.HAPPINESS;
                break;
            case "TALENT":
                StatToDecrease = STAT.TALENT;
                break;
            case "FANS":
                StatToDecrease = STAT.FANS;
                break;
            case "CASH":
                StatToDecrease = STAT.CASH;
                break;
            default:
                StatToDecrease = STAT.NULL;
                break;
        }

        DecreaseAmount = decreaseAmount;
    }
}
