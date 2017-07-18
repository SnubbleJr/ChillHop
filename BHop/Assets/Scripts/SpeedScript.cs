using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public
class SpeedScript : MonoBehaviour {

private
  Rigidbody player;

private
  Text text;

  // Use this for initialization
  void Start() {
    player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update() {
    Vector3 vel = player.velocity;
    vel.y = 0;
    text.text = Mathf.Floor(vel.magnitude).ToString();
  }
}
