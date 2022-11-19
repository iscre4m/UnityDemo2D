using Unity.VisualScripting;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidBody2D.AddForce(new Vector2(8, 6), ForceMode2D.Impulse);
            rigidBody2D.gravityScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rigidBody2D.AddForce(new Vector2(10, 7), ForceMode2D.Impulse);
            rigidBody2D.gravityScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidBody2D.AddForce(new Vector2(12, 8), ForceMode2D.Impulse);
            rigidBody2D.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "Walls")
        {
            foreach(Transform child in other.transform.parent)
            {
                child.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }
}