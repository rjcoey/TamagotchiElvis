using System;

public class RequestEventBus
{
    public static event Action<RequestDataSO> OnStartRequest;
    public static void RaiseStartRequest(RequestDataSO request) => OnStartRequest?.Invoke(request);

    public static event Action OnCompleteRequest;
    public static void RaiseCompleteRequest() => OnCompleteRequest?.Invoke();
}
