using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagerBehaviour : MonoBehaviour
{
    public delegate void RespawnEvent(int checkpoint);
    public static event RespawnEvent RespawnCalled;

    private int currentCheckpoint = 0;

    void OnEnable()
    {
        BHopBehaviour.respawnPlayer += RespawnPlayer;
        CheckpointBehaviour.checkpointHit += SetCurrentCheckpoint;
        EndPointBehaviour.runEnded += ResetCurrentCheckpoint;
    }

    void OnDisable()
    {
        BHopBehaviour.respawnPlayer -= RespawnPlayer;
        CheckpointBehaviour.checkpointHit -= SetCurrentCheckpoint;
        EndPointBehaviour.runEnded -= ResetCurrentCheckpoint;
    }

    void Awake()
    {
        CheckCheckpointChildren();
    }

    private void CheckCheckpointChildren()
    {
        List<int> checkpointNos = new List<int>();

        foreach (CheckpointBehaviour checkpoint in GetComponentsInChildren<CheckpointBehaviour>())
        {
            int number = checkpoint.CheckpointNumber;
            if (checkpointNos.Contains(number))
            {
                Debug.LogError("Duplicate Checkpoint numbers! Fix!");
                this.enabled = false;
                return;
            }
            checkpointNos.Add(number);
        }
    }

    private void RespawnPlayer()
    {
        if (RespawnCalled != null)
            RespawnCalled(currentCheckpoint);
    }

    private void ResetCurrentCheckpoint(float t = 0)
    {
        currentCheckpoint = 0;
    }

    private void SetCurrentCheckpoint(int checkpoint)
    {
        currentCheckpoint = checkpoint;
    }
}
