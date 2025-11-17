using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastTest : MonoBehaviour
{
    // Drag your main scene camera here in the Inspector to be 100% sure.
    [SerializeField] private Camera specificCamera;

    // Set this to the layer your cube is on.
    [SerializeField] private LayerMask clickableLayers;

    void Awake()
    {
        // Fallback if you don't assign the camera in the inspector
        if (specificCamera == null)
        {
            specificCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();

            // Use the specific camera you assigned
            Ray ray = specificCamera.ScreenPointToRay(screenPos);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green, 10.0f);

            // Replace the Physics.Raycast line with this SphereCast
            if (Physics.SphereCast(ray, 0.1f, out hit, Mathf.Infinity, clickableLayers))
            {
                Debug.Log($"SUCCESS (SphereCast): Hit {hit.collider.name}");
            }
            else
            {
                Debug.Log("FAILURE: Both Raycast and SphereCast missed.");
            }
        }
    }
}
