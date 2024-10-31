using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotFollowByMainCamera : MonoBehaviour
{
    public Transform mainCamera;
    //public float rotationSpeed = 50f;
    public float maxRotationSpeed = 50f; // 최대 회전 속도
    public float minRotationSpeed = 20f; // 최소 회전 속도

    private void Update()
    {
        // 메인 카메라의 Y 회전 값 가져오기
        float cameraYRotation = mainCamera.localRotation.eulerAngles.y;

        // 플레이어와 메인 카메라의 거리 계산
        float distance = Vector3.Distance(transform.position, mainCamera.position);

        // 거리에 따른 회전 속도 계산
        float rotationSpeedFactor = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, Mathf.InverseLerp(0f, 10f, distance));

        // 플레이어 오브젝트의 회전 값 설정하기
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                     Quaternion.Euler(0, cameraYRotation, 0),
                                                     Time.deltaTime * rotationSpeedFactor);
    }
}
