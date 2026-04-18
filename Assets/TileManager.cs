using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;      // обычное препятствие
    public GameObject lowObstaclePrefab;   // низкое препятствие

    public float zSpawn = 0;
    public float tileLength = 10;
    public int numberOfTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i < 2) SpawnTile(false);
            else SpawnTile(true);
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 15 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(true);
            DeleteTile();
        }
    }

    public void SpawnTile(bool spawnObstacles)
    {
        GameObject go = Instantiate(tilePrefab, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);

        if (spawnObstacles)
        {
            SpawnObstacle(go.transform);
        }

        zSpawn += tileLength;
    }

    void SpawnObstacle(Transform parentTile)
    {
        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * 3;

        // ВАЖНО: как в твоём первом рабочем варианте — препятствие ставится по zSpawn
        Vector3 spawnPos;

        // выбираем тип препятствия
        int type = Random.Range(0, 2); // 0 = обычное, 1 = низкое

        if (type == 0)
        {
            // обычное препятствие
            spawnPos = new Vector3(xPos, 1f, zSpawn);
            GameObject obj = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            obj.transform.parent = parentTile;
        }
        else
        {
            // низкое препятствие
            spawnPos = new Vector3(xPos, 0.5f, zSpawn);
            GameObject obj = Instantiate(lowObstaclePrefab, spawnPos, Quaternion.identity);
            obj.transform.parent = parentTile;
        }
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
