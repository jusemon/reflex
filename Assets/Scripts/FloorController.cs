using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject floorTileInversed;
    [SerializeField] float speed = 5f;
    [SerializeField] float positionY = 0.5f;
    [SerializeField] short pollSize = 30;

    private GameObject container;
    private List<GameObject> poolTiles;
    private List<GameObject> poolTilesInversed;

    // Build the floor
    void Start()
    {
        container = new GameObject("Floor Container");
        poolTiles = new List<GameObject>();
        poolTilesInversed = new List<GameObject>();
        for (int x = 0; x < pollSize; x++)
        {
            poolTiles.Add(CreateFloorTile(floorTile, x, positionY, Constants.Floor));
            poolTilesInversed.Add(CreateFloorTile(floorTileInversed, x, positionY - 1, Constants.Floor));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pollSize; i++)
        {
            UpdateTilePosition(poolTiles[i]);
            UpdateTilePosition(poolTilesInversed[i]);
        }
    }

    private GameObject CreateFloorTile(GameObject gameObject, int posX, float posY, string tag)
    {
        var tile = Instantiate(gameObject, new Vector3(posX - pollSize / 2, posY), Quaternion.identity, container.transform);
        tile.tag = tag;
        return tile;
    }

    private void UpdateTilePosition(GameObject tile)
    {
        tile.transform.position += Vector3.left * speed / 100;
        if (tile.transform.position.x < -pollSize / 2)
        {
            tile.transform.position = new Vector3(pollSize / 2, tile.transform.position.y, 0);
        }
    }
}
