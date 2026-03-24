using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f;

    private NavMeshAgent agent;
    private Animator anim;
    private Camera mainCamera;
    private InputAction pointAction;
    private InputAction clickAction;

    private Resource currentResource;

    private bool active = false;
    private bool usingResource = false;

    void OnEnable()
    {
        PlayerEventBus.OnEnablePlayer += EnablePlayerControl;
        PlayerEventBus.OnDisablePlayer += DisablePlayerControl;

        PlayerEventBus.OnUseButtonClicked += HandleUseButtonClicked;

        GameEventBus.OnGameOver += GameOver;


        pointAction?.Enable();
        clickAction?.Enable();
    }

    void OnDisable()
    {
        PlayerEventBus.OnEnablePlayer -= EnablePlayerControl;
        ClockEventBus.OnDayComplete -= DisablePlayerControl;

        PlayerEventBus.OnUseButtonClicked -= HandleUseButtonClicked;

        GameEventBus.OnGameOver -= GameOver;
    }

    void Awake()
    {
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        pointAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");

    }

    void Update()
    {
        if (!active) return;
        if (clickAction == null || pointAction == null) return;

        Vector2 screenPosition = pointAction.ReadValue<Vector2>();

        if (clickAction.WasCompletedThisFrame())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;


            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.SphereCast(ray, sphereCastRadius, out hit))
            {
                if (hit.collider.TryGetComponent(out Resource resource))
                {
                    if (currentResource != resource)
                    {
                        currentResource?.HideTooltip();
                        resource.ShowToolTip();
                    }
                }
                else
                {
                    agent.SetDestination(hit.point);
                    if (usingResource) usingResource = false;
                    currentResource?.StopUsing();
                    currentResource?.HideTooltip();
                    currentResource = null;
                }
            }
            else
            {
                Debug.Log("Raycast missed, no collision");
            }
        }

        if (currentResource != null && Vector2.Distance(currentResource.UsePoint.position, transform.position) < 0.2f && !usingResource)
        {
            usingResource = true;
            currentResource.Use();
        }

        if (agent.hasPath || agent.remainingDistance > agent.stoppingDistance)
        {
            anim.SetFloat("speed", agent.speed);
        }
        else
        {
            anim.SetFloat("speed", 0.0f);
        }
    }

    private void EnablePlayerControl()
    {
        agent.isStopped = false;
        active = true;
    }

    private void DisablePlayerControl()
    {
        agent.ResetPath();
        agent.isStopped = true;
        currentResource?.StopUsing();
        currentResource = null;
        usingResource = false;
        active = false;
    }

    private void HandleUseButtonClicked(Resource resource)
    {
        if (currentResource != resource)
        {
            currentResource?.StopUsing();
            if (usingResource) usingResource = false;
            currentResource = resource;
            currentResource.HideTooltip();
            agent.SetDestination(resource.UsePoint.position);
        }
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
