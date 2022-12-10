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

    private const string _recordsDataFilename = "max_data.json";

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

    private RecordsData recordsData;

    private void Start()
    {
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        spawnPoint = GameObject.Find("SpawnPoint").GetComponent<SpawnPoint>();

        LivesCount = 3;
        GameEnergy = energy.fillAmount;
        recordsData = new RecordsData();

        if (System.IO.File.Exists(_recordsDataFilename))
        {
            recordsData = JsonUtility.FromJson<RecordsData>(
                System.IO.File.ReadAllText(_recordsDataFilename)
            );

            _maxScore = recordsData.Scores[0];
            _maxTime = recordsData.Times[0];
        }

        for (int i = 0; i < 3; ++i)
        {
            Debug.Log($"{recordsData.Scores[i]} - {recordsData.Times[i]}");
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
            if (_gameScore > recordsData.Scores[i])
            {
                switch (i)
                {
                    case 0:
                        for (int j = 0; j < 2; ++j)
                        {
                            recordsData.Scores[2 - j] =
                            recordsData.Scores[1 - j];
                            recordsData.Times[2 - j] =
                            recordsData.Times[1 - j];
                        }
                        break;
                    case 1:
                        recordsData.Scores[2] = recordsData.Scores[1];
                        recordsData.Times[2] = recordsData.Times[1];
                        break;
                    default:
                        break;
                }

                recordsData.Scores[i] = _gameScore;
                recordsData.Times[i] = _gameTime;
                break;
            }
        }

        System.IO.File.WriteAllText(
            _recordsDataFilename,
            JsonUtility.ToJson(recordsData, true)
        );
    }

    private void UpdateUITime()
    {
        clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";

        if (_gameTime > _maxTime)
        {
            clock.fontStyle = TMPro.FontStyles.Bold;
        }
    }

    private void UpdateUIScore()
    {
        score.text = $"{_gameScore:0000}";

        if (_gameScore > _maxScore)
        {
            score.fontStyle = TMPro.FontStyles.Bold;
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

    class RecordsData
    {
        public short[] Scores = new short[3];
        public float[] Times = new float[3];
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
