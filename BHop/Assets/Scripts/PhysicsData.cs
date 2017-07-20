using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsData", menuName = "Physics Data/Create New", order = 1)]
public class PhysicsData : ScriptableObject
{
    [Range(0f, 100f)]
    public float friction = 0.01f;

    [Range(0f, 100f)]
    public float ground_accelerate = 0.01f;

    [Range(0f, 100f)]
    public float max_velocity_ground = 0.01f;

    [Range(0f, 100f)]
    public float air_accelerate = 0.01f;

    [Range(0f, 100f)]
    public float max_velocity_air = 0.01f;

    public float jumpHeight;

    public bool autoHop;

    [Range(0, 5f)]
    public float secondsResetAfterRunEnds = 2f;
}