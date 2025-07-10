using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> tileList;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
        SpawnTile();
    }

    private void SpawnTile()
    {
        if (saveService == null)
        {
            return;
        }

        int stage = saveService.Load().currentStage;

        GameObject tilePrefab = null;

        if (stage >= 10 && stage <= 19 && tileList.Count > 0)
            tilePrefab = tileList[0];
        else if (stage >= 20 && stage <= 29 && tileList.Count > 1)
            tilePrefab = tileList[1];

        if (tilePrefab != null)
        {
            Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
