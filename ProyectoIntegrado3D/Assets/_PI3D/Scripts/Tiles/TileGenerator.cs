using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public GameObject[] tilePrefabs;
    public float tileLength = 1f;
    public int tilesOnScreen = 5;

    public Transform player;

    private float spawnZ = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    private int lastPrefabIndex = -1;

    private void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        if (spawnZ < player.position.z + (tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        int index = GetRandomPrefabIndex();

        GameObject tile = Instantiate(tilePrefabs[index], Vector3.forward * spawnZ, Quaternion.identity);

        activeTiles.Add(tile);
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    int GetRandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1) return 0;

        int randomIndex = lastPrefabIndex;

        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
