using UnityEngine;

public class GameStat : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI clock;
    [SerializeField]
    private TMPro.TextMeshProUGUI score;
    [SerializeField]
    private UnityEngine.UI.Image energy;

    private float _gameTime;
    private short _gameScore;
    private float _gameEnergy;
    private short _maxScore;
    private float _maxTime;
    private string _maxScoreFilename = "max_score.sav";
    private string _maxDataFilename = "max_data.json";

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

    private void Start()
    {
        GameEnergy = energy.fillAmount;
        if (System.IO.File.Exists(_maxScoreFilename))
        {
            string[] lines = System.IO.File.ReadAllLines(_maxScoreFilename);
            _maxScore = short.Parse(lines[0]);
            _maxTime = float.Parse(lines[1]);
        }
        else
        {
            System.IO.File.WriteAllText(_maxScoreFilename, "0\n0");
            _maxScore = 0;
            _maxTime = 0;
        }

        if (System.IO.File.Exists(_maxDataFilename))
        {
            var maxData = JsonUtility.FromJson<MaxData>(
                System.IO.File.ReadAllText(_maxDataFilename)
            );
            _maxScore = maxData.GameScore;
            _maxTime = maxData.GameTime;
        }

        Debug.Log($"maxScore = {_maxScore}");
        Debug.Log($"maxTime = {_maxTime}");
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        System.IO.File.WriteAllText(_maxScoreFilename,
                                    $"{(_gameScore > _maxScore ? _gameScore : _maxScore)}\n" +
                                    $"{(_gameTime > _maxTime ? _gameTime : _maxTime)}");
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

        //Debug.LogError($"gameEnergy out of range: {_gameEnergy}");
    }

    class MaxData
    {
        public short GameScore;
        public float GameTime;
    }
}
