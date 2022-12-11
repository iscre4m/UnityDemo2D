using System.Text;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI clock;
    [SerializeField]
    private TMPro.TextMeshProUGUI score;
    [SerializeField]
    private UnityEngine.UI.Image energy;
    [SerializeField]
    private TMPro.TextMeshProUGUI lives;

    private float _gameTime;
    private short _gameScore;
    private float _gameEnergy;
    private short _maxScore;
    private float _maxTime;
    private byte _livesCount;

    private GameMenu gameMenu;
    private SpawnPoint spawnPoint;

    private const string _recordsDataFilename = "records_data.json";

    public float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateUITime();
        }
    }

    public short GameScore
    {
        get => _gameScore;
        set
        {
            _gameScore = value;
            UpdateUIScore();
        }
    }

    public float GameEnergy
    {
        get => _gameEnergy;
        set
        {
            _gameEnergy = value;
            UpdateUIEnergy();
        }
    }

    public byte LivesCount
    {
        get => _livesCount;
        set
        {
            _livesCount = value;
            UpdateUILives();
        }
    }

    public short MaxScore
    {
        get => _maxScore;
    }

    public float MaxTime
    {
        get => _maxTime;
    }

    public static RecordsData Records;
    private readonly Color[] RECORD_COLORS =
    {
        new Color(1, .84f, 0),
        new Color(.75f, .75f, .75f),
        new Color(1, .34f, .2f)
    };

    private void Start()
    {
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        spawnPoint = GameObject.Find("SpawnPoint").GetComponent<SpawnPoint>();

        LivesCount = 3;
        GameEnergy = energy.fillAmount;
        Records = new RecordsData();

        if (System.IO.File.Exists(_recordsDataFilename))
        {
            Records = JsonUtility.FromJson<RecordsData>(
                System.IO.File.ReadAllText(_recordsDataFilename)
            );

            _maxScore = Records.Scores[0];
            _maxTime = Records.Times[0];
        }
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (_gameScore > Records.Scores[i])
            {
                switch (i)
                {
                    case 0:
                        for (int j = 0; j < 2; ++j)
                        {
                            Records.Scores[2 - j] =
                            Records.Scores[1 - j];
                            Records.Times[2 - j] =
                            Records.Times[1 - j];
                        }
                        break;
                    case 1:
                        Records.Scores[2] = Records.Scores[1];
                        Records.Times[2] = Records.Times[1];
                        break;
                    default:
                        break;
                }

                Records.Scores[i] = _gameScore;
                Records.Times[i] = _gameTime;
                break;
            }
        }

        System.IO.File.WriteAllText(
            _recordsDataFilename,
            JsonUtility.ToJson(Records, true)
        );
    }

    private void UpdateUITime()
    {
        clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";
    }

    private void UpdateUIScore()
    {
        score.text = $"{_gameScore:0000}";

        for (int i = 2; i > -1; --i)
        {
            if (_gameScore > Records.Scores[i])
            {
                score.fontStyle = TMPro.FontStyles.Bold;
                score.color = RECORD_COLORS[i];
            }
        }
    }

    private void UpdateUIEnergy()
    {
        if (_gameEnergy >= 0 && _gameEnergy <= 1)
        {
            energy.fillAmount = _gameEnergy;

            return;
        }
    }

    private void UpdateUILives()
    {
        lives.text = $"{_livesCount}";
    }

    public class RecordsData
    {
        public short[] Scores = new short[3];
        public float[] Times = new float[3];

        public override string ToString()
        {
            StringBuilder result = new();
            for(int i = 0; i < 3; ++i)
            {
                result.Append(i == 0 ? "1st" : i == 1 ? "2nd" : "3rd");
                result.Append("\n");
                result.Append($"Score: {Records.Scores[i]}\n");
                result.Append($"Time: {Records.Times[i]:00.0}");
                if (i < 2)
                {
                    result.Append("\n\n");
                }
            }

            return result.ToString();
        }
    }

    public void Reset()
    {
        GameEnergy = .5f;
        transform.position = new Vector2(-4, 0);
        spawnPoint.Clear();
    }

    public void GameOver()
    {
        GameTime = 0;
        GameScore = 0;
        gameMenu.ShowMenu(buttonText: "Again");
    }
}
