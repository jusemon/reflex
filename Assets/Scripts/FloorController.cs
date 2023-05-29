using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject floorTileInverted;
    [SerializeField] float speed = 5f;
    [SerializeField] float positionY = 0.5f;
    [SerializeField] short pollSize = 30;

    private GameObject container;
    private List<GameObject> poolTiles;
    private List<GameObject> poolTilesInverted;

    private void Awake()
    {
        container = new GameObject("Floor Container");
        poolTiles = new List<GameObject>();
        poolTilesInverted = new List<GameObject>();
        for (int x = 0; x < pollSize; x++)
        {
            poolTiles.Add(CreateFloorTile(floorTile, x, positionY, Constants.Floor));
            poolTilesInverted.Add(CreateFloorTile(floorTileInverted, x, -positionY, Constants.Floor));
        }
    }

    void Update()
    {
        for (int i = 0; i < pollSize; i++)
        {
            UpdateTilePosition(poolTiles[i]);
            UpdateTilePosition(poolTilesInverted[i]);
        }
    }

    private GameObject CreateFloorTile(GameObject gameObject, int posX, float posY, string tag)
    {
        var tile = Instantiate(gameObject, new Vector3(posX - pollSize / 2, posY), Quaternion.identity, container.transform);
        tile.name = $"{gameObject.name}_{posX.ToString("00")}";
        tile.tag = tag;
        return tile;
    }

    private void UpdateTilePosition(GameObject tile)
    {
        tile.transform.position += Vector3.left * Time.deltaTime * speed;
        if (tile.transform.position.x < -pollSize / 2)
        {
            tile.transform.position = new Vector3(pollSize / 2, tile.transform.position.y, 0);
        }
    }
}
