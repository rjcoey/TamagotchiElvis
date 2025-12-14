using UnityEngine;
using UnityEngine.UI;

public class HungerUI : MonoBehaviour
{
    [SerializeField] private Image hungerFill;

    void OnEnable()
    {
        PlayerEventBus.OnHungerIncreased += IncreaseHungerUI;
        PlayerEventBus.OnHungerDecreased += DecreaseHungerUI;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHungerIncreased -= IncreaseHungerUI;
        PlayerEventBus.OnHungerDecreased -= DecreaseHungerUI;
    }

    private void IncreaseHungerUI(float newFill, bool playFeedback)
    {
        hungerFill.fillAmount = newFill;
    }

    private void DecreaseHungerUI(float newFill, bool playFeedback)
    {
        hungerFill.fillAmount = newFill;
    }
}
