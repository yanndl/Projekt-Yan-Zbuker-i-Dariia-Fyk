using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float maxSpeed = 30f;
    public float speedIncrease = 0.1f;

    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    public float jumpForce = 8f;
    public float gravity = -20f;
    private float verticalVelocity;

    private int desiredLane = 1;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Speed increase
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += speedIncrease * Time.deltaTime;
        }

        // Forward movement
        Vector3 moveVector = new Vector3(0, 0, forwardSpeed);

        // Jump & gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveVector.y = verticalVelocity;

        // Lane switching
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (desiredLane < 2) desiredLane++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (desiredLane > 0) desiredLane--;
        }

        // Target lane position
        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;
        if (desiredLane == 0) targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2) targetPosition += Vector3.right * laneDistance;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * laneChangeSpeed * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

        controller.Move(moveVector * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager.instance.CheckHighScore();
        ScoreManager.instance.ResetScore();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}
