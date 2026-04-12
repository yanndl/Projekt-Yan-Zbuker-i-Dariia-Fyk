using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject tilePrefab;
    public GameObject obstaclePrefab; // Field for your red cube prefab

    public float zSpawn = 0;
    public float tileLength = 10;
    public int numberOfTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start() {
        for (int i = 0; i < numberOfTiles; i++) {
            // First 2 tiles stay empty so the player has time to start
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

    void SpawnObstacle(Transform parentTile) {
        // Choose a random lane: 0, 1, or 2
        int lane = Random.Range(0, 3);
        // Calculate X position (-3, 0, 3) to match player lanes
        float xPos = (lane - 1) * 3;

        // Spawn obstacle slightly above the floor
        Vector3 spawnPos = new Vector3(xPos, 1, zSpawn);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Parent obstacle to the tile so they get deleted together
        obstacle.transform.parent = parentTile;
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
