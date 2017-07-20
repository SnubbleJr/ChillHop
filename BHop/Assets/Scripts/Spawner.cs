using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float startVelocity = 5f;

    private Rigidbody player;

    void OnEnable()
    {
        BHopBehaviour.resetRun += Respawn;
    }

    void OnDisable()
    {
        BHopBehaviour.resetRun -= Respawn;
    }

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void Respawn()
    {
        player.transform.position = transform.position;
        player.velocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;
        player.AddForce(transform.forward * startVelocity, ForceMode.Impulse);
        player.transform.rotation = transform.rotation;
    }
}
