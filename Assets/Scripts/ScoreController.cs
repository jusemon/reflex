using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float score = 0;

    private void Update()
    {
        score += Time.deltaTime;
    }
}
