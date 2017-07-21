using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Range(0f, 100f)]
    public float SpawnVelocity = 10f;

    private RespawnData checkpointRespawnData;
    private RespawnData startRespawnData;
    private Rigidbody playeRigidbody;

    [Inject]
    public void Init([Inject(Id = "CheckpointRespawn")] RespawnData data, [Inject(Id = "StartRespawn")] RespawnData spawnData,
        [Inject(Id = "PlayerBody")] Rigidbody playeRigidbody)
    {
        this.checkpointRespawnData = data;
        this.startRespawnData = spawnData;
        this.playeRigidbody = playeRigidbody;
    }

    public void Respawn()
    {
        RespawnPlayer(checkpointRespawnData);
    }

    public void RespawnFromStart()
    {
        RespawnPlayer(startRespawnData);
    }

    private void RespawnPlayer(RespawnData data)
    {
        playeRigidbody.transform.position = transform.position;

        if (data.ResetOrientation)
            playeRigidbody.transform.rotation = transform.rotation;

        if (data.RetainVelocity)
            playeRigidbody.velocity = transform.forward * playeRigidbody.velocity.magnitude;
        else
            playeRigidbody.velocity = transform.forward * SpawnVelocity;
    }
}
