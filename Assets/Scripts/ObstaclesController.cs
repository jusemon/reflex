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
    private float timeToNextSpawn = 0f;
    private float timeToNextSpawnInverted = 0f;

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
        for (int i = 0; i < pollSize; i++)
        {
            UpdateObstaclePosition(pool[i]);
            UpdateObstaclePosition(poolInverted[i]);
        }

        // Spawn some random obstacles at irregular intervals
        Spawn();
    }

    private void Spawn()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn <= 0)
        {
            SpawnObstacle(pool);
            timeToNextSpawn = Random.Range(1f, 4f);
        }

        timeToNextSpawnInverted -= Time.deltaTime;
        if (timeToNextSpawnInverted <= 0)
        {
            SpawnObstacle(poolInverted);
            timeToNextSpawnInverted = Random.Range(1f, 4f);
        }
    }

    private void SpawnObstacle(List<GameObject> gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                return;
            }
        }
    }

    private IEnumerable<GameObject> CreateObstacles(List<GameObject> gameObjects, int id, float posY, string tag)
    {
        foreach (var gameObject in gameObjects)
        {
            var obstacle = Instantiate(gameObject, new Vector3(positionX, posY), Quaternion.identity, container.transform);
            obstacle.name = $"{gameObject.name}_{id.ToString("00")}";
            obstacle.tag = tag;
            obstacle.SetActive(false);
            yield return obstacle;
        }
    }

    private void UpdateObstaclePosition(GameObject obstacle)
    {
        if (!obstacle.activeSelf)
        {
            return;
        }

        obstacle.transform.position += Vector3.left * Time.deltaTime * speed;
        if (obstacle.transform.position.x < -positionX)
        {
            obstacle.transform.position = new Vector3(positionX, obstacle.transform.position.y, 0);
            obstacle.SetActive(false);
        }
    }
}
