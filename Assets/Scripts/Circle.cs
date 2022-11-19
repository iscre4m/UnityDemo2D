using UnityEngine;

public class Circle : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;


    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody2D.AddForce(Vector2.up * 10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            rigidBody2D.AddTorque(-1);
        }
    }
}
