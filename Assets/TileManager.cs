using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

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

    public void SpawnTile(bool spawnItems) {
        GameObject go = Instantiate(tilePrefab, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);

        if (spawnItems) {
            SpawnObstaclesAndCoins(go.transform);
        }

        zSpawn += tileLength;
    }

    void SpawnObstaclesAndCoins(Transform parentTile) {
        int obstacleCount = Random.Range(1, 3);
        List<int> availableLanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < obstacleCount; i++) {
            int randomIndex = Random.Range(0, availableLanes.Count);
            int lane = availableLanes[randomIndex];
            availableLanes.RemoveAt(randomIndex);

            float xPos = (lane - 1) * 3;
            Vector3 spawnPos = new Vector3(xPos, 1, zSpawn);
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

            // Obstacles can stay as children because they are cubes
            obstacle.transform.parent = parentTile;
        }

        if (availableLanes.Count > 0) {
            int coinLane = availableLanes[Random.Range(0, availableLanes.Count)];
            float xPosCoin = (coinLane - 1) * 3;
            Vector3 coinPos = new Vector3(xPosCoin, 1, zSpawn);

            // IMPORTANT: We create the coin WITHOUT making it a child of parentTile
            // This prevents stretching from the floor's scale
            // Final fix for scale and coins
            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
