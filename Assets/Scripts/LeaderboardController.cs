using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] List<Sprite> availableFlags;
    [SerializeField] Sprite defaultFlag;
    [SerializeField] float rowSize = 50f;
    [SerializeField] int numberOfPlayers = 10;
    [SerializeField] GameObject playersContainer;
    [SerializeField] GameObject playerTemplate;
    [SerializeField] Vector2 startPosition = new Vector2(620, -100);
    private Dictionary<string, Sprite> flags;

    void Start()
    {
        flags = availableFlags.ToDictionary((sprite) => sprite.name);
        var ramdomFlags = flags.Keys.ToList().GetRange(20, 30);
        for (int i = 0; i < numberOfPlayers; i++)
        {
            var player = new Player("Player " + (i + 1).ToString("00"), 9999999 - i, GetFlag(ramdomFlags[i]));
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
