using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DifficultController))]
public class ObstaclesController : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] List<GameObject> obstaclesInverted;
    [SerializeField] Range intervalRange = new Range(1f, 4f);
    [SerializeField] float positionY = 1.5f;
    [SerializeField] float positionX = 15f;
    [Tooltip("Poll size per dimention")]
    [SerializeField] short pollSize = 10;

    private GameObject container;
    private List<GameObject> pool;
    private List<GameObject> poolInverted;
    private float timeToNextSpawn = 0f;
    private float timeToNextSpawnInverted = 0f;
    private DifficultController difficult;

    private void Start()
    {
        difficult = GetComponent<DifficultController>();
    }

    private void Awake()
    {
        container = new GameObject("Obstacles Container");
        pool = new List<GameObject>();
        poolInverted = new List<GameObject>();
        for (int i = 0; i < pollSize; i++)
        {
            pool.AddRange(CreateObstacles(obstacles, i, positionY));
            poolInverted.AddRange(CreateObstacles(obstaclesInverted, i, -positionY));
        }
    }

    private void Update()
    {
        // Move existing active obstacles
        for (int i = 0; i < pollSize; i++)
        {
            UpdateObstaclePosition(pool[i]);
            UpdateObstaclePosition(poolInverted[i]);
        }

        // Spawn some random obstacles at random intervals
        Spawn();
    }

    private void Spawn()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn <= 0)
        {
            SpawnObstacle(pool);
            timeToNextSpawn = Random.Range(intervalRange.min, intervalRange.max);
        }

        timeToNextSpawnInverted -= Time.deltaTime;
        if (timeToNextSpawnInverted <= 0)
        {
            SpawnObstacle(poolInverted);
            timeToNextSpawnInverted = Random.Range(intervalRange.min, intervalRange.max);
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

    private IEnumerable<GameObject> CreateObstacles(List<GameObject> gameObjects, int id, float posY)
    {
        foreach (var gameObject in gameObjects)
        {
            var obstacle = Instantiate(gameObject, new Vector3(positionX, posY), Quaternion.identity, container.transform);
            obstacle.name = $"{gameObject.name}_{id.ToString("00")}";
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

        obstacle.transform.position += Vector3.left * Time.deltaTime * difficult.speed;
        if (obstacle.transform.position.x < -positionX)
        {
            obstacle.transform.position = new Vector3(positionX, obstacle.transform.position.y, 0);
            obstacle.SetActive(false);
        }
    }
}
