using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public RespawnData Data;
    private Rigidbody player;

    // Use this for initialization
    void Awake()
    {
        //depenancy injection here
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        player.transform.position = transform.position;

        if (Data.ResetOrientation)
            player.transform.rotation = transform.rotation;
        
        if (Data.RetainVelocity)
            player.velocity = transform.forward * player.velocity.magnitude;
        else        
            player.velocity = transform.forward * Data.SpawnVelocity;
        
    }
}
