using System;

public class GigEventBus
{
    public static Action OnStartGigSelection;
    public static void RaiseStartGigSelection() => OnStartGigSelection?.Invoke();

    public static event Action<GigDataSO> OnGigSelected;
    public static void RaiseGigSelected(GigDataSO gigData) => OnGigSelected?.Invoke(gigData);

    public static event Action<GigDataSO> OnPlayGig;
    public static void RaisePlayGig(GigDataSO gigData) => OnPlayGig?.Invoke(gigData);

    public static event Action OnGigComplete;
    public static void RaiseGigComplete() => OnGigComplete?.Invoke();
}
