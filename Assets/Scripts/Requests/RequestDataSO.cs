using UnityEngine;

[CreateAssetMenu(fileName = "Request Data", menuName = "Scriptable Objects/Request Data")]
public class RequestDataSO : ScriptableObject
{
    [field: SerializeField] public string RequestTitle { get; private set; }
    [field: SerializeField][TextArea] public string SetupText { get; private set; }
    [field: SerializeField][TextArea] public string AcceptedText { get; private set; }
    [field: SerializeField][TextArea] public string RejectedText { get; private set; }

    [field: SerializeField] public StatName StatToIncrease { get; private set; }
    [field: SerializeField] public float IncreaseAmount { get; private set; }
    [field: SerializeField] public StatName StatToDecrease { get; private set; }
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
                StatToIncrease = StatName.HUNGER;
                break;
            case "HAPPINESS":
                StatToIncrease = StatName.HAPPINESS;
                break;
            case "TALENT":
                StatToIncrease = StatName.TALENT;
                break;
            case "FANS":
                StatToIncrease = StatName.FANS;
                break;
            case "CASH":
                StatToIncrease = StatName.CASH;
                break;
            default:
                StatToIncrease = StatName.NULL;
                break;
        }

        IncreaseAmount = increaseAmount;


        switch (decreasedStat)
        {
            case "HUNGER":
                StatToDecrease = StatName.HUNGER;
                break;
            case "HAPPINESS":
                StatToDecrease = StatName.HAPPINESS;
                break;
            case "TALENT":
                StatToDecrease = StatName.TALENT;
                break;
            case "FANS":
                StatToDecrease = StatName.FANS;
                break;
            case "CASH":
                StatToDecrease = StatName.CASH;
                break;
            default:
                StatToDecrease = StatName.NULL;
                break;
        }

        DecreaseAmount = decreaseAmount;
    }
}
