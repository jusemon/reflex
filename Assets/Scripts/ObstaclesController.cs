using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> obstaclesInverted;
    [SerializeField] float speed = 5f;
    [SerializeField] float positionY = 1.5f;
    [SerializeField] float positionX = 15f;
    [Tooltip("Poll size per dimention")]
    [SerializeField] short pollSize = 10;

    private GameObject container;
    private List<GameObject> pool;
    private List<GameObject> poolInverted;

    // Start is called before the first frame update
    void Awake()
    {
        container = new GameObject("Obstacles Container");
        pool = new List<GameObject>();
        poolInverted = new List<GameObject>();
        for (int i = 0; i < pollSize; i++)
        {
            pool.AddRange(CreateObstacles(obstacles, i, positionY, Constants.Obstacle));
            poolInverted.AddRange(CreateObstacles(obstaclesInverted, i, -positionY, Constants.Obstacle));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move existing active obstacles

        // Deactivate the ones that reach the end

        // Activate some random obstacles at irregular intervals
    }

    private IEnumerable<GameObject> CreateObstacles(List<GameObject> gameObjects, int id, float posY, string tag)
    {
        foreach (var gameObject in gameObjects)
        {
            var tile = Instantiate(gameObject, new Vector3(positionX, posY), Quaternion.identity, container.transform);
            tile.name = $"{gameObject.name}_{id.ToString("00")}";
            tile.tag = tag;
            tile.SetActive(false);
            yield return tile;
        }
    }
}
