using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� �� ����������� TextMeshPro ��� ������

public class TimerController : MonoBehaviour
{
    public Slider timerSlider; // ������ �� ��������
    public TextMeshProUGUI timerText; // ������ �� ��������� ���� (����������� Text ������ TextMeshProUGUI, ���� ����������� ����������� �����)
    public float timeElapsed = 0f; // ��������� �����
    public float totalTime = 130f; // ����� ����� � �������� (2 ������)
    public bool timerIsRunning = false;
    [SerializeField] private Image imageCompleted;
    [SerializeField] private RedFaceScript RFS;
    [SerializeField] private StartCountDown SCD;

    private void Start()
    {
        timerSlider.maxValue = totalTime;
        timerSlider.value = 0;
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeElapsed < totalTime)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerDisplay(timeElapsed);
            }
            else
            {
                timeElapsed = totalTime;
                timerIsRunning = false;
                UpdateTimerDisplay(timeElapsed);
                imageCompleted.gameObject.SetActive(true);
                RFS.isTurnOn = false;
                SCD.isOn = false;
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        // ���������� ��������
        timerSlider.value = time;

        // ���������� ���������� �����������
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}