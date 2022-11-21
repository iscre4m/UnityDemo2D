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
    private GameStat gameStat;

    void Start()
    {
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        gameStat = GameObject.Find("GameStat").GetComponent<GameStat>();
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
            default:
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
        
        if (jump > 0)
        {
            gameStat.GameEnergy -= jump / 1000;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 3 * rigidBody2D.velocity.y);

        if (rigidBody2D.velocity.x != 0)
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Energy"))
        {
            Destroy(other.gameObject);
            if (gameStat.GameEnergy < .5f)
            {
                gameStat.GameEnergy += .5f;
            }
            else
            {
                gameStat.GameEnergy = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tube"))
        {
            ++gameStat.GameScore;
        }
    }
}