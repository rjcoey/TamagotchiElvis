public class HungerUI : StatUI
{
    void OnEnable()
    {
        PlayerEventBus.OnHungerUpdated += UpdateUIBar;
    }

    void OnDisable()
    {
        PlayerEventBus.OnHungerUpdated -= UpdateUIBar;
    }
}
