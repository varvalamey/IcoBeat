using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI ����������

public class MoveImageOnBeat : MonoBehaviour
{
    public Image image;  // ������ �� Image
    private Vector3 centerPosition;  // ��������� �������
    private Vector3 initialPosition;  // ��������� �������
    public float moveDuration = 0.2f;  // ������������ �������� �� ������
    private bool isMoving = false;  // ����, ����� �������� �������������� ������� ���������� �������

    private void Start()
    {
        // ������������� ��������� ������� image
        initialPosition = image.rectTransform.localPosition;
        centerPosition = new Vector3(0, 0, initialPosition.z);  // �����������, ��� ����� ������ � (0,0)
        transform.localPosition = centerPosition;
    }

    // ���� ����� ������ ���������� � ���� ������
    public void OnBeat()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToCenterAndBack());
        }
    }

    private IEnumerator MoveToCenterAndBack()
    {
        isMoving = true;

        // ����������� � ������
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            image.rectTransform.localPosition = Vector3.Lerp(initialPosition, centerPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.rectTransform.localPosition = centerPosition;

        // ��������, ���� ����������
        yield return new WaitForSeconds(0.1f);  // ����� � ������

        // ����������� ������� � ��������� �������
        elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            image.rectTransform.localPosition = Vector3.Lerp(centerPosition, initialPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.rectTransform.localPosition = initialPosition;

        isMoving = false;
    }
}