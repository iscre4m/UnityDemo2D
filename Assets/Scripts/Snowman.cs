using UnityEngine;

public class Snowman : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.centerOfMass = new Vector2(0, -2f);
    }

    void Update()
    {
        
    }
}