using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static int ControlType;

    [SerializeField]
    private GameObject menuContainer;
    [SerializeField]
    private TMPro.TextMeshProUGUI menuButtonText;

    void Start()
    {
        ShowMenu(menuContainer.activeInHierarchy, "Start");
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu(!menuContainer.activeInHierarchy);
        }
    }

    private void ShowMenu(bool isVisible = true, string buttonText = "Continue")
    {
        if (isVisible)
        {
            menuContainer.SetActive(true);
            Time.timeScale = 0;
            menuButtonText.text = buttonText;

            return;
        }

        menuContainer.SetActive(false);
        Time.timeScale = 1;
    }

    public void MenuButtonClick()
    {
        ShowMenu(false);
    }

    public void ControlTypeChanged(int index)
    {
        ControlType = index;
    }
}