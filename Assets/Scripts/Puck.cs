using TMPro;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField]
    private float ForceMagnitude = 10f;
    [SerializeField]
    private TextMeshProUGUI CollisionsTMPro;
    private int collisionsCount;
    private Rigidbody2D RigidBody2D;

    void Start()
    {
        RigidBody2D = GetComponent<Rigidbody2D>();
        collisionsCount = 0;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        RigidBody2D.AddForce(new Vector2(inputX * ForceMagnitude, inputY * ForceMagnitude));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Walls")
        {
            CollisionsTMPro.text = $"Points: {--collisionsCount}";
            return;
        }

        CollisionsTMPro.text = $"Points: {++collisionsCount}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollisionsTMPro.text = $"Points: {collisionsCount += 2}";
        other.gameObject.transform.position = new Vector2(Random.Range(-21f, -3.5f), Random.Range(-4, 4));
    }
}