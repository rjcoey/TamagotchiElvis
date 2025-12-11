using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerStats playerStats;
    private Camera mainCamera;
    private InputAction pointAction;
    private InputAction clickAction;

    private Resource currentResource;

    void OnEnable()
    {
        PlayerEventBus.OnEnablePlayer += EnablePlayerControl;
        PlayerEventBus.OnDisablePlayer += DisablePlayerControl;

        GameEventBus.OnGameOver += GameOver;


        pointAction?.Enable();
        clickAction?.Enable();
    }

    void OnDisable()
    {
        PlayerEventBus.OnEnablePlayer -= EnablePlayerControl;
        ClockEventBus.OnDayComplete -= DisablePlayerControl;

        GameEventBus.OnGameOver -= GameOver;

        // pointAction?.Disable();
        // clickAction?.Disable();
    }

    void Awake()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        playerStats = GetComponent<PlayerStats>();
        pointAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");

    }

    void Update()
    {

        if (!playerStats.IsGameRunning) return;

        if (clickAction == null || pointAction == null) return;

        Vector2 screenPosition = pointAction.ReadValue<Vector2>();

        if (clickAction.WasCompletedThisFrame())
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out Resource resource))
                {
                    if (currentResource != resource)
                    {
                        currentResource?.StopUsing();
                        currentResource = resource;
                        agent.SetDestination(resource.UsePoint.position);
                    }
                }
                else
                {
                    agent.SetDestination(hit.point);
                    currentResource?.StopUsing();
                    currentResource = null;
                }
            }
            else
            {
                Debug.Log("Raycast missed, no collision");
            }
        }

        if (currentResource != null)
        {
            if (Vector3.Distance(transform.position, currentResource.UsePoint.position) < 0.1f)
            {
                currentResource.Use(playerStats);
            }
        }
    }

    private void EnablePlayerControl()
    {
        agent.isStopped = false;
    }

    private void DisablePlayerControl()
    {
        agent.ResetPath();
        agent.isStopped = true;
        currentResource = null;
    }

    private void GameOver(GameOverReason reason)
    {
        foreach (GameOverLocation location in HouseLocationManager.Instance.GameOverLocations)
        {
            if (location.Reason == reason)
            {
                agent.SetDestination(location.UsePoint.position);
            }
        }
    }
}
