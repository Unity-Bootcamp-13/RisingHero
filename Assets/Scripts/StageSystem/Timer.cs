using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("¼³Á¤")]
    [SerializeField] private float totalTime = 60f;

    private float currentTime;
    private bool isRunning = false;

    public Action OnTimeout;

    private Color defaultColor;

    private void Awake()
    {
        if (timerText != null)
        {
            defaultColor = timerText.color;
        }
    }

    public void StartTimer(float duration)
    {
        totalTime = duration;
        currentTime = duration;
        isRunning = true;
        UpdateUI();
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentTime = 0f;
            isRunning = false;
            OnTimeout?.Invoke();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = $"{minutes:D2}:{seconds:D2}";

        if (currentTime <= 60f)
            timerText.color = Color.red;
        else
            timerText.color = defaultColor;
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }
}
