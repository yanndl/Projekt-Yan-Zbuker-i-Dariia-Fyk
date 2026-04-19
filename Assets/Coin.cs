using UnityEngine;

public class Coin : MonoBehaviour {
    public float rotationSpeed = 100f;

    void Start() {
        // Auto-destroy after 15 seconds to keep the hierarchy clean
        Destroy(gameObject, 15f);
    }

    void Update() {
        // Rotating the coin around its axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other) {
        // Checking if the player touched the coin
        if (other.CompareTag("Player")) {
            // Adding coin to the score manager
            if (ScoreManager.instance != null) {
                ScoreManager.instance.AddCoin();
            }

            Destroy(gameObject);
        }
    }
}