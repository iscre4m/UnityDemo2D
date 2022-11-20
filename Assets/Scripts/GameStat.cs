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
    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private void UpdateUITime()
    {
        clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";
    }

    private void UpdateUIScore()
    {
        score.text = $"{_gameScore:0000}";
    }

    private void UpdateUIEnergy()
    {
        if (_gameEnergy >= 0 && _gameEnergy <= 1)
        {
            energy.fillAmount = _gameEnergy;

            return;
        }

        Debug.LogError($"gameEnergy out of range: {_gameEnergy}");
    }
}