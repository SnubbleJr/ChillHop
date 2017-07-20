using UnityEngine;

[CreateAssetMenu(fileName = "RespawnData", menuName = "Respawn Data/Create New", order = 1)]
public class RespawnData : ScriptableObject
{
    public bool RetainVelocity = true;

    [Range(0f, 100f)]
    public float SpawnVelocity = 10f;

    public bool ResetOrientation = true;
}
