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

    // private const string _maxScoreFilename = "max_score.sav";
    private string _maxDataFilename;

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

    private void Start()
    {
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        spawnPoint = GameObject.Find("SpawnPoint").GetComponent<SpawnPoint>();

        _maxDataFilename = "max_data.json";
        GameEnergy = energy.fillAmount;

        // if (System.IO.File.Exists(_maxScoreFilename))
        // {
        //     string[] lines = System.IO.File.ReadAllLines(_maxScoreFilename);
        //     _maxScore = short.Parse(lines[0]);
        //     _maxTime = float.Parse(lines[1]);
        // }
        // else
        // {
        //     System.IO.File.WriteAllText(_maxScoreFilename, "0\n0");
        //     _maxScore = 0;
        //     _maxTime = 0;
        // }

        if (System.IO.File.Exists(_maxDataFilename))
        {
            var maxData = JsonUtility.FromJson<MaxData>(
                System.IO.File.ReadAllText(_maxDataFilename)
            );

            _maxScore = maxData.GameScore;
            _maxTime = maxData.GameTime;
        }

        LivesCount = 3;
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        // System.IO.File.WriteAllText(
        //     _maxScoreFilename,
        //     $"{(_gameScore > _maxScore ? _gameScore : _maxScore)}\n" +
        //     $"{(_gameTime > _maxTime ? _gameTime : _maxTime)}"
        // );

        var maxData = new MaxData
        {
            GameScore = (_gameScore > _maxScore ? _gameScore : _maxScore),
            GameTime = (_gameTime > _maxTime ? _gameTime : _maxTime)
        };

        System.IO.File.WriteAllText(_maxDataFilename, JsonUtility.ToJson(maxData, true));
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

    class MaxData
    {
        public short GameScore;
        public float GameTime;
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
