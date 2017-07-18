using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointBehaviour : MonoBehaviour
{
    public delegate void RunEvent(float time);
    public static event RunEvent runStarted;

    void OnEnable()
    {
        BHopBehaviour.resetRun += startRun;
    }

    void OnDisable()
    {
        BHopBehaviour.resetRun -= startRun;
    }

    void startRun()
    {
        if (runStarted != null)
            runStarted(Time.time);
    }
}
