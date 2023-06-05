using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject floorTileInverted;
    [SerializeField] float positionY = 0.5f;
    [SerializeField] short poolSize = 31;

    private GameObject container;
    private List<GameObject> poolTiles;
    private List<GameObject> poolTilesInverted;
    private GameController gameController;

    private void Start()
    {
        gameController = GetComponent<GameController>();
    }

    private void Awake()
    {
        container = new GameObject("Floor Container");
        poolTiles = new List<GameObject>();
        poolTilesInverted = new List<GameObject>();
        for (int x = 0; x < poolSize; x++)
        {
            poolTiles.Add(CreateFloorTile(floorTile, x, positionY));
            poolTilesInverted.Add(CreateFloorTile(floorTileInverted, x, -positionY));
        }
    }

    private void Update()
    {
        if (gameController.paused) return;
        for (int i = 0; i < poolSize; i++)
        {
            UpdateTilePosition(poolTiles, i);
            UpdateTilePosition(poolTilesInverted, i);
        }
    }

    private GameObject CreateFloorTile(GameObject gameObject, int posX, float posY)
    {
        var tile = Instantiate(gameObject, new Vector3(posX - Mathf.Ceil(poolSize / 2), posY), Quaternion.identity, container.transform);
        tile.name = $"{gameObject.name}_{posX.ToString("00")}";
        return tile;
    }

    private void UpdateTilePosition(List<GameObject> tiles, int pos)
    {
        var tile = tiles[pos];
        var size = Mathf.Ceil(poolSize / 2);
        var range = new Range(pos - size - 1, pos - size);

        tile.transform.position += Vector3.left * Time.deltaTime * gameController.speed;
        if (tile.transform.position.x < range.min)
        {
            tile.transform.position = new Vector3(range.max, tile.transform.position.y, 0);
        }
    }
}
