using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static int ControlType;
    public static float GameDifficulty;

    [SerializeField]
    private GameObject menuContainer;
    [SerializeField]
    private TMPro.TextMeshProUGUI menuButtonText;
    [SerializeField]
    private TMPro.TextMeshProUGUI messageText;
    [SerializeField]
    private UnityEngine.UI.Slider difficultySlider;
    [SerializeField]
    private TMPro.TextMeshProUGUI records;

    private GameStat gameStat;
    private string statsMessage;

    private const int MAX_ENERGY = 100;

    void Start()
    {
        GameDifficulty = difficultySlider.value;
        gameStat = GameObject.Find("GameStat").GetComponent<GameStat>();
        statsMessage = $"Current time: {(int)gameStat.GameTime / 60:00}:" +
                       $"{gameStat.GameTime % 60:00.0} - " +
                       $"Record time: {(int)gameStat.MaxTime / 60:00}:" +
                       $"{gameStat.MaxTime % 60:00.0}\n" +
                       $"Current score: {gameStat.GameScore} - " +
                       $"Max score: {gameStat.MaxScore}\n";
        SetRecords();
        ShowMenu(menuContainer.activeInHierarchy, "Start", statsMessage);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu(
                !menuContainer.activeInHierarchy,
                message: statsMessage
            );
        }
    }

    public void ShowMenu(bool isVisible = true, string buttonText = "Continue", string message = "")
    {
        if (isVisible)
        {
            menuContainer.SetActive(true);
            Time.timeScale = 0;
            menuButtonText.text = buttonText;
            messageText.text = message;

            return;
        }

        menuContainer.SetActive(false);
        Time.timeScale = 1;
    }

    public void MenuButtonClick()
    {
        if (menuButtonText.text == "Again")
        {
            gameStat.LivesCount = 3;
        }
        ShowMenu(false);
    }

    public void ControlTypeChanged(int index)
    {
        ControlType = index;
    }

    public void DifficultyChanged(float value)
    {
        GameDifficulty = value;
    }

    public void SetRecords()
    {
        records.text = $"{GameStat.Records}";
    }
}
