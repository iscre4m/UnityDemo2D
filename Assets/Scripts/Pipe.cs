using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private float Speed = 1;
    private Vector2 direction = Vector2.left;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime * direction);
    }
}