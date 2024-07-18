using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;  // ������ �� �������� ������
    public float zoomFactor = 1.5f;  // ������ ���������� ����
    public float zoomTime = 0.5f;  // ����� ��� ����������� � ������ (� ��������)
    public float returnTime = 0.2f;  // ����� ��� ����������� � ��������� ���� (� ��������)
    public float beatsPerMinute = 90f;  // ��������� BPM (������ � ������)

    private float originalFOV;  // �������� ���� ������ ������
    private float targetFOV;  // ������� ���� ������ ������
    [SerializeField] private BeatController BC;
    public bool isOn;

    private void Start()
    {
        originalFOV = mainCamera.orthographicSize;
        targetFOV = originalFOV / zoomFactor;
    }

    public void StartZooming()
    {
        if (isOn)
            StartCoroutine(ZoomCamera());
    }

    IEnumerator ZoomCamera()
    {
        float zoomSpeed = (targetFOV - mainCamera.orthographicSize) / (zoomTime * (beatsPerMinute / 60f));
        float elapsedTime = 0f;

        // �������� ������
        while (elapsedTime < zoomTime)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ������ � ��������� ����
        float returnSpeed = (originalFOV - mainCamera.orthographicSize) / returnTime;
        elapsedTime = 0f;

        while (elapsedTime < returnTime)
        {
            mainCamera.orthographicSize += returnSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ���������� ��������
        mainCamera.orthographicSize = originalFOV;
        BC.isAlreadyZoomed = false;

    }
}