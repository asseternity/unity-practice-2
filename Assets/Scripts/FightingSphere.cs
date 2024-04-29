using UnityEngine;
using UnityEngine.InputSystem;
public class FightingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float maxSpeed = 10f, maxAcceleration = 10f, maxAirAcceleration = 1f, maxGroundAngle = 45f, maxSnapSpeed = 100f;
    [SerializeField, Range(0f, 10f)] float jumpHeight = 2f;
    [SerializeField, Range(0f, 10)] int maxAirJumps = 1;
    [SerializeField, Min(0f)] float probeDistance = 1f;
    [SerializeField] LayerMask probeMask = -1;
    public int gemsCollected = 0;
    float minGroundDotProduct;
    int jumpPhase = 0;
    Vector3 velocity = Vector3.zero;
    Vector3 desiredVelocity = Vector3.zero;
    Vector3 contactNormal;
    bool desiredJump = false;
    bool onGround = false;
    int stepsSinceLastGrounded, stepsSinceLastJump;
    Rigidbody body;
    [SerializeField] Transform playerInputSpace = default;
    public AudioSource src;
    void OnValidate() { minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad); } 
    void Awake() { body = GetComponent<Rigidbody>(); OnValidate(); }
    bool jumpPressed = false;
    void Update()
    {
        jumpPressed = false;
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        if (playerInputSpace)
        {
            Vector3 forward = playerInputSpace.forward;
            forward.y = 0f;
            forward.Normalize();
            Vector3 right = playerInputSpace.right;
            right.y = 0f;
            right.Normalize();
            desiredVelocity = (forward * playerInput.y + right * playerInput.x) * maxSpeed;
        } else { desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed; }
        float jumpDown = jumpButton.ReadValue<float>();
        if (jumpDown > 0f) { jumpPressed = true; }
        desiredJump |= jumpPressed;
        GetComponent<Renderer>().material.SetColor("_Color", onGround ? Color.black : Color.white );

        if (health <= 0) { Destroy(gameObject, 1f); }
        MeleeAttack();
        Shoot();
        if (attacking == true) {
            weapon.transform.position = transform.position;
            weapon.transform.Rotate(0, 0, 540 * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        UpdateState();
        AdjustVelocity();
        if (desiredJump) { desiredJump = false; Jump(); }
        body.velocity = velocity;
        ClearState();
    }
    void ClearState()
    { onGround = false; contactNormal = Vector3.zero; }

    void Jump()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            src.Play();
            stepsSinceLastJump = 0;
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            float alignedSpeed = Vector3.Dot(velocity, contactNormal); // this is the dot product (middle) between the two vectors / angles
            if (alignedSpeed > 0f) { jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f); } // it is done to make jumps angled to the contact surface (Physics, 3.4)
            velocity += contactNormal * jumpSpeed;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "gem") {
            gemsCollected++;
            Destroy(other.gameObject);
        }    
    }
    void OnCollisionEnter(Collision collision)
    { 
        EvaluateCollision(collision); 
        if (collision.gameObject.tag == "TestTag") {
            transform.localScale *= 1.1f;
        } else if (collision.gameObject.tag == "enemy") {
            health -= 10;
        }    
    }
    void OnCollisionStay(Collision collision)
    { EvaluateCollision(collision); }
    void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            if (normal.y >= minGroundDotProduct) { onGround = true; contactNormal += normal; }
        }
    }
    void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = body.velocity; // update the velocity variable with the current velocity of a Rigidbody component (body) attached to a game object.
        if (onGround || SnapToGround()) { stepsSinceLastGrounded = 0; jumpPhase = 0; contactNormal.Normalize(); }
        else { contactNormal = Vector3.up; }
    }
    Vector3 ProjectOnContactPlane (Vector3 vector) { return vector - contactNormal * Vector3.Dot(vector, contactNormal); }
    // Method above takes a vector (like the right or forward direction) and returns a new vector that is aligned to be parallel with the slope's surface.

    void AdjustVelocity()
    {
        Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
        Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

        // we want to know how much of the current velocity is in the direction of the slope
        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);

        // calculate regular acceleration, maxSpeedChange and regular incremented vector change
        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);
        // last line updates the velocity along the slope's plane by adding the change in velocity along the xAxis and zAxis directions 
        // multiplied by the difference between the new and current velocities (because that we we calculate how much speed along an axis needs to change)
        // so, xAxis and zAxis are directions, and newX minus currentX is the speed.
        // xAxis and zAxis are directions (unit vectors), and newX - currentX represents the change in speed along those directions
        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }
    bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2) { return false; }
        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed) { return false; }
        if (!Physics.Raycast(body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask)) { return false; }
        if (hit.normal.y < minGroundDotProduct) { return false;}

        contactNormal = hit.normal;
        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f) { velocity = (velocity - hit.normal * dot).normalized * speed; }
        return true;
    }
    public int health = 100;
    public GameObject weapon;
    Vector3 initialWeaponPosition;
    void Start()
    {
        initialWeaponPosition = weapon.transform.position;
    }
    bool attacking = false;
    void MeleeAttack()
    {
        float attackDown = attackButton.ReadValue<float>();
        if (attackDown > 0) {
            if (attacking == false) {
                attacking = true;
                weapon.transform.position = transform.position;
                Invoke("RemoveWeapon", 1f);
            }
        }
    }
    void RemoveWeapon()
    {
        weapon.transform.position = initialWeaponPosition;
        attacking = false;
    }
    public InputAction attackButton;
    public InputAction shootButton;
    public InputAction jumpButton;
    private void OnEnable() { attackButton.Enable(); shootButton.Enable(); jumpButton.Enable(); }
    private void OnDisable() { attackButton.Disable(); shootButton.Disable(); jumpButton.Enable(); }
    public GameObject laser;
    bool shootCooldown = false;
    public GameObject camera;
    void Shoot()
    {
        transform.rotation = playerInputSpace.transform.rotation;
        float shootDown = shootButton.ReadValue<float>();
        if (shootDown > 0 && shootCooldown == false) {
            GameObject newLaser = Instantiate(laser, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            UnityEngine.Vector3 shootDirection = transform.forward;
            shootDirection.y = 0f;
            shootDirection.Normalize();
            float shootSpeed = 10f;
            newLaser.GetComponent<Rigidbody>().velocity = shootDirection * shootSpeed;
            newLaser.GetComponent<laserScript>().fired = true;
            shootCooldown = true;
            Invoke("ShootCooldown", 0.5f);
        }
    }
    void ShootCooldown()
    {
        shootCooldown = false;
    }
}