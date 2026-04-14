using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;

    public float zSpawn = 0;
    public float tileLength = 10;
    public int numberOfTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start() {
        for (int i = 0; i < numberOfTiles; i++) {
            if (i < 2) SpawnTile(false);
            else SpawnTile(true);
        }
    }

    void Update() {
        if (playerTransform.position.z - 15 > zSpawn - (numberOfTiles * tileLength)) {
            SpawnTile(true);
            DeleteTile();
        }
    }

    public void SpawnTile(bool spawnObstacles) {
        GameObject go = Instantiate(tilePrefab, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);

        if (spawnObstacles) {
            SpawnObstacle(go.transform);
        }

        zSpawn += tileLength;
    }

    // UPDATED LOGIC: Spawning 1 or 2 obstacles to increase difficulty
    void SpawnObstacle(Transform parentTile) {
        // Randomly choose to spawn 1 or 2 blocks
        int obstacleCount = Random.Range(1, 3);

        // List of available lanes: 0 (Left), 1 (Middle), 2 (Right)
        List<int> availableLanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < obstacleCount; i++) {
            // Pick a random lane from what's left
            int randomIndex = Random.Range(0, availableLanes.Count);
            int lane = availableLanes[randomIndex];

            // Remove it so we don't spawn two blocks in the same spot
            availableLanes.RemoveAt(randomIndex);

            float xPos = (lane - 1) * 3;
            Vector3 spawnPos = new Vector3(xPos, 1, zSpawn);
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

            // Link to the floor tile for clean deletion
            obstacle.transform.parent = parentTile;
        }
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
