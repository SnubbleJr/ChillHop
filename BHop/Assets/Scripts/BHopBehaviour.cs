using System.Collections;
using UnityEngine;

public class BHopBehaviour : MonoBehaviour
{

    [Range(0f, 100f)]
    public float friction = 0.01f;

    [Range(0f, 100f)]
    public float ground_accelerate = 0.01f;

    [Range(0f, 100f)]
    public float max_velocity_ground = 0.01f;

    [Range(0f, 100f)]
    public float air_accelerate = 0.01f;

    [Range(0f, 100f)]
    public float max_velocity_air = 0.01f;

    public float jumpHeight;

    public bool autoHop;

    [Range(0, 5f)]
    public float secondsResetAfterRunEnds = 2f;

    public delegate void BHopEvent();
    public static event BHopEvent resetRun;

    private Rigidbody rigidbody;

    private bool jumped = false;

    private bool grounded = false;

    void OnEnable()
    {
        EndPointBehaviour.runEnded += delayedReset;
    }

    void OnDisable()
    {
        EndPointBehaviour.runEnded -= delayedReset;
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
            reset();
#endif
    }

    void FixedUpdate()
    {
        if (autoHop)
            autoJump();

        Vector3 accelDir = getAccelDirection();
        Vector3 prevVelocity = rigidbody.velocity;

        rigidbody.velocity = MoveAir(accelDir, prevVelocity);
    }

    void autoJump()
    {
        if (grounded & !jumped)
            Jump();
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpHeight);
        jumped = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                grounded = true;
                break;
            case "Reset":
                reset();
                break;
            default:
                break;
        }
    }

    void reset()
    {
        if (resetRun == null)
            return;

        resetRun();
        jumped = false;
        grounded = false;
    }

    void delayedReset(float time)
    {
        StartCoroutine(resetAfterSecond(secondsResetAfterRunEnds));
    }

    IEnumerator resetAfterSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        reset();
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Floor"))
        {
            grounded = false;
            jumped = false;
        }
    }

    Vector3 getAccelDirection()
    {
        Vector3 dir = Vector3.zero;

        float input_x = Input.GetAxisRaw("Mouse X");

        if (Mathf.Abs(input_x) > 0)
            dir = transform.right;

        if (input_x < 0)
            dir *= -1f;
        //#if UNITY_STANDALONE || UNITY_EDITOR
        //#endif

        //#if UNITY_IOS || UNITY_ANDROID

        //        if (Input.touchCount > 0)
        //        {
        //            Touch touch = Input.GetTouch(0);

        //            Vector2 delta = touch.deltaPosition;

        //            if (Mathf.Abs(delta.x) > 0)
        //                dir = transform.right;

        //            if (delta.x < 0)
        //                dir *= -1f;
        //        }
        //#endif

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
            float max_velocity)
    {
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
        Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
    {
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
        Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
    {
        // air_accelerate and max_velocity_air are server-defined movement variables
        return Accelerate(accelDir, prevVelocity, air_accelerate, max_velocity_air);
    }
}
