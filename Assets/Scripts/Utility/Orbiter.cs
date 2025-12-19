using UnityEngine;

public class Orbiter : MonoBehaviour
{
    [SerializeField] private Transform orbitTarget;
    [SerializeField] private float orbitSpeed;

    // We need to know the radius of the orbit.
    private float orbitRadius;
    private float angle;

    float yStartPos;

    void Start()
    {
        Vector3 offset = transform.position - orbitTarget.position;
        orbitRadius = new Vector2(offset.x, offset.z).magnitude;
        yStartPos = transform.position.y;
    }

    void Update()
    {
        angle += orbitSpeed * Mathf.Deg2Rad * Time.deltaTime;

        float x = Mathf.Cos(angle) * orbitRadius;
        float z = Mathf.Sin(angle) * orbitRadius;

        Vector3 newPosition = orbitTarget.position + new Vector3(x, yStartPos, z);
        transform.position = newPosition;
    }
}
