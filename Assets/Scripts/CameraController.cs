using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Vector3 cameraLocation;

    [SerializeField]
    private float moveCameraSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp
            (
            transform.position,
            playerTransform.position + cameraLocation,
            moveCameraSpeed * Time.fixedDeltaTime
            );
    }
}
