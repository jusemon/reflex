using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DifficultController))]
public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject floorTileInverted;
    [SerializeField] float positionY = 0.5f;
    [SerializeField] short pollSize = 30;

    private GameObject container;
    private List<GameObject> poolTiles;
    private List<GameObject> poolTilesInverted;
    private DifficultController difficult;

    private void Start()
    {
        difficult = GetComponent<DifficultController>();
    }

    private void Awake()
    {
        container = new GameObject("Floor Container");
        poolTiles = new List<GameObject>();
        poolTilesInverted = new List<GameObject>();
        for (int x = 0; x < pollSize; x++)
        {
            poolTiles.Add(CreateFloorTile(floorTile, x, positionY));
            poolTilesInverted.Add(CreateFloorTile(floorTileInverted, x, -positionY));
        }
    }

    private void Update()
    {
        for (int i = 0; i < pollSize; i++)
        {
            UpdateTilePosition(poolTiles[i]);
            UpdateTilePosition(poolTilesInverted[i]);
        }
    }

    private GameObject CreateFloorTile(GameObject gameObject, int posX, float posY)
    {
        var tile = Instantiate(gameObject, new Vector3(posX - pollSize / 2, posY), Quaternion.identity, container.transform);
        tile.name = $"{gameObject.name}_{posX.ToString("00")}";
        return tile;
    }

    private void UpdateTilePosition(GameObject tile)
    {
        tile.transform.position += Vector3.left * Time.deltaTime * difficult.speed;
        if (tile.transform.position.x < -pollSize / 2)
        {
            tile.transform.position = new Vector3(pollSize / 2, tile.transform.position.y, 0);
        }
    }
}
