using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField]
    List<Sprite> availableFlags;

    [SerializeField]
    Sprite defaultFlag;

    [SerializeField]
    float rowSize = 50f;

    [SerializeField]
    int numberOfPlayers = 10;

    [SerializeField]
    float apiCallMaxTime = 60f;

    [SerializeField]
    GameObject playersContainer;

    [SerializeField]
    GameObject playerTemplate;

    [SerializeField]
    Vector2 startPosition = new Vector2(620, -100);

    [SerializeField]
    string scoresUrlApi = "https://reflex-api.jusemon.com/api/v1/scores";

    private Dictionary<string, Sprite> flags;

    private float apiCallCountdown = 60f;

    void Awake()
    {
        flags = availableFlags.ToDictionary((sprite) => sprite.name);
    }

    void OnEnable()
    {
        RefreshLeaderboard();
    }

    void Update()
    {
        apiCallCountdown -= Time.unscaledDeltaTime;
        if (apiCallCountdown <= 0)
        {
            apiCallCountdown = apiCallMaxTime;
            RefreshLeaderboard();
        }
    }

    private void RefreshLeaderboard()
    {
        var apiParams = new Dictionary<string, string>();
        apiParams.Add("page", "0");
        apiParams.Add("pageSize", numberOfPlayers.ToString());
        StartCoroutine(
            ApiService.Instance.GetDataFromAPI<ApiResponse<PlayerResponse>>(
                scoresUrlApi,
                apiParams,
                HandleGetDataFromAPIResponse
            )
        );
    }

    private void HandleGetDataFromAPIResponse(ApiResponse<PlayerResponse> response)
    {
        var players = response.data;
        if (players?.Length == 0)
            return;

        // Clear old scores
        foreach (Transform child in playersContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            var player = new Player(players[i].name, players[i].score, GetFlag(players[i].country));
            AddPlayer(i, player);
        }
    }

    private void AddPlayer(int i, Player player)
    {
        var anchoredPosition = new Vector2(startPosition.x, startPosition.y - (i * rowSize));
        var playerGameObject = Instantiate(playerTemplate, playersContainer.transform);
        playerGameObject.name = $"{playerTemplate.name}_{(i + 1).ToString("00")}";
        playerGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        playerGameObject.GetComponent<LeaderboardItem>().SetData(player);
    }

    private Sprite GetFlag(string flagName)
    {
        return flags.ContainsKey(flagName) ? flags[flagName] : defaultFlag;
    }
}
