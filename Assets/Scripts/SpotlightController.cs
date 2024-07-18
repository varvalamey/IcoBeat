using UnityEngine;
using System.Collections.Generic;

public class SpotLightController : MonoBehaviour
{
    public List<Light> spotLights; // ������ Spot Light
    [SerializeField] public float rotationSpeed = 10f; // �������� ��������
    private Quaternion[] targetRotations; // ������� �������� ��� ������� Spot Light
    [SerializeField] private float minAngleDifference = 7f; // ����������� ���� ����� ������
    [SerializeField] private float maxRotationAngle = 50f; // ������������ ���������� �� ������� �������

    void Start()
    {
        // ������������� ������� ��������
        targetRotations = new Quaternion[spotLights.Count];
        for (int i = 0; i < spotLights.Count; i++)
        {
            targetRotations[i] = spotLights[i].transform.rotation;
        }
    }

    void Update()
    {
        for (int i = 0; i < spotLights.Count; i++)
        {
            // ������� �������� � �������� ��������
            spotLights[i].transform.rotation = Quaternion.Slerp(spotLights[i].transform.rotation, targetRotations[i], Time.deltaTime * rotationSpeed);

            // ��������, ���� �������� �������� ��������
            if (Quaternion.Angle(spotLights[i].transform.rotation, targetRotations[i]) < 0.1f)
            {
                // ��������� ������ �������� ��������
                GenerateNewTargetRotation(i);
            }
        }
    }

    void GenerateNewTargetRotation(int index)
    {
        bool validRotation = false;
        Quaternion newTargetRotation = Quaternion.identity;

        while (!validRotation)
        {
            // ��������� ���������� �������� � �������� �����������
            Vector3 randomRotation = new Vector3(
                Random.Range(-maxRotationAngle, maxRotationAngle),
                Random.Range(-maxRotationAngle, maxRotationAngle),
                Random.Range(-maxRotationAngle, maxRotationAngle)
            );
            newTargetRotation = Quaternion.Euler(randomRotation);

            validRotation = true;
            for (int i = 0; i < spotLights.Count; i++)
            {
                if (i != index)
                {
                    float angle = Quaternion.Angle(newTargetRotation, spotLights[i].transform.rotation);
                    if (angle < minAngleDifference)
                    {
                        validRotation = false;
                        break;
                    }
                }
            }
        }

        targetRotations[index] = newTargetRotation;
    }
}
