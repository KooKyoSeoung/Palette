using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    [SerializeField] private float leftEndSize = 11f;
    private float rightEndSize;
    public float RightEndSize 
    { 
        get { return rightEndSize; } 
        set { rightEndSize = value; } 
    }

    private Transform playerTransform;
    private Vector3 position = new Vector3(0, 0, -10);

    private float movementSpeed = 50.0f;
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
        position.x = playerTransform.position.x;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * movementSpeed);

        float leftM = -leftEndSize + cameraWidth;
        float rightM = rightEndSize - cameraWidth;

        float clampX = Mathf.Clamp(transform.position.x, leftM, rightM);

        transform.position = new Vector3(clampX, 0, -10f);
    }
}
