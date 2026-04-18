using UnityEngine;

public class Coin : MonoBehaviour {
    public float rotationSpeed = 100f;

    void Start() {
        // Auto-destroy after 15 seconds to keep the hierarchy clean
        // since coins are no longer children of the tiles
        Destroy(gameObject, 15f);
    }

    void Update() {
        // Rotating the coin around its axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other) {
        // Checking if the player touched the coin
        if (other.CompareTag("Player")) {
            // We will add score logic here later
            Destroy(gameObject);
        }
    }
}