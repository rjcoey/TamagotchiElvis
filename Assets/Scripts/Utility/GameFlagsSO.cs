using UnityEngine;

[CreateAssetMenu(fileName = "GameFlagsSO", menuName = "GameFlagsSO", order = 0)]
public class GameFlagsSO : ScriptableObject
{
    public bool HasPlayedTutorial { get; private set; } = false;
}
