using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}