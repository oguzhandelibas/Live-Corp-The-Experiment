using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Variables")] 
    public bool timeDone;
    public float totalTime = 10.0f;
    private float currentTime;
    private TimerEventHandler _timerEventHandler;
    [SerializeField] private TextMeshProUGUI timerText;
    
    public void OnStart(int time)
    {
        timeDone = false;
        totalTime = time;
        currentTime = totalTime;
    }

    public void SetTimerEventHandler(TimerEventHandler timerEventHandler)
    {
        _timerEventHandler = timerEventHandler;
    }

    private void Update()
    {
        if(timeDone) return;
        currentTime -= Time.deltaTime / Time.timeScale;
        UpdateTimerUI();
        
        if (currentTime <= 0f)
        {
            _timerEventHandler.OnTimerEnd?.Invoke();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
