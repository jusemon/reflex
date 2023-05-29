using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TwinkleController : MonoBehaviour
{
    [SerializeField][Min(0)] float waitForSeconds = 0.35f;

    private TextMeshProUGUI textMeshPro;
    private string text;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        text = textMeshPro.text;
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            textMeshPro.text = text;
            yield return new WaitForSeconds(waitForSeconds);
            textMeshPro.text = string.Empty;
            yield return new WaitForSeconds(waitForSeconds);
        }
    }
}
