using System;

public class GigEventBus
{
    public static event Action<GigDataSO> OnGigSelected;
    public static void RaiseGigSelected(GigDataSO gigData) => OnGigSelected?.Invoke(gigData);

    public static event Action OnPlayGig;
    public static void RaisePlayGig() => OnPlayGig?.Invoke();
}
