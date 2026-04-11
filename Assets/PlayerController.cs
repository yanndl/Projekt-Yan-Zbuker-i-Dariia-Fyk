using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Movement settings
    public float forwardSpeed = 10f;
    public float laneDistance = 3f; // Distance between lanes
    public float laneChangeSpeed = 10f; // How fast the player shifts lanes

    private int desiredLane = 1; // 0: Left, 1: Middle, 2: Right
    private Vector3 targetPosition;

    void Update() {
        // 1. Calculate forward movement
        Vector3 moveVector = Vector3.forward * forwardSpeed * Time.deltaTime;

        // 2. Handle Input (Lane switching)
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if (desiredLane < 2) desiredLane++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if (desiredLane > 0) desiredLane--;
        }

        // 3. Calculate target position based on the lane
        // Keep current Y (height) and Z (forward progress)
        targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;

        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // 4. Apply movement
        // Lerp makes the lane change smooth instead of instant teleporting
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);

        // Move the object forward in world space
        transform.Translate(moveVector, Space.World);
    }
}
