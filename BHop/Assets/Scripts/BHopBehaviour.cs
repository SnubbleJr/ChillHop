using System.Collections;
using UnityEngine;
using Zenject;

public class BHopBehaviour : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate playerJumped;
    public static event PlayerDelegate respawnPlayer;

    private PhysicsData data;

    [Range(0, 5f)] public float secondsResetAfterRunEnds = 2f;
    

    private new Rigidbody rigidbody;

    private bool jumped = false;

    private bool grounded = false;

    [Inject]
    public void Init(PhysicsData physicsData)
    {
        data = physicsData;
    }

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

    void Start()
    {
        reset();
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
        if (data.autoHop)
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
        if (playerJumped != null)
            playerJumped();

        rigidbody.AddForce(Vector3.up * data.jumpHeight);
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
        if (respawnPlayer == null)
            return;

        respawnPlayer();
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
        float input_x = Input.GetAxisRaw("Mouse X");

        if (input_x > 0)
            return transform.right;

        if (input_x < 0)
            return transform.right * -1.0f;

        return Vector3.zero;
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

    }

    // accelDir: normalized direction that the player has requested to move
    // (taking into account the movement keys and look direction)
    // prevVelocity: The current velocity of the player, before any additional
    // calculations
    // accelerate: The server-defined player acceleration value
    // max_velocity: The server-defined maximum player velocity (this is not
    // strictly adhered to due to strafejumping)
    private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate,
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

    private Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
    {
        // Apply Friction
        float speed = prevVelocity.magnitude;
        if (speed != 0) // To avoid divide by zero errors
        {
            float drop = speed * data.friction * Time.fixedDeltaTime;
            prevVelocity *= Mathf.Max(speed - drop, 0) /
                            speed; // Scale the velocity based on friction.
        }

        // ground_accelerate and max_velocity_ground are server-defined movement
        // variables
        return Accelerate(accelDir, prevVelocity, data.ground_accelerate,
            data.max_velocity_ground);
    }

    private Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
    {
        // air_accelerate and max_velocity_air are server-defined movement variables
        return Accelerate(accelDir, prevVelocity, data.air_accelerate, data.max_velocity_air);
    }
}
