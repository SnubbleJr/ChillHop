using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Spawner))]
public class CheckpointBehaviour : MonoBehaviour
{
    public delegate void CheckpointEvent(int checkpointNo);
    public static event CheckpointEvent checkpointHit;

    public int CheckpointNumber;

    private Spawner spawner;

    void OnEnable()
    {
        CheckpointManagerBehaviour.RespawnCalled += Respawn;
    }

    void OnDisable()
    {
        CheckpointManagerBehaviour.RespawnCalled -= Respawn;
    }

    void Awake()
    {
        spawner = GetComponent<Spawner>();
    }

    private void Respawn(int checkpointNo)
    {
        if (CheckpointNumber != checkpointNo)
            return;

        spawner.Respawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (checkpointHit != null)
                checkpointHit(CheckpointNumber);
        }
    }
}
