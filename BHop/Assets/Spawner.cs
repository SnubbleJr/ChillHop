using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public
class Spawner : MonoBehaviour {

public
  float startVelocity = 5f;

private
  Rigidbody player;

  // Use this for initialization
  void Start() {
    player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    respawn();
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
      respawn();
  }

  void respawn() {
    player.transform.position = transform.position;
    player.velocity = Vector3.zero;
    player.angularVelocity = Vector3.zero;
    player.AddForce(transform.forward * startVelocity, ForceMode.Impulse);
  }
}
