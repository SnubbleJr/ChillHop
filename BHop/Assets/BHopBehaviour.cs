using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public
class BHopBehaviour : MonoBehaviour {

  [Range(0f, 100f)] public float friction = 0.01f;

  [Range(0f, 100f)] public float ground_accelerate = 0.01f;

  [Range(0f, 100f)] public float max_velocity_ground = 0.01f;

  [Range(0f, 100f)] public float air_accelerate = 0.01f;

  [Range(0f, 100f)] public float max_velocity_air = 0.01f;

public
  float jumpHeight;

private
  Rigidbody rigidbody;

private
  bool jumped = false;

private
  bool grounded = false;
  void Awake() { rigidbody = GetComponent<Rigidbody>(); }

  void FixedUpdate() {
    // autoJump();

    Vector3 accelDir = getAccelDirection();
    Vector3 prevVelocity = rigidbody.velocity;

    rigidbody.velocity = MoveAir(accelDir, prevVelocity);
  }

  void autoJump() {
    if (grounded & !jumped)
      Jump();
  }

  void Jump() {
    rigidbody.AddForce(Vector3.up * jumpHeight);
    jumped = true;
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.rigidbody.CompareTag("Floor"))
      grounded = true;
  }

  void OnCollisionExit(Collision collision) {
    if (collision.rigidbody.CompareTag("Floor")) {
      grounded = false;
      jumped = false;
    }
  }

  Vector3 getAccelDirection() {
    float input_x = Input.GetAxisRaw("Mouse X");
    Vector3 dir = Vector3.zero;

    if (Mathf.Abs(input_x) > 0)
      dir = transform.right;

    if (input_x < 0)
      dir *= -1f;

    return dir;
  }

  // accelDir: normalized direction that the player has requested to move
  // (taking into account the movement keys and look direction)
  // prevVelocity: The current velocity of the player, before any additional
  // calculations
  // accelerate: The server-defined player acceleration value
  // max_velocity: The server-defined maximum player velocity (this is not
  // strictly adhered to due to strafejumping)
private
  Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate,
                     float max_velocity) {
    float projVel = Vector3.Dot(
        prevVelocity,
        accelDir); // Vector projection of Current velocity onto accelDir.
    float accelVel =
        accelerate *
        Time.fixedDeltaTime; // Accelerated velocity in direction of movment

    // If necessary, truncate the accelerated velocity so the vector projection
    // does not exceed max_velocity
    if (projVel + accelVel > max_velocity)
      accelVel = max_velocity - projVel;

    return prevVelocity + accelDir * accelVel;
  }

private
  Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity) {
    // Apply Friction
    float speed = prevVelocity.magnitude;
    if (speed != 0) // To avoid divide by zero errors
    {
      float drop = speed * friction * Time.fixedDeltaTime;
      prevVelocity *= Mathf.Max(speed - drop, 0) /
                      speed; // Scale the velocity based on friction.
    }

    // ground_accelerate and max_velocity_ground are server-defined movement
    // variables
    return Accelerate(accelDir, prevVelocity, ground_accelerate,
                      max_velocity_ground);
  }

private
  Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity) {
    // air_accelerate and max_velocity_air are server-defined movement variables
    return Accelerate(accelDir, prevVelocity, air_accelerate, max_velocity_air);
  }
}
