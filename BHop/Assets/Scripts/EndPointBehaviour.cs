using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointBehaviour : MonoBehaviour
{
    public static event StartPointBehaviour.RunEvent runEnded;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (runEnded != null)
                runEnded(Time.time);
        }
    }
}
