using UnityEngine;

[CreateAssetMenu(fileName = "RespawnData", menuName = "Respawn Data/Create New", order = 1)]
public class RespawnData : ScriptableObject
{
    public bool RetainVelocity = true;
    
    public bool ResetOrientation = true;
}
