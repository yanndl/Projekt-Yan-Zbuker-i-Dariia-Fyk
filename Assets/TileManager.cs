using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject tilePrefab;
    public float zSpawn = 0;
    public float tileLength = 10;
    public int numberOfTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start() {
        // Initial path generation
        for (int i = 0; i < numberOfTiles; i++) {
            SpawnTile(0);
        }
    }

    void Update() {
        // Check if player moved far enough to spawn a new tile
        if (playerTransform.position.z - 15 > zSpawn - (numberOfTiles * tileLength)) {
            SpawnTile(0);
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex) {
        GameObject go = Instantiate(tilePrefab, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile() {
        // Remove old tiles to save memory
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
