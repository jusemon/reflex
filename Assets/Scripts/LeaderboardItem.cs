using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] Image flagComponent;
    [SerializeField] TMPro.TextMeshProUGUI nameComponent;
    [SerializeField] TMPro.TextMeshProUGUI scoreComponent;

    public void SetData(Player player)
    {
        flagComponent.sprite = player.flag;
        nameComponent.SetText(player.name);
        scoreComponent.SetText(player.score.ToString());
    }
}
