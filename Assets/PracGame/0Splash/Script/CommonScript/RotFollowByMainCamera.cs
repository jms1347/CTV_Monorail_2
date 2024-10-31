using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFollowByMainCamera : MonoBehaviour
{
    public Transform mainCamera;
    //public float rotationSpeed = 50f;
    public float maxRotationSpeed = 50f; // �ִ� ȸ�� �ӵ�
    public float minRotationSpeed = 20f; // �ּ� ȸ�� �ӵ�

    private void Update()
    {
        // ���� ī�޶��� Y ȸ�� �� ��������
        float cameraYRotation = mainCamera.localRotation.eulerAngles.y;

        // �÷��̾�� ���� ī�޶��� �Ÿ� ���
        float distance = Vector3.Distance(transform.position, mainCamera.position);

        // �Ÿ��� ���� ȸ�� �ӵ� ���
        float rotationSpeedFactor = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, Mathf.InverseLerp(0f, 10f, distance));

        // �÷��̾� ������Ʈ�� ȸ�� �� �����ϱ�
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                     Quaternion.Euler(0, cameraYRotation, 0),
                                                     Time.deltaTime * rotationSpeedFactor);
    }
}
