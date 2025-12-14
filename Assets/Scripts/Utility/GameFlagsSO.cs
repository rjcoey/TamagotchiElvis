using UnityEngine;

[CreateAssetMenu(fileName = "NewGameFlags", menuName = "Scriptable Objects/GameFlags", order = 0)]
public class GameFlagsSO : ScriptableObject
{
    public bool HasPlayedTutorial { get; private set; } = false;

    public void SetHasPlayTutorial(bool value)
    {
        HasPlayedTutorial = value;
    }
}
