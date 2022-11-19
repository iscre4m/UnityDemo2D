using UnityEngine;

public class GameStat : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI clock;
    private float _gameTime;
    public float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateUITime();
        }
    }

    void Start()
    {

    }

    void LateUpdate()
    {
        GameTime += Time.deltaTime;
    }

    private void UpdateUITime()
    {
        clock.text = $"{(int)_gameTime / 60:00}:{_gameTime % 60:00.0}";
    }
}