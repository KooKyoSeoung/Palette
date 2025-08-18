using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCam : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 cameraPosition = new Vector3(0, 0, -10);

    private float cameraMovementSpeed = 50.0f;
    private float cameraHeight;
    private float cameraWidth;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        cameraPosition.x = playerTransform.position.x;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * cameraMovementSpeed);
    }
}
