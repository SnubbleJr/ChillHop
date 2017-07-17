using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public
class LookScript : MonoBehaviour {

public
  float speed = 1f;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    float mouseX = Input.GetAxis("Mouse X");

    transform.Rotate(0, mouseX * speed, 0);

    if (Input.GetButtonDown("Cancel")) {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
  }
}
