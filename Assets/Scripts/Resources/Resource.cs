using UnityEngine;

public class Resource : MonoBehaviour
{
    [field: SerializeField] public Transform UsePoint { get; private set; }
    [SerializeField] protected float resourceFillRate = 5.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered use trigger");
        }
    }

    public virtual void Use(PlayerStats playerStats) { }
    public virtual void StopUsing(PlayerStats playerStats) { }
}
