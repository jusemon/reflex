using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultController : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float increment = 0.01f;

    private void Update()
    {
        speed += increment * Time.deltaTime;
    }
}
