using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera levelCamera;
    [SerializeField] private CinemachineCamera tvCamera;
    [SerializeField] private CinemachineCamera fridgeCamera;
    [SerializeField] private CinemachineCamera guitarCamera;

    public void ActivateTVCam()
    {
        levelCamera.Priority = 0;
        tvCamera.Priority = 1;
        fridgeCamera.Priority = 0;
        guitarCamera.Priority = 0;
    }

    public void ActivateFridgeCam()
    {
        levelCamera.Priority = 0;
        tvCamera.Priority = 0;
        fridgeCamera.Priority = 1;
        guitarCamera.Priority = 0;
    }

    public void ActivateGuitarCam()
    {
        levelCamera.Priority = 0;
        tvCamera.Priority = 0;
        fridgeCamera.Priority = 0;
        guitarCamera.Priority = 1;
    }

    public void ActivateLevelCam()
    {
        levelCamera.Priority = 1;
        tvCamera.Priority = 0;
        fridgeCamera.Priority = 0;
        guitarCamera.Priority = 0;
    }
}
