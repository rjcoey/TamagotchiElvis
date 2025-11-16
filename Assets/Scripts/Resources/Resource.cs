using UnityEngine;

public class Resource : MonoBehaviour
{
    [field: SerializeField] public Transform UsePoint { get; private set; }
    [SerializeField] protected float resourceFillRate = 5.0f;

    public static bool IsEating = false;
    public static bool IsHappy = false;
    public static bool IsPractising = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered use trigger");
        }
    }

    public virtual void Use(PlayerStats playerStats) { }
    public virtual void StopUsing() { }
}
