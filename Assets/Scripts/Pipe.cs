using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private float Speed = 1;

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime * Vector2.left);
    }
}
