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
                        if (currentResource != null)
                        {
                            currentResource.StopUsing();
                        }
                        currentResource = resource;
                        agent.SetDestination(resource.UsePoint.position);
                    }
                }
                else
                {
                    agent.SetDestination(hit.point);

                    if (currentResource != null)
                    {
                        currentResource.StopUsing();
                        currentResource = null;
                    }
                }
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
}
