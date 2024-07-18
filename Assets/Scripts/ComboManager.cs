using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public TMP_Text comboText; // ��������� ���� ��� ����������� �����
    public TMP_Text scoreText; // ��������� ���� ��� ����������� �����
    public Image comboImage; // ����������� ��� ����� ��� ���������� ������������ �����
    public Sprite[] comboSprites; // ������ ����������� ��� �����
    [SerializeField] private BeatController BC;
    private int comboCount = 0;
    private bool comboTime = false;
    private float comboTimer;
    private int score;
    private int previousComboCount;


    private void Start()
    {
        comboText.text = "x0";
        scoreText.text = "0";
        score = 0;
    }

    private void Update()
    {

        if (BC.canCombo)
        {
            comboTime = true;

        }
        else
        {
            comboTime = false;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            if (comboTime)
            {
                score += 20;
                scoreText.text = score.ToString();
                comboCount++;
                UpdateComboDisplay();
                ResetComboTimer();
            }
            else
            {
                score += 10;
                scoreText.text = score.ToString();
                previousComboCount = comboCount;
                comboCount = 0;
                UpdateComboDisplay();
            }

        }
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                // ���������� �����, ���� ������ ����
                previousComboCount = comboCount;
                comboCount = 0;
                UpdateComboDisplay();
                comboTimer = 1.5f * BC.beatInterval;
            }
        }

    }

    void ResetComboTimer()
    {
        // ���������� ������ �����
        comboTimer = 1.5f * BC.beatInterval;
    }

    private void UpdateComboDisplay()
    {
        comboText.text = "x" + comboCount.ToString();
        if (comboCount == 0 || comboCount == 5 || (comboCount > 5 && (comboCount - 5) % 10 == 0))
        {
            ChangeComboImage();
        }
    }

    private void ChangeComboImage()
    {
        if (comboSprites.Length > 0)
        {
            int spriteIndex = (comboCount - 5) / 10;
            if (spriteIndex < comboSprites.Length)
            {
                comboImage.sprite = comboSprites[spriteIndex];
            }
            if (comboCount == 0)
            {
                comboImage.sprite = comboSprites[0];
                score += (int)(Math.Round(0.5f * Math.Pow(previousComboCount, 2)));
                scoreText.text = score.ToString();
                //Debug.Log((int)(Math.Round(0.5f * Math.Pow(previousComboCount, 2))));
            }
        }
        
    }
}