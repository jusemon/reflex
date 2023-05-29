using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpForce = 200;
    [SerializeField] bool inverted = false;

    private Animator animator;
    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown(Constants.Jump) && !animator.GetBool(Constants.IsJumping))
        {
            animator.SetBool(Constants.IsJumping, true);
            rigidBody2D.AddForce(new Vector2(0, inverted ? -jumpForce : jumpForce));
        }

        // Debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Constants.Floor))
        {
            animator.SetBool(Constants.IsJumping, false);
        }

        if (other.gameObject.CompareTag(Constants.Obstacle))
        {
            Debug.Log("GameOver");
            Time.timeScale = 0;
        }
    }
}
