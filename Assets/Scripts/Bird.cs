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
    private bool energyDraining = false;

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
            gameStat.GameEnergy -= jump / 2000;
        }

        if (energyDraining)
        {
            gameStat.GameEnergy -= Time.deltaTime * .05f;
        }

        if (gameStat.GameEnergy <= 0)
        {
            Reset();
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
        switch (other.gameObject.tag)
        {
            case "Pipe":
                Reset();
                break;
            case "Range":
                energyDraining = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Range"))
        {
            energyDraining = false;
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

    private void Reset()
    {
        transform.position = new Vector2(-4, 0);
        foreach (var pipe in SpawnPoint.SpawnedPipes)
        {
            Destroy(pipe);
        }
        SpawnPoint.SpawnedPipes.Clear();
        SpawnPoint.PipeTime = 0;
        gameStat.GameTime = 0;
        gameStat.GameScore = 0;
        --gameStat.LivesCount;
        gameStat.GameEnergy = .5f;
        gameMenu.ShowMenu(buttonText: "Again");
    }
}
