using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TileGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public GameObject[] tilePrefabs;
    public float tileLength = 1f;
    public int tilesOnScreen = 5;

    public Transform player;
    private List<GameObject> activeTiles = new List<GameObject>();

    private int lastPrefabIndex = -1;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI meters;
    int metersCount = 0;

    [Header("Speed")]
    [SerializeField] float speedIncrease = 0.5f;
    [SerializeField] float maxSpeed = 30f;

    private void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
            UpdateUI();
        }
    }

    private void Update()
    {
        if (activeTiles.Count == 0) return;

        GameObject lastTile = activeTiles[activeTiles.Count - 1];

        if (lastTile.transform.position.z < player.position.z + (tilesOnScreen * tileLength)) SpawnTile();

        GameObject firstTile = activeTiles[0];

        if (firstTile.transform.position.z < player.position.z - tileLength) DeleteTile();
    }

    void SpawnTile()
    {

        int index;

        if (metersCount < 0.5f) index = 0;
        else index = GetRandomPrefabIndex();

        Vector3 spawnPos;

        if (activeTiles.Count == 0)
        {
            spawnPos = Vector3.zero;
        }
        else
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];
            spawnPos = lastTile.transform.position + Vector3.forward * tileLength;
        }

        GameObject tile = Instantiate(tilePrefabs[index], spawnPos, Quaternion.identity);
        activeTiles.Add(tile);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

        metersCount += 1;

        TilesMovement.speed = Mathf.Clamp(10f + speedIncrease * 0.05f, 0, maxSpeed);
        UpdateUI();
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

    void UpdateUI()
    {
        meters.text = metersCount + " m";
    }
}
