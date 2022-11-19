using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private TextMeshProUGUI clock;
    private float time;

    void Start()
    {
        clock = GetComponent<TextMeshProUGUI>();
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        clock.text = $"{(int)time / 60:00}:{time % 60:00.0}";
    }
}