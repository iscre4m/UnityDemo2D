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

    private GameStat gameStat;

    private const int MAX_ENERGY = 100;

    void Start()
    {
        GameDifficulty = .5f;
        gameStat = GameObject.Find("GameStat").GetComponent<GameStat>();
        ShowMenu(menuContainer.activeInHierarchy, "Start");
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu(
                !menuContainer.activeInHierarchy,
                message:
                $"Paused on time: " +
                $"{(int)gameStat.GameTime / 60:00}:" +
                $"{gameStat.GameTime % 60:00.0}. " +
                $"Score: {gameStat.GameScore:0000}. " +
                $"Energy: {(int)(gameStat.GameEnergy * 100)}/{MAX_ENERGY}"
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
}
