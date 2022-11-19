using UnityEngine;

public class Bird : MonoBehaviour
{
    public static float PipeShift = 2;

    [SerializeField]
    private float JumpMagnitude = 10;
    private Rigidbody2D rigidBody2D;
    private Vector2 jumpForce;
    private float holdTime;
    private GameMenu gameMenu;

    void Start()
    {
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        jumpForce = Vector2.up * JumpMagnitude;
        holdTime = 0;
    }

    void Update()
    {
        float jump;

        switch (GameMenu.ControlType)
        {
            case 0:
                jump = Input.GetAxis("Jump");
                jump *= Time.deltaTime * 100;
                rigidBody2D.AddForce(jumpForce * jump);
                break;
            case 1:
                jump = Input.GetKeyDown(KeyCode.Space) ? 50 : 0;
                jump *= Time.deltaTime * 100;
                rigidBody2D.AddForce(jumpForce * jump);
                break;
            case 2:
                jump = 1;
                if (Input.GetKey(KeyCode.Space))
                {
                    if (holdTime == 0)
                    {
                        holdTime = 1;
                    }
                    if (holdTime > 0)
                    {
                        holdTime -= Time.deltaTime;
                    }
                }
                else
                {
                    holdTime = 0;
                }
                jump *= Time.deltaTime * 100;
                if (holdTime > 0)
                {
                    rigidBody2D.AddForce(jumpForce * jump);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            transform.position = new Vector2(-4, 0);
            foreach (var pipe in SpawnPoint.SpawnedPipes)
            {
                Destroy(pipe);
            }
            SpawnPoint.SpawnedPipes.Clear();
            SpawnPoint.PipeTime = 0;
            gameMenu.ShowMenu(buttonText: "Again");
        }
    }
}