using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -45f;
    [SerializeField] float maxVerticalAngle = 45f;

    [SerializeField] Vector2 framinOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    [SerializeField] LayerMask cameraCollision;

     
    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;

   


    private void Update()
    {

        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        
        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        Vector3 focusPosition = followTarget.position + new Vector3(framinOffset.x, framinOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
        
        Vector3 rayDir = transform.position - followTarget.position; // �÷��̾��� ��������
        if (Physics.Raycast(followTarget.position, rayDir, out RaycastHit hit,
                float.MaxValue, cameraCollision))
        {
            transform.position = hit.point - rayDir.normalized; // ������ġ���� ��¦�� ��������
        }
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}