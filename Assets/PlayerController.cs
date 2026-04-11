using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Movement settings
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    // Jump and Gravity settings
    public float jumpForce = 8f;
    public float gravity = -20f;
    private float verticalVelocity;

    private int desiredLane = 1; // 0: Left, 1: Middle, 2: Right
    private CharacterController controller;

    void Start() {
        // Finding the CharacterController component on the Player object
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        // 1. Calculate forward movement
        Vector3 moveVector = new Vector3(0, 0, forwardSpeed);

        // 2. Jump and Gravity logic
        if (controller.isGrounded) {
            verticalVelocity = -0.5f; // Keep player stuck to the ground
            if (Input.GetKeyDown(KeyCode.Space)) {
                verticalVelocity = jumpForce;
            }
        } else {
            // Apply gravity over time
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveVector.y = verticalVelocity;

        // 3. Lane Switching logic
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if (desiredLane < 2) desiredLane++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if (desiredLane > 0) desiredLane--;
        }

        // 4. Calculate the precise target position
        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;
        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        // Calculate smooth shift toward the target lane
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * laneChangeSpeed * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

        // Final move execution (Forward + Jump/Fall)
        controller.Move(moveVector * Time.deltaTime);
    }
}
