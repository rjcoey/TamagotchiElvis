using System.Collections.Generic;
using UnityEngine;

public class HouseLocationManager : MonoBehaviour
{
    public static HouseLocationManager Instance { get; private set; }
    [field: SerializeField] public List<GameOverLocation> GameOverLocations = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
