using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InvertedGravity : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(-2 * Physics2D.gravity);
    }
}
