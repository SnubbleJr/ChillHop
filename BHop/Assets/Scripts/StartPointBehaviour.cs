using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointBehaviour : MonoBehaviour
{
    public delegate void RunEvent(float time);
    public static event RunEvent runStarted;

    void OnEnable()
    {
        CheckpointManagerBehaviour.RespawnCalled += RestartRun;
    }

    void OnDisable()
    {
        CheckpointManagerBehaviour.RespawnCalled -= RestartRun;
    }

    void RestartRun(int checkpointNo)
    {
        if (checkpointNo == 0 && runStarted != null)
            runStarted(Time.time);
    }
}
